using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModGL.NativeGL;
using ModGL.Numerics;
using ModGL.Shaders;
using ModGL.VertexInfo;

namespace Cube
{
    public class CubeShader
    {
        private readonly IShader vertexShader;
        private readonly IShader fragmentShader;
        private readonly IProgram program;
        private readonly MatrixUniform modelViewProjection;
        private readonly MatrixUniform viewProjection;

        public IProgram Program { get { return program; } }

        public Uniform<Matrix4f> ModelViewProjection { get { return modelViewProjection; } }
        public Uniform<Matrix4f> ViewProjection { get { return viewProjection; } } 

        private string GetEmbeddedResourceAsString(string name)
        {
            var resourceStreams = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Cube.Resources." + name))
            {
                var rd = new StreamReader(stream);
                return rd.ReadToEnd();
            }
        }

        public CubeShader(IOpenGL30 gl)
        {
            vertexShader = new VertexShader(gl, GetEmbeddedResourceAsString("cube.vs"));
            fragmentShader = new FragmentShader(gl, GetEmbeddedResourceAsString("cube.fs"));
            var p = new ModGL.Shaders.Program(gl, vertexShader, fragmentShader);
            p.BindVertexAttributeLocations(PositionNormalTexCoord.Descriptor);
            gl.BindFragDataLocation(p.Handle, 0, "Color");
            p.Compile();
            program = p;
            modelViewProjection = p.GetUniform<MatrixUniform, Matrix4f>("ModelViewProjection");
            viewProjection = p.GetUniform<MatrixUniform, Matrix4f>("ViewProjection");
        }
    }
}
