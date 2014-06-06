﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ModGL;
using ModGL.NativeGL;
using ModGL.Numerics;
using ModGL.VertexInfo;

namespace Cube
{
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct Vertex
    {
        public static readonly VertexDescriptor Descriptor = VertexDescriptor.Create<Vertex>();

        [FieldOffset(0)]
        public Vector3f Position;
        [FieldOffset(12)]
        public Vector3f Normal;
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool appRunning = true;

            // Create out render form
            var renderForm = new Form {Text = "Cube Sample"};
            renderForm.Closing += (sender, e) => appRunning = false;
            renderForm.ClientSize = new Size(800, 600);
            renderForm.Show();

            // We need a Graphics object to render to
            var graphics = renderForm.CreateGraphics();
            var hdc = graphics.GetHdc();

            // Create the OpenGL rendering context
            var context =
                ContextFactory.Instance.Create(new ContextCreationParameters
                {
                    Device = (long) hdc,
                    Window = (long) renderForm.Handle,
                });

            
            using (context.Bind())
            {
                

                var model = Matrix4f.Identity;
                var view = ViewMatrixHelper.LookAt(new Vector3f(1.75f, 1.75f, 1.75f), new Vector3f(0, 1, 0), new Vector3f(0, 0, 0));
                var projection = ProjectionMatrixHelper.RightHandPerspective((float) Math.PI/2,
                    renderForm.ClientSize.Width/(float) renderForm.ClientSize.Height, 0.01f, 10f);
                var gl = context.CreateInterface<IOpenGL30>(InterfaceFlags.Debug);
                var cube = new Cube(gl);
                var bgColor = Color.DodgerBlue;

                renderForm.Resize +=
                    (sender, args) =>
                    {
                        gl.Viewport(0, 0, renderForm.ClientSize.Width, renderForm.ClientSize.Height);
                        projection = ProjectionMatrixHelper.RightHandPerspective((float) Math.PI/2,
                            renderForm.ClientSize.Width/(float) renderForm.ClientSize.Height, 0.01f, 10f);
                    };

                gl.ClearColor(bgColor.R / 255f, bgColor.G / 255f, bgColor.B / 255f, 1);
                gl.Enable(StateCaps.DepthTest);
                gl.Enable(StateCaps.CullFace);
                var deltaTime = DeltaTime.Init();
                double sumFps = 0;
                int frameCount = 0;
                while (appRunning) // Despite what resharper thinks, this loop will end when the form is closed.
                {
                    // Calculate time since last frame
                    deltaTime = new DeltaTime(ref deltaTime);
                    model = model.RotateY(deltaTime.Delta) * MatrixHelper.RotateZ(deltaTime.Delta / 4);
                    sumFps += deltaTime.Delta;
                    ++frameCount;
                    if (sumFps > 1.0f)
                    {
                        var averageDelta = sumFps / frameCount;
                        renderForm.Text = string.Format("Cube - {0:0.00} FPS", 1.0 / (averageDelta));
                        sumFps = 0;
                        frameCount = 0;
                    }

                    // Let windows handle Windows stuff
                    Application.DoEvents();

                    gl.Clear(ClearTarget.Color | ClearTarget.Depth);
                    
                    // Set cube matrix data.
                    cube.Model = model; // The rotation of our model (rotates around Y axis for every frame)
                    cube.View = view; 
                    cube.Projection = projection;
                    cube.DiffuseColor = Color.DodgerBlue;
                    cube.Draw(); // Draw the cube!
                    gl.Finish();
                    context.SwapBuffers(); // Swap the presentation buffer with the one we just finished rendering
                }
                cube.Dispose();
            }
            
            graphics.ReleaseHdc(hdc);
            graphics.Dispose();
        }
    }

    public struct DeltaTime
    {
        private readonly long lastState;
        private readonly float delta;

        public DeltaTime(ref DeltaTime previous)
        {
            lastState = Stopwatch.GetTimestamp();
            var diff = lastState - previous.lastState;
            delta = (float) (diff / (double) Stopwatch.Frequency);
        }

        private DeltaTime(long startTicks)
        {
            lastState = startTicks;
            delta = 0f;
        }

        public float Delta { get { return delta; } }

        public static DeltaTime Init()
        {
            return new DeltaTime (Stopwatch.GetTimestamp());
        }
    }
}
