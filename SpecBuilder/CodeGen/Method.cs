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

        public Method(string name, DataType returnType, IEnumerable<MethodParameter> parameters)
        {
            _name = name;
            _returnType = returnType;
            _parameters = parameters.ToArray();
        }

        public void Write(StreamWriter writer, int tabs)
        {
            var indent = NameFormatter.Indent(tabs);

            var ret = _returnType as SystemDataType;
            if(ret != null && ret.Type == typeof(string))
                writer.WriteLine(indent + "[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstStringReturnMarshaller))]");

            writer.Write(indent);
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