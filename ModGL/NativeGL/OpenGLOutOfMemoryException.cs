using System;

namespace ModGL.NativeGL
{
    [Serializable]
    public class OpenGLOutOfMemoryException : OpenGLException
    {
        public OpenGLOutOfMemoryException(string message, Exception innerException) : base(message, ErrorCode.OutOfMemory, innerException)
        {
        }

        public OpenGLOutOfMemoryException(string message) : base(message, ErrorCode.OutOfMemory)
        {
        }

        public OpenGLOutOfMemoryException(Exception innerException) : base(ErrorCode.OutOfMemory, innerException)
        {
        }

        public OpenGLOutOfMemoryException() : base(ErrorCode.OutOfMemory)
        {
        }
    }
}
