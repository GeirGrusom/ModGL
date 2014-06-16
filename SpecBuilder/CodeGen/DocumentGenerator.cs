using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            {"GLsync", typeof(IntPtr)},
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
                namespaces.Add(new Namespace(group.Key, Enumerable.Empty<Namespace>(), new [] {constant}, enums, Enumerable.Empty<CodeDelegate>(), Enumerable.Empty<Interface>()));
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

            HashSet<string> validGroups = new HashSet<string>(groups.Select(g => g.Name));

            /* (GLenum source,GLenum type,GLuint id,GLenum severity,GLsizei length,const GLchar *message,const void *userParam) */
            var gldebugprocDelegate = new CodeDelegate("GLDEBUGPROC", new CustomDataType("void"),
                new MethodParameter("source", TypeFlags.None, new SystemDataType(typeof(uint))),
                new MethodParameter("type", TypeFlags.None, new SystemDataType(typeof(uint))),
                new MethodParameter("id", TypeFlags.None, new SystemDataType(typeof(uint))),
                new MethodParameter("severity", TypeFlags.None, new SystemDataType(typeof(uint))),
                new MethodParameter("length", TypeFlags.None, new SystemDataType(typeof(int))),
                new MethodParameter("message", TypeFlags.Out, new SystemDataType(typeof(string))),
                new MethodParameter("userParam", TypeFlags.None, new SystemDataType(typeof(IntPtr))));

            foreach (var feature in spec.Features.Where(f => f.Api.Length == 0 || f.Api.Contains("gl")))
            {
                string interfaceName;
                var renameMatch = InterfaceRenameRegex.Match(feature.Name);
                if (renameMatch.Success)
                {
                    interfaceName = string.Format("IOpenGL{0}{1}", renameMatch.Groups["Major"].Value,
                        renameMatch.Groups["Minor"].Value);
                }
                else
                    interfaceName = "I" + NameFormatter.FormatEnumName(feature.Name);

                
                var interf = new Interface(interfaceName, from command in feature.Requirements
                    .SelectMany(req => req.Commands)
                    let cmd = glCommands.Single(c => c.ReturnType.Name == command)
                    select
                        new Method(command.Substring(2), Convert(cmd.ReturnType, validGroups),
                            cmd.Arguments.Select(m => ConvertToMethodParameter(m, validGroups))));
                interfaces.Add(interf);
            }

            var glNamespace = namespaces.Where(ns => ns.Name == "GL").ToArray();
            var everythingElse = namespaces.Where(ns => ns.Name != "GL").ToArray();

            return new Document(new Namespace("ModGL.NativeGL", everythingElse, glNamespace.SelectMany(x => x.Constants), groups, new [] { gldebugprocDelegate }, interfaces.Concat(glNamespace.SelectMany(x => x.Interfaces))));
        }
        private static readonly Regex InterfaceRenameRegex = new Regex("GL_VERSION_(?<Major>[0-9]+)_(?<Minor>[0-9]+)", RegexOptions.Compiled);
        private static MethodParameter ConvertToMethodParameter(Parser.DataType dataType, HashSet<string> validGroups)
        {
            TypeFlags flags;
            var dt = Convert(dataType, validGroups);

            if (dt is SystemDataType && ((SystemDataType)dt).Type == typeof(IntPtr))
                flags = TypeFlags.None;
            else if (dataType.IsConst)
            {
                if (dataType.PointerIndirection > 0)
                    flags = TypeFlags.In;
                else
                    flags = TypeFlags.None;
            }
            else
            {
                if (dataType.PointerIndirection > 0)
                    flags = TypeFlags.Out;
                else
                    flags = TypeFlags.None;
            }

            return new MethodParameter(dataType.Name, flags, dt);
        }

        private static readonly Dictionary<string, string> enumTranslation = new Dictionary<string, string>
        {
            {"PixelInternalFormat", "InternalFormat" } // PixelInternalFormat has a comment that says "use InternalFormat instead"
        }; 

        private static DataType Convert(Parser.DataType dataType, HashSet<string> validGroups)
        {
            if (dataType.Type == "GLenum")
            {

                string group = dataType.Group != null && enumTranslation.ContainsKey(dataType.Group)
                    ? enumTranslation[dataType.Group]
                    : (validGroups.Contains(dataType.Group) ? dataType.Group : null);

                return group != null
                    ? (DataType) new CustomDataType(group)
                    : new SystemDataType(typeof (uint));
            }

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
            Type type;
            if (dataType.Type == "GLchar" && dataType.PointerIndirection > 0)
            {

                if (dataType.PointerIndirection > 1)
                    type = typeof (string).MakeArrayType(dataType.PointerIndirection - 1);
                else
                    type = typeof (string);
            }
            else
            {
                type = TypeLookup[dataType.Type];
                if (dataType.PointerIndirection > 0)
                    type = type.MakeArrayType(dataType.PointerIndirection);
            }
            return new SystemDataType(type);
        }
    }
}
