using System;
using System.Collections.Generic;

namespace ModGL.Shaders
{
    [Serializable]
    public class CompilationResults
    {
        public IEnumerable<ShaderCompilationResults> ShaderResults { get; private set; }
        public string Message { get; private set; }
        public bool Validated { get; private set; }
        public bool Linked { get; private set; }

        public CompilationResults(IEnumerable<ShaderCompilationResults> shaderResults, string message, bool validated, bool linked)
        {
            this.ShaderResults = shaderResults;
            this.Message = message;
            this.Validated = validated;
            this.Linked = linked;
        }
    }
}