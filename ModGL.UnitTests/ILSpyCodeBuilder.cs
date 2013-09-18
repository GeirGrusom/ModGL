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

    public class ILSpyCodeBuilderWithDouble
    {
        private readonly Func<double, double, double> foo;

        public ILSpyCodeBuilderWithDouble()
        {
            foo = SFoo;
        }

        private static double SFoo(double a, double b)
        {
            return a + b;
        }

        public double Foo(double a, double b)
        {
            return foo(a, b);
        }

        public double StaticFooCallser(double a, double b)
        {
            return Proxy.Foo(a, b);
        }
    }

    public class Proxy
    {
        public static double Foo(double a, double b)
        {
            return a + b;
        }
    }

    public class ILSpyCodeBuilder
    {
        private Func<int, long, byte, int> a;
        private Action b;
        
        public void ThrowError(string extension)
        {
            throw new ExtensionNotSupportedException(extension);
        }

        public ILSpyCodeBuilder(IExtensionFoo foo)
        {
            b = (Action)foo.GetProcedure("Abc123", typeof(Action));
        }

        public ILSpyCodeBuilder()
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
            NativeGL.GL.GetError();
            var ret = a(i, j, k);
            NativeGL.GL.HandleOpenGLError();
            return ret;
        }
    }
}
