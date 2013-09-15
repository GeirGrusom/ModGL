using System;

namespace ModGL.Shaders
{
    [Serializable]
    public class ShaderCompilationException : ShaderException
    {
        public ShaderCompilationResults CompilationResults { get; private set; }

        public ShaderCompilationException(Shader shader, ShaderCompilationResults results, string message, Exception innerException)
            : base(shader, message, innerException)
        {
            this.CompilationResults = results;
        }

        public ShaderCompilationException(Shader shader, ShaderCompilationResults results, Exception innerException)
            : this(shader, results, "Program compilation error occurred.", innerException)
        {
        }

        public ShaderCompilationException(Shader shader, ShaderCompilationResults results, string message)
            : base(shader, message)
        {
            this.CompilationResults = results;
        }

        public ShaderCompilationException(Shader shader, ShaderCompilationResults results)
            : this(shader, results, "Program compilation error occurred.")
        {
        }

    }
}