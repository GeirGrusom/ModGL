using System;

namespace ModGL.ObjectModel.Shaders
{
    [Serializable]
    public class ShaderCompilationResults
    {
        public string Message { get; private set; }
        public bool Success { get; private set; }
        public ShaderCompilationResults(string message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
}