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
        public void CompileShader_Ok()
        {
            var gl = Substitute.For<IOpenGL30>();
            var mockShader = Substitute.For<IShader>();
            gl.glGetProgramiv(Arg.Any<uint>(), ProgramParameters.ValidateStatus, Arg.Any<int[]>());

            var Program = new Program(gl, new[] { mockShader });

            Program.Compile();
        }
    }
}
