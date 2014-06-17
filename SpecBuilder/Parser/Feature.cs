using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SpecBuilder.Parser
{
    public class FeatureSet
    {
        private readonly string _profile;
        private readonly HashSet<string> _enums;
        private readonly HashSet<string> _commands;

        public string Profile { get { return _profile; } }
        public ISet<string> Enumerations { get { return _enums; } }
        public ISet<string> Commands { get { return _commands; } }

        public FeatureSet(string profile, IEnumerable<string> enums, IEnumerable<string> commands)
        {
            
            _profile = profile;
            _enums = new HashSet<string>(enums);
            _commands = new HashSet<string>(commands);
        }
    }

    [DebuggerDisplay("Api = {_api}; Name = {_name}; Version = {_version}")]
    public class Feature
    {
        private readonly string _api;
        private readonly string _name;
        private readonly string _version;

        private readonly FeatureSet[] _requirements;
        private readonly FeatureSet[] _remove;

        public string Api { get { return _api; } }
        public string Name { get { return _name; } }
        public string Version { get { return _version; } }
        public IEnumerable<FeatureSet> Requirements { get { return _requirements; } }

        public IEnumerable<FeatureSet> Remove { get { return _remove; } }

        public IEnumerable<string> GetCommands(string profile)
        {
            var commands = new HashSet<string>(_requirements.SelectMany(x => x.Commands));
            commands.ExceptWith(_remove.Where(x => x.Profile == profile).SelectMany(x => x.Commands));
            return commands;
        }

        public IEnumerable<string> GetEnums(string profile)
        {
            var enums = new HashSet<string>(_requirements.SelectMany(x => x.Enumerations));
            enums.ExceptWith(_remove.Where(x => x.Profile == profile).SelectMany(x => x.Enumerations));
            return enums;
        }

        public Feature (string api, string name, string version, IEnumerable<FeatureSet> requirements, IEnumerable<FeatureSet> remove)
        {
            _api = api;
            _name = name;
            _version = version;
            _requirements = requirements.ToArray();
            _remove = remove.ToArray();
        }
    }
}
