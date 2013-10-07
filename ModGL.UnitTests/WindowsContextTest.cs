using System;

using ModGL.Binding;
using ModGL.Windows;
using NSubstitute;

using NUnit.Framework;

namespace ModGL.UnitTests
{
    [TestFixture]
    public class WindowsContextTest
    {
        [Test]
        public void CreateContext_OlderThan_30_NotSupported()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            
            // Act
            var exception = Assert.Catch<VersionNotSupportedException>( () => new WindowsContext(wgl, null, new ContextCreationParameters { MajorVersion = 2 }));

            // Assert
            Assert.AreEqual("OpenGL version below 3.0 is not supported.", exception.Message);
        }
    }
}