using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;

namespace ModGL.NativeGL
{
    public class OpenGLBindingFactory
    {

        private TypeBuilder CreateDelegateType(MethodInfo method, TypeBuilder module)
        {
            string name = string.Format("{0}Proc", method.Name);

            var typeBuilder = module.DefineNestedType(
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
        public TGLInterface CreateBinding<TGLInterface>(IExtensionSupport context, bool debugInterface = false)
        {
            var type = typeof (TGLInterface);

            var methods = type.GetMethods();

            var assembly = AssemblyBuilder.DefineDynamicAssembly(
                    new AssemblyName(string.Format("{0}_Dynamic", typeof (TGLInterface).Name)),
                    AssemblyBuilderAccess.RunAndCollect);
            var module = assembly.DefineDynamicModule("OpenGLInterfaces");

            var definedType = module.DefineType(string.Format("{0}_DynamicProxy", typeof (TGLInterface).Name),
                                         TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class);
            definedType.AddInterfaceImplementation(typeof(TGLInterface));

            var constructor = definedType.DefineConstructor(MethodAttributes.Public | MethodAttributes.Final, CallingConventions.Any, new[] {typeof (IExtensionSupport)});

            GenerateInvocationFields(methods, definedType);

            foreach (var method in methods)
            {
                var newMethod = definedType.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.Virtual, CallingConventions.Any, method.ReturnType, method.GetParameters().Select(t => t.ParameterType).ToArray());

                var throwAttrib = method.GetCustomAttributes<ThrowOnErrorAttribute>().LastOrDefault();

                var generator = newMethod.GetILGenerator();

                if (throwAttrib != null)
                {
                    GenerateThrowingInvocation(generator, method);
                }

            }

            throw new NotImplementedException();
        }

        private void GenerateInvocationFields(IEnumerable<MethodInfo> methods, TypeBuilder builder)
        {
            foreach (var method in methods)
            {
                var delegateType = CreateDelegateType(method, builder).CreateType();
                builder.DefineField(string.Format("_{0}", method.Name), delegateType, FieldAttributes.Private);
            }
        }

        private void GenerateThrowingInvocation(ILGenerator generator, MethodInfo method)
        {
            GenerateInvocation(generator, method);
        }

        private void GenerateInvocation(ILGenerator generator, MethodInfo method)
        {
            
        }
    }
}
