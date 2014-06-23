using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpecBuilder.CodeGen
{
    public class AttributeElement : ICodeDomWriteable
    {
        private readonly string _name;
        private readonly IEnumerable<string> _arguments;
        private readonly IEnumerable<KeyValuePair<string, string>> _assignedArguments;

        public AttributeElement(string name, IEnumerable<string> arguments,
            IEnumerable<KeyValuePair<string, string>> assignedArguments)
        {
            _name = name;
            _arguments = arguments;
            _assignedArguments = assignedArguments;
        }

        public void Write(StreamWriter writer, int tabs)
        {
            writer.Write(NameFormatter.Indent(tabs));
            var args = _arguments.ToArray();
            var explicitArgs = _assignedArguments.ToArray();

            writer.Write("[{0}", _name);
            if (args.Any() && explicitArgs.Any())
                writer.WriteLine("({0}, {1})]", string.Join(", ", args), string.Join(", ", explicitArgs.Select(x => x.Key + " = " + x.Value)));
            else if(args.Any())
                writer.WriteLine("({0})]", string.Join(", ", args));
            else if(explicitArgs.Any())
                writer.WriteLine("({0})]", string.Join(", ", explicitArgs.Select(x => x.Key + " = " + x.Value)));
            else
                writer.WriteLine("]");
        }
    }
}
