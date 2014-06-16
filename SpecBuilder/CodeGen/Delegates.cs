using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecBuilder.CodeGen
{
    public class CodeDelegate : ICodeDomWriteable
    {
        private readonly DataType _returnType;
        private readonly IEnumerable<MethodParameter> _parameters;
        private readonly string _name;
        
        public DataType ReturnType { get { return _returnType; } }
        public IEnumerable<MethodParameter> Parameters { get { return _parameters; } }
        public string Name { get { return _name; } }

        public CodeDelegate(string name, DataType returnType, params MethodParameter[] parameters)
        {
            _name = name;
            _returnType = returnType;
            _parameters = parameters;
        }

        public void Write(StreamWriter writer, int tabs)
        {
            writer.Write(NameFormatter.Indent(tabs) + "public delegate ");
            _returnType.Write(writer, 0);
            writer.Write(" {0}(", _name);
            var allParameters = _parameters.ToArray();
            for (int i = 0; i < allParameters.Length; i++)
            {
                allParameters[i].Write(writer, 0);
                if(i < allParameters.Length - 1)
                    writer.Write(", ");
            }
            writer.WriteLine(");");
        }
    }
}
