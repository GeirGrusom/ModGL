using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModGL.NativeGL;
using ModGL.Numerics;
using ModGL.ObjectModel.Buffers;
using ModGL.ObjectModel.Shaders;
using ModGL.ObjectModel.VertexInfo;

namespace TexturedTerrain
{
    public interface INoise3D
    {
        double Noise(double x, double y, double z);
    }

    public class Terrain : IDisposable
    {
        private readonly IVertexArray vertexArray;
        private readonly IVertexBuffer vertexBuffer;
        private readonly IElementArray elementBuffer;
        private readonly TerrainShader shader;
        private readonly IOpenGL33 gl;

        public Matrix4f Model { get; set; }
        public Matrix4f View { get; set; }
        public Matrix4f Projection { get; set; }
        public Vector4f Diffuse { get; set; }

        public void Dispose()
        {
            vertexArray.Dispose();
            vertexBuffer.Dispose();
            elementBuffer.Dispose();
            shader.Dispose();
        }

        private Terrain(IOpenGL33 gl, IVertexArray vertexArray, IVertexBuffer vertexBuffer, IElementArray elementBuffer, TerrainShader shader)
        {
            this.gl = gl;
            this.vertexArray = vertexArray;
            this.vertexBuffer = vertexBuffer;
            this.elementBuffer = elementBuffer;
            this.shader = shader;
            Model = Matrix4f.Identity;
            View = Matrix4f.Identity;
            Projection = Matrix4f.Identity;
            Diffuse = new Vector4f(Color.PaleGreen.R / 255f, Color.PaleGreen.G / 255f, Color.PaleGreen.B / 255f, 1f);
        }

        public static Terrain Build(IOpenGL33 gl, int columns, int rows, float width, float depth)
        {
            var noise = new PerlinNoise();
            
            var vertexBuffer = new VertexBuffer<PositionNormalTexCoord>((columns + 1) * (rows + 1), gl);
            var indexBuffer = new ElementBuffer<uint>(columns * rows * 6 , gl);

            float fx = - width / 2;
            float fy = -depth / 2;

            float dx = width / columns;
            float dy = depth / rows;

            int i = 0;

            for (int y = 0; y < (rows + 1); y++)
            {
                for (int x = 0; x < (columns + 1); x++, i++)
                {
                    float ix = fx + x * dx;
                    float iy = fy + y * dy;

                    const float scale = 2f;

                    float d2 = depth/2;
                    float w2 = width/2;
                    const float detail = 0.25f;
                    const float detailScale = 0.2f;
                    const float macro = 0.008f;
                    const float macroScale = 4f;
                    float fUp = (float)(noise.Noise(x * detail, 0, (y + 1) * detail) * detailScale + noise.Noise(x * macro, 0, (y + 1) * macro) * macroScale) ;
                    float fDown = (float)(noise.Noise(x * detail, 0,  (y - 1) * detail) * detailScale + noise.Noise(x * macro, 0,  (y - 1) * macro) * macroScale) ;
                    float fLeft = (float)(noise.Noise((x - 1) * detail, 0, y * detail) * detailScale + noise.Noise((x - 1) * macro, 0, y * macro) * macroScale);
                    float fRight = (float)(noise.Noise((x + 1) * detail, 0, y * detail) * detailScale + noise.Noise((x + 1) * macro, 0, y * macro) * macroScale);
                    float fThis = (float)(noise.Noise(x * detail, 0, y * detail) * detailScale + noise.Noise(x * macro, 0, y * macro) * macroScale);

                    
                    var vUp = new Vector3f(ix, fUp, iy - dy);
                    var vDown = new Vector3f(ix, fDown, iy + dy);
                    var vLeft = new Vector3f(ix - 1, fLeft, iy);
                    var vRight = new Vector3f(ix + 1, fRight, iy);
                    var vThis = new Vector3f(ix, fThis, iy);

                    var upperLeft = CalculateNormal(vLeft, vUp, vThis);
                    var upperRight = CalculateNormal(vUp, vRight, vThis);
                    var lowerRight = CalculateNormal(vRight, vDown, vThis);
                    var lowerLeft = CalculateNormal(vDown, vLeft, vThis);

                    var normal = upperLeft + upperRight + lowerLeft + lowerRight;
                    normal = new Vector3f(normal.X * 0.25f, normal.Y * 0.25f, normal.Z * 0.25f);

                    vertexBuffer[i] = new PositionNormalTexCoord { Normal = normal.Normalize(), Position = vThis};
                }
            }
            int offset = 0;
            foreach(var index in Enumerable.Range(0, columns * rows + columns).Where(index => (index % (columns + 1)) != columns))
            {
                
                indexBuffer[offset] = (uint)index;                    // 1 *-* 2
                indexBuffer[offset + 1] = (uint)index + 1;            //    \|
                indexBuffer[offset + 2] = (uint)(index + (columns + 1) + 1);  //     * 3

                indexBuffer[offset + 3] = (uint)index;                // 1 * 
                indexBuffer[offset + 4] = (uint)(index + (columns + 1) + 1);  //   |\
                indexBuffer[offset + 5] = (uint)(index + (columns + 1));      // 3 *-* 2
                offset += 6;
            }

            using(vertexBuffer.Bind())
                vertexBuffer.BufferData(BufferUsage.StaticDraw);

            using(indexBuffer.Bind())
                indexBuffer.BufferData(BufferUsage.StaticDraw);

            var vertexArray = new VertexArray(gl, new[] {vertexBuffer}, new[] {PositionNormalTexCoord.Descriptor});

            var shader = new TerrainShader(gl);

            return new Terrain(gl, vertexArray, vertexBuffer, indexBuffer, shader);
        }

        private static Vector3f CalculateNormal(Vector3f v1, Vector3f v2, Vector3f v3)
        {
            var vr1 = v3 - v1;
            var vr2 = v2 - v1;
            return vr1.Cross(vr2).Normalize();
        }

        public void Draw()
        {
            using (shader.Program.Bind())
            {
                shader.ModelViewProjection.Value = Model * View * Projection;
                shader.ViewProjection.Value = View * Projection;
                shader.DiffuseUniform.Value = Diffuse;

                using(vertexArray.Bind())
                using (elementBuffer.Bind())
                {
                    gl.DrawElements(PrimitiveType.Triangles, (int)elementBuffer.Elements, ElementBufferItemType.UnsignedInt);
                }
            }
        }

    }
}
