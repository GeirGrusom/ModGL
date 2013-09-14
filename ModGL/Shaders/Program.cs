using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ModGL.NativeGL;

namespace ModGL.Shaders
{
    public interface IProgram : IGLObject, IBindable, IDisposable
    {
        void Compile();
    }

    [Serializable]
    public class Program : IGLObject, IProgram
    {
        [NonSerialized]
        private readonly IOpenGL30 _gl;
        public uint Handle { get; private set; }
        public IEnumerable<IShader> Shaders { get; private set; } 

        public Program(IOpenGL30 gl, IEnumerable<IShader> shaders)
        {
            if(shaders == null)
                throw new ArgumentNullException("shaders");
            if(gl == null)
                throw new ArgumentNullException("gl");

            _gl = gl;
            Handle = gl.glCreateProgram();
            Shaders = shaders.ToArray();
        }

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

            return new CompilationResults(Shaders.Select(s => s.GetCompilationResults()), Encoding.UTF8.GetString(log), IsValid, IsLinked);
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
            if (!IsLinked || !IsValid)
            {
                var compileResults = GetCompilationResults();
                throw new ProgramLCompilationException(this, compileResults, string.Format("Program compilation failed: {0}", compileResults.Message));
            }
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
