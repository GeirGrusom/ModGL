using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.NativeGL
{
    [Serializable]
    public class InvalidFramebufferOperationException : OpenGLException
    {
        public InvalidFramebufferOperationException(string message, Exception innerException) 
            : base(message, ErrorCode.InvalidFramebufferOperation, innerException)
        {
        }

        public InvalidFramebufferOperationException(string message) 
            : base(message, ErrorCode.InvalidFramebufferOperation)
        {
        }

        public InvalidFramebufferOperationException(Exception innerException) 
            : base(ErrorCode.InvalidFramebufferOperation, innerException)
        {
        }

        public InvalidFramebufferOperationException() 
            : base(ErrorCode.InvalidFramebufferOperation)
        {
        }
    }
}
