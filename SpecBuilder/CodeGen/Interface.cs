using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpecBuilder.CodeGen
{
    public sealed class Interface : ICodeDomWriteable
    {
        private readonly string _name;
        private readonly Method[] _methods;
        private readonly string[] _implements;
        private readonly AttributeElement[] _attributes;

        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<Method> Methods
        {
            get { return _methods; }
        }

        public IEnumerable<AttributeElement> Attributes { get { return _attributes; } }

        public IEnumerable<string> Implements { get { return _implements; }}

        public Interface(string name, IEnumerable<Method> methods)
            : this(name, methods, Enumerable.Empty<string>(), Enumerable.Empty<AttributeElement>())
        {

        }


        public Interface(string name, IEnumerable<Method> methods, IEnumerable<string> implements)
            : this(name, methods, implements, Enumerable.Empty<AttributeElement>())
        {
            
        }

        public Interface(string name, IEnumerable<Method> methods, IEnumerable<string> implements, IEnumerable<AttributeElement> attributes)
        {
            _name = name;
            _methods = methods.ToArray();
            _implements = implements.ToArray();
            _attributes = attributes.ToArray();
        }

        private static readonly HashSet<string> dontOverloadMethods = new HashSet<string>
        {
            "CreateShaderProgramv"
        }; 

        public void Write(StreamWriter writer, int tabs)
        {
            var indent = NameFormatter.Indent(tabs);

            foreach(var element in _attributes)
                element.Write(writer, tabs);

            writer.Write(indent + "public interface {0}", _name);
            var impl = (_implements ?? Enumerable.Empty<string>()).ToArray();
            if(impl.Any())
                writer.WriteLine(" : {0}", string.Join(", ", impl));
            else
                writer.WriteLine();
            writer.WriteLine(indent + "{");
            foreach (var method in _methods)
            {
                method.Write(writer, tabs + 1);
                // Check if we can create overloads.
                if (dontOverloadMethods.Contains(method.Name))
                    continue;
                
                var last = method.Parameters.LastOrDefault();
                if (last == null || method.Parameters.Except(new[] {last}).Any(p => p.Flags == TypeFlags.In) ||
                    last.Flags != TypeFlags.In) 
                    continue;

                // Create a T* and IntPtr overloads.

                // We only support system datatypes, since there is no real need for CustomDataType suport.
                var type = last.DataType as SystemDataType;

                if (type == null)
                    continue;

                if (type.Type == typeof (IntPtr))
                {
                    // void* overload for IntPtr methods.
                    CreateOverload(method, new SystemDataType(typeof(void*))).Write(writer, tabs + 1);
                    continue;
                }

                if (!type.Type.IsArray || type.Type.GetArrayRank() > 1)
                    continue;

                CreateOverload(method, new SystemDataType(typeof (IntPtr))).Write(writer, tabs + 1); // IntPtr overload
                CreateOverload(method, new SystemDataType(type.Type.GetElementType().MakePointerType())).Write(writer, tabs + 1); // T* overload
            }
            writer.WriteLine(indent + "}");
        }

        private static Method CreateOverload(Method method, SystemDataType overloadType, bool removeFlags = true)
        {
            var last = method.Parameters.Last();

            var lastParameter = new MethodParameter(last.Name, removeFlags ? TypeFlags.None : last.Flags, overloadType);

            return new Method(method.Name, method.ReturnType,
                method.Parameters.Except(new[] { last }).Concat(new[] { lastParameter }));
            
        }
    }
}