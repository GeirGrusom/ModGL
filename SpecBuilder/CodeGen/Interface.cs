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

        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<Method> Methods
        {
            get { return _methods; }
        }

        public IEnumerable<string> Implements { get { return _implements; }}

        public Interface(string name, IEnumerable<Method> methods, IEnumerable<string> implements)
        {
            _name = name;
            _methods = methods.ToArray();
            _implements = implements.ToArray();
        }

        public void Write(StreamWriter writer, int tabs)
        {
            var indent = NameFormatter.Indent(tabs);
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
            }
            writer.WriteLine(indent + "}");
        }
    }
}