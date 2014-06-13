using System.Xml.Linq;

namespace SpecBuilder.Parser
{
    public static class XmlExtensions
    {
        public static string TryGetAttributeValue(this XElement element, string attributeName)
        {
            var attrib = element.Attribute(XName.Get(attributeName));
            if (attrib != null)
                return attrib.Value;
            return null;
        }
    }
}