using System;

namespace ModGL.Shaders
{
    [Serializable]
    public class ProgramLCompilationException : ShaderProgramException
    {
        public CompilationResults CompilationResults { get; set; }

        public ProgramLCompilationException(Program program, CompilationResults results, string message, Exception innerException)
            : base(program, message, innerException)
        {
            this.CompilationResults = results;
        }

        public ProgramLCompilationException(Program program, CompilationResults results, Exception innerException)
            : this(program, results, "Program compilation error occurred.", innerException)
        {
        }

        public ProgramLCompilationException(Program program, CompilationResults results, string message)
            : base(program, message)
        {
            this.CompilationResults = results;
        }

        public ProgramLCompilationException(Program program, CompilationResults results)
            : this(program, results, "Program compilation error occurred.")
        {
        }

    }
}