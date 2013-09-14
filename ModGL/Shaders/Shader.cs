using System;
using System.Linq;
using System.Text;

using ModGL.NativeGL;

namespace ModGL.Shaders
{
    public interface IShader : IGLObject
    {
        ShaderCompilationResults GetCompilationResults();
        bool IsCompiled { get; }
        void Compile();
    }

    [Serializable]
    public abstract class Shader : IDisposable, IShader
    {
        private readonly ShaderType _shaderType;
        private readonly IOpenGL30 _gl;
        private readonly string _code;

        public uint Handle { get; private set; }

        public string Code { get { return this._code; } }

        public bool IsCompiled
        {
            get
            {
                int[] compileStatus = new int[1];
                this._gl.glGetShaderiv(this.Handle, ShaderParameters.CompileStatus, compileStatus);
                return compileStatus.Single() == (int)GLboolean.True;
            }
        }

        public void Dispose()
        {
            this._gl.glDeleteShader(this.Handle);
        }

        protected Shader(IOpenGL30 gl, ShaderType shaderType, string code)
        {
            this._gl = gl;
            this.Handle = gl.glCreateShader((uint)shaderType);
            this._shaderType = shaderType;
            this._code = code;
        }

        public ShaderCompilationResults GetCompilationResults()
        {
            int[] logLength = new int[1];
            
            this._gl.glGetShaderiv(this.Handle, ShaderParameters.InfoLogLength, logLength);
            byte[] log = new byte[logLength.Single()];
            this._gl.glGetShaderInfoLog(this.Handle, log.Length, out logLength[0], log);
            return new ShaderCompilationResults(Encoding.UTF8.GetString(log), success: this.IsCompiled);
        }

        public void Compile()
        {
            int[] compileStatus = new int[1];
            this._gl.glShaderSource(this.Handle, 1, new [] { this.Code }, new [] { this.Code.Length} );
            this._gl.glCompileShader(this.Handle);
            this._gl.glGetShaderiv(this.Handle, ShaderParameters.CompileStatus, compileStatus);
            if (compileStatus.Single() != (int)GLboolean.True)
                throw new ShaderCompilationException(this, GetCompilationResults());
        }
    }
}