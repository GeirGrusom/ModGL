using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.Binding;
using ModGL.Windows;

using NUnit.Framework;
using NSubstitute;

namespace ModGL.UnitTests.Binding
{
    [TestFixture]
    public class WindowLibraryLoaderTest
    {
        [Test]
        public void LoadLibrary()
        {
            WindowsLibraryLoader loader = new WindowsLibraryLoader(f => new IntPtr(100));

            var result = loader.Load("Foo");

            Assert.AreEqual("Foo", result.Name);
            Assert.IsNotNull(result);
        }

        [Test]
        public void LoadLibrary_NotFound_ThrowsFileNotFoundException()
        {
            WindowsLibraryLoader loader = new WindowsLibraryLoader(f => IntPtr.Zero);

            var result = Assert.Throws<FileNotFoundException>(() => loader.Load("Foo"));
        }

    }
}
