using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;
using ModGL.ObjectModel.Shaders;

using NSubstitute;

using NUnit.Framework;

namespace ModGL.ObjectModel.UnitTests
{
    [TestFixture]
    public class ShaderTest
    {

        [Test]
        public void Constructor_NoHandleCreated_ThrowsNoHandleCreatedException()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateShader((uint)ShaderType.VertexShader).Returns(0u);

            // Act
            // Assert
            var exception = Assert.Catch<NoHandleCreatedException>(() => new VertexShader(gl, "Foo"));
        }

        [Test]
        public void Constructor_CreatesHandle()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateShader((uint)ShaderType.VertexShader).Returns(1u);

            // Act
            var newShader = new VertexShader(gl, "Foo");

            // Assert
            Assert.AreEqual(1, newShader.Handle);
            Assert.AreEqual("Foo", newShader.Code);
        }

        [Test]
        public void Constructor_GlIsNull_ThrowsArgumentNullException()
        {
            // Arrange

            // Act
            var exception = Assert.Catch<ArgumentNullException>(() => new VertexShader(null, "Foo"));

            // Assert
            Assert.AreEqual("gl", exception.ParamName);
        }

        [Test]
        public void Constructor_CodeIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();

            // Act
            var exception = Assert.Catch<ArgumentNullException>(() => new VertexShader(gl, null));

            // Assert
            Assert.AreEqual("code", exception.ParamName);
        }

        [Test]
        public void Constructor_CodeIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();

            // Act
            var exception = Assert.Catch<ArgumentNullException>(() => new VertexShader(gl, string.Empty));

            // Assert
            Assert.AreEqual("code", exception.ParamName);
        }


        [Test]
        public void GetCompileResults_ReturnsString()
        {
            // Arrange

            var gl = Substitute.For<IOpenGL30>();
            gl.CreateShader(Arg.Any<uint>()).Returns(1u);

            gl.WhenForAnyArgs(g => g.GetShaderiv(0, (uint)ShaderParameters.InfoLogLength, Arg.Any<int[]>()))
                .Do(x => ((int[])x[2])[0] = 1 );

            var result = new string(' ', 1024);

            gl.WhenForAnyArgs(g => g.GetShaderInfoLog(Arg.Any<uint>(), Arg.Any<int>(), Arg.Any<int[]>(), ref result))
                .Do( x =>
                    {
                        ((int[])x[2])[0] = 1;
                        x[3] = "A";
                    });
            var shader = new VertexShader(gl, "Foo");

            // Act
            var results = shader.GetCompilationResults();

            // Assert
            Assert.AreEqual("A", results.Message);
        }

        [Test]
        public void Compile_CompilationFails_ThrowsShaderCompilationResults_ReturnsMessage_SuccessIsFalse()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateShader(Arg.Any<uint>()).Returns(1u);

            gl.WhenForAnyArgs(g => g.GetShaderiv(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<int[]>()))
              .Do(
                  x =>
                  {
                      if ((ShaderParameters)x[1] == ShaderParameters.InfoLogLength)
                          ((int[])x[2])[0] = 1;
                  });
            var log = new string(' ', 1024);
            gl.WhenForAnyArgs(g => g.GetShaderInfoLog(Arg.Any<uint>(), Arg.Any<int>(), Arg.Any<int[]>(), ref log))
                .Do(x =>
                {
                    (x[3]) = "A";
                });
            var shader = new VertexShader(gl, "Foo");

            // Act
            var exception = Assert.Catch<ShaderCompilationException>(shader.Compile);

            // Assert
            Assert.IsNotNull(exception.CompilationResults);
            Assert.AreEqual(false, exception.CompilationResults.Success);
            Assert.AreEqual("A", exception.CompilationResults.Message);
        }

        [Test]
        public void Compile_Succeeds_SetsShaderSource()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateShader(Arg.Any<uint>()).Returns(1u);

            gl.WhenForAnyArgs(g => g.GetShaderiv(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<int[]>()))
              .Do(
                  x =>
                  {
                      if ((ShaderParameters)x[1] == ShaderParameters.CompileStatus)
                          ((int[])x[2])[0] = (int)GLboolean.True;
                  });

            var shader = new VertexShader(gl, "Foo");

            // Act
            shader.Compile();

            // Assert
            gl.Received().ShaderSource(1, 1, Arg.Is<string[]>(s => s.SequenceEqual(new [] { "Foo"} )), Arg.Is<int[]>(s => s.SequenceEqual(new [] { 3 })) );
            gl.Received().CompileShader(1);
        }

    }
}
