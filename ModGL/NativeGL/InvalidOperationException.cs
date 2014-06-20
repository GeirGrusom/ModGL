using System;

namespace ModGL.NativeGL
{
    public class InvalidOperationException : OpenGLException
    {
        public InvalidOperationException(string message, Exception innerException)
            : base(message, ErrorCode.InvalidOperation, innerException)
        {
        }

        public InvalidOperationException(string message)
            : base(message, ErrorCode.InvalidOperation)
        {
        }

        public InvalidOperationException()
            : base(ErrorCode.InvalidOperation)
        {
        }

        public InvalidOperationException(Exception innerException)
            : base(ErrorCode.InvalidOperation, innerException)
        {
        }
    }
}