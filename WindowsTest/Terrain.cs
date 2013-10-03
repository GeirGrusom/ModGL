using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        private readonly IVertexBuffer _tangents;
        private readonly IElementArray _ib;

        private readonly IOpenGL30 _gl;
        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public struct Tangents
        {
            public static readonly VertexDescriptor Descriptor = VertexDescriptor.Create<Tangents>();

            [VertexElement(DataType.Float, 3)]
            public Vector3F Tangent;

            [VertexElement(DataType.Float, 3)]
            public Vector3F BiTangent; 
        }

        public Terrain(IOpenGL30 gl, int columns, int rows, Func<float, float, Vector3F> heightFunction)
        {
            _gl = gl;
            _vb = CreateVertexBuffer(gl, columns, rows, heightFunction);
            _ib = CreateElementBuffer(gl, columns, rows);
            _tangents = CreateTangentBuffer(gl, (ElementBuffer<uint>)_ib, (VertexBuffer<VertexType>)_vb);
            _vertexArray = new VertexArray(gl, new [] { _vb, _tangents }, new [] { VertexType.Descriptor, Tangents.Descriptor } );
        }

        public void Render()
        {
            var renderer = new ModGL.Rendering.Renderer(_gl);
            using (_vertexArray.Bind())
            {
                renderer.DrawElements(_ib, DrawMode.Triangles);
            }
        }

        private static Vector3F CreateNormal(Vector3F v1, Vector3F v2, Vector3F v3)
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

        private IVertexBuffer CreateTangentBuffer(IOpenGL30 gl, ElementBuffer<uint> ib, VertexBuffer<VertexType> vb)
        {
            var tangents = new VertexBuffer<Tangents>(vb.Elements, gl);
            int[] counter = new int[vb.Elements];

            for (int i = 0; i < ib.Elements; i += 3)
            {
                var v1 = vb[ib[i]];
                var v2 = vb[ib[i + 1]];
                var v3 = vb[ib[i + 2]];

                var delta1 = v2.Position - v1.Position;
                var delta2 = v3.Position - v1.Position;

                var deltaUv1 = v2.TexCoord - v1.TexCoord;
                var deltaUv2 = v3.TexCoord - v1.TexCoord;

                float r = 1.0f / (deltaUv1.X * deltaUv2.Y - deltaUv1.Y * deltaUv2.X);
                var tangent = (delta1 * deltaUv2.Y   - delta2 * delta1.Y)*r;
                var bitangent = (delta2 * deltaUv1.X   - delta1 * delta2.X)*r;

                counter[ib[i]]++;
                counter[ib[i + 1]]++;
                counter[ib[i + 2]]++;

                tangents[ib[i]] = new Tangents { Tangent = tangents[ib[i]].Tangent + tangent, BiTangent = tangents[ib[i]].BiTangent + bitangent };
                tangents[ib[i + 1]] = new Tangents { Tangent = tangents[ib[i + 1]].Tangent + tangent, BiTangent = tangents[ib[i + 1]].BiTangent + bitangent };
                tangents[ib[i + 2]] = new Tangents { Tangent = tangents[ib[i + 2]].Tangent + tangent, BiTangent = tangents[ib[i + 2]].BiTangent + bitangent };
            }

            for (int i = 0; i < tangents.Elements; i++)
            {
                int count = counter[i];
                tangents[i] = new Tangents { Tangent  = tangents[i].Tangent / count, BiTangent = tangents[i].BiTangent / count};
            }

            using (tangents.Bind())
            {
                tangents.BufferData(BufferUsage.StaticDraw);
            }
            return tangents;
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
                        Normal = ((CreateNormal(result, heightFunc(j - 1, i), heightFunc(j, i + 1))+
                                CreateNormal(result, heightFunc(j + 1, i), heightFunc(j, i + 1)) +
                                CreateNormal(result, heightFunc(j - 1, i), heightFunc(j, i - 1)) +
                                CreateNormal(result, heightFunc(j - 1, i), heightFunc(j, i - 1))) * 0.25f).Normalize(),
                                TexCoord = new Vector2F((i / 128f) % 1f, (j / 128f) % 1f)
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
