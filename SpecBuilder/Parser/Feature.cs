using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SpecBuilder.Parser
{
    public class Requirements
    {
        private readonly string _profile;
        private readonly string[] _enums;
        private readonly string[] _commands;

        public string Profile { get { return _profile; } }
        public IEnumerable<string> Enumerations { get { return _enums; } }
        public IEnumerable<string> Commands { get { return _commands; } }

        public Requirements(string profile, IEnumerable<string> enums, IEnumerable<string> commands)
        {
            _profile = profile;
            _enums = enums.ToArray();
            _commands = commands.ToArray();
        }
    }

    [DebuggerDisplay("Api = {_api}; Name = {_name}; Version = {_version}")]
    public class Feature
    {
        private readonly string _api;
        private readonly string _name;
        private readonly string _version;

        private readonly Requirements[] _requirements;

        public string Api { get { return _api; } }
        public string Name { get { return _name; } }
        public string Version { get { return _version; } }
        public IEnumerable<Requirements> Requirements { get { return _requirements; } } 

        public Feature (string api, string name, string version, IEnumerable<Requirements> requirements )
        {
            _api = api;
            _name = name;
            _version = version;
            _requirements = requirements.ToArray();
        }
    }
}
