using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModGL;
using ModGL.Windows;
using NSubstitute;

namespace ModGL.UnitTests
{
    public class WindowsContextTest
    {
        public void CreateContext()
        {
            var wgl = Substitute.For<IWGL>();
            var context = new Windows.WindowsContext(wgl, new ContextCreationParameters {MajorVersion = 2});
        }
    }
}
