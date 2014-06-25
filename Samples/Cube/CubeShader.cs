using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModGL.NativeGL;
using ModGL.Numerics;
using ModGL.ObjectModel.Shaders;
using ModGL.ObjectModel.VertexInfo;

namespace Cube
{
    public class CubeShader : IDisposable
    {
        private readonly IShader vertexShader;
        private readonly IShader fragmentShader;
        private readonly IProgram program;
        private readonly MatrixUniform modelViewProjection;
        private readonly MatrixUniform viewProjection;
        private readonly Vector4fUniform diffuseUniform;

        public IProgram Program { get { return program; } }

        public Uniform<Matrix4f> ModelViewProjection { get { return modelViewProjection; } }
        public Uniform<Matrix4f> ViewProjection { get { return viewProjection; } }
        public Uniform<Vector4f> DiffuseUniform { get { return diffuseUniform; } }

        public void Dispose()
        {
            program.Dispose();
            vertexShader.Dispose();
            fragmentShader.Dispose();
        }

        // Helper funcion to get resouce as a string.
        private static string GetEmbeddedResourceAsString(string name)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Cube.Resources." + name))
            {
                var rd = new StreamReader(stream);
                return rd.ReadToEnd();
            }
        }

        public CubeShader(IOpenGL30 gl)
        {
            // Create the vertex shader
            vertexShader = new VertexShader(gl, GetEmbeddedResourceAsString("cube.vs"));
            // Create the fragmet shader
            fragmentShader = new FragmentShader(gl, GetEmbeddedResourceAsString("cube.fs"));
            // Create the program for both shaders
            var p = new ModGL.ObjectModel.Shaders.Program(gl, vertexShader, fragmentShader);
            // Tell the shader what field names to use (taken from the vertex Descriptor)
            p.BindVertexAttributeLocations(PositionNormalTexCoord.Descriptor);
            // Bind output fragment to the specified nme
            gl.BindFragDataLocation(p.Handle, 0, "Color");
            // Compile program and shaders
            p.Compile();
            program = p;
            // Get the uniforms used by the shader program.
            modelViewProjection = p.GetUniform<MatrixUniform, Matrix4f>("ModelViewProjection");
            viewProjection = p.GetUniform<MatrixUniform, Matrix4f>("ViewProjection");
            diffuseUniform = p.GetUniform<Vector4fUniform, Vector4f>("DiffuseColor");
        }
    }
}
