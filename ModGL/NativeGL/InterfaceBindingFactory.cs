using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;

namespace ModGL.NativeGL
{

    public class ErrorHandling
    {
        public MethodInfo FlushError { get; set; }
        public MethodInfo CheckErrorState { get; set; }
    }

    public class InterfaceBindingFactory
    {
        private static string GetDelegateNameForMethodInfo(MethodInfo method)
        {
            return string.Format("{0}Proc", method.Name);
        }

        private static string GetFieldNameForMethodInfo(MethodInfo method)
        {
            return string.Format("_{0}", method.Name);
        }

        private static TypeBuilder CreateDelegateType(MethodInfo method, ModuleBuilder module)
        {
            string name = GetDelegateNameForMethodInfo(method);

            var typeBuilder = module.DefineType(
                name, TypeAttributes.Sealed | TypeAttributes.Public, typeof(MulticastDelegate));

            
            var constructor = typeBuilder.DefineConstructor(
                MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public,
                CallingConventions.Standard, new[] { typeof(object), typeof(IntPtr) });
            constructor.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);

            var parameters = method.GetParameters();

            var invokeMethod = typeBuilder.DefineMethod(
                "Invoke", MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Public,
                method.ReturnType, parameters.Select(p => p.ParameterType).ToArray());
            invokeMethod.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                invokeMethod.DefineParameter(i + 1, ParameterAttributes.None, parameter.Name);
            }

