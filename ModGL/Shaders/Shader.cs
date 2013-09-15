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

    /// <summary>
    /// Shader base class. This class is abstract.
    /// </summary>
    [Serializable]
    public abstract class Shader : IDisposable, IShader
    {
        private readonly ShaderType _shaderType;
        private readonly IOpenGL30 _gl;
        private readonly string _code;

        /// <summary>
        /// Gets the type of OpenGL shader.
        /// </summary>
        public ShaderType ShaderType { get { return _shaderType; } }

        public uint Handle { get; private set; }

        /// <summary>
        /// Gets the code used to initialize this shader.
        /// </summary>
        public string Code { get { return this._code; } }


        /// <summary>
        /// Gets a vlue indicating if the shader has been compiled or not.
        /// </summary>
        public bool IsCompiled
        {
            get
            {
                int[] compileStatus = new int[1];
                _gl.glGetShaderiv(Handle, ShaderParameters.CompileStatus, compileStatus);
                return compileStatus.Single() == (int)GLboolean.True;
            }
        }

        /// <summary>
        /// Marks the shader for deletion.
        /// </summary>
        public void Dispose()
        {
            _gl.glDeleteShader(Handle);
        }

        /// <summary>
        /// Creates a new shader.
        /// </summary>
        /// <param name="gl">GL interface containing shader entry points.</param>
        /// <param name="shaderType">Type of OpenGL shader to instantiate.</param>
        /// <param name="code">Shader code.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="gl"/> is null, or if <see cref="code"/> is null or empty.</exception>
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

        /// <summary>
        /// Gets shader compilation results. Success indicates if the shader has been compiled or not.
        /// </summary>
        /// <returns>Shader compilation results.</returns>
        public ShaderCompilationResults GetCompilationResults()
        {
            int[] logLength = new int[1];
            
            this._gl.glGetShaderiv(Handle, ShaderParameters.InfoLogLength, logLength);
            byte[] log = new byte[logLength.Single()];
            this._gl.glGetShaderInfoLog(Handle, log.Length, out logLength[0], log);
            return new ShaderCompilationResults(Encoding.UTF8.GetString(log), success: IsCompiled);
        }

        /// <summary>
        /// Compiles the shader.
        /// </summary>
        /// <exception cref="ShaderCompilationException">Thrown if the shader failed to compile.</exception>
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