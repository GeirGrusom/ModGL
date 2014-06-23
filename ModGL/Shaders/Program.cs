using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using ModGL.NativeGL;
using InvalidOperationException = System.InvalidOperationException;

namespace ModGL.Shaders
{
    public interface IProgram : IGLObject, IBindable, IDisposable
    {
        void Compile();
    }

    public class Program : IProgram
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
        public Program(IOpenGL30 gl, params IShader[] shaders)
            : this(gl, shaders.AsEnumerable())
        {
        }


        /// <summary>
        /// Creates an Shader program and attaches shaders to it.
        /// </summary>
        /// <param name="gl">OpenGL interface supplying shader functionality.</param>
        /// <param name="shaders">Shaders to attach.</param>
        /// <exception cref="ArgumentNullException">Thrown if gl or shaders is null.</exception>
        public Program(IOpenGL30 gl, IEnumerable<IShader> shaders)
        {
            if (shaders == null)
                throw new ArgumentNullException("shaders");
            if (gl == null)
                throw new ArgumentNullException("gl");

            _gl = gl;
            Handle = gl.CreateProgram();

            if (Handle == 0)
                throw new NoHandleCreatedException();

            Shaders = shaders.ToArray();

            foreach (var shader in Shaders)
                gl.AttachShader(Handle, shader.Handle);
        }

        /// <summary>
        /// Gets a value indicating if the program is valid. This value will be set after program is compiled.
        /// </summary>
        [Pure]
        public bool IsValid
        {
            get
            {
                int[] validateStatus = new int[1];
                _gl.GetProgramiv(Handle, (uint)ProgramParameters.ValidateStatus, validateStatus);
                return validateStatus.Single() == (int)GLboolean.True;
            }
        }

        /// <summary>
        /// Gets a value indicating if the program has been linked. This value will be set after the program is compiled.
        /// </summary>
        [Pure]
        public bool IsLinked
        {
            get
            {
                int[] linkStatus = new int[1];
                _gl.GetProgramiv(Handle, (uint)ProgramParameters.LinkStatus, linkStatus);
                return linkStatus.Single() == (int)GLboolean.True;
            }
        }

        /// <summary>
        /// Gets compilation results. This will also include compilation results of all attached shaders.
        /// </summary>
        /// <returns>Program compilation results.</returns>
        [Pure]
        public CompilationResults GetCompilationResults()
        {
            var logLength = new int[1];
            _gl.GetProgramiv(Handle, (uint)ProgramParameters.InfoLogLength, logLength);
            var log = new string(' ', logLength.Single());
            _gl.GetProgramInfoLog(Handle, log.Length, logLength, ref log);

            return new CompilationResults(Shaders.Select(s => s.GetCompilationResults()), log, IsValid, IsLinked);
        }

        public void BindVertexAttributeLocations(params VertexInfo.IVertexDescriptor[] definitions)
        {
            int offset = 0;
            foreach (var item in definitions)
            {
                BindVertexAttributeLocations(item, offset);
                offset += item.Elements.Count();
            }
        }

        /// <summary>
        /// Binds attribute locations to vertex description. This can only be done before the program has been compiled.
        /// </summary>
        /// <param name="definition">Vertex definition.</param>
        /// <param name="indexOffset">Optional. Offset for each index.</param>
        /// <exception cref="ArgumentException">Thrown if <see cref="indexOffset"/> is less than zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown if program has already been compiled. Vertex attribute locations cannot be bound after the program has been linked.</exception>
        public void BindVertexAttributeLocations(VertexInfo.IVertexDescriptor definition, int indexOffset = 0)
        {
            if (indexOffset < 0)
                throw new ArgumentException("Offset cannot be less than zero.", "indexOffset");

            if (IsLinked)
                throw new InvalidOperationException("Cannot bind attributes to an already linked program.");

            foreach (var item in definition.Elements.Select((item, index) => new { Item = item, Index = index }))
                _gl.BindAttribLocation(Handle, (uint)(item.Index + indexOffset), item.Item.Name);
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

            _gl.LinkProgram(Handle);
            _gl.ValidateProgram(Handle);
            

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
            _gl.UseProgram(Handle);
            return new BindContext(() => _gl.UseProgram(0));
        }

        /// <summary>
        /// Marks the program for deletion.
        /// </summary>
        /// <remarks>Shaders are not disposed and must be disposed by the caller.</remarks>
        public void Dispose()
        {
            _gl.DeleteProgram(Handle);
        }

