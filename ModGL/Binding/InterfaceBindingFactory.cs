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
            if(context == null)
                throw new ArgumentNullException("context");

            if(!typeof(TGLInterface).IsInterface)
                throw new ArgumentException("TGLInterface must be an interface.");

            if (interfaceMap.Keys.Any(interf => !interf.IsInterface))
                throw new InvalidOperationException(string.Format("Interface map must map interfaces : {0}", string.Join(", ", interfaceMap.Keys.Where(t => !t.IsInterface).Select(t => t.Name))));

            if(interfaceMap.Values.Any(t => t.IsInterface))
                throw new InvalidOperationException(string.Format("Interface map must map to classes with static members: {0}", string.Join(", ", interfaceMap.Values.Where(t => t.IsInterface).Select(t => t.Name))));

            var remapMethods = interfaceMap.SelectMany(t => t.Value.GetMethods()).ToArray();
            var type = typeof (TGLInterface);

            // Exclude methods defined in the interface map.
            var methods = type.GetMethods().Union(type.GetInterfaces().SelectMany(t => t.GetMethods())).Except(remapMethods, new MethodInfoEqualityComparer()).ToArray();

            var assembly = AssemblyBuilder.DefineDynamicAssembly(
                    new AssemblyName(string.Format("{0}.Dynamic", typeof (TGLInterface).Name)),
                    AssemblyBuilderAccess.RunAndSave);

            var module = assembly.DefineDynamicModule("OpenGLInterfaces", "interface.cs");
            
            //var delegateModule = assembly.DefineDynamicModule("OpenGLDelegates");

            var definedType = module.DefineType(string.Format("{0}_DynamicProxy", typeof (TGLInterface).Name),
                                         TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class);

            //definedType.AddInterfaceImplementation(typeof(TGLInterface));

            var constructor = definedType.DefineConstructor(MethodAttributes.Public | MethodAttributes.Final, CallingConventions.HasThis, new[] {typeof (IExtensionSupport)});

            var fields = GenerateInvocationFields(methods, module, definedType);

            GenerateConstructor(constructor, methods, fields);

            var interfaces = type.GetInterfaces().Concat(new[] { type }).Except(interfaceMap.Keys).ToArray();

            foreach(var @interface in interfaces)
                DefineProcMethods(errorHandling, @interface, remapMethods, definedType, fields);

            DefineStaticMethods(interfaceMap, errorHandling, definedType);

            var resultType = definedType.CreateType();

            object result;
            try
            {
                 result = Activator.CreateInstance(resultType, context);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }

            return (TGLInterface)result;
        }


        private void DefineStaticMethods(Dictionary<Type, Type> interfaceMap, ErrorHandling errorHandling, TypeBuilder definedType)
        {
            foreach (var item in interfaceMap)
            {
                // Implement interface
                definedType.AddInterfaceImplementation(item.Key);
                foreach (var method in item.Key.GetMethods())
                {
                    // Map interface to static methods.
                    var parameters =
                        method.GetParameters().OrderBy(p => p.Position).Select(t => t.ParameterType).ToArray();
                    const MethodAttributes attributes = MethodAttributes.Private | MethodAttributes.Virtual | MethodAttributes.Final |
                        MethodAttributes.HideBySig | MethodAttributes.NewSlot;

                    var newMethod = definedType.DefineMethod
                        (
                            method.Name,
                            attributes,
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

        internal class ParameterInfoEqualityComparer : IEqualityComparer<ParameterInfo>
        {
            public bool Equals(ParameterInfo x, ParameterInfo y)
            {
                return x.ParameterType == y.ParameterType;
            }


            public int GetHashCode(ParameterInfo obj)
            {
                return obj.ParameterType.FullName.GetHashCode();
            }
        }

        internal class MethodInfoEqualityComparer : IEqualityComparer<MethodInfo>
        {
            public bool Equals(MethodInfo x, MethodInfo y)
            {
                return x.Name == y.Name && x.GetParameters().SequenceEqual(y.GetParameters(), new ParameterInfoEqualityComparer());
            }


            public int GetHashCode(MethodInfo obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        private void DefineProcMethods(
            ErrorHandling errorHandling, Type interfaceType, IEnumerable<MethodInfo> except, TypeBuilder definedType, IList<FieldBuilder> fields)
        {
            var methods = interfaceType.GetMethods().Except(except, new MethodInfoEqualityComparer()).ToArray();
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
            definedType.AddInterfaceImplementation(interfaceType);
        }


        private void GenerateConstructor(ConstructorBuilder builder, IEnumerable<MethodInfo> methods, IEnumerable<FieldBuilder> fields)
        {
            var generator = builder.GetILGenerator();
            generator.DeclareLocal(typeof(Delegate));
            var getMethod = typeof(IExtensionSupport).GetMethod("GetProcedure", new[] { typeof(string), typeof(Type) });
            var notSupportedConstructor = typeof(ExtensionNotSupportedException).GetConstructor(
                new[] { typeof(string) });
            if(notSupportedConstructor == null) // Constructor required. Added to make unit tests fail if it is missing.
                throw new MissingMethodException("ExtensionNotSupportedException", ".ctr(string)");

            var fieldBuilders = fields as FieldBuilder[] ?? fields.ToArray();
            foreach (var method in methods)
            {
                var name = GetFieldNameForMethodInfo(method);
                var field = fieldBuilders.Single(f => f.Name == name);
                var okLabel = generator.DefineLabel();
                
                // _glMethodName = (MethodDelegateType)extensionSupport.GetProcedure("glMethodName", typeof(MethodDelegateType));
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldarg_1);
                generator.Emit(OpCodes.Ldstr, method.Name);  // load method name
                generator.Emit(OpCodes.Ldtoken, field.FieldType); // load field type
                generator.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle", new[] { typeof(RuntimeTypeHandle) }), null);
                generator.EmitCall(OpCodes.Callvirt, getMethod, null);
                generator.Emit(OpCodes.Stloc_0); // result = GetProcedure("MethodName", Type);
                // if result == null throw ExtensionNotSupportedException
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Brtrue, okLabel);
                generator.Emit(OpCodes.Ldstr, method.Name);
                generator.Emit(OpCodes.Newobj, notSupportedConstructor);
                generator.Emit(OpCodes.Throw);
                generator.MarkLabel(okLabel);
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Castclass, field.FieldType);
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
                    select builder.DefineField(GetFieldNameForMethodInfo(method), delegateType, FieldAttributes.Private | FieldAttributes.InitOnly)).ToList();
        }

        private void GenerateStaticInvocation(ILGenerator generator, MethodInfo method, Type staticMapping, bool emitReturn = true)
        {
            var invokeMethod = staticMapping.GetMethod(
                method.Name,
                BindingFlags.Public | BindingFlags.Static,
                null,
                method.GetParameters().Select(p => p.ParameterType).ToArray(),
                null);
            generator.Emit(OpCodes.Nop);
            foreach (var item in method.GetParameters().Select((p, i) => new { Type = p, Index = i }))
                generator.Emit(OpCodes.Ldarg, item.Index + 1);
            generator.EmitCall(OpCodes.Call, invokeMethod , null);
            if(emitReturn)
                generator.Emit(OpCodes.Ret);
        }

        private void GenerateThrowingStaticInvocation(ILGenerator generator, MethodInfo method, Type staticMapping, ErrorHandling err)
        {
            if(method.ReturnType != typeof(void))
                generator.DeclareLocal(method.ReturnType);
            generator.EmitCall(OpCodes.Call, err.FlushError, null);
            GenerateStaticInvocation(generator, method, staticMapping, emitReturn: false);
            if (method.ReturnType != typeof(void))
                generator.Emit(OpCodes.Stloc_0);
            generator.EmitCall(OpCodes.Call, err.CheckErrorState, null);
            if (method.ReturnType != typeof(void))
                generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);
        }

        private void GenerateThrowingInvocation(ILGenerator generator, MethodInfo method, IEnumerable<FieldBuilder> fieldBuilders, ErrorHandling err)
        {
            if(method.ReturnType != typeof(void))
                generator.DeclareLocal(method.ReturnType);

            generator.EmitCall(OpCodes.Call, err.FlushError, null);
            GenerateInvocation(generator, method, fieldBuilders, emitReturn: false);

            if (method.ReturnType != typeof(void))
                generator.Emit(OpCodes.Stloc_0);
            generator.EmitCall(OpCodes.Call, err.CheckErrorState, null);

            if (method.ReturnType != typeof(void))
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
