﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        internal class CommandComparer : IEqualityComparer<Command>
        {
            public static readonly CommandComparer Comparer = new CommandComparer(); 
            public bool Equals(Command x, Command y)
            {
                return x.ReturnType.Name == y.ReturnType.Name;
            }

            public int GetHashCode(Command obj)
            {
                return obj.ReturnType.Name.GetHashCode();
            }
        }

        public static Document Create(SpecFile spec)
        {

            var validEnums = new HashSet<string>(spec.Features.SelectMany(en => en.GetEnums("core")));
            var validCommands = new HashSet<string>(spec.Features.SelectMany(en => en.GetCommands("core")));

            validEnums.UnionWith(spec.Extensions.SelectMany(ex => ex.FeatureSet.SelectMany(f => f.Enumerations)));
            validCommands.UnionWith(spec.Extensions.SelectMany(ex => ex.FeatureSet.SelectMany(f => f.Commands)));

            var namespaces = new List<Namespace>();

            var enumNamespaceGroups = spec.Enumerations.GroupBy(en => en.Namespace);

            foreach (var group in enumNamespaceGroups)
            {
                var enums = new List<Enumeration>();
                var emptyEnums = group.Where(g =>  g.Name == null).SelectMany(g => g.Fields);
                var nonEmptyGroups = group.Where(g => g.Name != null);
                var constant = new Constants("Constants",
                    emptyEnums.Where(e => e.Api == null || e.Api.Equals("gl", StringComparison.OrdinalIgnoreCase)).OrderBy(e => e.Name).Where(e => validEnums.Contains(e.Name)).Select(e =>
                    {
                        if ((e.Value >> 32) != 0)
                            return new KeyValuePair<string, object>(e.Name, e.Value);
                        return new KeyValuePair<string, object>(e.Name, (uint)e.Value);
                    }));

                foreach (var en in nonEmptyGroups)
                {
                    var fields = en.Fields.Where(e => validEnums.Contains(e.Name) &&  (e.Api == null || e.Api.Equals("gl", StringComparison.OrdinalIgnoreCase))).ToArray();
                    Enumeration enumeration;
                    if (fields.Any(f => (f.Value >> 32) != 0))
                        // Enum only fits in 64-bit
                        enumeration = new Enumeration<ulong>(en.Name, en.Namespace, en.Type == EnumerationType.Bitmask,
                            fields.Select(
                                f => new KeyValuePair<string, ulong>(NameFormatter.FormatEnumName(f.Name), f.Value)));
                    else
                        enumeration = new Enumeration<uint>(en.Name, en.Namespace, en.Type == EnumerationType.Bitmask,
                            fields.Select(
                                f =>
                                    new KeyValuePair<string, uint>(NameFormatter.FormatEnumName(f.Name),
                                        unchecked ((uint) f.Value))));
                    if (enumeration.IsEmpty())
                        continue;
                    enums.Add(enumeration);
                }
                namespaces.Add(new Namespace(group.Key, Enumerable.Empty<Namespace>(), new [] {constant}, enums, Enumerable.Empty<CodeDelegate>(), Enumerable.Empty<Interface>()));
            }

            var groups = new List<Enumeration>();
            var allEnums =
                spec.Enumerations//.Where(s => s.Api == null || string.Equals(s.Api, "GL", StringComparison.OrdinalIgnoreCase))
                    
                    .SelectMany(x => x.Fields)
                    .Where(s => validEnums.Contains(s.Name) && (s.Api == null || string.Equals(s.Api, "gl", StringComparison.OrdinalIgnoreCase)))
                    .ToDictionary(x => x.Name, x => x.Value);
            foreach (var group in spec.Groups)
            {
                var enumeration = new Enumeration<uint>(group.Key, "Enumerations", false,
                    group.Value.Where(allEnums.ContainsKey)
                        .Select(
                            f =>
                                new KeyValuePair<string, uint>(NameFormatter.FormatEnumName(f),
                                    checked((uint) allEnums[f]))));
                if (enumeration.IsEmpty())
                    continue;
                groups.Add(enumeration);
            }
            var glCommands = spec.Commands["GL"].Where(c => validCommands.Contains(c.ReturnType.Name));
            
            var validGroups = new HashSet<string>(groups.Select(g => g.Name));

            /* (GLenum source,GLenum type,GLuint id,GLenum severity,GLsizei length,const GLchar *message,const void *userParam) */
            var gldebugprocDelegate = new CodeDelegate("GLDEBUGPROC", new CustomDataType("void"),
                new MethodParameter("source", TypeFlags.None, new SystemDataType(typeof(uint))),
                new MethodParameter("type", TypeFlags.None, new SystemDataType(typeof(uint))),
                new MethodParameter("id", TypeFlags.None, new SystemDataType(typeof(uint))),
                new MethodParameter("severity", TypeFlags.None, new SystemDataType(typeof(uint))),
                new MethodParameter("length", TypeFlags.None, new SystemDataType(typeof(int))),
                new MethodParameter("message", TypeFlags.Out, new SystemDataType(typeof(string))),
                new MethodParameter("userParam", TypeFlags.None, new SystemDataType(typeof(IntPtr))));

            var interfaces = GetInterfaces(spec, glCommands, validGroups);

            var glNamespace = namespaces.Where(ns => ns.Name == "GL").ToArray();
            var everythingElse = namespaces.Where(ns => ns.Name != "GL").ToArray();

            // There is currently just one namespace. 
            return new Document(new Namespace("ModGL.NativeGL", /*everythingElse */ Enumerable.Empty<Namespace>(), glNamespace.SelectMany(x => x.Constants), groups, new [] { gldebugprocDelegate }, interfaces.Concat(glNamespace.SelectMany(x => x.Interfaces))));
        }

        private static IEnumerable<Interface> GetInterfaces(SpecFile spec, IEnumerable<Command> glCommands, HashSet<string> validGroups)
        {
            var versionInterfaces = new SortedList<Version, string>();
            var interfaces = new List<Interface>();
            foreach (var feature in spec.Features.Where(f => f.Api.Length == 0 || f.Api.Contains("gl")))
            {
                IEnumerable<string> previousVersion;
                string interfaceName = GetInterfaceName(feature, versionInterfaces, out previousVersion);

                var interf = new Interface(interfaceName, from command in feature.Requirements
                    .SelectMany(req => req.Commands).Distinct()
                    let cmd = glCommands.Single(c => c.ReturnType.Name == command)
                    select
                        new Method(command.Substring(2), Convert(cmd.ReturnType, validGroups),
                            cmd.Arguments.Select(m => ConvertToMethodParameter(m, validGroups))), previousVersion);
                if(interf.Methods.Any())
                    interfaces.Add(interf);
            }
            return interfaces;
        }

        private static string GetInterfaceName(Feature feature, SortedList<Version, string> versionInterfaces, out IEnumerable<string> previousVersion)
        {
            string interfaceName;
            var renameMatch = InterfaceRenameRegex.Match(feature.Name);
            if (renameMatch.Success)
            {
                int major = int.Parse(renameMatch.Groups["Major"].Value);
                int minor = int.Parse(renameMatch.Groups["Minor"].Value);
                interfaceName = string.Format("IOpenGL{0}{1}", major, minor);
                if (versionInterfaces.Any())
                    previousVersion = new[] {versionInterfaces.Last().Value};
                else
                    previousVersion = Enumerable.Empty<string>();
                versionInterfaces.Add(new Version(major, minor), interfaceName);
            }
            else
            {
                interfaceName = "I" + NameFormatter.FormatEnumName(feature.Name);
                previousVersion = Enumerable.Empty<string>();
            }
            return interfaceName;
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
