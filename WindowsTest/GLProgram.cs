using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using LibNoise;

using ModGL;
using ModGL.Binding;
using ModGL.Math;
using ModGL.NativeGL;
using ModGL.Shaders;
using ModGL.Textures;
using ModGL.Windows;

using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace WindowsTest
{
    public class GLProgram : IDisposable
    {
        private IContext context;
        private readonly Form form;
        private Graphics hdc;
        private IOpenGL33 gl;

        private ModGL.Shaders.Program shader;

        private Matrix44F worldMatrix;
        private Matrix44F viewMatrix;
        private Matrix44F projectionMatrix;
        private Uniform<Matrix44F> worldUniform;
        private Uniform<Vector3F> lightUniform;
        private Terrain terrain;


        public GLProgram(Form form)
        {
            this.form = form;
        }

        public void Dispose()
        {
            if(hdc != null)
                hdc.Dispose();   
        }


        private Texture2D texture;
        private Uniform<int> normalUniform;

        public void Init()
        {
            hdc = form.CreateGraphics();
            context = new WindowsContext(new WGL(), new ContextCreationParameters
            {
                MajorVersion = 3,
                MinorVersion = 3,
                ColorBits = 32,
                DepthBits = 24,
                StencilBits = 8,
                Window = form.Handle,
                Device = hdc.GetHdc()
            });

            InterfaceBindingFactory binding = new InterfaceBindingFactory();
            gl = binding.CreateBinding<IOpenGL33>(context as IExtensionSupport, new Dictionary<Type, Type> { {typeof(IOpenGL), typeof(GL)} }, GL.OpenGLErrorFunctions, "gl");
            gl.Enable(StateCaps.DepthTest);

            var vs = new System.IO.StreamReader(GetType().Assembly.GetManifestResourceStream("WindowsTest.VertexShader.vs")).ReadToEnd();
            var fs = new System.IO.StreamReader(GetType().Assembly.GetManifestResourceStream("WindowsTest.FragmentShader.fs")).ReadToEnd();

            shader = new ModGL.Shaders.Program(gl, 
                new IShader[]
                {
                    new VertexShader(gl, vs),
                    new FragmentShader(gl, fs) 
                });

            shader.BindVertexAttributeLocations(Terrain.VertexType.Descriptor);
            gl.BindFragDataLocation(shader.Handle, 0, "output");

            shader.Compile();

            worldMatrix = Matrix44F.Identity;
            viewMatrix = ModelMatrix.RotateX((float)Math.PI / 4).Translate(new Vector3F(0, -5f, -5f));
            projectionMatrix = ProjectionMatrix.RightHandPerspective((float)Math.PI / 2, 1.0f, 20f, 0.1f);

            worldUniform = shader.GetUniform<ModGL.Math.Binding.MatrixUniform, Matrix44F>("World");
            Uniform<Matrix44F> viewUnifom = shader.GetUniform<ModGL.Math.Binding.MatrixUniform, Matrix44F>("View");
            Uniform<Matrix44F> projectionUniform = shader.GetUniform<ModGL.Math.Binding.MatrixUniform, Matrix44F>("Projection");
            lightUniform = shader.GetUniform<ModGL.Math.Binding.Vector3FUniform, Vector3F>("Light");

            gl.Enable(StateCaps.CullFace);
            var bitmap = (Bitmap)Image.FromFile("noiseTexture.png");

            var l = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            texture = new Texture2D(
                gl,
                l.Width,
                l.Height,
                TextureFormat.RGBA,
                TextureInternalFormat.RGBA8,
                TexturePixelType.UnsignedInt_8_8_8_8);

            using (texture.Bind())
            {
                texture.BufferData(l.Scan0);
            }

            
            bitmap.UnlockBits(l);
            bitmap.Dispose();
            normalUniform = shader.GetUniform<IntUniform, int>("NormalMap");
            using (shader.Bind())
            {
                lightUniform.Value = new Vector3F(1, 1, 1);
                worldUniform.Value = worldMatrix;
                viewUnifom.Value = viewMatrix;
                projectionUniform.Value = projectionMatrix;
            }

            var perlin = new LibNoise.Primitive.BevinsGradient(1024, NoiseQuality.Best);

            terrain = new Terrain(
                gl, 512, 512, (x, y) =>
                {
                    //var value = SimplexNoise.Noise.Generate(x / 64.0f, y / 64.0f) + SimplexNoise.Noise.Generate(x / 128, y / 128);

                    var value = perlin.GetValue(x / 32.0f, y / 32.0f, 0);
                    return new Vector3F(x / 16 - 16, value, y / 16 - 16);
                });
        }

        private Quaternion _rotation = Quaternion.Identity;

        public void Render()
        {
            var color = Color.DodgerBlue;
            gl.ClearColor(color.R, color.G, color.B, color.A);
            gl.ClearDepth(1f);
            gl.Clear(ClearTarget.Color | ClearTarget.Depth);
            gl.Viewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);

            _rotation = (_rotation * new Quaternion(Vector3F.UnitY, (float)(Math.PI / 180))).Normalize();

            using (shader.Bind())
            {
                gl.ActiveTexture(ActiveTexture.Texture0);
                texture.Bind();
                normalUniform.Value = 0;


                worldUniform.Value = _rotation.ToMatrix();
                terrain.Render();
            }

            context.SwapBuffers();
        }
    }
}
