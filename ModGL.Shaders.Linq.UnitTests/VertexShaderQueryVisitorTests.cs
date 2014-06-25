using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ModGL.Shaders.Linq.UnitTests
{

    public struct Vertex
    {
        public Vector3f Position;
    }
    public class VertexShaderQueryVisitorTests
    {
        [Test]
        public void Let_Uniform_DefinedUniform()
        {
            // Arrange
            var query = from vertex in (new VertexShaderQueryable<Vertex>())
                let model = Uniform.mat4
                select vertex;

            // Act
            var result = query.ToShaderString();

            // Assert
            Assert.That(result, Contains.Substring("uniform mat4 model;"));
        }
    }
}
