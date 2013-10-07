using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.Binding;

using NSubstitute;

using NUnit.Framework;

namespace ModGL.UnitTests.Binding
{
    [TestFixture]
    public class WindowsLibraryTest
    {
        [Test]
        public void GetProcedureAddress_Ok()
        {
            // Arrange
            IntPtr theAddress = new IntPtr(100);
            WindowsLibrary lib = new WindowsLibrary((i, s) => theAddress, i => true, new IntPtr(123), "Foo");

            // Act
            var fptr = lib.GetProcedureAddress("Foo");

            // Assert
            Assert.AreEqual(theAddress, fptr);
        }

        [Test]
        public void GetProcedureAddress_Disposed_ThrowsObjectDisposedException()
        {
            // Arrange
            IntPtr theAddress = new IntPtr(100);
            WindowsLibrary lib = new WindowsLibrary((i, s) => theAddress, i => true, new IntPtr(123), "Foo");
            lib.Dispose();

            // Act
            // Assert
            Assert.Throws<ObjectDisposedException>(() => lib.GetProcedureAddress("Foo"));
        }

        [Test]
        public void GetProcedure_NullPointer_ReturnsNull()
        {
            // Arrange
            var lib = new WindowsLibrary((i, s) => IntPtr.Zero, i => true, new IntPtr(123), "Foo");

            // Act
            var result = lib.GetProcedure("Foo", typeof(Func<int, bool>));

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetProcedure_Disposed_ThrowsObjectDisposedException()
        {
            // Arrange
            var lib = new WindowsLibrary((i, s) => IntPtr.Zero, i => true, new IntPtr(123), "Foo");
            lib.Dispose();

            // Act
            // Assert
            Assert.Throws<ObjectDisposedException>(() => lib.GetProcedure("Foo", typeof(Func<int, bool>)));
        }


        public static void FooFunc()
        {
            
        }

        [Test]
        public void GetProcedure_Function_ReturnsDelegate()
        {
            // Arrange
            var lib = new WindowsLibrary((i, s) => System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(new Action(FooFunc)), i => true, new IntPtr(123), "Foo");

            // Act
            var result = lib.GetProcedure("Foo", typeof(Action));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MulticastDelegate>(result);
        }

        [Test]
        public void GetProcedureGeneric_NullPointer_ReturnsNull()
        {
            // Arrange
            var lib = new WindowsLibrary((i, s) => IntPtr.Zero, i => true, new IntPtr(123), "Foo");

            // Act
            var result = lib.GetProcedure<Action>("Foo");

            // Assert
            Assert.IsNull(result);
            
        }

        [Test]
        public void GetProcedureGeneric_Function_ReturnsDelegate()
        {
            // Arrange
            var lib = new WindowsLibrary((i, s) => System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(new Action(FooFunc)), i => true, new IntPtr(123), "Foo");

            // Act
            var result = lib.GetProcedure<Action>("Foo");

            // Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetProcedureGeneric_Disposed_ThrowsObjectDisposedException()
        {
            // Arrange
            var lib = new WindowsLibrary((i, s) => IntPtr.Zero, i => true, new IntPtr(123), "Foo");
            lib.Dispose();

            // Act
            // Assert
            Assert.Throws<ObjectDisposedException>(() => lib.GetProcedure<Action>("Foo"));
        }

    }
}
