using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ModGL.Binding
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

        private static Type CreateDelegateType(MethodInfo method, ModuleBuilder module)
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

            return typeBuilder.CreateType();
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
                    AssemblyBuilderAccess.RunAndSave);

            var module = assembly.DefineDynamicModule("OpenGLInterfaces", "interface.cs");
            
            //var delegateModule = assembly.DefineDynamicModule("OpenGLDelegates");

            var definedType = module.DefineType(string.Format("{0}_DynamicProxy", typeof (TGLInterface).Name),
                                         TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class);

            definedType.AddInterfaceImplementation(typeof(TGLInterface));

            var constructor = definedType.DefineConstructor(MethodAttributes.Public | MethodAttributes.Final, CallingConventions.HasThis, new[] {typeof (IExtensionSupport)});

            var fields = GenerateInvocationFields(methods, module, definedType);

            GenerateConstructor(constructor, methods, fields);

            DefineProcMethods<TGLInterface>(errorHandling, methods, definedType, fields);

            DefineStaticMethods<TGLInterface>(interfaceMap, errorHandling, definedType);

            var resultType = definedType.CreateType();

            var ctrs = resultType.GetConstructors().Single();
            var args = ctrs.GetParameters();
            
            //assembly.Save("test.dll");
            var result = Activator.CreateInstance(resultType, context);
            var resultMethods = result.GetType().GetMethods();
            return (TGLInterface)result;
        }


        private void DefineStaticMethods<TGLInterface>(
            Dictionary<Type, Type> interfaceMap, ErrorHandling errorHandling, TypeBuilder definedType)
        {
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
                                                        MethodAttributes.NewSlot | MethodAttributes.Virtual
                                                        | MethodAttributes.Final;

                    var newMethod = definedType.DefineMethod
                        (
                            method.Name,
                            attributes,
                            CallingConventions.Any,
                            method.ReturnType,
                            parameters
                        );

                    var generator = newMethod.GetILGenerator();

                    if (errorHandling != null)
                        GenerateThrowingStaticInvocation(generator, method, item.Value, errorHandling);
                    else
                        GenerateStaticInvocation(generator, method, item.Value);

                    definedType.DefineMethodOverride(newMethod, method);
                }
            }
        }


        private void DefineProcMethods<TGLInterface>(
            ErrorHandling errorHandling, MethodInfo[] methods, TypeBuilder definedType, IList<FieldBuilder> fields)
        {
            foreach (var method in methods)
            {
                var newMethod = definedType.DefineMethod
                    (
                        method.Name,
                        MethodAttributes.Private | MethodAttributes.Virtual | MethodAttributes.Final |
                        MethodAttributes.HideBySig | MethodAttributes.NewSlot,
                        method.ReturnType,
                        method.GetParameters().OrderBy(p => p.Position).Select(t => t.ParameterType).ToArray()
                    );

                var generator = newMethod.GetILGenerator();

                if (errorHandling != null)
                    GenerateThrowingInvocation(generator, method, fields, errorHandling);
                else
                    GenerateInvocation(generator, method, fields);

                definedType.DefineMethodOverride(newMethod, method);
            }
        }


        private void GenerateConstructor(ConstructorBuilder builder, IEnumerable<MethodInfo> methods, IEnumerable<FieldBuilder> fields)
        {
            var generator = builder.GetILGenerator();
            generator.DeclareLocal(typeof(Delegate));
            // Call object constructor
            //generator.Emit(OpCodes.Ldarg_0);
            // ReSharper disable AssignNullToNotNullAttribute
             //generator.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[0]));
            // ReSharper restore AssignNullToNotNullAttribute
            var getMethod = typeof(IExtensionSupport).GetMethod("GetProcedure", new[] { typeof(string), typeof(Type) });

            foreach (var method in methods)
            {
                var name = GetFieldNameForMethodInfo(method);
                var field = fields.Single(f => f.Name == name);
                
                // _glMethodName = (MethodDelegateType)extensionSupport.GetProcedure("glMethodName", typeof(MethodDelegateType));
                generator.Emit(OpCodes.Ldarg_0); // load argument
                generator.Emit(OpCodes.Ldarg_1); // load this
                generator.Emit(OpCodes.Ldstr, method.Name);  // load method name
                generator.Emit(OpCodes.Ldtoken, field.FieldType); // load field type
                generator.EmitCall(OpCodes.Call, typeof(System.Type).GetMethod("GetTypeFromHandle", new[] { typeof(System.RuntimeTypeHandle) }), null);
                generator.EmitCall(OpCodes.Callvirt, getMethod, null);
                generator.Emit(OpCodes.Castclass, field.FieldType);
                generator.Emit(OpCodes.Stloc_0); // result = GetProcedure("MethodName", Type);
                generator.Emit(OpCodes.Ldarg_0); // this
                generator.Emit(OpCodes.Ldloc_0); // result
                generator.Emit(OpCodes.Stfld, field); // this._fieldName = result;

            }
            //generator.Emit(OpCodes.Ldloc_0); // Load this
            generator.Emit(OpCodes.Ret);
        }

        private IList<FieldBuilder> GenerateInvocationFields(IEnumerable<MethodInfo> methods, ModuleBuilder delegateModule, TypeBuilder builder)
        {
            return (from method in methods
                    let delegateType = CreateDelegateType(method, delegateModule)
                    select builder.DefineField(GetFieldNameForMethodInfo(method), delegateType, FieldAttributes.Private)).ToList();
        }

        private void GenerateStaticInvocation(ILGenerator generator, MethodInfo method, Type staticMapping, bool emitReturn = true)
        {
            foreach (var item in method.GetParameters().Select((p, i) => new { Type = p, Index = i }))
                generator.Emit(OpCodes.Ldarg, item.Index);
            generator.EmitCall(OpCodes.Call, staticMapping.GetMethod(method.Name, BindingFlags.Public | BindingFlags.Static, null, method.GetParameters().Select(p => p.ParameterType).ToArray(), null), null);
            generator.Emit(OpCodes.Ret);
        }

        private void GenerateThrowingStaticInvocation(ILGenerator generator, MethodInfo method, Type staticMapping, ErrorHandling err)
        {
            generator.DeclareLocal(method.ReturnType);
            generator.EmitCall(OpCodes.Call, err.FlushError, null);
            GenerateStaticInvocation(generator, method, staticMapping, emitReturn: false);
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
