using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using ModGL;
using ModGL.Buffers;
using ModGL.NativeGL;
using ModGL.Numerics;

namespace Cube
{
    public class Cube : IDisposable
    {
        private readonly IVertexArray vertexArray;
        private readonly IVertexBuffer vertexBuffer;
        private readonly CubeShader shader;
        private readonly IOpenGL30 gl;

        public Matrix4f Model { get; set; }
        public Matrix4f View { get; set; }
        public Matrix4f Projection { get; set; }
        public Color DiffuseColor { get; set; }

        private IVertexBuffer CreateBuffer()
        {
            var result = new VertexBuffer<Vertex>(36, gl);
            
            // Set up every face omn the cube
            // Top
            result[0]  = new Vertex { Position = new Vector3f(-1, 1,  1), Normal = new Vector3f(0, 1, 0)};
            result[1]  = new Vertex { Position = new Vector3f( 1, 1,  1), Normal = new Vector3f(0, 1, 0) };
            result[2]  = new Vertex { Position = new Vector3f(-1, 1, -1), Normal = new Vector3f(0, 1, 0) };
            
            result[3]  = new Vertex { Position = new Vector3f(-1, 1, -1), Normal = new Vector3f(0, 1, 0) };
            result[4]  = new Vertex { Position = new Vector3f( 1, 1,  1), Normal = new Vector3f(0, 1, 0) };
            result[5]  = new Vertex { Position = new Vector3f( 1, 1, -1), Normal = new Vector3f(0, 1, 0) };

            // Bottom
            result[6]  = new Vertex { Position = new Vector3f(-1, -1,  1), Normal = new Vector3f(0,  -1, 0) };
            result[7] = new Vertex { Position = new Vector3f(-1, -1, -1), Normal = new Vector3f(0, -1, 0) };
            result[8]  = new Vertex { Position = new Vector3f( 1, -1,  1), Normal = new Vector3f(0,  -1, 0) };
            

            result[9]  = new Vertex { Position = new Vector3f(-1, -1, -1), Normal = new Vector3f(0, -1, 0) };
            result[10] = new Vertex { Position = new Vector3f(1, -1, -1), Normal = new Vector3f(0, -1, 0) };
            result[11] = new Vertex { Position = new Vector3f( 1, -1,  1), Normal = new Vector3f(0, -1, 0) };
            
            // Left
            result[12] = new Vertex { Position = new Vector3f(-1, 1, 1), Normal = new Vector3f(-1, 0, 0)};
            result[13] = new Vertex { Position = new Vector3f(-1, 1, -1), Normal = new Vector3f(-1, 0, 0) };
            result[14] = new Vertex { Position = new Vector3f(-1, -1, -1), Normal = new Vector3f(-1, 0, 0) };

            result[15] = new Vertex { Position = new Vector3f(-1, 1, 1), Normal = new Vector3f(-1, 0, 0) };
            result[16] = new Vertex { Position = new Vector3f(-1, -1, -1), Normal = new Vector3f(-1, 0, 0) };
            result[17] = new Vertex { Position = new Vector3f(-1, -1, 1), Normal = new Vector3f(-1, 0, 0) };

            // Right
            result[18] = new Vertex { Position = new Vector3f(1, 1, 1), Normal = new Vector3f(1, 0, 0) };
            result[19] = new Vertex { Position = new Vector3f(1, -1, -1), Normal = new Vector3f(1, 0, 0) };
            result[20] = new Vertex { Position = new Vector3f(1, 1, -1), Normal = new Vector3f(1, 0, 0) };

            result[21] = new Vertex { Position = new Vector3f(1, 1, 1), Normal = new Vector3f(1, 0, 0) };
            result[22] = new Vertex { Position = new Vector3f(1, -1, 1), Normal = new Vector3f(1, 0, 0) };
            result[23] = new Vertex { Position = new Vector3f(1, -1, -1), Normal = new Vector3f(1, 0, 0) };

            // Front
            result[24] = new Vertex {Position = new Vector3f(-1, 1, 1), Normal = new Vector3f(0, 0, 1)};
            result[25] = new Vertex { Position = new Vector3f(1, -1, 1), Normal = new Vector3f(0, 0, 1) };
            result[26] = new Vertex { Position = new Vector3f(1, 1, 1), Normal = new Vector3f(0, 0, 1) };

            result[27] = new Vertex { Position = new Vector3f(-1, 1, 1), Normal = new Vector3f(0, 0, 1) };
            result[29] = new Vertex { Position = new Vector3f(1, -1, 1), Normal = new Vector3f(0, 0, 1) };
            result[28] = new Vertex { Position = new Vector3f(-1, -1, 1), Normal = new Vector3f(0, 0, 1) };

            // Back
            result[30] = new Vertex { Position = new Vector3f(-1, 1, -1), Normal = new Vector3f(0, 0, -1) };
            result[32] = new Vertex { Position = new Vector3f(1, -1, -1), Normal = new Vector3f(0, 0, -1) };
            result[31] = new Vertex { Position = new Vector3f(1, 1, -1), Normal = new Vector3f(0, 0, -1) };

            result[33] = new Vertex { Position = new Vector3f(-1, 1, -1), Normal = new Vector3f(0, 0, -1) };
            result[34] = new Vertex { Position = new Vector3f(1, -1, -1), Normal = new Vector3f(0, 0, -1) };
            result[35] = new Vertex { Position = new Vector3f(-1, -1, -1), Normal = new Vector3f(0, 0, -1) };
            

            using (result.Bind())
            {
                result.BufferData(BufferUsage.StaticDraw);
            }

            return result;
        }

        public Cube(IOpenGL30 gl)
        {
            Model = Matrix4f.Identity;
            View = Matrix4f.Identity;
            Projection = Matrix4f.Identity;
            this.gl = gl;
            shader = new CubeShader(gl);
            
            vertexBuffer = CreateBuffer();
            vertexArray = new VertexArray(gl, new [] { vertexBuffer }, new [] {  Vertex.Descriptor });
        }

        public void Draw()
        {
            using (shader.Program.Bind())
            using (vertexArray.Bind())
            {
                shader.ViewProjection.Value = View * Projection;
                shader.ModelViewProjection.Value = Model * View * Projection;
                shader.DiffuseUniform.Value = new Vector4f(DiffuseColor.R / 255.0f, DiffuseColor.G / 255f, DiffuseColor.B / 255f, DiffuseColor.A / 255f);
                gl.DrawArrays(PrimitiveType.Triangles,  0, (int)vertexBuffer.Elements);
            }


        }

        public void Dispose()
        {
            vertexArray.Dispose();
            vertexBuffer.Dispose();
            shader.Dispose();
        }
    }
}