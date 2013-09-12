using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceBuilder
{
    public interface IRetriever
    {
        XDocument Retrieve();
    }

    public class OpenGLOrgXmlRetriever
    {
        public const string WebPageAddress =
            "https://cvs.khronos.org/svn/repos/ogl/trunk/doc/registry/public/api/gl.xml";
        public XDocument Retrieve()
        {
            using (var response = System.Net.HttpWebRequest.Create(WebPageAddress).GetResponse())
            {
                return XDocument.Load(response.GetResponseStream());
            }
        }
    }
}
