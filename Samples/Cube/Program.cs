using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using ModGL.NativeGL;
using ModGL.ObjectModel.VertexInfo;
using SampleBase;

namespace Cube
{
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct Vertex
    {
        public static readonly IVertexDescriptor<Vertex> Descriptor = VertexDescriptor.Create<Vertex>();

        [FieldOffset(0)]
        public Vector3f Position;
        [FieldOffset(12)]
        public Vector3f Normal;
    }

    public class App : SampleApplication<IOpenGL30>
    {
        private Cube _cube;
        public App() : base("Cube sample")
        {
        }

        protected override void Initialize()
        {
            _cube = new Cube(GL);

        }

        protected override void FrameTick()
        {
            _cube.Model = Model;
            _cube.View = View;
            _cube.Projection = Projection;
            _cube.DiffuseColor = Color.DodgerBlue;
            _cube.Draw();
        }

        public override void Exiting()
        {
            _cube.Dispose();
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
            var app = new App();
            app.Run();
        }
    }
}
