using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

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
        private IContext _context;
        private readonly Form _form;
        private Graphics _hdc;
        private IOpenGL33 _gl;

        private ModGL.Shaders.Program _shader;

        private Matrix44F _worldMatrix;
        private Matrix44F _viewMatrix;
        private Matrix44F _projectionMatrix;
        private Uniform<Matrix44F> _worldUniform;
        private Uniform<Vector3F> _lightUniform;
        private Terrain _terrain;


        public GLProgram(Form form)
        {
            this._form = form;
        }

        public void Dispose()
        {
            if(this._hdc != null)
                this._hdc.Dispose();   
        }


        private Texture2D _texture;
        private Uniform<int> _normalUniform;

        public void Init()
        {
            this._hdc = this._form.CreateGraphics();
            this._context = new WindowsContext(new WGL(), new ContextCreationParameters
            {
                MajorVersion = 3,
                MinorVersion = 3,
                ColorBits = 32,
                DepthBits = 24,
                StencilBits = 8,
                Window = this._form.Handle,
                Device = this._hdc.GetHdc()
            });

            InterfaceBindingFactory binding = new InterfaceBindingFactory();
            this._gl = binding.CreateBinding<IOpenGL33>(this._context as IExtensionSupport, new Dictionary<Type, Type> { {typeof(IOpenGL), typeof(GL)} }, GL.OpenGLErrorFunctions, "gl");
            this._gl.Enable(StateCaps.DepthTest);

            var vs = new System.IO.StreamReader(GetType().Assembly.GetManifestResourceStream("WindowsTest.VertexShader.vs")).ReadToEnd();
            var fs = new System.IO.StreamReader(GetType().Assembly.GetManifestResourceStream("WindowsTest.FragmentShader.fs")).ReadToEnd();

            this._shader = new ModGL.Shaders.Program(this._gl, 
                new IShader[]
                {
                    new VertexShader(this._gl, vs),
                    new FragmentShader(this._gl, fs) 
                });

            this._shader.BindVertexAttributeLocations(Terrain.VertexType.Descriptor);
            this._gl.BindFragDataLocation(this._shader.Handle, 0, "output");

            this._shader.Compile();

            this._worldMatrix = Matrix44F.Identity;
            this._viewMatrix = ModelMatrix.RotateX((float)Math.PI / 4).Translate(new Vector3F(0, -5f, -5f));
            this._projectionMatrix = ProjectionMatrix.RightHandPerspective((float)Math.PI / 2, 1.0f, 20f, 0.1f);

            this._worldUniform = this._shader.GetUniform<ModGL.Math.Binding.MatrixUniform, Matrix44F>("World");
            Uniform<Matrix44F> viewUnifom = this._shader.GetUniform<ModGL.Math.Binding.MatrixUniform, Matrix44F>("View");
            Uniform<Matrix44F> projectionUniform = this._shader.GetUniform<ModGL.Math.Binding.MatrixUniform, Matrix44F>("Projection");
            this._lightUniform = this._shader.GetUniform<ModGL.Math.Binding.Vector3FUniform, Vector3F>("Light");

            this._gl.Enable(StateCaps.CullFace);
            var bitmap = (Bitmap)Image.FromFile("noiseTexture.png");

            var l = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            this._texture = new Texture2D(
                this._gl,
                l.Width,
                l.Height,
                TextureFormat.RGBA,
                TextureInternalFormat.RGBA8,
                TexturePixelType.UnsignedInt_8_8_8_8);

            using (this._texture.Bind())
            {
                this._texture.BufferData(l.Scan0);
            }

            
            bitmap.UnlockBits(l);
            bitmap.Dispose();
            this._normalUniform = this._shader.GetUniform<IntUniform, int>("NormalMap");
            using (this._shader.Bind())
            {
                this._lightUniform.Value = new Vector3F(1, 1, 1);
                this._worldUniform.Value = this._worldMatrix;
                viewUnifom.Value = this._viewMatrix;
                projectionUniform.Value = this._projectionMatrix;
            }

            this._terrain = new Terrain(
                this._gl, 512, 512, (x, y) =>
                {
                    var value = SimplexNoise.Noise.Generate(x / 64.0f, y / 64.0f) + SimplexNoise.Noise.Generate(x / 128, y / 128);
                    return new Vector3F(x / 16 - 16, value, y / 16 - 16);
                });
        }

        private Quaternion _rotation = Quaternion.Identity;

        public void Render()
        {
            var color = Color.DodgerBlue;
            this._gl.ClearColor(color.R, color.G, color.B, color.A);
            this._gl.ClearDepth(1f);
            this._gl.Clear(ClearTarget.Color | ClearTarget.Depth);
            this._gl.Viewport(0, 0, this._form.ClientSize.Width, this._form.ClientSize.Height);

            _rotation = (_rotation * new Quaternion(Vector3F.UnitY, (float)(Math.PI / 180))).Normalize();

            using (this._shader.Bind())
            {
                this._gl.ActiveTexture(ActiveTexture.Texture0);
                this._texture.Bind();
                this._normalUniform.Value = 0;


                this._worldUniform.Value = _rotation.ToMatrix();
                this._terrain.Render();
            }

            this._context.SwapBuffers();
        }
    }
}
