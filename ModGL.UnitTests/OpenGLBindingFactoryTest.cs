using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int FooSub(int a, int b)
        {
            return a - b;
        }

        [Test]
        public void CreatesType()
        {
            var extensions = Substitute.For<IExtensionSupport>();
            extensions.GetProcedure(Arg.Any<string>(), Arg.Any<Type>()).Returns(new Func<int, int, int>(FooSub));
            OpenGLBindingFactory factory = new OpenGLBindingFactory();
            var result = factory.CreateBinding<IFoo>(extensions, false);

            Assert.AreEqual(1, result.Sub(2, 1));
        }
    }
}
