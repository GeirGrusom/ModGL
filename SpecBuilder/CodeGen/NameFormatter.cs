using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecBuilder.CodeGen
{
    public static class NameFormatter
    {

        public static string Indent(int tabs)
        {
            return new string('\t', tabs);
        }

        private static readonly Dictionary<Type, string> friendlyTypeName = new Dictionary<Type, string>
        {
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(long), "long"},
            {typeof(ulong), "long"},
            {typeof(string), "string"}
        };

        public static string FriendlyName(this Type type)
        {
            string result;
            if(friendlyTypeName.TryGetValue(type, out result))
                return result;
            return type.Name;
        }

        private static readonly HashSet<string> abbreviations = new HashSet<string>
        {
            "AMD",
            "ARB",
            "APPLE",
            "ATI",
            "EXT",
            "HP",
            "IBM",
            "ILM",
            "IMG",
            "KHR",
            "NV",
            "OES",
            "PGI",
            "QCOM",
            "SGI",
            "SGIS",
            "SGIX",
        };

        private static string FormatEnumNameFirstPart(string name)
        {
            if (char.IsDigit(name[0]))
                return "_" + FormatEnumNamePart(name);
            return FormatEnumNamePart(name);
        }

        private static string FormatEnumNamePart(string name)
        {
            if (abbreviations.Contains(name))
                return name;
            return name.Substring(0, 1) + name.Substring(1).ToLower();
        }

        public static string FormatEnumName(string name)
        {
            var parts = name.Split('_');
            return FormatEnumNameFirstPart(parts.ElementAt(1)) + string.Concat(parts.Skip(2).Select(FormatEnumNamePart));
        }
    }
}
