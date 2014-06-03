using System.Numerics;
using ModGL.Numerics;
using NUnit.Framework;

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

        // This constant is used because Cos(PI/2) isn't exactly equal to 0 due to floating point
        // rounding errors.
        private const float c0 = -4.371139e-08f;

        [Test]
        public void RotateX_ReturnsRotatedMatrix()
        {
            // Arrange
            // Act
            var mat = MatrixHelper.RotateX((float)(System.Math.PI / 2));
            var results = new[] {mat.Row(0), mat.Row(1), mat.Row(2), mat.Row(3)};

            // Assert
            Assert.That(results, Is.EquivalentTo(new []
            {
                new Vector4f(1, 0, 0, 0), 
                new Vector4f(0, c0, -1, 0), 
                new Vector4f(0, 1, c0, 0), 
                new Vector4f(0, 0, 0, 1)
            }));
        }

        [Test]
        public void RotateY_ReturnsRotatedMatrix()
        {
            // Arrange
            // Act
            var mat = MatrixHelper.RotateY((float)(System.Math.PI / 2));
            var results = new[] {mat.Row(0), mat.Row(1), mat.Row(2), mat.Row(3)};

            // Assert
            Assert.That(results, Is.EquivalentTo(new []
            {
                new Vector4f(c0, 0, 1, 0), 
                new Vector4f(0, 1, 0, 0), 
                new Vector4f(-1, 0, c0, 0), 
                new Vector4f(0, 0, 0, 1)
            }));
        }

        [Test]
        public void RotateZ_ReturnsRotatedMatrix()
        {
            // Arrange
            // Act
            var mat = MatrixHelper.RotateZ((float)(System.Math.PI / 2));
            var results = new[] {mat.Row(0), mat.Row(1), mat.Row(2), mat.Row(3)};
            
            // Assert
            Assert.That(results, Is.EquivalentTo(new []
            {
                new Vector4f(c0, -1, 0, 0), 
                new Vector4f(1, c0, 0, 0), 
                new Vector4f(0, 0, 1, 0), 
                new Vector4f(0, 0, 0, 1)
            }));
        }
    }
}
