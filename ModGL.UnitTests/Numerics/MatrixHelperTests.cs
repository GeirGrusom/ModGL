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
    public class MatrixHelperTests
    {
        [Test]
        public void Translate_Vector_ReturnsTranslationMatrix()
        {
            // Arrange
            // Act
            var result = MatrixHelper.Translate(new Vector3f(1, 2, 3));

            // Assert
            Assert.That(result.Row(0), Is.EqualTo(new Vector4f(1, 0, 0, 0)));
            Assert.That(result.Row(1), Is.EqualTo(new Vector4f(0, 1, 0, 0)));
            Assert.That(result.Row(2), Is.EqualTo(new Vector4f(0, 0, 1, 0)));
            Assert.That(result.Row(3), Is.EqualTo(new Vector4f(1, 2, 3, 1)));
        }

        [Test]
        public void Translate_Xyz_ReturnsTranslationMatrix()
        {
            // Arrange
            // Act
            var result = MatrixHelper.Translate(1, 2, 3);

            // Assert
            Assert.That(result.Row(0), Is.EqualTo(new Vector4f(1, 0, 0, 0)));
            Assert.That(result.Row(1), Is.EqualTo(new Vector4f(0, 1, 0, 0)));
            Assert.That(result.Row(2), Is.EqualTo(new Vector4f(0, 0, 1, 0)));
            Assert.That(result.Row(3), Is.EqualTo(new Vector4f(1, 2, 3, 1)));
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
    }
}
