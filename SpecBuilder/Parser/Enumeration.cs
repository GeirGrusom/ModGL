using System.Collections.Generic;

namespace SpecBuilder.Parser
{
 

    public class Enumeration
    {
        public class EnumerationField
        {
            public string Name { get; private set; }
            public ulong Value { get; private set; }
            public string Api { get; private set; }

            public EnumerationField(string name, ulong value, string api)
            {
                Name = name;
                Value = value;
                Api = api;
            }
        }
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public string Vendor { get; private set; }
        public string Api { get; private set; }
        public EnumerationType Type { get; private set; } 
        public IEnumerable<EnumerationField> Fields { get; private set; }

        public Enumeration(string name, string @namespace, string vendor, string api, EnumerationType type, IEnumerable<EnumerationField> fields)
        {
            Name = name;
            Namespace = @namespace;
            Vendor = vendor;
            Api = api;
            Type = type;
            Fields = fields;
        }
    }
}