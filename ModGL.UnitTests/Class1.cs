using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.UnitTests
{

    public interface IExtensionFoo
    {
        Delegate GetProcedure(string procName, Type delegateType);
    }

    public class FooClass
    {
        
    }

    public class Class1
    {
        private Func<int, long, byte, int> a;
        private Action b;
        

        public Class1(IExtensionFoo foo)
        {
            b = (Action)foo.GetProcedure("Abc123", typeof(Action));
        }

        public Class1()
        {

            a = Foo;
        }

        

        private int Foo(int i, long j, byte k)
        {
            return (int)(i + j + k);
        }
        public void CallTest()
        {
            int result = DelegateCallTest(1, 2, 3);
        }

        public interface IFoo
        {
            
        }

        public void FactoryTest()
        {
            
        }

        public int DelegateCallTest(int i, long j, byte k)
        {
            NativeGL.GL.glGetError();
            var ret = a(i, j, k);
            NativeGL.GL.HandleOpenGLError();
            return ret;
        }
    }
}
