using System;
using ModGL.Windows;
using NSubstitute;
using NUnit.Framework;
using Platform.Invoke;

namespace ModGL.UnitTests
{
    [TestFixture]
    public class ContextFactoryTests
    {
        [Test]
        public void Create_OsIsWindows_ReturnsWindowsContext()
        {
            // Arrange
            var loader = Substitute.For<ILibraryLoader>();
            var libmapper = Substitute.For<ILibraryInterfaceMapper>();
            var factory = new ContextFactory(loader, libmapper, PlatformID.Win32NT);
        
            // Act
            var context = factory.Create(new ContextCreationParameters{ Device = 1, Window = 1});

            // Assert
            Assert.That(context, Is.InstanceOf<WindowsContext>());
            loader.Received(1).Load("GDI32");
            loader.Received(1).Load("OpenGL32");
        }
    }
}
