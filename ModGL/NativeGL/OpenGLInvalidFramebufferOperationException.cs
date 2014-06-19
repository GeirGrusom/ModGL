using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.NativeGL
{
    [Serializable]
    public class OpenGLInvalidFramebufferOperationException : OpenGLException
    {
        public OpenGLInvalidFramebufferOperationException(string message, Exception innerException) 
            : base(message, ErrorCode.InvalidFramebufferOperation, innerException)
        {
        }

        public OpenGLInvalidFramebufferOperationException(string message) 
            : base(message, ErrorCode.InvalidFramebufferOperation)
        {
        }

        public OpenGLInvalidFramebufferOperationException(Exception innerException) 
            : base(ErrorCode.InvalidFramebufferOperation, innerException)
        {
        }

        public OpenGLInvalidFramebufferOperationException() 
            : base(ErrorCode.InvalidFramebufferOperation)
        {
        }
    }
}
