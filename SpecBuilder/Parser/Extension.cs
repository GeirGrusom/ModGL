using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SpecBuilder.Parser
{
    [DebuggerDisplay("Name = {_name}")]
    public class Extension
    {
        private readonly string _name;
        private readonly string[] _supported;
        private readonly Requirements[] _requirements;

        public string Name { get { return _name; } }
        public IEnumerable<string> Supported { get { return _supported; } }
        public IEnumerable<Requirements> Requirements { get { return _requirements; } }

        public Extension(string name, IEnumerable<string> supported, IEnumerable<Requirements> requirements)
        {
            _name = name;
            _supported = supported.ToArray();
            _requirements = requirements.ToArray();
        }
    }
}
