using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL.Shaders
{
    [Serializable]
    public class Program : ModGL.IGLObject, IBindable, IDisposable
    {
        [NonSerialized]
        private readonly IOpenGL30 _gl;
        public Program(IOpenGL30 gl, IEnumerable<Shader> shaders)
        {
            _gl = gl;
            Handle = gl.glCreateProgram();
            Shaders = shaders;
        }

        public uint Handle { get; private set; }

        public IEnumerable<Shader> Shaders { get; private set; } 

        public bool IsValid
        {
            get
            {
                int[] validateStatus = new int[1];
                _gl.glGetProgramiv(Handle, ProgramParameters.ValidateStatus, validateStatus);
                return validateStatus.Single() == (int)GLboolean.True;
            }
        }

        public bool IsLinked
        {
            get
            {
                int[] linkStatus = new int[1];
                _gl.glGetProgramiv(Handle, ProgramParameters.LinkStatus, linkStatus);
                return linkStatus.Single() == (int)GLboolean.True;                
            }
        }

        public CompilationResults GetCompilationResults()
        {
            int[] logLength = new int[1];
            _gl.glGetProgramiv(Handle, ProgramParameters.InfoLogLength, logLength);
            byte[] log = new byte[logLength.Single()];
            _gl.glGetProgramInfoLog(Handle, log.Length, out logLength[0], log);

            return new CompilationResults(Shaders.Select(s => s.GetCompilationResults()), Encoding.UTF8.GetString(log), this.IsValid, IsLinked);
        }

        public void Compile()
        {
            try
            {
                foreach (var shader in Shaders.Where(s => !s.IsCompiled))
                {
                    shader.Compile();
                }
            }
            catch (ShaderCompilationException ex)
            {
                throw new ProgramLCompilationException(this, GetCompilationResults(), ex);
            }

            _gl.glValidateProgram(Handle);
            _gl.glLinkProgram(Handle);
            if(!IsLinked || !this.IsValid)
                throw new ProgramLCompilationException(this, GetCompilationResults());
        }

        public BindContext Bind()
        {
            _gl.glUseProgram(Handle);
            return new BindContext(() => _gl.glUseProgram(0));
        }

        /// <summary>
        /// Marks the program for deletion.
        /// </summary>
        /// <remarks>Shaders are not disposed, and must be disposed by the caller.</remarks>
        public void Dispose()
        {
            _gl.glDeleteProgram(Handle);
        }
    }
}
