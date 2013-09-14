using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModGL;
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
            var exception = Assert.Catch<VersionNotSupportedException>( () => new WindowsContext(wgl, new ContextCreationParameters { MajorVersion = 2 }));

            // Assert
            Assert.AreEqual("OpenGL version below 3.0 is not supported.", exception.Message);
        }

        [Test, Ignore]
        public void Context_Ok()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            PixelFormatDescriptor pfd = new PixelFormatDescriptor();
            wgl.wglGetProcAddress<wglChoosePixelFormatARB>(Arg.Any<string>()).Returns(
                callInfo => (hdc, piAttribIList, pfAttribFList, nMaxFormats, piFormats, nNumFormats) => true);
            wgl.ChoosePixelFormat(new IntPtr(1), ref pfd).Returns(1);
            wgl.SetPixelFormat(Arg.Any<IntPtr>(), Arg.Any<int>(), ref pfd).Returns(true);
            
            
            var createParams = new ContextCreationParameters
            {
                ColorBits = 32,
                DepthBits = 24,
                Device = new IntPtr(1),
                Window = new IntPtr(1),
                MajorVersion = 3,
                MinorVersion = 0
            };

            // Act
            var context = new WindowsContext(wgl, createParams);
        }
    }
}