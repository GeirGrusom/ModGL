using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModGL.Windows;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ModGL.UnitTests.Windows
{
    [TestFixture]
    public class WindowsContextTests
    {
        [Test]
        public void Constructor_WglIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            TestDelegate createAction = () => new WindowsContext(null, null, new ContextCreationParameters());
            
            // Assert
            Assert.That(createAction, Throws.InstanceOf<ArgumentNullException>());
        }
        
        [Test]
        public void Constructor_ContextCreationParametersIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            TestDelegate createAction = () => new WindowsContext(Substitute.For<IWGL>(), null, null);

            // Assert
            Assert.That(createAction, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void Constructor_OpenGLVersionLessThan3_ThrowsVersionNotSupportedException()
        {
            // Arrange
            TestDelegate createAction = () => new WindowsContext(Substitute.For<IWGL>(), null, new ContextCreationParameters { Device = 1, Window = 1, MajorVersion = 2, MinorVersion = 0});

            // Assert
            Assert.That(createAction, Throws.InstanceOf<VersionNotSupportedException>());
        }

        [Test]
        public void Constructor_DeviceNotSet_ThrowsContextCreationException()
        {
            // Arrange
            TestDelegate createAction = () => new WindowsContext(Substitute.For<IWGL>(), null, new ContextCreationParameters { Device = 0, Window = 1 });

            // Assert
            Assert.That(createAction, Throws.InstanceOf<ContextCreationException>());
        }

        [Test]
        public void Constructor_WindowNotSet_ThrowsContextCreationException()
        {
            // Arrange
            TestDelegate createAction = () => new WindowsContext(Substitute.For<IWGL>(), null, new ContextCreationParameters { Device = 1, Window = 0 });

            // Assert
            Assert.That(createAction, Throws.InstanceOf<ContextCreationException>());
        }

    }
}
