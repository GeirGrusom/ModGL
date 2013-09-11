using System;

namespace ModGL.NativeGL
{
    public class OpenGLStackOverflowException : OpenGLException
    {
        public OpenGLStackOverflowException(string message, Exception innerException)
            : base(message, ErrorCode.StackOverflow, innerException)
        {
        }

        public OpenGLStackOverflowException(string message)
            : base(message, ErrorCode.StackOverflow)
        {
        }

        public OpenGLStackOverflowException()
            : base(ErrorCode.StackOverflow)
        {
        }

        public OpenGLStackOverflowException(Exception innerException)
            : base(ErrorCode.StackOverflow, innerException)
        {
        }
    }
}