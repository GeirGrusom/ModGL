using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.Buffers;
using ModGL.NativeGL;
using ModGL.VertexInfo;

using NUnit.Framework;
using NSubstitute;

namespace ModGL.UnitTests
{
    [TestFixture]
    public class VertexArrayTest
    {

        public struct TestVertex
        {
            public float Value;
        }

        [Test]
        public void Constructor_BindsAttributeInfo_Ok()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.WhenForAnyArgs(g => g.glGenVertexArrays(Arg.Any<int>(), Arg.Any<uint[]>()))
                .Do(x => ((uint[])x[1])[0] = 1);
            var vertexBuffer = Substitute.For<IVertexBuffer>();
            var descriptor = new VertexDescriptor(typeof(TestVertex), new[] { new VertexElement("Value", DataType.Float, 1, 0) });

            // Act
            var vertexArray = new VertexArray(gl, new[] { vertexBuffer }, new[] { descriptor });

            // Assert
            gl.Received().glVertexAttribPointer(0, 1, DataType.Float, GLboolean.False, 4, IntPtr.Zero); // Stride should be size of vertex element.
            vertexBuffer.Received().Bind();
            gl.Received().glBindVertexArray(0);
            Assert.Contains(vertexBuffer, vertexArray.Buffers.ToArray());
        }

        public struct TestVertexTwoFields
        {
            public float Value;
            public int ValueInt;
        }

        [Test]
        public void Constructor_BindsAttributeInfoForTwoFields_Ok()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();

            gl.WhenForAnyArgs(g => g.glGenVertexArrays(Arg.Any<int>(), Arg.Any<uint[]>()))
                .Do(x => ((uint[])x[1])[0] = 1);

            var vertexBuffer = Substitute.For<IVertexBuffer>();
            var descriptor = new VertexDescriptor(typeof(TestVertexTwoFields), new[] { new VertexElement("Value", DataType.Float, 1, 0), new VertexElement("ValueInt", DataType.Int, 1, 4) });

            // Act
            var vertexArray = new VertexArray(gl, new[] { vertexBuffer }, new[] { descriptor });

            // Assert
            gl.Received().glVertexAttribPointer(0, 1, DataType.Float, GLboolean.False, 8, IntPtr.Zero); // Stride should be size of vertex element.
            gl.Received().glVertexAttribIPointer(1, 1, DataType.Int, 8, new IntPtr(4));
        }

    }
}
