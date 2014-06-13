using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecBuilder.Parser;

namespace SpecBuilder.CodeGen
{
    public partial class Document
    {

        private static readonly IDictionary<string, Type> TypeLookup = new Dictionary<string, Type>
        {
            {"GLchar", typeof(sbyte)},
            {"GLbyte", typeof(sbyte)},
            {"GLubyte", typeof(byte)},
            {"GLshort", typeof(short)},
            {"GLushort", typeof(ushort)},
            {"GLclampx", typeof(int)},
            {"GLfixed", typeof(int)},
            {"GLintptr", typeof(IntPtr)},
            {"GLsizeiptr", typeof(IntPtr)},
            {"GLint", typeof(int)},
            {"GLbitfield", typeof(uint)},
            {"GLuint", typeof(uint)},
            {"GLint64", typeof(long)},
            {"GLuint64", typeof(ulong)},
            {"GLboolean", typeof(byte)},
            {"GLsizei", typeof(int)},
            {"GLfloat", typeof(float)},
            {"GLclampf", typeof(float)},
            {"GLdouble", typeof(double)},
            {"GLclampd", typeof(double)},
            {"GLsync", typeof(IntPtr)}
        }; 

        private static HashSet<string> PreserveTypes = new HashSet<string>
        {
            "GLDEBUGPROC"
        };

        public static Document Create(Parser.SpecFile spec)
        {
            var namespaces = new List<Namespace>();

            var enumNamespaceGroups = spec.Enumerations.GroupBy(en => en.Namespace);

            foreach (var group in enumNamespaceGroups)
            {
                var enums = new List<Enumeration>();
                var emptyEnums = group.Where(g =>  g.Name == null).SelectMany(g => g.Fields);
                var nonEmptyGroups = group.Where(g => g.Name != null);
                var constant = new Constants("Constants",
                    emptyEnums.Where(e => e.Api == null || e.Api.Equals("gl", StringComparison.OrdinalIgnoreCase)).OrderBy(e => e.Name).Select(e =>
                    {
                        if ((e.Value >> 32) != 0)
                            return new KeyValuePair<string, object>(e.Name, e.Value);
                        return new KeyValuePair<string, object>(e.Name, (uint)e.Value);
                    }));

                foreach (var en in nonEmptyGroups)
                {
                    var fields = en.Fields.Where(e => e.Api == null || e.Api.Equals("gl", StringComparison.OrdinalIgnoreCase)).ToArray();
                    if (fields.Any(f => (f.Value >> 32) != 0))
                        // Enum only fits in 64-bit
                        enums.Add(new Enumeration<ulong>(en.Name, en.Namespace, en.Type == EnumerationType.Bitmask,
                            fields.Select(
                                f => new KeyValuePair<string, ulong>(NameFormatter.FormatEnumName(f.Name), f.Value))));
                    else
                        enums.Add(new Enumeration<uint>(en.Name, en.Namespace, en.Type == EnumerationType.Bitmask,
                            fields.Select(
                                f =>
                                    new KeyValuePair<string, uint>(NameFormatter.FormatEnumName(f.Name),
                                        unchecked ((uint) f.Value)))));
                }
                namespaces.Add(new Namespace(group.Key, Enumerable.Empty<Namespace>(), new [] {constant}, enums, Enumerable.Empty<Interface>()));
            }

            List<Enumeration> groups = new List<Enumeration>();
            var allEnums =
                spec.Enumerations//.Where(s => s.Api == null || string.Equals(s.Api, "GL", StringComparison.OrdinalIgnoreCase))
                    .SelectMany(x => x.Fields)
                    .Where(s => s.Api == null || string.Equals(s.Api, "gl", StringComparison.OrdinalIgnoreCase))
                    .ToDictionary(x => x.Name, x => x.Value);
            foreach (var group in spec.Groups)
            {
                groups.Add(new Enumeration<uint>(group.Key, "Enumerations", false,
                    group.Value.Where(allEnums.ContainsKey).Select(f => new KeyValuePair<string, uint>(NameFormatter.FormatEnumName(f), checked((uint)allEnums[f])))));
            }
            var glCommands = spec.Commands["GL"];
            List<Interface> interfaces = new List<Interface>();
            foreach (var feature in spec.Features.Where(f => f.Api.Contains("gl")))
            {
                string interfaceName = "I" + NameFormatter.FormatEnumName(feature.Name);
                var interf = new Interface(interfaceName, from command in feature.Requirements
                    .SelectMany(req => req.Commands)
                    let cmd = glCommands.Single(c => c.ReturnType.Name == command)
                    select
                        new Method(command.Substring(2), Convert(cmd.ReturnType),
                            cmd.Arguments.Select(ConvertToMethodParameter)));
                interfaces.Add(interf);
            }

            return new Document(new Namespace("ModGL.NativeGL", namespaces, Enumerable.Empty<Constants>(), groups, interfaces));
        }

        private static MethodParameter ConvertToMethodParameter(Parser.DataType dataType)
        {
            return new MethodParameter(dataType.Name, dataType.IsConst && dataType.PointerIndirection > 0 ? TypeFlags.In : dataType.PointerIndirection > 0 ? TypeFlags.Out : TypeFlags.None, Convert(dataType));
        }

        private static DataType Convert(Parser.DataType dataType)
        {
            if (dataType.Type == "GLenum")
                return dataType.Group != null ? (DataType)new CustomDataType(dataType.Group) : new SystemDataType(typeof(uint));

            if(PreserveTypes.Contains(dataType.Type))
                return new CustomDataType(dataType.Type);

            if (dataType.Type == "void")
            {
                if(dataType.PointerIndirection == 0)
                    return new CustomDataType("void");

                if (dataType.PointerIndirection == 1)
                    return new SystemDataType(typeof (IntPtr));

                return new SystemDataType(typeof(IntPtr).MakeArrayType(dataType.PointerIndirection - 1));
            }
            var type = TypeLookup[dataType.Type];
            if (dataType.PointerIndirection > 0)
                type = type.MakeArrayType(dataType.PointerIndirection);
            return new SystemDataType(type);
        }
    }
}
