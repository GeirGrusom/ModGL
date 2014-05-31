using System.Numerics;
using ModGL.Numerics;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ModGL.UnitTests.Numerics
{
    [TestFixture]
    public class MatrixHelperTests
    {
        [Test]
        public void Translate_Vector_ReturnsTranslationMatrix()
        {
            // Arrange
            // Act
            var result = MatrixHelper.Translate(new Vector3f(1, 2, 3));

            // Assert
            Assert.That(result.Row(0), Is.EqualTo(new Vector4f(1, 0, 0, 1)));
            Assert.That(result.Row(1), Is.EqualTo(new Vector4f(0, 1, 0, 2)));
            Assert.That(result.Row(2), Is.EqualTo(new Vector4f(0, 0, 1, 3)));
            Assert.That(result.Row(3), Is.EqualTo(new Vector4f(0, 0, 0, 1)));
        }

        [Test]
        public void Translate_Xyz_ReturnsTranslationMatrix()
        {
            // Arrange
            // Act
            var result = MatrixHelper.Translate(1, 2, 3);

            // Assert
            Assert.That(result.Row(0), Is.EqualTo(new Vector4f(1, 0, 0, 1)));
            Assert.That(result.Row(1), Is.EqualTo(new Vector4f(0, 1, 0, 2)));
            Assert.That(result.Row(2), Is.EqualTo(new Vector4f(0, 0, 1, 3)));
            Assert.That(result.Row(3), Is.EqualTo(new Vector4f(0, 0, 0, 1)));
        }

        [Test]
        public void Scale_Vector_ReturnsScaleMatrix()
        {
            // Arrange
            // Act
            var result = MatrixHelper.Scale(new Vector3f(1, 2, 3));

            // Assert
            Assert.That(result.Row(0), Is.EqualTo(new Vector4f(1, 0, 0, 0)));
            Assert.That(result.Row(1), Is.EqualTo(new Vector4f(0, 2, 0, 0)));
            Assert.That(result.Row(2), Is.EqualTo(new Vector4f(0, 0, 3, 0)));
            Assert.That(result.Row(3), Is.EqualTo(new Vector4f(0, 0, 0, 1)));
        }

        [Test]
        public void Scale_Xyz_ReturnsScaleMatrix()
        {
            // Arrange
            // Act
            var result = MatrixHelper.Scale(1, 2, 3);

            // Assert
            Assert.That(result.Row(0), Is.EqualTo(new Vector4f(1, 0, 0, 0)));
            Assert.That(result.Row(1), Is.EqualTo(new Vector4f(0, 2, 0, 0)));
            Assert.That(result.Row(2), Is.EqualTo(new Vector4f(0, 0, 3, 0)));
            Assert.That(result.Row(3), Is.EqualTo(new Vector4f(0, 0, 0, 1)));
        }

        [Test]
        public void RotateZ_ReturnsRotatedMatrix()
        {
            // Arrange
            // Act
            var mat = MatrixHelper.RotateZ((float) System.Math.PI/2);

            // Assert
            Assert.That(mat.Row(0), Is.EqualTo(new Vector4f(0, 1, 0, 0)));
            Assert.That(mat.Row(1), Is.EqualTo(new Vector4f(-1, 0, 0, 0)));
            Assert.That(mat.Row(2), Is.EqualTo(new Vector4f(0, 0, 1, 0)));
            Assert.That(mat.Row(3), Is.EqualTo(new Vector4f(0, 0, 0, 0)));
        }
    }
}
