using System;

namespace ModGL.NativeGL
{
    [Obsolete]
    public class OpenGLStackOverflowException : OpenGLException
    {
        public OpenGLStackOverflowException(string message, Exception innerException)
            : base(message, (ErrorCode)0x0503, innerException)
        {
        }

        public OpenGLStackOverflowException(string message)
            : base(message, (ErrorCode)0x0503)
        {
        }

        public OpenGLStackOverflowException()
            : base((ErrorCode)0x0503)
        {
        }

        public OpenGLStackOverflowException(Exception innerException)
            : base((ErrorCode)0x0503, innerException)
        {
        }
    }
}