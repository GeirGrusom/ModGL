using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace ModGL.Binding
{

    public class ErrorHandling
    {
        public MethodInfo FlushError { get; set; }
        public MethodInfo CheckErrorState { get; set; }
    }

    public interface IInterfaceBindingFactory
    {
        /// <summary>
        /// Creates a binding for the specified interface to extension methods defined by IExtensions support.
        /// </summary>
        /// <typeparam name="TGLInterface">Interface type to implement.</typeparam>
        /// <param name="context">Extension context. This will return method delegates that the binder will use.</param>
        /// <param name="errorHandling">Defines error handling routines. Setting this to null disables error handling.</param>
        /// <param name="extensionMethodPrefix">Optional prefix to extension methods. For prefix "Foo" this will bind Bar() with FooBar().</param>
        /// <returns>Implementation of the interface.</returns>
        TGLInterface CreateBinding<TGLInterface>(IExtensionSupport context, ErrorHandling errorHandling = null, string extensionMethodPrefix = "");


        /// <summary>
        /// Creates a binding for the specified interface to extension methods defined by IExtensions support.
        /// </summary>
        /// <typeparam name="TGLInterface">Interface type to implement.</typeparam>
        /// <param name="context">Extension context. This will return method delegates that the binder will use.</param>
        /// <param name="interfaceMap">Additional interfaces to implement using static functions in the mpa types.</param>
        /// <param name="errorHandling">Defines error handling routines. Setting this to null disables error handling.</param>
        /// <param name="extensionMethodPrefix">Optional prefix to extension methods. For prefix "Foo" this will bind Bar() with FooBar().</param>
        /// <returns>Implementation of the interface.</returns>
        /// <remarks>The interface map will override interface implementations from <see cref="TGLInterface"/> if they intersect.</remarks>
        TGLInterface CreateBinding<TGLInterface>(IExtensionSupport context, Dictionary<Type, Type> interfaceMap, ErrorHandling errorHandling = null, string extensionMethodPrefix = null);
    }

    public class InterfaceBindingFactory : IInterfaceBindingFactory
    {

        private class CustomAttributeNamedArgumentComparer : IEqualityComparer<CustomAttributeNamedArgument>
        {
            public bool Equals(CustomAttributeNamedArgument x, CustomAttributeNamedArgument y)
            {
                return x.MemberName == y.MemberName;
            }

            public int GetHashCode(CustomAttributeNamedArgument obj)
            {
                return obj.MemberName.GetHashCode();
            }
        }

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

            var oldType = module.GetType(name);
            if (oldType != null)
                return oldType;

            var typeBuilder = module.DefineType(
                name, TypeAttributes.Sealed | TypeAttributes.Public, typeof(MulticastDelegate));

            
            var constructor = typeBuilder.DefineConstructor(
                MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public,
                CallingConventions.Standard, new[] { typeof(object), typeof(IntPtr) });
            constructor.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);

            var parameters = method.GetParameters();

            var invokeMethod = typeBuilder.DefineMethod(
                "Invoke", 
                MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Public,
                method.ReturnType,
                parameters.Select(p => p.ParameterType).ToArray());

            invokeMethod.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);

            // Copy return type attributes
            if (method.ReturnType != typeof(void))
            {
                invokeMethod.SetReturnType(method.ReturnType);
                if (method.ReturnParameter != null)
                {
                    var returnParameter = invokeMethod.DefineParameter(0, method.ReturnParameter.Attributes, method.ReturnParameter.Name);
                    foreach (var attrib in method.ReturnParameter.CustomAttributes)
                    {
                        if (attrib.NamedArguments == null || attrib.NamedArguments.Count == 0)
                            continue;

                        // The marshaller will prefer to use the MarshalType over MarshalTypeRef.
                        // and will automatically set MarshalType if you specify MarshalTypeRef.
                        // this will make it unable to locate the type (since without assembly specification, it will look in the dynamic assembly)
                        // Therefore we have to remove MarshalType if both MarshalType and MarshalTypeRef is set.
                        IEnumerable<CustomAttributeNamedArgument> namedArguments = attrib.NamedArguments;

                        if (attrib.NamedArguments.Any(f => f.MemberName == "MarshalTypeRef") && attrib.NamedArguments.Any(f => f.MemberName == "MarshalType"))
                        {
                            namedArguments =
                                namedArguments.Except(namedArguments.Where(f => f.MemberName == "MarshalType"), new CustomAttributeNamedArgumentComparer()).ToArray();
                        }

                        returnParameter.SetCustomAttribute(
                            new CustomAttributeBuilder(
                                attrib.Constructor,
                                attrib.ConstructorArguments.Select(a => a.Value).ToArray(),
                                attrib.NamedArguments.Where(a => !a.IsField).Select(s => s.MemberInfo).OfType<PropertyInfo>().ToArray(),
                                attrib.NamedArguments.Where(a => !a.IsField).Select(s => s.TypedValue).Select(s => s.Value).ToArray(),
                                namedArguments.Where(a => a.IsField).Select(s => s.MemberInfo).OfType<FieldInfo>().ToArray(),
                                namedArguments.Where(a => a.IsField).Select(s => s.TypedValue).Select(s => s.Value).ToArray()));
                    }
                }
            }

            // Copy parameter types.
            foreach (var p in parameters.Where(p=> p.Position > 0).Select((Param, Index) => new { Param, Index}))
            {
                var newParameter = invokeMethod.DefineParameter(p.Param.Position, p.Param.Attributes, p.Param.Name);
                // Copy custom attributes.
                foreach (var attrib in p.Param.CustomAttributes)
                {
                    if(attrib.NamedArguments == null || attrib.NamedArguments.Count == 0)
                        continue;

                    // The marshaller will prefer to use the MarshalType over MarshalTypeRef.
                    // and will automatically set MarshalType if you specify MarshalTypeRef (what?! why?).
                    // this will make it unable to locate the type (since without assembly specification, it will look in the dynamic assembly)
                    // Therefore we have to remove MarshalType if both MarshalType and MarshalTypeRef is set.
                    IEnumerable<CustomAttributeNamedArgument> namedArguments = attrib.NamedArguments;

                    if (attrib.NamedArguments.Any(f => f.MemberName == "MarshalTypeRef") && attrib.NamedArguments.Any(f => f.MemberName == "MarshalType"))
                    {
                        namedArguments =
                            namedArguments.Except(namedArguments.Where(f => f.MemberName == "MarshalType"), new CustomAttributeNamedArgumentComparer()).ToArray();
                    }

                    newParameter.SetCustomAttribute(
                        new CustomAttributeBuilder(
                            attrib.Constructor, 
                            attrib.ConstructorArguments.Select(a => a.Value).ToArray(), 
                            attrib.NamedArguments.Where(a => !a.IsField).Select(s => s.MemberInfo).OfType<PropertyInfo>().ToArray(),
                            attrib.NamedArguments.Where(a => !a.IsField).Select(s => s.TypedValue).Select(s => s.Value).ToArray(),
                            namedArguments.Where(a => a.IsField).Select(s => s.MemberInfo).OfType<FieldInfo>().ToArray(),
                            namedArguments.Where(a => a.IsField).Select(s => s.TypedValue).Select(s => s.Value).ToArray()));
                }
            }


            return typeBuilder.CreateType();
        }


        /// <summary>
        /// Creates a binding for the specified interface to extension methods defined by IExtensions support.
        /// </summary>
        /// <typeparam name="TGLInterface">Interface type to implement.</typeparam>
        /// <param name="context">Extension context. This will return method delegates that the binder will use.</param>
        /// <param name="errorHandling">Defines error handling routines. Setting this to null disables error handling.</param>
        /// <param name="extensionMethodPrefix">Optional prefix to extension methods. For prefix "Foo" this will bind Bar() with FooBar().</param>
        /// <returns>Implementation of the interface.</returns>
        [Pure]
        public TGLInterface CreateBinding<TGLInterface>(IExtensionSupport context, ErrorHandling errorHandling = null, string extensionMethodPrefix = "")
        {
            return CreateBinding<TGLInterface>(context, new Dictionary<Type, Type>(), errorHandling, extensionMethodPrefix);
        }

        /// <summary>
        /// Creates a binding for the specified interface to extension methods defined by IExtensions support.
        /// </summary>
        /// <typeparam name="TGLInterface">Interface type to implement.</typeparam>
        /// <param name="context">Extension context. This will return method delegates that the binder will use.</param>
        /// <param name="interfaceMap">Additional interfaces to implement using static functions in the mpa types.</param>
        /// <param name="errorHandling">Defines error handling routines. Setting this to null disables error handling.</param>
        /// <param name="extensionMethodPrefix">Optional prefix to extension methods. For prefix "Foo" this will bind Bar() with FooBar().</param>
        /// <returns>Implementation of the interface.</returns>
        /// <remarks>The interface map will override interface implementations from <see cref="TGLInterface"/> if they intersect.</remarks>
        [Pure]
        public TGLInterface CreateBinding<TGLInterface>(IExtensionSupport context, Dictionary<Type, Type> interfaceMap, ErrorHandling errorHandling = null, string extensionMethodPrefix = null)
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

            var definedType = module.DefineType(string.Format("{0}_DynamicProxy", typeof (TGLInterface).Name),
                                         TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class);

            var constructor = definedType.DefineConstructor(MethodAttributes.Public | MethodAttributes.Final, CallingConventions.HasThis, new[] {typeof (IExtensionSupport)});

            var fields = GenerateInvocationFields(methods, module, definedType);

            GenerateConstructor(constructor, methods, fields, extensionMethodPrefix);

            var interfaces = type.GetInterfaces().Concat(new[] { type }).Except(interfaceMap.Keys).ToArray();

            DefineStaticMethods(interfaceMap, errorHandling, definedType, extensionMethodPrefix);

            foreach(var @interface in interfaces)
                DefineProcMethods(errorHandling, @interface, remapMethods, definedType, fields);


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


        private void DefineStaticMethods(Dictionary<Type, Type> interfaceMap, ErrorHandling errorHandling, TypeBuilder definedType, string extensionMethodPrefix = null)
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
                        GenerateThrowingStaticInvocation(generator, method, item.Value, errorHandling, extensionMethodPrefix: extensionMethodPrefix);
                    else
                        GenerateStaticInvocation(generator, method, item.Value, extensionMethodPrefix: extensionMethodPrefix );

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


        private static void GenerateConstructor(ConstructorBuilder builder, IEnumerable<MethodInfo> methods, IEnumerable<FieldBuilder> fields, string extensionMethodPrefix)
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
                string methodName = (extensionMethodPrefix ?? "") + method.Name;
                var name = GetFieldNameForMethodInfo(method);
                var field = fieldBuilders.Single(f => f.Name == name);
                var okLabel = generator.DefineLabel();
                
                // _glMethodName = (MethodDelegateType)extensionSupport.GetProcedure("glMethodName", typeof(MethodDelegateType));
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldarg_1);
                generator.Emit(OpCodes.Ldstr, methodName);  // load method name
                generator.Emit(OpCodes.Ldtoken, field.FieldType); // load field type
                generator.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle", new[] { typeof(RuntimeTypeHandle) }), null);
                generator.EmitCall(OpCodes.Callvirt, getMethod, null);
                generator.Emit(OpCodes.Stloc_0); // result = GetProcedure("MethodName", Type);
                // if result == null throw ExtensionNotSupportedException
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Brtrue, okLabel);
                generator.Emit(OpCodes.Ldstr, methodName);
                generator.Emit(OpCodes.Newobj, notSupportedConstructor);
                generator.Emit(OpCodes.Throw);
                generator.MarkLabel(okLabel);
                // Everything went okay. Set the delegate to the returned function.
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Castclass, field.FieldType);
                generator.Emit(OpCodes.Ldarg_0); // this
                generator.Emit(OpCodes.Ldloc_0); // result
                generator.Emit(OpCodes.Stfld, field); // this._fieldName = result;
            }
            generator.Emit(OpCodes.Ret);
        }

        private static IList<FieldBuilder> GenerateInvocationFields(IEnumerable<MethodInfo> methods, ModuleBuilder delegateModule, TypeBuilder builder)
        {
            return (from method in methods
                    let delegateType = CreateDelegateType(method, delegateModule)
                    select builder.DefineField(GetFieldNameForMethodInfo(method), delegateType, FieldAttributes.Private | FieldAttributes.InitOnly)).ToList();
        }

        private void GenerateStaticInvocation(ILGenerator generator, MethodInfo method, Type staticMapping, bool emitReturn = true, string extensionMethodPrefix = null)
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

        private void GenerateThrowingStaticInvocation(ILGenerator generator, MethodInfo method, Type staticMapping, ErrorHandling err, string extensionMethodPrefix = null)
        {
            if(method.ReturnType != typeof(void))
                generator.DeclareLocal(method.ReturnType);
            generator.EmitCall(OpCodes.Call, err.FlushError, null);
            GenerateStaticInvocation(generator, method, staticMapping, false, extensionMethodPrefix);
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
