using System;

namespace ModGL.NativeGL
{
    public class OpenGLInvalidEnumException : OpenGLException
    {
        public OpenGLInvalidEnumException(string message, Exception innerException)
            : base(message, ErrorCode.InvalidEnum, innerException)
        {
            
        }

        public OpenGLInvalidEnumException(string message)
            : base(message, ErrorCode.InvalidEnum)
        {

        }

        public OpenGLInvalidEnumException()
            : base(ErrorCode.InvalidEnum)
        {

        }

        public OpenGLInvalidEnumException(Exception innerException)
            : base(ErrorCode.InvalidEnum, innerException)
        {

        }
    }
}