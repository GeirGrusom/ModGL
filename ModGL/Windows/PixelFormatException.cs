using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Windows
{
    [Serializable]
    public class PixelFormatException : ContextCreationException
    {
        public PixelFormatException
        (
            string message,
            ContextCreationParameters parameters,
            PixelFormatDescriptor descriptor,
            Exception innerException
        ) : base(message, parameters, innerException)
        {
            Descriptor = descriptor;
        }
        public PixelFormatException
        (
            string message,
            ContextCreationParameters parameters,
            PixelFormatDescriptor descriptor
        ) : base(message, parameters)
        {
            Descriptor = descriptor;
        }

        public PixelFormatDescriptor Descriptor { get; private set; }
    }
}
