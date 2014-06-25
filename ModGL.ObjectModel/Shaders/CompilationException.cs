using System;

namespace ModGL.ObjectModel.Shaders
{
    [Serializable]
    public class CompilationException : Exception
    {
        public CompilationResults Results { get; set; }
    }
}