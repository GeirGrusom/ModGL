using System.IO;

namespace SpecBuilder.CodeGen
{
    public interface ICodeDomWriteable
    {
        void Write(StreamWriter writer, int tabs);
    }
}