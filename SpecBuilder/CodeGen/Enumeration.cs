using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpecBuilder.CodeGen
{
    public abstract class Enumeration : ICodeDomWriteable
    {
        private readonly bool _flags;
        private readonly string _name;
        private readonly string _namespace;

        protected Enumeration(string name, string @namespace, bool isFlags)
        {
            _name = name;
            _namespace = @namespace;
            _flags = isFlags;
        }

        public bool Flags
        {
            get { return _flags; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Namespace { get { return _namespace; } }

        public abstract void Write(StreamWriter writer, int tabs);
    }

    public class Enumeration<TBaseType> : Enumeration
        where TBaseType : struct, IFormattable
    {
        private readonly IList<Field> _fields;


        public IDictionary<string, TBaseType> Values
        {
            get { return _fields.ToDictionary(x => x.Name, x => x.Value); }
        }

        public Enumeration(string name, string @namespace, bool isFlags, IEnumerable<KeyValuePair<string, TBaseType>> values)
            : base(name, @namespace, isFlags)
        {
            _fields = values.Select(v => new Field(v.Key, v.Value)).ToArray();
        }

        private class Field : ICodeDomWriteable
        {

            public string Name { get; private set; }
            public TBaseType Value { get; private set; }

            public Field(string name, TBaseType value)
            {
                Name = name;
                Value = value;
            }

            public void Write(StreamWriter writer, int tabs)
            {
                writer.WriteLine(NameFormatter.Indent(tabs) + "{0} = 0x{1:x},", Name, Value);
            }
        }

        public override void Write(StreamWriter writer, int tabs)
        {
            var indent = NameFormatter.Indent(tabs);
            if (Flags)
                writer.WriteLine(indent + "[Flags]");
            writer.WriteLine(indent + "public enum {0} : {1}", Name, TypeNameHelper.TypeNameLookup[typeof(TBaseType)]);
            writer.WriteLine(indent + "{");

            foreach (var field in _fields)
            {
                field.Write(writer, tabs + 1);
            }
            writer.WriteLine(indent + "}");
        }
    }
}