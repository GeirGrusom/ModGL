using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SpecBuilder.Parser
{
    public class SpecFile
    {
        private readonly XDocument _document;
        private readonly IDictionary<string, ulong> _constants;
        private readonly IList<Enumeration> _enums;
        private readonly IDictionary<string, IList<string>> _groups;
        private readonly IDictionary<string, IList<Command>> _commands;
        private readonly IList<Feature> _features;
        private readonly IList<Extension> _extensions; 

        public IDictionary<string, ulong> Constants { get { return _constants; } }
        public IList<Enumeration> Enumerations { get { return _enums; } }
        public IDictionary<string, IList<string>> Groups { get { return _groups; } }
        public IDictionary<string, IList<Command>> Commands { get { return _commands; } }
        public IList<Feature> Features { get { return _features; } }
        public IList<Extension> Extensions { get { return _extensions; } } 

        private static readonly Dictionary<string, string> typeConvertDictionary = new Dictionary<string, string>
        {
            {"GLchar", "byte"},
            {"GLbyte", "sbyte"},
            {"GLubyte", "byte"},
            {"GLshort", "short"},
            {"GLushort", "ushort"},
            {"GLint", "int"},
            {"GLuint", "uint"},
            {"GLenum", "uint"},
            {"GLsizei", "int"},
            {"GLfloat", "float"},
            {"GLclampf", "float"},
            {"GLdouble", "double"},
            {"GLclampd", "double"},
            {"GLfixed", "int"},
            {"GLintptr", "IntPtr"},
            {"GLsizeiptr", "IntPtr"},
            {"GLint64", "long"},
            {"GLuint64", "ulong"},
            {"GLsync", "IntPtr"}
        }; 

        public SpecFile(Uri documentUri)
        {
            _document = XDocument.Load(documentUri.ToString());
            _enums = new List<Enumeration>();
            _groups = new Dictionary<string, IList<string>>();
            _constants = new Dictionary<string, ulong>();
            _commands = new Dictionary<string, IList<Command>>();
            _features = new List<Feature>();
            _extensions = new List<Extension>();
        }

        public void Build()
        {
            var root = _document.Root;

            if(root == null || root.Name != "registry")
                throw new FormatException("The document is not in an expected format.");

            //var types = (from node in root.Nodes().OfType<XElement>() where node.Name == "types" select node).SelectMany(n => n.Nodes()).ToArray();
            var enums = from en in root.Nodes().OfType<XElement>()
                where en.Name == "enums"
                select ParseEnumeration(en);

            foreach(var en in enums)
                _enums.Add(en);

            var groups = from node in root.Nodes().OfType<XElement>().Single(n => n.Name == "groups").Nodes().OfType<XElement>()
                            where node.Name == "group"
                            select node;

            foreach (var group in groups)
            {
                var groupName = group.TryGetAttributeValue("name");
                var elements = from node in @group.Nodes().OfType<XElement>()
                    where node.Name == "enum"
                    select node.TryGetAttributeValue("name");
                    
                _groups.Add(groupName, elements.ToArray());
            }

            var allCommands =
                from commands in root.Nodes().OfType<XElement>()
                where commands.Name == "commands"
                select new {Key = commands.TryGetAttributeValue("namespace"), Values = ParseCommands(commands)};
                
            
            foreach(var commandgroup in allCommands)            
                _commands.Add(commandgroup.Key, commandgroup.Values.ToArray());

            var features = from feature in root.Nodes().OfType<XElement>()
                where feature.Name == "feature"
                let reqs = (from requirement in feature.Nodes().OfType<XElement>()
                    where requirement.Name == "require" select requirement)
                select
                    new Feature(feature.TryGetAttributeValue("api"), feature.TryGetAttributeValue("name"),
                            feature.TryGetAttributeValue("number"),
                            reqs.Select(r => new Requirements(
                                r.TryGetAttributeValue("profile"),
                                r.Nodes().OfType<XElement>().Where(n => n.Name == "enum").Select(n => n.TryGetAttributeValue("name")),
                                r.Nodes().OfType<XElement>().Where(n => n.Name == "command").Select(n => n.TryGetAttributeValue("name")))));
            foreach(var feature in features)
                _features.Add(feature);

            var extensions =
                from extension in
                    root.Nodes().OfType<XElement>().Single(n => n.Name == "extensions").Nodes().OfType<XElement>()
                where extension.Name == "extension"
                let requirements = (from requirement in extension.Nodes().OfType<XElement>()
                    where requirement.Name == "requirement"
                    select new Requirements(requirement.TryGetAttributeValue("profile"),
                        requirement.Nodes()
                            .OfType<XElement>()
                            .Where(n => n.Name == "enum")
                            .Select(n => n.TryGetAttributeValue("name")),
                        requirement.Nodes()
                            .OfType<XElement>()
                            .Where(n => n.Name == "command")
                            .Select(n => n.TryGetAttributeValue("name"))))
                select
                    new Extension(extension.TryGetAttributeValue("name"),
                        (extension.TryGetAttributeValue("supported") ?? "").Split('|'), requirements);

            foreach(var extension in extensions)
                _extensions.Add(extension);

        }

        private static Enumeration ParseEnumeration(XElement enumNode)
        {
            var group = enumNode.TryGetAttributeValue("group");
            var ns = enumNode.TryGetAttributeValue("namespace");
            var vendor = enumNode.TryGetAttributeValue("vendor");
            var enApi = enumNode.TryGetAttributeValue("api");
            var type = enumNode.TryGetAttributeValue("type");

            var elements = from member in enumNode.Nodes().OfType<XElement>()
                where member.Name == "enum"
                let name = member.TryGetAttributeValue("name")
                let value = member.TryGetAttributeValue("value")
                let api = member.TryGetAttributeValue("api")
                let intValue = value.StartsWith("0x")
                    ? ulong.Parse(value.Substring(2), NumberStyles.HexNumber)
                    : unchecked((ulong) long.Parse(value))
                select new {Name = name, Api = api, Value = intValue};

            return new Enumeration(
                group,
                ns,
                vendor,
                enApi,
                type == "bitmask" ? EnumerationType.Bitmask : EnumerationType.None,
                elements.Select(x => new Enumeration.EnumerationField(x.Name, x.Value, x.Api)));            
        }

        private static IEnumerable<Command> ParseCommands(XElement commandsNode)
        {
            return from command in commandsNode.Nodes().OfType<XElement>().Where(n => n.Name == "command")
                let proto = ParseDataType(command.Nodes().OfType<XElement>().Single(n => n.Name == "proto"))
                let parameters = command.Nodes().OfType<XElement>().Where(n => n.Name == "param").Select(ParseDataType)
                select new Command(proto, parameters);
        }

        private static DataType ParseDataType(XElement element)
        {
            var nodes = element.Nodes().ToArray();
            var ptype = element.Nodes().OfType<XElement>().FirstOrDefault(n => n.Name == "ptype");
            string group = element.TryGetAttributeValue("group");
            string type;
            int ptypeIndex;
            if (ptype == null)
            {
                type = "void";
                ptypeIndex = int.MaxValue;
            }
            else
            {
                type = ptype.Value;
                ptypeIndex = Array.IndexOf(nodes, ptype);
            }
            bool isConst = false;
            bool isPointerConst = false;
            int pointerIndirection = 0;
            string name = element.Nodes().OfType<XElement>().Single(n => n.Name == "name").Value;
            for (int i = 0; i < nodes.Length; i++)
            {
                var textnode = nodes[i] as XText;
                if (textnode != null)
                {
                    string text = textnode.Value.Trim();
                    if (textnode.Value.Trim() == "const")
                    {
                        if (i < ptypeIndex)
                            isConst = true;
                    }
                    else if (text.Contains("*"))
                        pointerIndirection = text.Count(c => c == '*');
                }
            }
            return new DataType(name, group, pointerIndirection, type, isConst, isPointerConst);
        }
    }
}