namespace ModGL.Shaders
{
    public class ShaderCompilationResults
    {
        public string Message { get; private set; }
        public bool Success { get; private set; }
        public ShaderCompilationResults(string message, bool success)
        {
            this.Message = message;
            this.Success = success;
        }
    }
}