using System;

namespace ModGL.NativeGL
{
    public class OpenGLInvalidOperationException : OpenGLException
    {
        public OpenGLInvalidOperationException(string message, Exception innerException)
            : base(message, ErrorCode.InvalidOperation, innerException)
        {
        }

        public OpenGLInvalidOperationException(string message)
            : base(message, ErrorCode.InvalidOperation)
        {
        }

        public OpenGLInvalidOperationException()
            : base(ErrorCode.InvalidOperation)
        {
        }

        public OpenGLInvalidOperationException(Exception innerException)
            : base(ErrorCode.InvalidOperation, innerException)
        {
        }
    }
}