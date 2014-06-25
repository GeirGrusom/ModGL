using System;

namespace ModGL.ObjectModel.Shaders
{
    [Serializable]
    public class ShaderCompilationException : ShaderException
    {
        public ShaderCompilationResults CompilationResults { get; private set; }

        public ShaderCompilationException(Shader shader, ShaderCompilationResults results, string message, Exception innerException)
            : base(shader, message, innerException)
        {
            CompilationResults = results;
        }

        public ShaderCompilationException(Shader shader, ShaderCompilationResults results, Exception innerException)
            : this(shader, results, "Program compilation error occurred.", innerException)
        {
        }

        public ShaderCompilationException(Shader shader, ShaderCompilationResults results, string message)
            : base(shader, message)
        {
            CompilationResults = results;
        }

        public ShaderCompilationException(Shader shader, ShaderCompilationResults results)
            : this(shader, results, "Program compilation error occurred.")
        {
        }

    }
}