        [Pure]
        public IEnumerable<Tuple<string, int>> GetUniforms()
        {
            int[] numUniforms = new int[1];
            int[] uniformCount = new int[1];
            _gl.GetProgramiv(Handle, (uint)ProgramParameters.ActiveUniforms, numUniforms);
            _gl.GetProgramiv(Handle, (uint)ProgramParameters.ActiveUniformMaxLength, uniformCount);

            string buffer = new string(' ', 1024);
            for (int i = 0; i < numUniforms.Single(); i++)
            {
                var realLength = new int[1];
                var size = new int[1];
                var type = new uint[1];
                _gl.GetActiveUniform(Handle, (uint)i, buffer.Length, realLength, size, type, ref buffer);
                string name = buffer.Substring(0, realLength.Single());
                yield return new Tuple<string, int>(name, i);
            }
        }

        internal static class ProcCache<TKey>
        {
            private static readonly Func<IOpenGL30, string, int, TKey> func = Create();

            private static Func<IOpenGL30, string, int, TKey> Create()
            {
                var ctr = typeof(TKey).GetConstructor(new[] { typeof(IOpenGL30), typeof(string), typeof(int) });
                if (ctr == null)
                    throw new MissingMethodException("Missing constructor taking IOpenGL30, string and int.", "ctor");
                var p1 = Expression.Parameter(typeof(IOpenGL30));
                var p2 = Expression.Parameter(typeof(string));
                var p3 = Expression.Parameter(typeof(int));
                return Expression.Lambda<Func<IOpenGL30, string, int, TKey>>(Expression.New(ctr, p1, p2, p3), p1, p2, p3).Compile();
            }

            public static TKey Create(IOpenGL30 gl, string uniformName, int location)
            {
                return func(gl, uniformName, location);
            }
        }

        /// <summary>
        /// Creates a uniform object for the specified uniform.
        /// </summary>
        /// <typeparam name="TUniformType">Type of uniform to create.</typeparam>
        /// <typeparam name="TValueType">Type of element in the uniform.</typeparam>
        /// <param name="uniformName">Name of the shader uniform.</param>
        /// <returns></returns>
        /// <exception cref="NoHandleCreatedException">Thrown if there is no uniform by that name.</exception>
        [Pure]
        public TUniformType GetUniform<TUniformType, TValueType>(string uniformName)
            where TUniformType : Uniform<TValueType>
        {
            var uniformLoc = _gl.GetUniformLocation(Handle, uniformName);

            if (uniformLoc == -1)
                throw new NoHandleCreatedException("Unable to find uniform with the specified name.");

            return ProcCache<TUniformType>.Create(_gl, uniformName, uniformLoc);
        }

        /// <summary>
        /// Creates a uniform object for the specified uniform. If the uniform does not exist, an inert uniform will be created instead.
        /// </summary>
        /// <typeparam name="TUniformType">Type of uniform to create.</typeparam>
        /// <typeparam name="TValueType">Type of element in the uniform.</typeparam>
        /// <param name="uniformName">Name of the shader uniform.</param>
        /// <returns>If function is successful, a TUniformType instance will be returned, otherwise InertUniform will be returned.</returns>
        [Pure]
        public Uniform<TValueType> GetUniformInert<TUniformType, TValueType>(string uniformName)
            where TUniformType : Uniform<TValueType>
        {
            var uniformLoc = _gl.GetUniformLocation(Handle, uniformName);

            if (uniformLoc == -1)
                return new InertUniform<TValueType>(_gl, uniformName, uniformLoc);

            return ProcCache<TUniformType>.Create(_gl, uniformName, uniformLoc);
        }

        /// <summary>
        /// Tries to get a uniform.
        /// </summary>
        /// <typeparam name="TUniformType">Type of uniform to create.</typeparam>
        /// <typeparam name="TValueType">Type of element in the uniform.</typeparam>
        /// <param name="uniformName">Name of the shader uniform.</param>
        /// <param name="result">If function is successful, return true and the new uniform in result. Otherwise false is returned and result is set to null.</param>
        /// <returns></returns>
        public bool TryGetUniform<TUniformType, TValueType>(string uniformName, out Uniform<TValueType> result)
            where TUniformType : Uniform<TValueType>
        {
            var uniformLoc = _gl.GetUniformLocation(Handle, uniformName);

            result = uniformLoc != -1 
                ? ProcCache<TUniformType>.Create(this._gl, uniformName, uniformLoc) 
                : null;

            return uniformLoc != -1;
        }
    }
}
