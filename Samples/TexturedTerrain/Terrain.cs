using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using ModGL.NativeGL;
using ModGL.Numerics;
using ModGL.ObjectModel.Buffers;
using ModGL.ObjectModel.VertexInfo;

namespace TexturedTerrain
{
    public interface INoise3D
    {
        double Noise(double x, double y, double z);
    }

    public class Terrain : IDisposable
    {
        private readonly IVertexArray _vertexArray;
        private readonly IVertexBuffer _vertexBuffer;
        private readonly IElementArray _elementBuffer;
        private readonly TerrainShader _shader;
        private readonly IOpenGL33 _gl;

        public Matrix4f Model { get; set; }
        public Matrix4f View { get; set; }
        public Matrix4f Projection { get; set; }
        public Vector4f Diffuse { get; set; }

        public void Dispose()
        {
            _vertexArray.Dispose();
            _vertexBuffer.Dispose();
            _elementBuffer.Dispose();
            _shader.Dispose();
        }

        private Terrain(IOpenGL33 gl, IVertexArray vertexArray, IVertexBuffer vertexBuffer, IElementArray elementBuffer, TerrainShader shader)
        {
            this._gl = gl;
            this._vertexArray = vertexArray;
            this._vertexBuffer = vertexBuffer;
            this._elementBuffer = elementBuffer;
            this._shader = shader;
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

                    const float detail = 0.25f;
                    const float detailScale = 0.2f;
                    const float macro = 0.008f;
                    const float macroScale = 4f;
                    var fUp = (float)(noise.Noise(x * detail, 0, (y + 1) * detail) * detailScale + noise.Noise(x * macro, 0, (y + 1) * macro) * macroScale) ;
                    var fDown = (float)(noise.Noise(x * detail, 0,  (y - 1) * detail) * detailScale + noise.Noise(x * macro, 0,  (y - 1) * macro) * macroScale) ;
                    var fLeft = (float)(noise.Noise((x - 1) * detail, 0, y * detail) * detailScale + noise.Noise((x - 1) * macro, 0, y * macro) * macroScale);
                    var fRight = (float)(noise.Noise((x + 1) * detail, 0, y * detail) * detailScale + noise.Noise((x + 1) * macro, 0, y * macro) * macroScale);
                    var fThis = (float)(noise.Noise(x * detail, 0, y * detail) * detailScale + noise.Noise(x * macro, 0, y * macro) * macroScale);

                    
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
            using (_shader.Program.Bind())
            {
                _shader.ModelViewProjection.Value = Model * View * Projection;
                _shader.ViewProjection.Value = View * Projection;
                _shader.DiffuseUniform.Value = Diffuse;

                using(_vertexArray.Bind())
                using (_elementBuffer.Bind())
                {
                    _gl.DrawElements(PrimitiveType.Triangles, (int)_elementBuffer.Elements, ElementBufferItemType.UnsignedInt);
                }
            }
        }

    }
}
