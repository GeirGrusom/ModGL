using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

using ModGL;
using ModGL.Math;
using ModGL.NativeGL;
using ModGL.Shaders;
using ModGL.Textures;

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

        public void Init()
        {
            this._hdc = this._form.CreateGraphics();
            var creationParameters = new ContextCreationParameters
            {
                MajorVersion = 3,
                MinorVersion = 3,
                ColorBits = 32,
                DepthBits = 24,
                StencilBits = 8,
                Window = (long)_form.Handle,
                Device = (long)_hdc.GetHdc()
            };

            var factory = new InterfaceFactory();
            
            _gl = factory.CreateInterface<IOpenGL33>(creationParameters, new ContextFactory(), new LibraryLoaderFactory(), true, out _context);
            _gl.Enable(StateCaps.DepthTest);
            _gl.Enable(StateCaps.CullFace);

            var vs = new System.IO.StreamReader(GetType().Assembly.GetManifestResourceStream("WindowsTest.VertexShader.vs")).ReadToEnd();
            var fs = new System.IO.StreamReader(GetType().Assembly.GetManifestResourceStream("WindowsTest.FragmentShader.fs")).ReadToEnd();

            this._shader = new ModGL.Shaders.Program(this._gl, 
                new IShader[]
                {
                    new VertexShader(this._gl, vs),
                    new FragmentShader(this._gl, fs) 
                });

            this._shader.BindVertexAttributeLocations(Terrain.VertexType.Descriptor);
            this._shader.BindVertexAttributeLocations(Terrain.Tangents.Descriptor, Terrain.VertexType.Descriptor.Elements.Count());
            this._gl.BindFragDataLocation(this._shader.Handle, 0, "output");

            this._shader.Compile();

            this._worldUniform = this._shader.GetUniform<ModGL.Math.Binding.MatrixUniform, Matrix44F>("World");
            Uniform<Matrix44F> viewUnifom = this._shader.GetUniform<ModGL.Math.Binding.MatrixUniform, Matrix44F>("View");
            Uniform<Matrix44F> projectionUniform = this._shader.GetUniform<ModGL.Math.Binding.MatrixUniform, Matrix44F>("Projection");
            this._lightUniform = this._shader.GetUniform<ModGL.Math.Binding.Vector3FUniform, Vector3F>("Light");

            _shader.Bind();


            _worldUniform.Value = Matrix44F.Identity;
            viewUnifom.Value = ViewMatrix.LookAt(new Vector3F(10, 10, 10), Vector3F.UnitY, new Vector3F());
            projectionUniform.Value = ProjectionMatrix.RightHandPerspective((float)Math.PI / 1.8f, 1.0f, 64f, 0.1f);
            _lightUniform.Value = new Vector3F(5, -10, 20);

            var bmp = (Bitmap)Image.FromFile("noiseTexture.png");
            var l = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var normalMap = new Texture2D(
                _gl,
                l.Width,
                l.Height,
                TextureFormat.RGBA,
                TextureInternalFormat.RGBA8,
                TexturePixelType.UnsignedInt_8_8_8_8);
            
            using (normalMap.Bind())
            {
                _gl.TexParameteri(TextureTarget.Texture2D, TexParameterName.TextureWrapR, 0x2901);
                _gl.TexParameteri(TextureTarget.Texture2D, TexParameterName.TextureWrapS, 0x2901);
                _gl.TexParameteri(TextureTarget.Texture2D, TexParameterName.TextureWrapT, 0x2901);
                normalMap.BufferData(l.Scan0);
                _gl.GenerateMipmap((uint)TextureTarget.Texture2D);
            }
            bmp.UnlockBits(l);
            bmp.Dispose();


            var version = _gl.GetString(GetStringNames.Version);
            var renderer = _gl.GetString(GetStringNames.Renderer);
            var vendor = _gl.GetString(GetStringNames.Vendor);
            var shaderVersion = _gl.GetString(GetStringNames.ShadingLanguageVersion);
            _form.Text = string.Format("OpenGL Test - {0} - {1} - {2} - Shading language version {3}", version, renderer, vendor, shaderVersion);

            

            _gl.ActiveTexture(ActiveTexture.Texture1);
            normalMap.Bind();
            var nm = _shader.GetUniform<IntUniform, int>("NormalMap");
            nm.Value = 1;

            this._terrain = new Terrain(
                this._gl, 512, 512, (x, y) =>
                {
                    var value = SimplexNoise.Noise.Generate(x / 64.0f, y / 64.0f) + SimplexNoise.Noise.Generate(x / 128, y / 128);
                    return new Vector3F(x / 4 - 64, value, y / 4 - 64);
                });

            var color = Color.DodgerBlue;
            this._gl.ClearColor(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
            this._gl.ClearDepth(1f);
            _gl.Clear(ClearTarget.All);
            
        }

        private Quaternion _rotation = Quaternion.Identity;

        public void Render()
        {
            this._gl.Clear(ClearTarget.Depth | ClearTarget.Color);
            this._gl.Viewport(0, 0, this._form.ClientSize.Width, this._form.ClientSize.Height);

            _rotation = (_rotation * new Quaternion(Vector3F.UnitY, (float)(Math.PI / 180))).Normalize();

            this._worldUniform.Value = _rotation.ToMatrix();
            this._terrain.Render();

            this._context.SwapBuffers();
        }
    }
}
