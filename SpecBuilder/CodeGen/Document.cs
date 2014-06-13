using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpecBuilder.CodeGen
{
    public partial class Document : ICodeDomWriteable
    {
        private readonly Namespace _namespace;

        public Document(Namespace @namespace)
        {
            _namespace = @namespace;
        }

        public void Write(string filename)
        {
            using (var writer = new StreamWriter(filename, false, Encoding.UTF8))
            {
                Write(writer, 0);
            }
        }

        public string Write()
        {
            using (var memoryStream = new MemoryStream())
            using(var writer = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                Write(writer, 0);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public void Write(StreamWriter writer, int tabs)
        {
            writer.WriteLine(NameFormatter.Indent(tabs) + "using System;");
            writer.WriteLine(NameFormatter.Indent(tabs) + "using System.Runtime.InteropServices;");
            writer.WriteLine();
            _namespace.Write(writer, tabs);
        }
    }
}
