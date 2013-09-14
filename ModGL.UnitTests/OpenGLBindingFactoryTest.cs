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

        public interface IFooVoid
        {
            void Sub(int a, int b);
        }


        public interface IFoo2 : IFoo
        {
            int Div(int a, int b);
        }


        public static int FooSub(int a, int b)
        {
            return a - b;
        }

        public static void FooVoid(int a, int b)
        {
            
        }


        [Test]
        public void ExensionSupportIsNull_ThrowsArgumentNullException()
        {
            var factory = new InterfaceBindingFactory();

            Assert.Throws<ArgumentNullException>(() => factory.CreateBinding<IFoo>(null));
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
            extensions.Received(1).GetProcedure("Sub", Arg.Is<Type>(t => t.IsSubclassOf(typeof(MulticastDelegate)) && t.Name == "SubProc"));
        }

        [Test]
        public void CreatesType_InvocesCorrectFunction_ReturnTypeVoid_Ok()
        {
            // Arrange
            var extensions = Substitute.For<IExtensionSupport>();

            extensions.GetProcedure(Arg.Any<string>(), Arg.Any<Type>())
                .Returns(x => Delegate.CreateDelegate
                    (
                        (Type)x.Args()[1],
                        GetType().GetMethod("FooVoid", new[] { typeof(int), typeof(int) }))
                    );
            var factory = new InterfaceBindingFactory();
            var result = factory.CreateBinding<IFooVoid>(extensions);

            // Act
            result.Sub(1, 2);

            // Assert
            extensions.Received(1).GetProcedure("Sub", Arg.Is<Type>(t => t.IsSubclassOf(typeof(MulticastDelegate)) && t.Name == "SubProc"));
        }


        [Test]
        public void InterfaceMap_NotInterfaces_ThrowsInvalidOperation()
        {
            // Arrange
            var extensions = Substitute.For<IExtensionSupport>();

            var factory = new InterfaceBindingFactory();

            // Act
            var exception = Assert.Catch<InvalidOperationException>(() => factory.CreateBinding<IFoo>(extensions, new Dictionary<Type, Type> { { typeof(OpenGLBindingFactoryTest), typeof(OpenGLBindingFactoryTest) }}));

            // Assert
            Assert.AreEqual("Interface map must map interfaces : OpenGLBindingFactoryTest", exception.Message);
        }

        [Test]
        public void InterfaceMap_MapInterfaceToInterface_ThrowsInvalidOperation()
        {
            // Arrange
            var extensions = Substitute.For<IExtensionSupport>();

            var factory = new InterfaceBindingFactory();

            // Act
            var exception = Assert.Catch<InvalidOperationException>(() => factory.CreateBinding<IFoo>(extensions, new Dictionary<Type, Type> { { typeof(IFoo), typeof(IFoo) } }));

            // Assert
            Assert.AreEqual("Interface map must map to classes with static members: IFoo", exception.Message);
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
        public void UnsupportedExtensionThrows()
        {
            // Arrange
            var extensions = Substitute.For<IExtensionSupport>();
            extensions.GetProcedure<Action>(Arg.Any<string>()).Returns((Action)null);
            var factory = new InterfaceBindingFactory();

            // Act
            var exception = Assert.Catch<ExtensionNotSupportedException>(() => factory.CreateBinding<IFoo>(extensions));

            // Assert
            Assert.AreEqual("Extension not suported by the current context: 'Sub'", exception.Message);


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
            extensions.Received(1).GetProcedure("Sub", Arg.Any<Type>());
            extensions.Received(1).GetProcedure("Div", Arg.Any<Type>());
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
