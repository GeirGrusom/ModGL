using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using ModGL.Buffers;
using ModGL.NativeGL;
using ModGL.VertexInfo;
using NUnit.Framework;
using NSubstitute;

namespace ModGL.UnitTests.Buffers
{
    [TestFixture]
    public class BufferTest
    {
        [Test]
        public void Constructor_NullElements_ThrowsArgumentNullException()
        {
            var gl = Substitute.For<IOpenGL30>();
            gl.WhenForAnyArgs(g => g.GenBuffers(Arg.Any<int>(), Arg.Any<uint[]>()))
                .Do(x =>
                { ((uint[])x[1])[0] = 1; });

            var exception = Assert.Catch<ArgumentNullException>(() => new ElementBuffer<int>(null, gl));
            
            Assert.AreEqual("elements", exception.ParamName);
        }

        [Test]
        public void Constructor_Ok()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.WhenForAnyArgs(g => g.GenBuffers(Arg.Any<int>(), Arg.Any<uint[]>()))
                .Do(x =>
                { ((uint[])x[1])[0] = 1; });


            // Act
            var buffer = new ElementBuffer<int>(new[] { 1, 2, 3 }, gl);

            // Assert
            Assert.AreEqual(1, buffer.Handle);
            Assert.AreEqual(3, buffer.Elements);
            Assert.AreEqual(4, buffer.ElementSize);
            Assert.AreEqual(typeof(int), buffer.ElementType);
        }

        [Test]
        public void Constructor_NullGl_ThrowsArgumentNullException()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();

            //Act
            var exception = Assert.Catch<ArgumentNullException>(() => new ElementBuffer<int>(new int[0], null));

            // Assert
            Assert.AreEqual("gl", exception.ParamName);
        }

        [Test]
        public void Bind_BindsToGl_UnbindsOnDispose()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.When(g => g.GenBuffers(Arg.Any<int>(), Arg.Any<uint[]>())).Do(x => ((uint[])x[1])[0] = 1);
            var buffer = new ElementBuffer<int>(new[] { 1, 2, 3 }, gl);

            // Act
            using (buffer.Bind())
            {
            }

            // Assert
            gl.Received().BindBuffer((uint)BufferTarget.ElementArray, 1);
            gl.Received().BindBuffer((uint)BufferTarget.ElementArray, 0);
        }

        [Test]
        public void Bind_WithIndex_BindsToGl_UnbindsOnDispose()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.When(g => g.GenBuffers(Arg.Any<int>(), Arg.Any<uint[]>())).Do(x => ((uint[])x[1])[0] = 1);
            var buffer = new ElementBuffer<int>(new[] { 1, 2, 3 }, gl);

            // Act
            using (buffer.Bind(index: 2))
            {
            }

            // Assert
            gl.Received().BindBufferBase((uint)BufferTarget.ElementArray, 2, 1);
            gl.Received().BindBufferBase((uint)BufferTarget.ElementArray, 2, 0);
        }

        [Test]
        public void Bind_WithIndexAndRange_BindsToGl_UnbindsOnDispose()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.When(g => g.GenBuffers(Arg.Any<int>(), Arg.Any<uint[]>())).Do(x => ((uint[])x[1])[0] = 1);
            var buffer = new ElementBuffer<int>(new[] { 1, 2, 3 }, gl);

            // Act
            using (buffer.Bind(index: 2, startIndex:2, elements: 4))
            {
            }

            // Assert
            gl.Received().BindBufferRange((uint)BufferTarget.ElementArray, 2,  1, new IntPtr(8), new IntPtr(16));
            gl.Received().BindBufferBase((uint)BufferTarget.ElementArray, 2,  0);
        }

        [Test]
        public void BufferData_Ok()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.WhenForAnyArgs(g => g.GenBuffers(Arg.Any<int>(), Arg.Any<uint[]>()))
                .Do(x =>
                { ((uint[])x[1])[0] = 1; });
            var buffer = new ElementBuffer<int>(new[] { 1, 2, 3 }, gl);

            // Act
            buffer.BufferData(BufferUsage.StaticDraw);

            // Assert
            gl.Received().BufferData((uint)BufferTarget.ElementArray, new IntPtr(12), Arg.Any<IntPtr>(), (uint)BufferUsage.StaticDraw);
        }

        [Test]
        public void BufferSubData_Expression_UsesCorrectOffsetAndSize()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.WhenForAnyArgs(g => g.GenBuffers(Arg.Any<int>(), Arg.Any<uint[]>()))
                .Do(x =>
                { ((uint[])x[1])[0] = 1; });

            var buffer = new VertexBuffer<PositionNormalTexCoord>(0, gl);

            // Act
            buffer.BufferSubData(BufferUsage.StaticDraw, x => x.TexCoord);

            // Assert
            gl.Received().BufferSubData((uint)BufferTarget.Array, new IntPtr(24), new IntPtr(8), Arg.Any<IntPtr>());

        }

        [Test]
        public void BufferSubData_Direct_Ok()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.WhenForAnyArgs(g => g.GenBuffers(Arg.Any<int>(), Arg.Any<uint[]>()))
                .Do(x =>
                { ((uint[])x[1])[0] = 1; });

            var buffer = new ElementBuffer<int>(new[] { 1, 2, 3 }, gl);

            // Act
            buffer.BufferSubData(BufferUsage.StaticDraw, 1, 2);

            // Assert
            gl.Received().BufferSubData((uint)BufferTarget.ElementArray, new IntPtr(1), new IntPtr(2), Arg.Any<IntPtr>());
        }
    }
}
