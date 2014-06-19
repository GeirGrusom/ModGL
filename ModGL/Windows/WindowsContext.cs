using System;
using System.Diagnostics.Contracts;

using ModGL.NativeGL;
using Platform.Invoke;

namespace ModGL.Windows
{
     public enum WGLColorBufferMask
     {
        FrontColorBufferBit= 1,
        BackColorBufferBit = 2,
        DepthBufferBit = 4,
        StencilBufferBit = 8
     }


    public enum WGLContextFlagsMask
    {
        DebugBit = 1,
        ForwardCompatibleBit = 2,
        RobustAccessBit = 4,
        ResetIsolationBit = 8
    }

    public enum WGLContextProfileMask
    {
        CoreProfileBit = 1,
        CompatibilityProfileBit = 2,
        EsProfileBit = 4,
        Es2ProfileBit = 8
    }

    public enum WGLContextAttributes
    {
        MajorVersion     =      0x2091,
        MinorVersion     =      0x2092,
        LayerPlane       =      0x2093,
        Flags            =      0x2094,
        ProfileMask      =      0x9126
    }

    public enum SwapInterval
    {
        Immediate = 0,
        VerticalSync = 1
    }
 
    public class WindowsContext : Context
    {
        private readonly IWGL _wgl;
        private readonly ILibrary _glLibraryProvider;
        private readonly IntPtr _hdc;
        private readonly ContextCreationParameters _contextParameters;
        private readonly IContext _sharedContext;
        private bool _initialized;

        public static class WGLPixelFormatConstants
        {
            public const int WGL_SAMPLE_BUFFERS_ARB = 0x2041;
            public const int WGL_SAMPLES_ARB = 0x2042;
            public const int WGL_NUMBER_PIXEL_FORMATS_ARB = 0x2000;
            public const int WGL_DRAW_TO_WINDOW_ARB = 0x2001;
            public const int WGL_DRAW_TO_BITMAP_ARB = 0x2002;
            public const int WGL_ACCELERATION_ARB = 0x2003;
            public const int WGL_NEED_PALETTE_ARB = 0x2004;
            public const int WGL_NEED_SYSTEM_PALETTE_ARB = 0x2005;
            public const int WGL_SWAP_LAYER_BUFFERS_ARB = 0x2006;
            public const int WGL_SWAP_METHOD_ARB = 0x2007;
            public const int WGL_NUMBER_OVERLAYS_ARB = 0x2008;
            public const int WGL_NUMBER_UNDERLAYS_ARB = 0x2009;
            public const int WGL_TRANSPARENT_ARB = 0x200A;
            public const int WGL_TRANSPARENT_RED_VALUE_ARB = 0x2037;
            public const int WGL_TRANSPARENT_GREEN_VALUE_ARB = 0x2038;
            public const int WGL_TRANSPARENT_BLUE_VALUE_ARB = 0x2039;
            public const int WGL_TRANSPARENT_ALPHA_VALUE_ARB = 0x203A;
            public const int WGL_TRANSPARENT_INDEX_VALUE_ARB = 0x203B;
            public const int WGL_SHARE_DEPTH_ARB = 0x200C;
            public const int WGL_SHARE_STENCIL_ARB = 0x200D;
            public const int WGL_SHARE_ACCUM_ARB = 0x200E;
            public const int WGL_SUPPORT_GDI_ARB = 0x200F;
            public const int WGL_SUPPORT_OPENGL_ARB = 0x2010;
            public const int WGL_DOUBLE_BUFFER_ARB = 0x2011;
            public const int WGL_STEREO_ARB = 0x2012;
            public const int WGL_PIXEL_TYPE_ARB = 0x2013;
            public const int WGL_COLOR_BITS_ARB = 0x2014;
            public const int WGL_RED_BITS_ARB = 0x2015;
            public const int WGL_RED_SHIFT_ARB = 0x2016;
            public const int WGL_GREEN_BITS_ARB = 0x2017;
            public const int WGL_GREEN_SHIFT_ARB = 0x2018;
            public const int WGL_BLUE_BITS_ARB = 0x2019;
            public const int WGL_BLUE_SHIFT_ARB = 0x201A;
            public const int WGL_ALPHA_BITS_ARB = 0x201B;
            public const int WGL_ALPHA_SHIFT_ARB = 0x201C;
            public const int WGL_ACCUM_BITS_ARB = 0x201D;
            public const int WGL_ACCUM_RED_BITS_ARB = 0x201E;
            public const int WGL_ACCUM_GREEN_BITS_ARB = 0x201F;
            public const int WGL_ACCUM_BLUE_BITS_ARB = 0x2020;
            public const int WGL_ACCUM_ALPHA_BITS_ARB = 0x2021;
            public const int WGL_DEPTH_BITS_ARB = 0x2022;
            public const int WGL_STENCIL_BITS_ARB = 0x2023;
            public const int WGL_AUX_BUFFERS_ARB = 0x2024;

