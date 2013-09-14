using System;

namespace ModGL.Shaders
{
    [Serializable]
    public class CompilationException : Exception
    {
        public CompilationResults Results { get; set; }
    }
}