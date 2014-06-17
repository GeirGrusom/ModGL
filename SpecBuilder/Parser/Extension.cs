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
        private readonly FeatureSet[] _featureSet;

        public string Name { get { return _name; } }
        public IEnumerable<string> Supported { get { return _supported; } }
        public IEnumerable<FeatureSet> FeatureSet { get { return _featureSet; } }

        public Extension(string name, IEnumerable<string> supported, IEnumerable<FeatureSet> requirements)
        {
            _name = name;
            _supported = supported.ToArray();
            _featureSet = requirements.ToArray();
        }
    }
}