            public const int WGL_NO_ACCELERATION_ARB = 0x2025;
            public const int WGL_GENERIC_ACCELERATION_ARB = 0x2026;
            public const int WGL_FULL_ACCELERATION_ARB = 0x2027;
            public const int WGL_SWAP_EXCHANGE_ARB = 0x2028;
            public const int WGL_SWAP_COPY_ARB = 0x2029;
            public const int WGL_SWAP_UNDEFINED_ARB = 0x202A;
            public const int WGL_TYPE_RGBA_ARB = 0x202B;
            public const int WGL_TYPE_COLORINDEX_ARB = 0x202C;
        }

        public override void Dispose()
        {
            _wgl.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
            _wgl.wglDeleteContext(Handle);
        }

        public override void SwapBuffers()
        {
            _wgl.SwapBuffers(_hdc);
        }

        public override void Initialize()
        {
            if(_initialized)
                throw new InvalidOperationException("Context has already been initialized.");

            
            var builder = new ContextBuilder(_wgl);
            var tempContext = builder.BuildLegacyContext(_contextParameters);
            var finalContext = builder.BuildModernContext(_contextParameters, this, _sharedContext, tempContext);
            Handle = finalContext;
            _initialized = true;

            if (_contextParameters.SwapInterval != null)
            {
                using (Bind())
                {
                    SetSwapInterval((SwapInterval)_contextParameters.SwapInterval.Value);
                }
            }
        }

        private delegate bool wglSwapIntervalEXTProc(int interval);

        private wglSwapIntervalEXTProc _setSwapInterval;

        private void SetSwapInterval(SwapInterval swapInterval)
        {
            if (_setSwapInterval == null)
                _setSwapInterval = GetProcedure<wglSwapIntervalEXTProc>("wglSwapIntervalEXT");
            if (_setSwapInterval != null)
            {
                _setSwapInterval((int) swapInterval);
            }
        }

        public WindowsContext(IWGL wgl, ILibrary glLibraryProvider, IContext shareContext, ContextCreationParameters parameters)
        {
            if(wgl == null)
                throw new ArgumentNullException("wgl");

            if(glLibraryProvider == null)
                throw new ArgumentNullException("glLibraryProvider");

            if(parameters == null)
                throw new ArgumentNullException("parameters");

            if(parameters.MajorVersion < 3)
                throw new VersionNotSupportedException("OpenGL version below 3.0 is not supported.", parameters);

            if(parameters.Device == 0)
                throw new ContextCreationException("Device cannot be null.", parameters);

            if(parameters.Window == 0)
                throw new ContextCreationException("Window cannot be null.", parameters);

            if(parameters.Display != 0)
                throw new ContextCreationException("Display is not supported on this platform.", parameters);

            _wgl = wgl;
            _glLibraryProvider = glLibraryProvider;
            _hdc = new IntPtr(parameters.Device);
            _contextParameters = parameters;
            _sharedContext = shareContext;


        }

        private IntPtr CreateTempOpenGLContext(ContextCreationParameters parameters)
        {
            var builder = new ContextBuilder(_wgl);
            return builder.BuildLegacyContext(parameters);
        }

        public override BindContext Bind()
        {
            if(!_initialized)
                Initialize();
            _wgl.wglMakeCurrent(_hdc, Handle);
            CurrentContext = this;
            return new BindContext(() => { _wgl.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
                                             CurrentContext = null;
            } );
        }

        [Pure]
        public override Delegate GetProcedure(Type delegateType, string extensionName)
        {
            var delegPtr = _wgl.wglGetProcAddress(extensionName);

            if (delegPtr == IntPtr.Zero)
                return _glLibraryProvider.GetProcedure(delegateType, extensionName);

            return System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer(delegPtr, delegateType);
        }
    }
}
