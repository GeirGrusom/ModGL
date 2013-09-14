using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ModGL;
using ModGL.Binding;
using ModGL.NativeGL;
using ModGL.Windows;

namespace WindowsTest
{
    public class GLProgram : IDisposable
    {
        private IContext context;
        private readonly Form form;
        private Graphics hdc;
        private IOpenGL30 gl;

        public interface IClearInterface
        {
            void glClear(ClearTarget target);
        }

        public GLProgram(Form form)
        {
            this.form = form;
        }

        public void Dispose()
        {
            if(hdc != null)
                hdc.Dispose();   
        }
        public void Init()
        {
            hdc = form.CreateGraphics();
            context = new WindowsContext(new WGL(), new ContextCreationParameters
            {
                MajorVersion = 3,
                MinorVersion = 0,
                Revision = 0,
                ColorBits = 32,
                DepthBits = 24,
                StencilBits = 8,
                Window = form.Handle,
                Device = hdc.GetHdc()
            });

            InterfaceBindingFactory binding = new InterfaceBindingFactory();
            gl = binding.CreateBinding<IOpenGL30>(context as IExtensionSupport, new Dictionary<Type, Type> { {typeof(IOpenGL), typeof(GL)} }, GL.OpenGLErrorFunctions);
        }
        public void Render()
        {
            //gl.glClearColor(0f, 0f, 0f, 1f);
            //gl.glClearDepth(0f);
            gl.glClear(ClearTarget.Color | ClearTarget.Depth);
            context.SwapBuffers();
        }
    }
}
