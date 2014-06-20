using System;

namespace ModGL.NativeGL
{
    public class InvalidValueException : OpenGLException
    {
        public InvalidValueException(string message, Exception innerException)
            : base(message, ErrorCode.InvalidValue, innerException)
        {
        }

        public InvalidValueException(string message)
            : base(message, ErrorCode.InvalidValue)
        {
        }

        public InvalidValueException()
            : base(ErrorCode.InvalidValue)
        {
        }

        public InvalidValueException(Exception innerException)
            : base(ErrorCode.InvalidValue, innerException)
        {
        }
    }
}