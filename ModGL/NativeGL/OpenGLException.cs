using System;

namespace ModGL.NativeGL
{
    [Serializable]
    public class OpenGLException : Exception
    {
        public ErrorCode ErrorCode { get; private set; }

        public OpenGLException(string message, ErrorCode error, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = error;
        }

        public OpenGLException(string message, ErrorCode error)
            : base(message)
        {
            ErrorCode = error;
        }

        public OpenGLException(ErrorCode error, Exception innerException)
            : base(string.Format("OpenGL call failed: {0}", error), innerException)
        {

            ErrorCode = error;
        }

        public OpenGLException(ErrorCode error)
            : base(string.Format("OpenGL call failed: {0}", error))
        {
            ErrorCode = error;
        }
    }
}
