using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModGL.Windows;
using NSubstitute;
using NUnit.Framework;
using Platform.Invoke;

namespace ModGL.UnitTests.Windows
{
    [TestFixture]
    public class ContextBuilderTests
    {
        [Test]
        public void BuildLegacyContext_NullDevice_Fails_WithContextCreationException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            var createParameters = new ContextCreationParameters { Window = 1 };
            var contextBuilder = new ContextBuilder(wgl);

            // Act
            TestDelegate test = () => contextBuilder.BuildLegacyContext(createParameters);

            // Assert
            Assert.That(test, Throws.TypeOf<ContextCreationException>());
        }

        [Test]
        public void BuildLegacyContext_NullWindow_Fails_WithContextCreationException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            var createParameters = new ContextCreationParameters { Device = 1 };
            var contextBuilder = new ContextBuilder(wgl);

            // Act
            TestDelegate test = () => contextBuilder.BuildLegacyContext(createParameters);

            // Assert
            Assert.That(test, Throws.TypeOf<ContextCreationException>());
        }

        [Test]
        public void BuildLegacyContext_CannotChoosePixelFormat_ThrowsPixelFormatException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            var desc = new PixelFormatDescriptor();
            wgl.ChoosePixelFormat(Arg.Any<IntPtr>(), ref desc).ReturnsForAnyArgs(0);
            var builder = new ContextBuilder(wgl);

            // Act
            TestDelegate build = () => builder.BuildLegacyContext(new ContextCreationParameters {Window = 1, Device = 1});

            // Assert
            Assert.That(build, Throws.TypeOf<PixelFormatException>());
        }

        [Test]
        public void BuildLegacyContext_CannotSetPixelFormat_ThrowsPixelFormatException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            var desc = new PixelFormatDescriptor();
            wgl.ChoosePixelFormat(Arg.Any<IntPtr>(), ref desc).ReturnsForAnyArgs(1);
            wgl.SetPixelFormat(Arg.Any<IntPtr>(), Arg.Any<int>(), ref desc).ReturnsForAnyArgs(false);
            var builder = new ContextBuilder(wgl);

            // Act
            TestDelegate build = () => builder.BuildLegacyContext(new ContextCreationParameters { Window = 1, Device = 1 });

            // Assert
            Assert.That(build, Throws.TypeOf<PixelFormatException>());
        }

        [Test]
        public void BuildLegacyContext_CreateContextReturnsNull_ThrowsContextCreationException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            var desc = new PixelFormatDescriptor();
            wgl.ChoosePixelFormat(Arg.Any<IntPtr>(), ref desc).ReturnsForAnyArgs(1);
            wgl.SetPixelFormat(Arg.Any<IntPtr>(), Arg.Any<int>(), ref desc).ReturnsForAnyArgs(true);
            wgl.wglCreateContext(Arg.Any<IntPtr>()).Returns(IntPtr.Zero);
            var builder = new ContextBuilder(wgl);

            // Act
            TestDelegate build = () => builder.BuildLegacyContext(new ContextCreationParameters { Window = 1, Device = 1 });

            // Assert
            Assert.That(build, Throws.TypeOf<ContextCreationException>());
        }

        [Test]
        public void BuildLegacyContext_ValidWindowAndHdc_ReturnsContextHandle()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            var desc = new PixelFormatDescriptor();
            wgl.ChoosePixelFormat(Arg.Any<IntPtr>(), ref desc).ReturnsForAnyArgs(1);
            wgl.SetPixelFormat(Arg.Any<IntPtr>(), Arg.Any<int>(), ref desc).ReturnsForAnyArgs(true);
            wgl.wglCreateContext(Arg.Any<IntPtr>()).Returns((IntPtr)1);
            var builder = new ContextBuilder(wgl);

            // Act
            var result = builder.BuildLegacyContext(new ContextCreationParameters { Window = 1, Device = 1 });

            // Assert
            Assert.That(result, Is.EqualTo((IntPtr)1));
        }

        public void BuildModernContext_CannotBindLegacyContext_ThrowsContextCreationException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            wgl.wglMakeCurrent(Arg.Any<IntPtr>(), Arg.Any<IntPtr>()).Returns(false);
            var lib = Substitute.For<ILibrary>();
            var builder = new ContextBuilder(wgl);

            // Act
            TestDelegate build =
                () => builder.BuildModernContext(new ContextCreationParameters {Window = 1, Device = 1}, lib, null, (IntPtr) 1);

            // Assert
            Assert.That(build, Throws.TypeOf<ContextCreationException>());
        }

        [Test]
        public void BuildModernContext_CannotFindChoosePixelFormatARB_ThrowsMissingEntryPointException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            wgl.wglMakeCurrent(Arg.Any<IntPtr>(), Arg.Any<IntPtr>()).Returns(true);
            var lib = Substitute.For<ILibrary>();
            lib.GetProcedure<wglChoosePixelFormatARB>("wglChoosePixelFormatARB").Returns((wglChoosePixelFormatARB)null);
            lib.GetProcedure<wglCreateContextAttribsARB>("wglCreateContextAttribsARB").Returns((wglCreateContextAttribsARB)null);
            var builder = new ContextBuilder(wgl);

            // Act
            TestDelegate build =
                () => builder.BuildModernContext(new ContextCreationParameters { Window = 1, Device = 1 }, lib, null, (IntPtr)1);

            // Assert
            Assert.That(build, Throws.TypeOf<MissingEntryPointException>());
        }

        [Test]
        public void BuildModernContext_CannotFindCreateContextARB_ThrowsMissingEntryPointException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            wgl.wglMakeCurrent(Arg.Any<IntPtr>(), Arg.Any<IntPtr>()).Returns(true);
            var lib = Substitute.For<ILibrary>();
            wglChoosePixelFormatARB choosePixelFormat = (hdc, list, fList, formats, piFormats, numFormats) => true;
            lib.GetProcedure<wglChoosePixelFormatARB>("wglChoosePixelFormatARB").Returns(choosePixelFormat);
            lib.GetProcedure<wglCreateContextAttribsARB>("wglCreateContextAttribsARB").Returns((wglCreateContextAttribsARB)null);
            var builder = new ContextBuilder(wgl);

            // Act
            TestDelegate build =
                () => builder.BuildModernContext(new ContextCreationParameters { Window = 1, Device = 1 }, lib, null, (IntPtr)1);

            // Assert
            Assert.That(build, Throws.TypeOf<MissingEntryPointException>());
        }

        [Test]
        public void BuildModernContext_ChoosePixelFormat_Fails_ThrowsContextCreationException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            wgl.wglMakeCurrent(Arg.Any<IntPtr>(), Arg.Any<IntPtr>()).Returns(false);
            var lib = Substitute.For<ILibrary>();
            wglChoosePixelFormatARB choosePixelFormat = (hdc, list, fList, formats, piFormats, numFormats) => false;
            wglCreateContextAttribsARB createContextAttribs = (dc, context, list) => (IntPtr) 0;
            lib.GetProcedure<wglChoosePixelFormatARB>("wglChoosePixelFormatARB").Returns(choosePixelFormat);
            lib.GetProcedure<wglCreateContextAttribsARB>("wglCreateContextAttribsARB").Returns(createContextAttribs);
            var builder = new ContextBuilder(wgl);

            // Act
            TestDelegate build =
                () => builder.BuildModernContext(new ContextCreationParameters { Window = 1, Device = 1 }, lib, null, (IntPtr)1);

            // Assert
            Assert.That(build, Throws.TypeOf<ContextCreationException>());
        }

        [Test]
        public void BuildModernContext_CreateContextAttribsReturnsNull_ThrowsContextCreationException()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            wgl.wglMakeCurrent(Arg.Any<IntPtr>(), Arg.Any<IntPtr>()).Returns(true);
            var lib = Substitute.For<ILibrary>();
            wglChoosePixelFormatARB choosePixelFormat = (hdc, list, fList, formats, piFormats, numFormats) => true;
            wglCreateContextAttribsARB createContextAttribs = (dc, context, list) => IntPtr.Zero;
            lib.GetProcedure<wglChoosePixelFormatARB>("wglChoosePixelFormatARB").Returns(choosePixelFormat);
            lib.GetProcedure<wglCreateContextAttribsARB>("wglCreateContextAttribsARB").Returns(createContextAttribs);
            var builder = new ContextBuilder(wgl);

            // Act
            TestDelegate build =
                () => builder.BuildModernContext(new ContextCreationParameters { Window = 1, Device = 1 }, lib, null, (IntPtr)1);

            // Assert
            Assert.That(build, Throws.TypeOf<ContextCreationException>());
        }

        [Test]
        public void BuildModernContext_ValidArguments_ReturnsContextHandle()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            wgl.wglMakeCurrent(Arg.Any<IntPtr>(), Arg.Any<IntPtr>()).Returns(true);
            var lib = Substitute.For<ILibrary>();
            wglChoosePixelFormatARB choosePixelFormat = (hdc, list, fList, formats, piFormats, numFormats) => true;
            wglCreateContextAttribsARB createContextAttribs = (dc, context, list) => (IntPtr)1;
            lib.GetProcedure<wglChoosePixelFormatARB>("wglChoosePixelFormatARB").Returns(choosePixelFormat);
            lib.GetProcedure<wglCreateContextAttribsARB>("wglCreateContextAttribsARB").Returns(createContextAttribs);
            var builder = new ContextBuilder(wgl);

            // Act
            var result = builder.BuildModernContext(new ContextCreationParameters { Window = 1, Device = 1 }, lib, null, (IntPtr)1);

            // Assert
            Assert.That(result, Is.Not.EqualTo(IntPtr.Zero));
        }
    }
}
