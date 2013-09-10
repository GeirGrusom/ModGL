using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    public class ExtensionMissingException : Exception
    {
        public string Extension { get; private set; }

        public ExtensionMissingException(string message, string extension, Exception innerException)
            : base(message, innerException)
        {
            Extension = extension;
        }
        public ExtensionMissingException(string message, string extension)
            : base(message)
        {
            Extension = extension;
        }
        public ExtensionMissingException(string extension, Exception innerException)
            : base(string.Format("Missing extension '{0}'.", extension), innerException)
        {
            Extension = extension;
        }

        public ExtensionMissingException(string extension)
            : base(string.Format("Missing extension '{0}'.", extension))
        {
            Extension = extension;
        }

        
    }
}
