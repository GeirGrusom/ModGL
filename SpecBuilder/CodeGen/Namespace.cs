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

        private readonly IEnumerable<CodeDelegate> _delegates; 

        public string Name { get { return _name; } }

        public IEnumerable<Namespace> Namespaces { get { return _namespaces; } }
        public IEnumerable<Constants> Constants { get { return _constants; } }
        public IEnumerable<Enumeration> Enumerations { get { return _enums; } }
        public IEnumerable<CodeDelegate> Delegates { get { return _delegates; } } 
        public IEnumerable<Interface> Interfaces { get { return _interfaces; } } 

        public Namespace(string name, IEnumerable<Namespace> namespaces, IEnumerable<Constants> constants,
            IEnumerable<Enumeration> enums, IEnumerable<CodeDelegate> delegates, IEnumerable<Interface> interfaces)
        {
            _name = name;
            _namespaces = namespaces;
            _constants = constants;
            _enums = enums;
            _delegates = delegates;
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
            foreach (var dl in _delegates) 
                dl.Write(writer, tabs + 1);
            foreach(var it in _interfaces)
                it.Write(writer, tabs + 1);
            writer.WriteLine(indent + "}");
        }
    }
}
