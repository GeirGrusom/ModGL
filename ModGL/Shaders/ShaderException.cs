using System;

namespace ModGL.Shaders
{
    [Serializable]
    public class ShaderException : Exception
    {
        public Shader Shader { get; private set; }

        public ShaderException(Shader shader, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Shader = shader;
        }

        public ShaderException(Shader shader, Exception innerException)
            : this(shader, "Shader exception occurred.", innerException)
        {
            
        }

        public ShaderException(Shader shader, string message)
            : base(message)
        {
            this.Shader = shader;
        }

        public ShaderException(Shader shader)
            : this(shader, "Shader exception occurred.")
        {

        }

    }
}