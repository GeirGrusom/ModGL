using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        private static IWGL CreateMockWgl()
        {
            var wgl = Substitute.For<IWGL>();
            var pfd = new PixelFormatDescriptor();
            wglChoosePixelFormatARB ch = (hdc, list, fList, formats, piFormats, numFormats) => true;
            wglCreateContextAttribsARB createContext = (dc, context, list) => new IntPtr(1);
            wgl.ChoosePixelFormat(Arg.Any<IntPtr>(), ref pfd).ReturnsForAnyArgs(1);
            wgl.SetPixelFormat(Arg.Any<IntPtr>(), Arg.Any<int>(), ref pfd).ReturnsForAnyArgs(true);
            wgl.wglCreateContext(Arg.Any<IntPtr>()).Returns(new IntPtr(1));
            wgl.wglMakeCurrent(Arg.Any<IntPtr>(), Arg.Any<IntPtr>()).Returns(true);
            wgl.wglGetProcAddress("wglChoosePixelFormatARB").Returns(Marshal.GetFunctionPointerForDelegate(ch));
            wgl.wglGetProcAddress("wglCreateContextAttribsARB").Returns(Marshal.GetFunctionPointerForDelegate(createContext));
            return wgl;
        }

        [Test]
        public void Bind_CallsInitialize()
        {
            // Arrange
            var wgl = CreateMockWgl();
            
            var context = new WindowsContext(wgl, null, new ContextCreationParameters {Device = 1, Window = 1});

            // Act
            context.Bind();
            
            // Assert
            wgl.Received(1).wglCreateContext(Arg.Any<IntPtr>());
        }

        [Test]
        public void Bind_DoesNotCallInitialize_IfItIsAlreadyInitialized()
        {
            // Arrange
            var wgl = CreateMockWgl();

            var context = new WindowsContext(wgl, null, new ContextCreationParameters { Device = 1, Window = 1 });

            // Act
            context.Bind();
            wgl.Received(1).wglCreateContext(Arg.Any<IntPtr>());
            context.Bind();

            // Assert
            wgl.Received(1).wglCreateContext(Arg.Any<IntPtr>());
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
