using ModGL.NativeGL;
using ModGL.ObjectModel.Shaders;

using NUnit.Framework;
using NSubstitute;

namespace ModGL.ObjectModel.UnitTests.Shaders
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
            Assert.That(() => new Program(gl, new[] { mockShader }), Throws.TypeOf<NoHandleCreatedException>());
        }
        [Test]
        public void CompileProgram_UncompiledShader_CompilesShadersAsWell()
        {
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateProgram().Returns(1u);
            var mockShader = Substitute.For<IShader>();
            // IsValid <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.ValidateStatus, Arg.Any<int[]>())) 
                .Do(x => ((int[])x.Args()[2])[0] = 1);
            // IsLinked <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);

            var program = new Program(gl, new[] { mockShader });

            program.Compile();

            mockShader.Received(1).Compile();
        }

        [Test]
        public void CompileProgram_CompiledShader_DoesNotCompileShader()
        {
            var gl = Substitute.For<IOpenGL30>();
            gl.CreateProgram().Returns(1u);
            var mockShader = Substitute.For<IShader>();
            mockShader.IsCompiled.Returns(true);
            // IsValid <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.ValidateStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);
            // IsLinked <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);

            var program = new Program(gl, new[] { mockShader });

            program.Compile();

            mockShader.DidNotReceive().Compile();
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
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.ValidateStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);
            // IsLinked <- true
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);

            var program = new Program(gl, new[] { mockShader });

            // Act
            var exception = Assert.Throws<ProgramCompilationException>(program.Compile);

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
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.ValidateStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);
            // IsLinked <- false
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 0);

            var program = new Program(gl, new IShader[0]);

            // Act
            
            var exception = Assert.Throws<ProgramCompilationException>(program.Compile);

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
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.ValidateStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 0);
            // IsLinked <- false
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x.Args()[2])[0] = 1);

            var program = new Program(gl, new IShader[0]);

            // Act
            var exception = Assert.Throws<ProgramCompilationException>(program.Compile);

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
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.ValidateStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x[2])[0] = 0);
            // IsLinked <- false
            gl.When(g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.LinkStatus, Arg.Any<int[]>()))
                .Do(x => ((int[])x[2])[0] = 1);

            
            gl.When( g => g.GetProgramiv(Arg.Any<uint>(), (uint)ProgramParameters.InfoLogLength, Arg.Any<int[]>()))
                .Do(c =>
                {
                    ((int[])c[2])[0] = 1;
                });
            string log = new string(' ', 1024);
            gl.WhenForAnyArgs(g => g.GetProgramInfoLog(Arg.Any<uint>(), Arg.Any<int>(), Arg.Any<int[]>(), ref log))
                .Do(c =>
                {
                    ((int[])c[2])[0] = 1;
                    (c[3]) = "A";
                });

            var program = new Program(gl, new IShader[0] );

            // Act
            var exception = Assert.Throws<ProgramCompilationException>(program.Compile);

            // Assert
            Assert.AreEqual("Program compilation failed: A", exception.Message);

        }

    }
}
