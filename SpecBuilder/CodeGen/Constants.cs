using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecBuilder.CodeGen
{
    public class Constants : ICodeDomWriteable
    {
        private readonly string _name;
        private readonly IDictionary<string, object> _values;

        public Constants(string name, IEnumerable<KeyValuePair<string, object>> values)
        {
            _name = name;
            _values = new Dictionary<string, object>();
            foreach (var element in values)
            {
                var key = NameFormatter.FormatEnumName(element.Key);

                if (_values.ContainsKey(key))
                {
                    var oldValue = _values[key];
                }
                else
                    _values.Add(key, element.Value);
            }

            //_values = values.ToDictionary(x => NameFormatter.FormatEnumName(x.Key), x => x.Value);
        }


        public void Write(StreamWriter writer, int tabs)
        {
            var indent = NameFormatter.Indent(tabs);
            writer.WriteLine(indent + "public static class {0}", _name);
            writer.WriteLine(indent + "{");

            foreach (var item in _values)
            {
                if(item.Value is string)
                    writer.WriteLine(indent + "\tpublic const string {0} = \"{1}\";", item.Key, item.Value);
                else
                    writer.WriteLine(indent + "\tpublic const {0} {1} = 0x{2:x};", item.Value.GetType().FriendlyName(), item.Key, item.Value);
            }
            writer.WriteLine(indent + "}");
        }
    }
}
