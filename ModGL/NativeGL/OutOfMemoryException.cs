using System;

namespace ModGL.NativeGL
{
    [Serializable]
    public class OutOfMemoryException : OpenGLException
    {
        public OutOfMemoryException(string message, Exception innerException) : base(message, ErrorCode.OutOfMemory, innerException)
        {
        }

        public OutOfMemoryException(string message) : base(message, ErrorCode.OutOfMemory)
        {
        }

        public OutOfMemoryException(Exception innerException) : base(ErrorCode.OutOfMemory, innerException)
        {
        }

        public OutOfMemoryException() : base(ErrorCode.OutOfMemory)
        {
        }
    }
}
