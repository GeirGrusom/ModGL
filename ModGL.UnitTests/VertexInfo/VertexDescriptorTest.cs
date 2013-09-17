using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;
using ModGL.VertexInfo;

using NUnit.Framework;

namespace ModGL.UnitTests.VertexInfo
{
    [TestFixture]
    public class VertexDescriptorTest
    {
        public struct TestVertex
        {
            public float Field;
        }

        public struct TestVertexOverridesToInt
        {
            [VertexElement(DataType.Int)]
            public float Field;
        }

        public struct TestVertexOverridesDimensions
        {
            [VertexElement(DataType.Float, 4)]
            public float Field;
        }


        public struct TestVertexWithToFields
        {
            public float Field;
            public int FieldInt;
        }

        public struct TestVertexWithTwoFieldsOneIgnored
        {
            [IgnoreVertexElement]
            public float Field;

            public int FieldInt;
        }



        [Test]
        public void Create_GeneratesVertexDescriptorForStruct_WithOneField()
        {
            // Arrange

            // Act
            var result = ModGL.VertexInfo.VertexDescriptor.Create<TestVertex>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(TestVertex), result.ElementType);
            Assert.AreEqual(1, result.Elements.Count());
            Assert.AreEqual("Field", result.Elements.Single().Name);
            Assert.AreEqual(DataType.Float, result.Elements.Single().Type);
            Assert.AreEqual(0, result.Elements.Single().Offset);
            Assert.AreEqual(1, result.Elements.Single().Dimensions);
        }

        [Test]
        public void Create_GeneratesVertexDescriptorForStruct_WithOneField_OvveridesDataType()
        {
            // Arrange

            // Act
            var result = VertexDescriptor.Create<TestVertexOverridesToInt>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Elements.Count());
            Assert.AreEqual("Field", result.Elements.Single().Name);
            Assert.AreEqual(DataType.Int, result.Elements.Single().Type);
            Assert.AreEqual(0, result.Elements.Single().Offset);
            Assert.AreEqual(1, result.Elements.Single().Dimensions);
        }

        [Test]
        public void Create_GeneratesVertexDescriptorForStruct_WithOneField_OvveridesDimensions()
        {
            // Arrange

            // Act
            var result = VertexDescriptor.Create<TestVertexOverridesDimensions>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Elements.Count());
            Assert.AreEqual("Field", result.Elements.Single().Name);
            Assert.AreEqual(DataType.Float, result.Elements.Single().Type);
            Assert.AreEqual(0, result.Elements.Single().Offset);
            Assert.AreEqual(4, result.Elements.Single().Dimensions);
        }

        [Test]
        public void Create_GeneratesVertexDescriptorForStruct_WithTwoFields_OneIsIgnored_CorrectOffset()
        {
            // Arrange

            // Act
            var result = VertexDescriptor.Create<TestVertexWithTwoFieldsOneIgnored>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(TestVertexWithTwoFieldsOneIgnored), result.ElementType);
            Assert.AreEqual(1, result.Elements.Count());
            Assert.AreEqual("FieldInt", result.Elements.Single().Name);
            Assert.AreEqual(DataType.Int, result.Elements.Single().Type);
            Assert.AreEqual(4, result.Elements.Single().Offset);
            Assert.AreEqual(1, result.Elements.Single().Dimensions);
        }


        [Test]
        public void Create_GeneratesVertexDescriptorForStruct_WithTwoFields_CorrectOffset()
        {
            // Precondition: Create_GeneratesVertexDescriptorForStruct_WithOneField
            // Arrange

            // Act
            var result = ModGL.VertexInfo.VertexDescriptor.Create<TestVertexWithToFields>();

            // Assert
            Assert.AreEqual(2, result.Elements.Count());
            Assert.AreEqual("FieldInt", result.Elements.Skip(1).Single().Name);
            Assert.AreEqual(DataType.Int, result.Elements.Skip(1).Single().Type);
            Assert.AreEqual(4, result.Elements.Skip(1).Single().Offset);
            Assert.AreEqual(1, result.Elements.Skip(1).Single().Dimensions);
        }

    }
}
