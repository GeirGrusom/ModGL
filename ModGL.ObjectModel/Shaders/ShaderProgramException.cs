using System;

namespace ModGL.ObjectModel.Shaders
{
    [Serializable]
    public class ShaderProgramException : Exception
    {
        public Program Program { get; private set; }

        public ShaderProgramException(Program program, string message, Exception innerException)
            : base(message, innerException)
        {
            Program = program;
        }

        public ShaderProgramException(Program program, Exception innerException)
            : this(program, "Shader program exception occurred.", innerException)
        {
        }

        public ShaderProgramException(Program program, string message)
            : base(message)
        {
            Program = program;
        }

        public ShaderProgramException(Program program)
            : this(program, "Shader program exception occurred.")
        {
        }
    }
}