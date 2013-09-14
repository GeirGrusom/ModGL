using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    public class VersionNotSupportedException : ContextCreationException
    {
        public VersionNotSupportedException(string message, ContextCreationParameters parameters, Exception innerException) : base(message, parameters, innerException)
        {
        }

        public VersionNotSupportedException(string message, ContextCreationParameters parameters) : base(message, parameters)
        {
        }

        public VersionNotSupportedException(ContextCreationParameters parameters, Exception innerException)
            : base("OpenGL version is not supported.", parameters, innerException)
        {
        }

        public VersionNotSupportedException(ContextCreationParameters parameters)
            : this(parameters, null)
        {
        }

    }
}
