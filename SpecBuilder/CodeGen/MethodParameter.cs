using System.Collections.Generic;
using System.IO;

namespace SpecBuilder.CodeGen
{
    public class MethodParameter : ICodeDomWriteable
    {
        private static readonly HashSet<string> protectedKeywords = new HashSet<string>
        {
            "as",
            "base",
            "bool",
            "byte",
            "break",
            "params",
            "ref",
            "string",
            
        }; 
        private readonly TypeFlags _flags;
        private readonly DataType _dataType;
        private readonly string _name;
        public TypeFlags Flags { get { return _flags; } }
        public DataType DataType { get { return _dataType; } }
        public string Name { get { return _name; } }

        public MethodParameter(string name, TypeFlags flags, DataType dataType)
        {
            _name = name;
            _flags = flags;
            _dataType = dataType;
        }

        public void Write(StreamWriter writer, int tabs)
        {
            if ((_flags & (TypeFlags.In | TypeFlags.Out)) == (TypeFlags.In | TypeFlags.Out))
                writer.Write("[InOut]");
            else if ((_flags & TypeFlags.In) == TypeFlags.In)
                writer.Write("[In]");
            else if ((_flags & TypeFlags.Out) == TypeFlags.Out)
            {
                var type = _dataType as SystemDataType;
                if(type != null && type.Type == typeof(string))
                    writer.Write("ref ");
                else
                    writer.Write("[Out]");
            }

            if ((_flags & TypeFlags.Ref) == TypeFlags.Ref)
                writer.Write("ref ");

            _dataType.Write(writer, tabs);
            writer.Write(' ');
            if(protectedKeywords.Contains(_name))
                writer.Write("@");
            writer.Write(_name);
        }
    }
}