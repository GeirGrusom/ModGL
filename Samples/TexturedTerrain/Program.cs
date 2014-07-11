using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModGL.NativeGL;
using ModGL.Numerics;
using ModGL.ObjectModel.Textures;
using SampleBase;

namespace TexturedTerrain
{
    public class TerrainApp : SampleApplication<IOpenGL33>
    {
        public TerrainApp() 
            : base("Textured Terrain")
        {
        }

        private Terrain terrain;

        private static Stream GetEmbeddedResourceAsStream(string name)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("TexturedTerrain.Resources." + name);
        }

        protected override void Initialize()
        {
            terrain = Terrain.Build(GL, 512, 512, 50, 50);
            terrain.Sampler = new Sampler(GL)
            {
                Magnification = MagnificationInterpolationMode.Linear,
                Minification = MinificationInterpolationMode.LinearMipmapLinear
            };
            //GL.PolygonMode(MaterialFace.FrontAndBack,  PolygonMode.Line);
            Bitmap img;
            using (var stream = GetEmbeddedResourceAsStream("ground.jpg"))
            {
                img = (Bitmap)Image.FromStream(stream);
            }

            var tex = new Texture2D(GL, img.Width, img.Height, TextureFormat.RGB, TextureInternalFormat.RGB8, TexturePixelType.UnsignedByte);
            var imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, img.PixelFormat);
            using (tex.Bind())
            {
                tex.BufferData(imgData.Scan0);
                GL.GenerateMipmap(Constants.Texture2d);
            }

            img.UnlockBits(imgData);
            img.Dispose();
            terrain.Texture = tex;
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.CullFace);
            //GL.PolygonMode(MaterialFace.FrontAndBack,  PolygonMode.Line);
        }

        protected override void RotateModel(float dt)
        {
            Model = Model.RotateY(dt * 0.5f);
        }

        protected override void FrameTick(float dt)
        {
            
            terrain.Model = Model;
            terrain.View = View;
            terrain.Projection = Projection;
            terrain.Draw();
        }

        public override void Exiting()
        {
            terrain.Dispose();
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var app = new TerrainApp();
            app.Run();
        }
    }
}
