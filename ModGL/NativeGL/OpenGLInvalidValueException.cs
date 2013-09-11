using System;

namespace ModGL.NativeGL
{
    public class OpenGLInvalidValueException : OpenGLException
    {
        public OpenGLInvalidValueException(string message, Exception innerException)
            : base(message, ErrorCode.InvalidValue, innerException)
        {
        }

        public OpenGLInvalidValueException(string message)
            : base(message, ErrorCode.InvalidValue)
        {
        }

        public OpenGLInvalidValueException()
            : base(ErrorCode.InvalidValue)
        {
        }

        public OpenGLInvalidValueException(Exception innerException)
            : base(ErrorCode.InvalidValue, innerException)
        {
        }
    }
}