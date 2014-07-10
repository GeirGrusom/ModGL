using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModGL.NativeGL;
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

        protected override void Initialize()
        {
            terrain = Terrain.Build(GL, 512, 512, 16, 16);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.CullFace);
            //GL.PolygonMode(MaterialFace.FrontAndBack,  PolygonMode.Line);
        }

        protected override void FrameTick()
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
