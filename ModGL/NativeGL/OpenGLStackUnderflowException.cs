using System;

namespace ModGL.NativeGL
{
    [Obsolete]
    public class OpenGLStackUnderflowException : OpenGLException
    {
        public OpenGLStackUnderflowException(string message, Exception innerException)
            : base(message, (ErrorCode)0x0504, innerException)
        {
        }

        public OpenGLStackUnderflowException(string message)
            : base(message, (ErrorCode)0x0504)
        {
        }

        public OpenGLStackUnderflowException()
            : base((ErrorCode)0x0504)
        {
        }

        public OpenGLStackUnderflowException(Exception innerException)
            : base((ErrorCode)0x0504, innerException)
        {
        }
    }
}