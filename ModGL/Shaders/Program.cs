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

        /// <summary>
        /// Creates an Shader program and attaches shaders to it.
        /// </summary>
        /// <param name="gl">OpenGL interface supplying shader functionality.</param>
        /// <param name="shaders">Shaders to attach.</param>
        /// <exception cref="ArgumentNullException">Thrown if gl or shaders is null.</exception>
        public Program(IOpenGL30 gl, IEnumerable<IShader> shaders)
        {
            if(shaders == null)
                throw new ArgumentNullException("shaders");
            if(gl == null)
                throw new ArgumentNullException("gl");

            _gl = gl;
            Handle = gl.glCreateProgram();
            Shaders = shaders.ToArray();

            foreach(var shader in Shaders)
                gl.glAttachShader(Handle, shader.Handle);
        }

        /// <summary>
        /// Gets a value indicating if the program is valid. This value will be set after program is compiled.
        /// </summary>
        public bool IsValid
        {
            get
            {
                int[] validateStatus = new int[1];
                _gl.glGetProgramiv(Handle, ProgramParameters.ValidateStatus, validateStatus);
                return validateStatus.Single() == (int)GLboolean.True;
            }
        }

        /// <summary>
        /// Gets a value indicating if the program has been linked. This value will be set after the program is compiled.
        /// </summary>
        public bool IsLinked
        {
            get
            {
                int[] linkStatus = new int[1];
                _gl.glGetProgramiv(Handle, ProgramParameters.LinkStatus, linkStatus);
                return linkStatus.Single() == (int)GLboolean.True;                
            }
        }

        /// <summary>
        /// Gets compilation results. This will also include compilation results of all attached shaders.
        /// </summary>
        /// <returns>Program compilation results.</returns>
        public CompilationResults GetCompilationResults()
        {
            int[] logLength = new int[1];
            _gl.glGetProgramiv(Handle, ProgramParameters.InfoLogLength, logLength);
            byte[] log = new byte[logLength.Single()];
            _gl.glGetProgramInfoLog(Handle, log.Length, out logLength[0], log);

            return new CompilationResults(Shaders.Select(s => s.GetCompilationResults()), Encoding.UTF8.GetString(log), IsValid, IsLinked);
        }

        /// <summary>
        /// Binds attribute locations to vertex description. This can only be done before the program has been compiled.
        /// </summary>
        /// <param name="definition">Vertex definition.</param>
        /// <param name="indexOffset">Optional. Offset for each index.</param>
        /// <exception cref="ArgumentException">Thrown if <see cref="indexOffset"/> is less than zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown if program has already been compiled. Vertex attribute locations cannot be bound after the program has been linked.</exception>
        public void BindVertexAttributeLocations(VertexInfo.VertexDescriptor definition, int indexOffset = 0)
        {
            if(indexOffset < 0)
                throw new ArgumentException("Offset cannot be less than zero.", "indexOffset");

            if(IsLinked)
                throw new InvalidOperationException("Cannot bind attributes to an already linked program.");

            foreach(var item in definition.Elements.Select((Item, Index) => new { Item, Index}))
                _gl.glBindAttribLocation(Handle, (uint)(item.Index + indexOffset), item.Item.Name);
        }

        /// <summary>
        /// Compiles the program and any uncompiled attached shaders.
        /// </summary>
        /// <exception cref="ProgramCompilationException">Thrown if the program fails to link or validate, or if any attaced shaders failed to compile. Shader compilation failure will include <see cref="ShaderCompilationException"/> as an inner exception.</exception>
        public void Compile()
        {
            try
            {
                foreach (var shader in Shaders.Where(s => !s.IsCompiled))
                    shader.Compile();
            }
            catch (ShaderCompilationException ex)
            {
                throw new ProgramCompilationException(this, GetCompilationResults(), "Program failed to compile due to shader errors. See inner exception for more details.", ex);
            }

            _gl.glValidateProgram(Handle);
            _gl.glLinkProgram(Handle);

            if (IsLinked && IsValid)
                return;

            var compileResults = GetCompilationResults();
            throw new ProgramCompilationException(this, compileResults, string.Format("Program compilation failed: {0}", compileResults.Message));
        }

        /// <summary>
        /// Binds the program to the OpenGL state
        /// </summary>
        /// <returns>Returns a binding context used to unbind the program.</returns>
        public BindContext Bind()
        {
            _gl.glUseProgram(Handle);
            return new BindContext(() => _gl.glUseProgram(0));
        }

        /// <summary>
        /// Marks the program for deletion.
        /// </summary>
        /// <remarks>Shaders are not disposed and must be disposed by the caller.</remarks>
        public void Dispose()
        {
            _gl.glDeleteProgram(Handle);
        }
    }
}
