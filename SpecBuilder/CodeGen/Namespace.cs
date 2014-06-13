using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecBuilder.CodeGen
{
    public class Namespace : ICodeDomWriteable
    {
        private readonly string _name;

        private readonly IEnumerable<Namespace> _namespaces;

        private readonly IEnumerable<Constants> _constants;

        private readonly IEnumerable<Enumeration> _enums;

        private readonly IEnumerable<Interface> _interfaces;

        public Namespace(string name, IEnumerable<Namespace> namespaces, IEnumerable<Constants> constants,
            IEnumerable<Enumeration> enums, IEnumerable<Interface> interfaces)
        {
            _name = name;
            _namespaces = namespaces;
            _constants = constants;
            _enums = enums;
            _interfaces = interfaces;
        }

        public void Write(StreamWriter writer, int tabs)
        {
            var indent = NameFormatter.Indent(tabs);
            writer.WriteLine();
            writer.WriteLine(indent + "namespace {0}", _name);
            writer.WriteLine(indent + "{");
            foreach (var ns in _namespaces)
                ns.Write(writer, tabs + 1);

            foreach(var co in _constants)
                co.Write(writer, tabs + 1);
            foreach(var en in _enums)
                en.Write(writer, tabs + 1);
            foreach(var it in _interfaces)
                it.Write(writer, tabs + 1);
            writer.WriteLine(indent + "}");
        }
    }
}
