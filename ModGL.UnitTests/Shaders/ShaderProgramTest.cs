using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;
using ModGL.Shaders;

using NUnit.Framework;
using NSubstitute;

namespace ModGL.UnitTests.Shaders
{
    [TestFixture]
    public class ShaderProgramTest
    {

        [Test]
        public void Constructor_ReturnsHandleZero_FailsWithNoHandleCreatedException()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateProgram().Returns(0u);
            var mockShader = Substitute.For<IShader>();

            // Act
            // Assert
            var exception = Assert.Catch<NoHandleCreatedException>(() =>  new Program(gl, new[] { mockShader }));
        }
        [Test]
        public void CompileProgram_Ok() // Test is fairly useless.
        {
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateProgram().Returns(1u);
            var mockShader = Substitute.For<IShader>();
            // IsValid <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.ValidateStatus, Arg.Any<int[]>())) 
                .Do(x => ((int[])x.Args()[2])[0] = 1);
            // IsLinked <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);

            var Program = new Program(gl, new[] { mockShader });

            Program.Compile();
        }

        [Test]
        public void Compile_ShaderComplation_Fails_ShaderException_In_ProgramException()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateProgram().Returns(1u);

            var mockShader = Substitute.For<IShader>();
            mockShader.When(i => i.Compile()).Do(x => { throw new ShaderCompilationException(null, null); });
            // IsValid <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.ValidateStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);
            // IsLinked <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);

            var Program = new Program(gl, new[] { mockShader });

            // Act
            var exception = Assert.Catch<ProgramCompilationException>(Program.Compile);

            // Assert
            Assert.IsNotNull(exception.InnerException);
            Assert.IsInstanceOf<ShaderCompilationException>(exception.InnerException);
        }

        [Test]
        public void Compile_LinkingFails_ThrowsProgramCompilationException()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateProgram().Returns(1u);

            // IsValid <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.ValidateStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);
            // IsLinked <- false
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 0);

            var Program = new Program(gl, new IShader[0]);

            // Act
            
            var exception = Assert.Throws<ProgramCompilationException>(Program.Compile);

            // Assert
            Assert.AreEqual(false, exception.CompilationResults.Linked);
            Assert.AreEqual(true, exception.CompilationResults.Validated);
        }

        [Test]
        public void Compile_ValidationFails_ThrowsProgramCompilationException()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateProgram().Returns(1u);
            // IsValid <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.ValidateStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 0);
            // IsLinked <- false
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);

            var Program = new Program(gl, new IShader[0]);

            // Act
            var exception = Assert.Throws<ProgramCompilationException>(Program.Compile);

            // Assert
            Assert.AreEqual(true, exception.CompilationResults.Linked);
            Assert.AreEqual(false, exception.CompilationResults.Validated);
        }

        [Test] //
        public void Compile_Fails_ThrowsProgramCompilationException_ReturnsCompileLog()
        {
            // Arrange
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateProgram().Returns(1u);


            // IsValid <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.ValidateStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x[2])[0] = 0);
            // IsLinked <- false
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x[2])[0] = 1);

            int count = 0;

            gl.When( g => g.GetProgramiv(Arg.Any<uint>(), ProgramParameters.InfoLogLength, Arg.Any<int[]>()))
                .Do(c => { ((int[])c[2])[0] = 1; });
            gl.WhenForAnyArgs(g => g.GetProgramInfoLog(Arg.Any<uint>(), Arg.Any<int>(), out count, Arg.Any<byte[]>()))
                .Do(c =>
                {
                    c[2] = 1;
                    ((byte[])c[3])[0] = (byte)'A';
                });

            var Program = new Program(gl, new IShader[0] );

            // Act
            var exception = Assert.Throws<ProgramCompilationException>(Program.Compile);

            // Assert
            Assert.AreEqual("Program compilation failed: A", exception.Message);

        }

    }
}
