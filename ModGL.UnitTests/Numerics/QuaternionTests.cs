using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ModGL.Numerics;
using NUnit.Framework;

namespace ModGL.UnitTests.Numerics
{
    [TestFixture]
    public class QuaternionTests
    {
        [Test]
        public void Multiply_ReturnsMultipliedQuaternion()
        {
            // Arrange
            var q = new Quaternion(1, 0, 1, 0);
            var r = new Quaternion(1, 0.5f, 0.5f, 0.75f);

            // Act
            Quaternion result = q.Multiply(r);

            // Assert
            Assert.That(result, Is.EqualTo(new Quaternion(0.5f, 1.25f, 1.5f, 0.25f)));
        }
        [Test]
        public void ToMatrix_ReturnsRotationMatrix()
        {
            // Arrange
            var q = new Quaternion(0.5f, 0.5f, 0.5f, 0.5f);

            // Act
            Matrix4f matrix = q.ToMatrix();
            var results = new[]
            {
                matrix.Row(0),
                matrix.Row(1),
                matrix.Row(2),
                matrix.Row(3)
            };

            // Assert
            Assert.That(results, Is.EquivalentTo(new []
            {
                new Vector4f(0, 0, 1, 0),
                new Vector4f(1, 0, 0, 0),
                new Vector4f(0, 1, 0, 0),
                new Vector4f(0, 0, 0, 1)
            }));
        }

        [Test]
        public void Conjugate_ReturnsNegatedQuaternion()
        {
            // Arrange
            var q = new Quaternion(1, 1, 1, 1);

            // Act
            Quaternion result = q.Conjugate();

            // Assert
            Assert.That(result, Is.EqualTo(new Quaternion(-1, -1, -1, 1)));
        }

        [Test]
        public void Normalize_ReturnsNormalizedQuaternion()
        {
            // Arrange
            var q = new Quaternion(1, 1, 1, 1);

            // Act
            var result = q.Normalize();

            // Assert
            Assert.That(result, Is.EqualTo(new Quaternion(0.5f, 0.5f, 0.5f, 0.5f)));
        }
    }
}
