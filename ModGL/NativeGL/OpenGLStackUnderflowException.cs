using System;

namespace ModGL.NativeGL
{
    public class OpenGLStackUnderflowException : OpenGLException
    {
        public OpenGLStackUnderflowException(string message, Exception innerException)
            : base(message, ErrorCode.StackUnderflow, innerException)
        {
        }

        public OpenGLStackUnderflowException(string message)
            : base(message, ErrorCode.StackUnderflow)
        {
        }

        public OpenGLStackUnderflowException()
            : base(ErrorCode.StackUnderflow)
        {
        }

        public OpenGLStackUnderflowException(Exception innerException)
            : base(ErrorCode.StackUnderflow, innerException)
        {
        }
    }
}