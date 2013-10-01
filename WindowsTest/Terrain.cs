using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL;
using ModGL.Buffers;
using ModGL.Math;
using ModGL.NativeGL;
using ModGL.VertexInfo;

namespace WindowsTest
{
    
    public class Terrain
    {
        private readonly IVertexArray _vertexArray;
        private readonly IVertexBuffer _vb;
        private readonly IElementArray _ib;

        private readonly IOpenGL30 _gl;
        public struct VertexType
        {
            public static readonly VertexDescriptor Descriptor = VertexDescriptor.Create<VertexType>();
            [VertexElement(DataType.Float, 3)]
            public Vector3F Position;
            [VertexElement(DataType.Float, 3)]
            public Vector3F Normal;
            [VertexElement(DataType.Float, 2)]
            public Vector2F TexCoord;

        }

        public Terrain(IOpenGL30 gl, int columns, int rows, Func<float, float, Vector3F> heightFunction)
        {
            _gl = gl;
            _vb = CreateVertexBuffer(gl, columns, rows, heightFunction);
            _ib = CreateElementBuffer(gl, columns, rows);
            
            _vertexArray = new VertexArray(gl, new [] { _vb }, new [] { VertexType.Descriptor } );
        }

        public void Render()
        {
            var renderer = new ModGL.Rendering.Renderer(_gl);
            using (_vertexArray.Bind())
            {
                renderer.DrawElements(_ib, DrawMode.Triangles);
            }
        }

        private Vector3F CreateNormal(Vector3F v1, Vector3F v2, Vector3F v3)
        {
            return (v3 - v1).Cross(v2 - v1);
        }

        private IElementArray CreateElementBuffer(IOpenGL30 gl, int columns, int rows)
        {
            var elementBuffer = new ElementBuffer<uint>((columns - 1) * (rows - 1) * 6, gl);
            int k = 0;
            uint v = 0;
            for (uint i = 0; i < (rows - 1); i++)
            {
                for (uint j = 0; j < (columns - 1); j++, v++)
                {
                    elementBuffer[k++] = v;
                    elementBuffer[k++] = v + (uint)columns ;
                    elementBuffer[k++] = v + 1;

                    elementBuffer[k++] = v + 1;
                    elementBuffer[k++] = v + (uint)columns;
                    elementBuffer[k++] = v + (uint)columns + 1;
                }
                v++;
            }
            using (elementBuffer.Bind())
            {
                elementBuffer.BufferData(BufferUsage.StaticDraw);
            }
            return elementBuffer;
        }

        private IVertexBuffer CreateVertexBuffer(IOpenGL30 gl, int columns, int rows, Func<float, float, Vector3F> heightFunc)
        {
            var vertexBuffer = new VertexBuffer<VertexType>(columns * rows, gl);
            int k = 0;
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; j++, k++)
                {
                    var result = heightFunc(j, i);
                    vertexBuffer[k] = new VertexType
                    {
                        Position = result,
                        TexCoord = new Vector2F(j / (float)columns, i / (float)rows),
                        Normal = ((CreateNormal(result, heightFunc(j - 1, i), heightFunc(j, i + 1))+
                                CreateNormal(result, heightFunc(j + 1, i), heightFunc(j, i + 1)) +
                                CreateNormal(result, heightFunc(j - 1, i), heightFunc(j, i - 1)) +
                                CreateNormal(result, heightFunc(j - 1, i), heightFunc(j, i - 1))) * 0.25f).Normalize()
                    };
                }
            }
            using (vertexBuffer.Bind())
            {
                vertexBuffer.BufferData(BufferUsage.StaticDraw);
            }
            return vertexBuffer;
        }
        
    }
}
