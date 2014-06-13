using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpecBuilder.CodeGen
{
    public sealed class Interface : ICodeDomWriteable
    {
        private readonly string _name;
        private readonly Method[] _methods;

        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<Method> Methods
        {
            get { return _methods; }
        }

        public Interface(string name, IEnumerable<Method> methods)
        {
            _name = name;
            _methods = methods.ToArray();
        }

        public void Write(StreamWriter writer, int tabs)
        {
            var indent = NameFormatter.Indent(tabs);
            writer.WriteLine(indent + "public interface {0}", _name);
            writer.WriteLine(indent + "{");
            foreach (var method in _methods)
            {
                method.Write(writer, tabs + 1);
            }
            writer.WriteLine(indent + "}");
        }
    }
}