            return typeBuilder;
        }
        /// <summary>
        /// Creates a binding for the specified interface to extension methods defined by IExtensions support.
        /// </summary>
        /// <typeparam name="TGLInterface">Interface type to implement.</typeparam>
        /// <param name="context">Extension context. This will return method delegates that the binder will use.</param>
        /// <param name="errorHandling">Defines error handling routines. Setting this to null disables error handling.</param>
        /// <returns>Implementation of the interface.</returns>
        public TGLInterface CreateBinding<TGLInterface>(IExtensionSupport context, ErrorHandling errorHandling = null)
        {
            return CreateBinding<TGLInterface>(context, new Dictionary<Type, Type>(), errorHandling);
        }

        /// <summary>
        /// Creates a binding for the specified interface to extension methods defined by IExtensions support.
        /// </summary>
        /// <typeparam name="TGLInterface">Interface type to implement.</typeparam>
        /// <param name="context">Extension context. This will return method delegates that the binder will use.</param>
        /// <param name="interfaceMap">Additional interfaces to implement using static functions in the mpa types.</param>
        /// <param name="errorHandling">Defines error handling routines. Setting this to null disables error handling.</param>
        /// <returns>Implementation of the interface.</returns>
        /// <remarks>The interface map will override interface implementations from <see cref="TGLInterface"/> if they intersect.</remarks>
        public TGLInterface CreateBinding<TGLInterface>(IExtensionSupport context, Dictionary<Type, Type> interfaceMap, ErrorHandling errorHandling = null)
        {
            if(!typeof(TGLInterface).IsInterface)
                throw new ArgumentException("TGLInterface must be an interface.");

            var remapMethods = interfaceMap.SelectMany(t => t.Value.GetMethods());
            var type = typeof (TGLInterface);

            // Exclude methods defined in the interface map.
            var methods = type.GetMethods().Except(remapMethods).ToArray();

            var assembly = AssemblyBuilder.DefineDynamicAssembly(
                    new AssemblyName(string.Format("{0}.Dynamic", typeof (TGLInterface).Name)),
                    AssemblyBuilderAccess.RunAndCollect);

            var module = assembly.DefineDynamicModule("OpenGLInterfaces");
            var delegateModule = assembly.DefineDynamicModule("OpenGLDelegates");

            var definedType = module.DefineType(string.Format("{0}_DynamicProxy", typeof (TGLInterface).Name),
                                         TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class);

            definedType.AddInterfaceImplementation(typeof(TGLInterface));

            var constructor = definedType.DefineConstructor(MethodAttributes.Public, CallingConventions.Any, new[] {typeof (IExtensionSupport)});

            var fields = GenerateInvocationFields(methods, delegateModule, definedType);

            GenerateConstructor(constructor, methods, fields);

            // Implement OpenGL 1.2+ as GetProc invocations
            foreach (var method in methods)
            {
                var newMethod = definedType.DefineMethod
                (
                    method.Name, 
                    MethodAttributes.Private | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final, CallingConventions.Any, 
                    method.ReturnType, 
                    method.GetParameters().OrderBy(p => p.Position).Select(t => t.ParameterType).ToArray()
                );
                definedType.DefineMethodOverride(newMethod, method);

                var generator = newMethod.GetILGenerator();

                if (errorHandling != null)
                    GenerateThrowingInvocation(generator, method, fields, errorHandling);
                else
                    GenerateInvocation(generator, method, fields);
            }

            foreach (var item in interfaceMap)
            {
                // Implement interface
                definedType.AddInterfaceImplementation(item.Key);
                foreach (var method in item.Value.GetMethods())
                {
                    // Map interface to static methods.
                    var parameters =
                        method.GetParameters().OrderBy(p => p.Position).Select(t => t.ParameterType).ToArray();
                    const MethodAttributes attributes = MethodAttributes.Private | MethodAttributes.HideBySig |
                                          MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final;

                    var newMethod = definedType.DefineMethod
                    (
                        method.Name, 
                        attributes,
                        CallingConventions.Any,
                        method.ReturnType,
                        parameters
                    );

                    definedType.DefineMethodOverride(newMethod, method);

                    var generator = newMethod.GetILGenerator();

                    if (errorHandling != null)
                        GenerateThrowingStaticInvocation(generator, method, errorHandling);
                    else
                        GenerateStaticInvocation(generator, method);
                }                
            }

            var resultType = definedType.CreateType();
            return (TGLInterface)Activator.CreateInstance(resultType, new object[] { context });
        }

        private void GenerateConstructor(ConstructorBuilder builder, IEnumerable<MethodInfo> methods, IEnumerable<FieldBuilder> fields)
        {
            var generator = builder.GetILGenerator();
            // Call object constructor
            generator.Emit(OpCodes.Ldarg_0);
            // ReSharper disable AssignNullToNotNullAttribute
            generator.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[0]));
            // ReSharper restore AssignNullToNotNullAttribute
            foreach (var method in methods)
            {
                var name = GetFieldNameForMethodInfo(method);
                var field = fields.Single(f => f.Name == name);
                
                // _glMethodName = (MethodDelegateType)extensionSupport.GetProcedure("glMethodName", typeof(MethodDelegateType));
                generator.Emit(OpCodes.Ldarg_0); // load this
                generator.Emit(OpCodes.Ldarg_1); // load argument
                generator.Emit(OpCodes.Ldstr, method.Name);  // load method name
                generator.Emit(OpCodes.Ldelem, field.FieldType); // load field type
                generator.EmitCall(OpCodes.Callvirt, typeof(IExtensionSupport).GetMethod("GetProcedure", new [] { typeof(string), typeof(Type)}), null);
                generator.Emit(OpCodes.Castclass, field.FieldType);
                generator.Emit(OpCodes.Stfld, field);

            }
        }

        private IList<FieldBuilder> GenerateInvocationFields(IEnumerable<MethodInfo> methods, ModuleBuilder delegateModule, TypeBuilder builder)
        {
            return (from method in methods
                    let delegateType = CreateDelegateType(method, delegateModule).CreateType()
                    select builder.DefineField(GetFieldNameForMethodInfo(method), delegateType, FieldAttributes.Private)).ToList();
        }

        private void GenerateStaticInvocation(ILGenerator generator, MethodInfo method, bool emitReturn = true)
        {
            foreach (var item in method.GetParameters().Select((p, i) => new { Type = p, Index = i }))
                generator.Emit(OpCodes.Ldarg, item.Index);
            generator.EmitCall(OpCodes.Call, typeof(GL).GetMethod(method.Name, BindingFlags.Public | BindingFlags.Static, null, method.GetParameters().Select(p => p.ParameterType).ToArray(), null), null);
            generator.Emit(OpCodes.Ret);
        }

        private void GenerateThrowingStaticInvocation(ILGenerator generator, MethodInfo method, ErrorHandling err)
        {
            generator.DeclareLocal(method.ReturnType);
            generator.EmitCall(OpCodes.Call, err.FlushError, null);
            GenerateStaticInvocation(generator, method, emitReturn: false);
            generator.Emit(OpCodes.Stloc_0);
            generator.EmitCall(OpCodes.Call, err.CheckErrorState, null);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);
        }


        private void GenerateThrowingInvocation(ILGenerator generator, MethodInfo method, IEnumerable<FieldBuilder> fieldBuilders, ErrorHandling err)
        {
            generator.DeclareLocal(method.ReturnType);
            generator.EmitCall(OpCodes.Call, err.FlushError, null);
            GenerateInvocation(generator, method, fieldBuilders, emitReturn: false);
            generator.Emit(OpCodes.Stloc_0);
            generator.EmitCall(OpCodes.Call, err.CheckErrorState, null);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);
        }

        private void GenerateInvocation(ILGenerator generator, MethodInfo method, IEnumerable<FieldBuilder> fieldBuilders, bool emitReturn = true)
        {
            var field = fieldBuilders.First(f => f.Name == GetFieldNameForMethodInfo(method));
            generator.Emit(OpCodes.Ldarg_0); //  this
            generator.Emit(OpCodes.Ldfld, field); // glMethodNameProc _glMethodName. Initialized by constructor.
            foreach (var item in method.GetParameters().Select((p, i) => new { Type = p, Index = i }))
            {
                generator.Emit(OpCodes.Ldarg, item.Index + 1);
            }
            generator.EmitCall(OpCodes.Callvirt, field.FieldType.GetMethod("Invoke"), null);
            if(emitReturn)
                generator.Emit(OpCodes.Ret);
        }
    }
}
