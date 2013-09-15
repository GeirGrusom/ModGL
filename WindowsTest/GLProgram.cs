using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ModGL;
using ModGL.Binding;
using ModGL.NativeGL;
using ModGL.Rendering;
using ModGL.Shaders;
using ModGL.VertexInfo;
using ModGL.Windows;

using DrawMode = ModGL.NativeGL.DrawMode;

namespace WindowsTest
{
    public class GLProgram : IDisposable
    {
        private IContext context;
        private readonly Form form;
        private Graphics hdc;
        private IOpenGL30 gl;

        private VertexArray array;
        private VertexBuffer<Vertex> buffer;
        private ModGL.Shaders.Program shader;

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
            gl.glEnable(StateCaps.DepthTest);

            
            buffer = new VertexBuffer<Vertex>(new []
            {
                new Vertex
                {
                    Position = new Vec4f { x = 0, y = 0, z = 0, w = 1},
                    Color = new Vec4f { x = 1.0f, w = 1.0f }
                },
                new Vertex
                {
                    Position = new Vec4f { x = 2, y = 2, z = 0, w = 1},
                    Color = new Vec4f {y = 1.0f, w = 1.0f }
                },                
                new Vertex
                {
                    Position = new Vec4f { x = 1, y = -1, z = 0, w = 1},
                    Color = new Vec4f { z = 1.0f, w = 1.0f }
                }
            }, gl);
            using (buffer.Bind())
            {
                buffer.BufferData(BufferUsage.StaticDraw);
                buffer.ReleaseClientData();
            }
            var desc = VertexDescriptor.Create<Vertex>();
            array = new VertexArray(gl, new [] { buffer }, new [] { desc });

            var vs = new System.IO.StreamReader(GetType().Assembly.GetManifestResourceStream("WindowsTest.VertexShader.vs")).ReadToEnd();
            var fs = new System.IO.StreamReader(GetType().Assembly.GetManifestResourceStream("WindowsTest.FragmentShader.fs")).ReadToEnd();

            shader = new ModGL.Shaders.Program(gl, 
                new IShader[]
                {
                    new VertexShader(gl, vs),
                    new FragmentShader(gl, fs) 
                });

            shader.BindVertexAttributeLocations(desc);
            gl.glBindFragDataLocation(shader.Handle, 0, "output");

            shader.Compile();
        }

        [ModGL.VertexInfo.VertexElement(DataType.Float, 4)]
        public struct Vec4f
        {
            public float x, y, z, w;
        }

        [ModGL.VertexInfo.VertexElement(DataType.Float, 3)]
        public struct Vec3f
        {
            public float x, y, z, w;
        }

        public struct Vertex
        {
            public Vec4f Position;
            public Vec4f Color;
        }

        public void Render()
        {
            gl.glClearColor(0f, 0f, 0f, 1f);
            gl.glClearDepth(1f);
            gl.glClear(ClearTarget.Color | ClearTarget.Depth);

            using (shader.Bind())
            using (array.Bind())
            {
                var render = new Renderer(gl);
                render.Draw(DrawMode.LineLoop, buffer);
            }

            context.SwapBuffers();
        }
    }
}
