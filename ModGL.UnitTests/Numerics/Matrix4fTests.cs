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
    public class Matrix4fTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetRow_ReturnsCorrectRow(int index)
        {
            // Arrange
            var vectors = new Vector4f[4];
            vectors[index] = new Vector4f(1, 2, 3, 4);
            var mat = new Matrix4f(vectors[0], vectors[1], vectors[2], vectors[3]);

            // Act
            var theRow = mat.Row(index);

            // Assert
            Assert.That(theRow, Is.EqualTo(new Vector4f(1, 2, 3, 4)));
        }

        [Test]
        public void GetColumn0_ReturnsColumn0()
        {
            // Arrange
            var mat = new Matrix4f(new Vector4f(1, 0, 0, 0), new Vector4f(2, 0, 0, 0), new Vector4f(3, 0, 0, 0), new Vector4f(4, 0, 0, 0));

            // Act
            var theRow = mat.Column(0);

            // Assert
            Assert.That(theRow, Is.EqualTo(new Vector4f(1, 2, 3, 4)));
        }

        [Test]
        public void GetColumn1_ReturnsColumn1()
        {
            // Arrange
            var mat = new Matrix4f(new Vector4f(0, 1, 0, 0), new Vector4f(0, 2, 0, 0), new Vector4f(0, 3, 0, 0), new Vector4f(0, 4, 0, 0));

            // Act
            var theRow = mat.Column(1);

            // Assert
            Assert.That(theRow, Is.EqualTo(new Vector4f(1, 2, 3, 4)));
        }

        [Test]
        public void GetColumn2_ReturnsColumn2()
        {
            // Arrange
            var mat = new Matrix4f(new Vector4f(0, 0, 1, 0), new Vector4f(0, 0, 2, 0), new Vector4f(0, 0, 3, 0), new Vector4f(0, 0, 4, 0));

            // Act
            var theRow = mat.Column(2);

            // Assert
            Assert.That(theRow, Is.EqualTo(new Vector4f(1, 2, 3, 4)));
        }

        [Test]
        public void GetColumn3_ReturnsColumn3()
        {
            // Arrange
            var mat = new Matrix4f(new Vector4f(0, 0, 0, 1), new Vector4f(0, 0, 0, 2), new Vector4f(0, 0, 0, 3), new Vector4f(0, 0, 0, 4));

            // Act
            var theRow = mat.Column(3);

            // Assert
            Assert.That(theRow, Is.EqualTo(new Vector4f(1, 2, 3, 4)));
        }

        [Test] // Work in progress
        public void Determinant_ReturnsCorrectDeterminant()
        {
            // Arrange
            var mat = new Matrix4f
                (
                new Vector4f(3, 2, -1, 4),
                new Vector4f(2, 1, 5, 7),
                new Vector4f(0, 5, 2, -6),
                new Vector4f(-1, 2, 1, 0)
                );

            // Act
            var determinant = mat.Determinant();

            // Assert
            Assert.That(determinant, Is.EqualTo(-418));
        }

        [Test]
        public void Invert_NotInvertible_ThrowsInvalidOperationException()
        {
            // Arrange
            var mat = new Matrix4f(
                new Vector4f(1, 2, 3, 4), 
                new Vector4f(5, 6, 7, 8), 
                new Vector4f(9, 10, 11, 12),
                new Vector4f(13, 14, 15, 16));

            // Act
            TestDelegate invert = () => mat.Invert();

            // Assert
            Assert.That(invert, Throws.InvalidOperationException);
        }

        [Test]
        public void Invert_ReturnsInvertedMatrix()
        {
            // Arrange
            var mat = new Matrix4f(
                new Vector4f(4, 0, 0, 0), 
                new Vector4f(0, 0, 2, 0), 
                new Vector4f(0, 1, 2, 0), 
                new Vector4f(1, 0, 0, 1)   );

            // Act
            var resultMat = mat.Invert();
            var results = new[] {resultMat.Row(0), resultMat.Row(1), resultMat.Row(2), resultMat.Row(3)};

            // Assert
            Assert.That(results, Is.EquivalentTo(new []
            {
                new Vector4f(),
                new Vector4f(),
                new Vector4f(),
                new Vector4f() 
            }));
        }

        [Test]
        public void Multiply_Matrix_ReturnsCorrectMatrix()
        {
            // Arrange
            var left = new Matrix4f(
                new Vector4f(1, 2, 3, 4), 
                new Vector4f(5, 6, 7, 8), 
                new Vector4f(9, 10, 11, 12),
                new Vector4f(13, 14, 15, 16));
            
            var right = new Matrix4f(
                new Vector4f(21, 22, 23, 24), 
                new Vector4f(25, 26, 27, 28),
                new Vector4f(29, 30, 31, 32), 
                new Vector4f(33, 34, 35, 36));

            // Act
            var result = left.Multiply(right);

            // Assert
            Assert.That(result.Row(0), Is.EqualTo(new Vector4f(650, 740, 830, 920)));
            Assert.That(result.Row(1), Is.EqualTo(new Vector4f(762, 868, 974, 1080)));
            Assert.That(result.Row(2), Is.EqualTo(new Vector4f(874, 996, 1118, 1240)));
            Assert.That(result.Row(3), Is.EqualTo(new Vector4f(986, 1124, 1262, 1400)));
        }

        [Test]
        public void Multiply_Vector4f_ReturnsTranslatedVector()
        {
            // Arrange
            var mat = MatrixHelper.Translate(2, 3, 4);
            
            // Act
            var result = mat.Multiply(new Vector4f(0, 0, 0, 1));

            // Assert
            Assert.That(result, Is.EqualTo(new Vector4f(2, 3, 4, 1)));
        }
        
        [Test]
        public void Transpose_ReturnsRowsAsColumns()
        {
            var mat = new Matrix4f(new Vector4f(1, 2, 3, 4), new Vector4f(1, 2, 3, 4), new Vector4f(1, 2, 3, 4),
                new Vector4f(1, 2, 3, 4));

            Matrix4f result = mat.Transpose();

            Assert.That(result.Column(0), Is.EqualTo(new Vector4f(1, 2, 3, 4)));
            Assert.That(result.Column(1), Is.EqualTo(new Vector4f(1, 2, 3, 4)));
            Assert.That(result.Column(2), Is.EqualTo(new Vector4f(1, 2, 3, 4)));
            Assert.That(result.Column(3), Is.EqualTo(new Vector4f(1, 2, 3, 4)));
        }
    }
}
