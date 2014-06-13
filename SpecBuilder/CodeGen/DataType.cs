using System.IO;

namespace SpecBuilder.CodeGen
{
    public abstract class DataType : ICodeDomWriteable
    {
        public abstract void Write(StreamWriter writer, int tabs);
    }
}