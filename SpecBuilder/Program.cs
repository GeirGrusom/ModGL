using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SpecBuilder.Parser;

namespace SpecBuilder
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            var spec = new SpecFile(new Uri("file:///C:/Users/Henning/Documents/Repos/modgl/gl.xml"));
            spec.Build();

            var document = CodeGen.Document.Create(spec);

            document.Write("output.cs");
        }
    }

}
