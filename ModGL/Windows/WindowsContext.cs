using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Windows
{
    public class WindowsContext : Context
    {
        private readonly IWGL _wgl;

        public WindowsContext(IWGL wgl)
        {
            _wgl = wgl;
        }

        public override void MakeCurrent()
        {
            throw new NotImplementedException();
        }

        public override TDelegate GetExtension<TDelegate>(string extensionName)
        {
            var deleg = _wgl.wglGetProcAddress(extensionName);
            return (TDelegate)Convert.ChangeType(deleg, typeof (TDelegate));
        }
    }
}
