using System;
using System.Collections.Generic;
using System.Reflection;

using ModGL.Binding;
using ModGL.NativeGL;

using NSubstitute;

using NUnit.Framework;

namespace ModGL.UnitTests
{
    [TestFixture]
    public class OpenGLBindingFactoryTest
    {
        public interface IFoo
        {
            int Sub(int a, int b);
        }

        public interface IFoo2 : IFoo
        {
            int Div(int a, int b);
        }


        public static int FooSub(int a, int b)
        {
            return a - b;
        }

        [Test]
        public void CreatesType_InvocesCorrectFunction()
        {
            // Arrange
            var extensions = Substitute.For<IExtensionSupport>();

            extensions.GetProcedure(Arg.Any<string>(), Arg.Any<Type>())
                .Returns(x => Delegate.CreateDelegate
                    (
                        (Type)x.Args()[1], 
                        GetType().GetMethod("FooSub", new[] { typeof(int), typeof(int) }))
                    );
            var factory = new InterfaceBindingFactory();

            // Act
            var result = factory.CreateBinding<IFoo>(extensions);

            // Assert
            Assert.AreEqual(1, result.Sub(2, 1));
            extensions.Received(2).GetProcedure("Sub", Arg.Is<Type>(t => t.IsSubclassOf(typeof(MulticastDelegate)) && t.Name == "SubProc"));
        }

        public static void FlushError()
        {
            
        }
        public static void FlushErrorThrows()
        {
            throw new OpenGLInvalidValueException();
        }

        public static void ThrowError()
        {
            throw new OpenGLInvalidEnumException("Foo");
        }

        [Test]
        public void CreatesType_ErrorFlush_Ok()
        {
            // Arrange
            
            var extensions = Substitute.For<IExtensionSupport>();
            var flushMethod = GetType().GetMethod("FlushErrorThrows", BindingFlags.Public | BindingFlags.Static);
            var throwError = GetType().GetMethod("ThrowError", BindingFlags.Public | BindingFlags.Static);

            extensions.GetProcedure(Arg.Any<string>(), Arg.Any<Type>())
                .Returns(x => Delegate.CreateDelegate
                    (
                        (Type)x.Args()[1],
                        GetType().GetMethod("FooSub", new[] { typeof(int), typeof(int) }))
                    );
            var factory = new InterfaceBindingFactory();
            var binding = factory.CreateBinding<IFoo>(extensions, new ErrorHandling { FlushError = flushMethod, CheckErrorState = throwError });

            // Act
            // Assert
            Assert.Throws<OpenGLInvalidValueException>(() => binding.Sub(1, 2));
        }


        [Test]
        public void CreatesType_InheritedInterface_ImplementsBoth()
        {
            // Arrange
            var extensions = Substitute.For<IExtensionSupport>();

            extensions.GetProcedure(Arg.Any<string>(), Arg.Any<Type>())
                .Returns(x => Delegate.CreateDelegate
                    (
                        (Type)x.Args()[1],
                        GetType().GetMethod("FooSub", new[] { typeof(int), typeof(int) }))
                    );
            var factory = new InterfaceBindingFactory();

            // Act
            var result = factory.CreateBinding<IFoo2>(extensions);

            // Assert
            Assert.AreEqual(1, result.Sub(2, 1));
            Assert.AreEqual(1, result.Div(2, 1));
            extensions.Received(2).GetProcedure(Arg.Any<string>(), Arg.Any<Type>());
        }

        public static class ImplementationTest
        {
            public static int Div(int a, int b)
            {
                return a + b;
            }
        }


        [Test]
        public void CreatesType_InheritedInterface_ImplementsStaticClass()
        {
            // Arrange
            var extensions = Substitute.For<IExtensionSupport>();

            extensions.GetProcedure(Arg.Any<string>(), Arg.Any<Type>())
                .Returns(x => Delegate.CreateDelegate
                    (
                        (Type)x.Args()[1],
                        GetType().GetMethod("FooSub", new[] { typeof(int), typeof(int) }))
                    );
            var factory = new InterfaceBindingFactory();

            // Act
            var result = factory.CreateBinding<IFoo2>(extensions, new Dictionary<Type, Type> {{ typeof(IFoo2), typeof(ImplementationTest) }});

            // Assert
            Assert.AreEqual(1, result.Sub(2, 1));
            Assert.AreEqual(3, result.Div(2, 1));
            extensions.Received(2).GetProcedure(Arg.Any<string>(), Arg.Any<Type>());
        }

    }
}
