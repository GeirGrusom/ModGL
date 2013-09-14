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

        public ShaderType ShaderType { get { return _shaderType; } }

        public uint Handle { get; private set; }

        public string Code { get { return this._code; } }

        public bool IsCompiled
        {
            get
            {
                int[] compileStatus = new int[1];
                _gl.glGetShaderiv(Handle, ShaderParameters.CompileStatus, compileStatus);
                return compileStatus.Single() == (int)GLboolean.True;
            }
        }

        public void Dispose()
        {
            _gl.glDeleteShader(Handle);
        }

        protected Shader(IOpenGL30 gl, ShaderType shaderType, string code)
        {
            if(gl == null)
                throw new ArgumentNullException("gl");
            if(string.IsNullOrEmpty(code))
                throw new ArgumentNullException("code");
            _gl = gl;
            Handle = gl.glCreateShader((uint)shaderType);
            _shaderType = shaderType;
            this._code = code;
        }

        public ShaderCompilationResults GetCompilationResults()
        {
            int[] logLength = new int[1];
            
            this._gl.glGetShaderiv(Handle, ShaderParameters.InfoLogLength, logLength);
            byte[] log = new byte[logLength.Single()];
            this._gl.glGetShaderInfoLog(Handle, log.Length, out logLength[0], log);
            return new ShaderCompilationResults(Encoding.UTF8.GetString(log), success: IsCompiled);
        }

        public void Compile()
        {
            int[] compileStatus = new int[1];
            this._gl.glShaderSource(Handle, 1, new [] { Code }, new [] { Code.Length} );
            this._gl.glCompileShader(Handle);
            this._gl.glGetShaderiv(Handle, ShaderParameters.CompileStatus, compileStatus);
            if (compileStatus.Single() != (int)GLboolean.True)
                throw new ShaderCompilationException(this, GetCompilationResults());
        }
    }
}