using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModGL.Windows;
using NSubstitute;

using NUnit.Framework;
using Platform.Invoke;

namespace ModGL.UnitTests
{
    [TestFixture]
    public class WindowsContextTest
    {
        private static readonly Random rnd = new Random();
        public static IEnumerable<char> RandomChar()
        {
            while (true)
                yield return (char)rnd.Next('a', 'z');
        }

        string CreateJumbledString(string input)
        {
            return CreateJumbledString(input.Count(c => c == ' '));
        }

        string CreateJumbledString(IEnumerable<int> lengths)
        {
            return string.Join(" ", lengths.Select(CreateJumbledString));
        }

        string CreateJumbledString(int length)
        {
            return new string(RandomChar().Take(length).ToArray());
        }

        [Test]
        public void TestString()
        {
            var jumbledString = CreateJumbledString(4);
        }

        [Test]
        public void CreateContext_OlderThan_30_NotSupported()
        {
            // Arrange
            var wgl = Substitute.For<IWGL>();
            
            // Act
            var exception = Assert.Catch<VersionNotSupportedException>( () => new WindowsContext(wgl, Substitute.For<ILibrary>(), null, new ContextCreationParameters { MajorVersion = 2 }));

            // Assert
            Assert.AreEqual("OpenGL version below 3.0 is not supported.", exception.Message);
        }
    }
}