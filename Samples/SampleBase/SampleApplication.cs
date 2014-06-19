using System;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;
using ModGL;
using ModGL.NativeGL;
using ModGL.Numerics;

namespace SampleBase
{
    public abstract class SampleApplication<TOpenGLInterface>
        where TOpenGLInterface : class, IOpenGL30
    {
        private readonly Form _renderForm;
        private bool _appRunning;
        private readonly IContext _context;
        private readonly string _applicationName;
        private readonly Graphics _graphics;
        private readonly IntPtr _hdc;
        protected Matrix4f Model;
        protected Matrix4f View;
        protected Matrix4f Projection;
        private TOpenGLInterface _gl;
        protected TOpenGLInterface GL { get { return _gl; } }
        
        public IContext Context { get { return _context; } }

        protected SampleApplication(string applicationName)
        {
            _applicationName = applicationName;
            _appRunning = true;

            // Create out render form
            _renderForm = new Form { Text = applicationName };
            _renderForm.Closing += (sender, e) => _appRunning = false;
            _renderForm.ClientSize = new Size(800, 600);
            _renderForm.Show();

            _graphics = _renderForm.CreateGraphics();
            _hdc = _graphics.GetHdc();

            Model = Matrix4f.Identity;
            View = Matrix4f.Identity;
            Projection = Matrix4f.Identity;
            var glVersion = typeof (TOpenGLInterface).GetCustomAttribute<GLVersionAttribute>() ?? new GLVersionAttribute(3, 3);
            // Create the OpenGL rendering context
            _context =
                ContextFactory.Instance.Create(new ContextCreationParameters
                {
                    Device = (long)_hdc,
                    Window = (long)_renderForm.Handle,
                    MajorVersion = glVersion.Major,
                    MinorVersion = glVersion.Minor,
                });

        }

        protected abstract void Initialize();

        protected abstract void FrameTick();

        public abstract void Exiting();

        public void Run()
        {
            using (_context.Bind())
            {

                _gl = _context.CreateInterface<TOpenGLInterface>(InterfaceFlags.Debug);
                string renderer = _gl.GetString(StringName.Renderer);
                string vendor = _gl.GetString(StringName.Vendor);
                string version = _gl.GetString(StringName.Version);
                var bgColor = Color.DodgerBlue;
                Projection = ProjectionMatrixHelper.RightHandPerspective((float) Math.PI/2,
                    _renderForm.ClientSize.Width/(float) _renderForm.ClientSize.Height, 0.01f, 10f);
                _renderForm.Resize +=
                    (sender, args) =>
                    {
                        _gl.Viewport(0, 0, _renderForm.ClientSize.Width, _renderForm.ClientSize.Height);
                        Projection = ProjectionMatrixHelper.RightHandPerspective((float)Math.PI / 2,
                            _renderForm.ClientSize.Width / (float)_renderForm.ClientSize.Height, 0.01f, 10f);
                    };

                _gl.ClearColor(bgColor.R / 255f, bgColor.G / 255f, bgColor.B / 255f, 1);
                _gl.Enable(EnableCap.DepthTest);
                _gl.Enable(EnableCap.CullFace);
                View = ViewMatrixHelper.LookAt(new Vector3f(1.75f, 1.75f, 1.75f), new Vector3f(0, 1, 0), new Vector3f(0, 0, 0));
                Initialize();
                var deltaTime = DeltaTime.Init();
                double sumFps = 0;
                int frameCount = 0;

                while (_appRunning)
                {
                    // Calculate average frames per second during the passed .5 seconds.
                    deltaTime = new DeltaTime(ref deltaTime);
                    sumFps += deltaTime.Delta;
                    ++frameCount;
                    if (sumFps > 0.5f)
                    {
                        var averageDelta = sumFps / frameCount;
                        _renderForm.Text = string.Format("{0} - {1:0.00} FPS - OpenGL {2} using {3}({4})", _applicationName, 1.0 / (averageDelta), version, renderer, vendor);
                        sumFps = 0;
                        frameCount = 0;
                    }

                    Model = Model.RotateY(deltaTime.Delta) * MatrixHelper.RotateZ(deltaTime.Delta / 4);
                    
                    Application.DoEvents();

                    _gl.Clear((uint)(ClearTarget.Color | ClearTarget.Depth)); 
                    FrameTick();
                    _gl.Finish();
                    _context.SwapBuffers();
                }
                Exiting();
                
            }
            _context.Dispose();
            _graphics.ReleaseHdc(_hdc);
            _graphics.Dispose();
        }
    }

    public struct DeltaTime
    {
        private readonly long _lastState;
        private readonly float _delta;

        public DeltaTime(ref DeltaTime previous)
        {
            _lastState = Stopwatch.GetTimestamp();
            var diff = _lastState - previous._lastState;
            _delta = (float)(diff / (double)Stopwatch.Frequency);
        }

        private DeltaTime(long startTicks)
        {
            _lastState = startTicks;
            _delta = 0f;
        }

        public float Delta { get { return _delta; } }

        public static DeltaTime Init()
        {
            return new DeltaTime(Stopwatch.GetTimestamp());
        }
    }

}
