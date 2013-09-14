using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    public class ExtensionNotSupportedException : Exception
    {
        public string Extension { get; private set; }

        public ExtensionNotSupportedException(string message, string extension, Exception innerException)
            : base(message, innerException)
        {
            Extension = extension;
        }
        public ExtensionNotSupportedException(string message, string extension)
            : base(message)
        {
            Extension = extension;
        }
        public ExtensionNotSupportedException(string extension, Exception innerException)
            : base(string.Format("Extension not suported by the current context: '{0}'", extension), innerException)
        {
            Extension = extension;
        }

        public ExtensionNotSupportedException(string extension)
            : base(string.Format("Extension not suported by the current context: '{0}'", extension))
        {
            Extension = extension;
        }

        
    }
}
