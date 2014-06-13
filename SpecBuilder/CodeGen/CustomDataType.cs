using System.IO;

namespace SpecBuilder.CodeGen
{
    public sealed class CustomDataType : DataType
    {
        private readonly string _type;

        public string Type
        {
            get { return _type; }
        }

        public CustomDataType(string type)
        {
            _type = type;
        }

        public override void Write(StreamWriter writer, int tabs)
        {
            writer.Write(_type, tabs);
        }
    }
}