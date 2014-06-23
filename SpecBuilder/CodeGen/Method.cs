using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpecBuilder.CodeGen
{
    public sealed class Method : ICodeDomWriteable
    {
        private readonly string _name;
        private readonly DataType _returnType;
        private readonly MethodParameter[] _parameters;
        private readonly AttributeElement[] _attributes;

        public string Name { get { return _name; } }

        public DataType ReturnType { get { return _returnType; } }

        public IEnumerable<MethodParameter> Parameters { get { return _parameters; } }

        public IEnumerable<AttributeElement> Attributes { get { return _attributes; } }

        public Method(string name, DataType returnType, IEnumerable<MethodParameter> parameters)
            : this(name, returnType, parameters, Enumerable.Empty<AttributeElement>())
        {
            
        }

        public Method(string name, DataType returnType)
            : this(name, returnType, Enumerable.Empty<MethodParameter>(), Enumerable.Empty<AttributeElement>())
        {
        }


        public Method(string name, DataType returnType, IEnumerable<MethodParameter> parameters, IEnumerable<AttributeElement> attributes)
        {
            _name = name;
            _returnType = returnType;
            _parameters = parameters.ToArray();
            _attributes = attributes.ToArray();
        }

        public void Write(StreamWriter writer, int tabs)
        {
            var indent = NameFormatter.Indent(tabs);
            foreach (var attribute in _attributes)
                attribute.Write(writer, tabs);

            var ret = _returnType as SystemDataType;
            if(ret != null && ret.Type == typeof(string))
                writer.WriteLine(indent + "[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstStringReturnMarshaller))]");

            writer.Write(indent);

            if(_parameters.Select(p => p.DataType).OfType<SystemDataType>().Any(p => p.Type.IsPointer))
                writer.Write("unsafe ");
            _returnType.Write(writer, tabs);
            writer.Write(" {0}(", _name);
            for (int i = 0; i < _parameters.Length; i++)
            {
                _parameters[i].Write(writer, tabs);
                if (i < _parameters.Length - 1)
                    writer.Write(", ");
            }
            writer.WriteLine(");");
        }
    }
}