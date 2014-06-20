using System;

namespace ModGL.NativeGL
{
    public class InvalidEnumException : OpenGLException
    {
        public InvalidEnumException(string message, Exception innerException)
            : base(message, ErrorCode.InvalidEnum, innerException)
        {
            
        }

        public InvalidEnumException(string message)
            : base(message, ErrorCode.InvalidEnum)
        {

        }

        public InvalidEnumException()
            : base(ErrorCode.InvalidEnum)
        {

        }

        public InvalidEnumException(Exception innerException)
            : base(ErrorCode.InvalidEnum, innerException)
        {

        }
    }
}