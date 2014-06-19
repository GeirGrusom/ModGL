using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ModGL.NativeGL;
using Platform.Invoke;

namespace ModGL.Windows
{
    public class ContextBuilder
    {
        private readonly IWGL _wgl;

        public ContextBuilder(IWGL wgl)
        {
            _wgl = wgl;
        }

        public IntPtr BuildModernContext(ContextCreationParameters contextParameters, ILibrary library, IContext sharedContext, IntPtr legacyContextHandle)
        {
            if (!_wgl.wglMakeCurrent((IntPtr)contextParameters.Device, legacyContextHandle))
                throw new ContextCreationException("Unable to make temporary context current.", contextParameters);


            var choosePixelFormat = library.GetProcedure<wglChoosePixelFormatARB>("wglChoosePixelFormatARB");
            var createContext = library.GetProcedure<wglCreateContextAttribsARB>("wglCreateContextAttribsARB");

            if (choosePixelFormat == null)
                throw new MissingEntryPointException("wglChoosePixelFormatARB", library);

            if (createContext == null)
                throw new MissingEntryPointException("wglCreateContextAttribsARB", library);

            var formats = new int[1];
            var numFormats = new uint[1];

            var pixelFormatParameters = new[]
                    {
                        WindowsContext.WGLPixelFormatConstants.WGL_DRAW_TO_WINDOW_ARB, (int) GLboolean.True,
                        WindowsContext.WGLPixelFormatConstants.WGL_SUPPORT_OPENGL_ARB, (int) GLboolean.True,
                        WindowsContext.WGLPixelFormatConstants.WGL_DOUBLE_BUFFER_ARB, (int) GLboolean.True,
                        WindowsContext.WGLPixelFormatConstants.WGL_PIXEL_TYPE_ARB, WindowsContext.WGLPixelFormatConstants.WGL_TYPE_RGBA_ARB,
                        WindowsContext.WGLPixelFormatConstants.WGL_COLOR_BITS_ARB, contextParameters.ColorBits.HasValue ? contextParameters.ColorBits.Value : 32,
                        WindowsContext.WGLPixelFormatConstants.WGL_DEPTH_BITS_ARB, contextParameters.DepthBits.HasValue ? contextParameters.DepthBits.Value : 24,
                        WindowsContext.WGLPixelFormatConstants.WGL_STENCIL_BITS_ARB, contextParameters.StencilBits.HasValue ? contextParameters.StencilBits.Value : 8,
                        WindowsContext.WGLPixelFormatConstants.WGL_SAMPLE_BUFFERS_ARB, 1,
                        WindowsContext.WGLPixelFormatConstants.WGL_SAMPLES_ARB, 4,
                        0 //End 
                    };

            if (!choosePixelFormat((IntPtr)contextParameters.Device, pixelFormatParameters, null, 1, formats, numFormats))
            {
                throw new ContextCreationException("Unable to choose pixel format.", contextParameters);
            }

            var finalContext = createContext((IntPtr)contextParameters.Device, sharedContext != null ? sharedContext.Handle : IntPtr.Zero, new[]
            {
                (int)WGLContextAttributes.MajorVersion, contextParameters.MajorVersion.HasValue ? contextParameters.MajorVersion.Value : 3,
                (int)WGLContextAttributes.MinorVersion, contextParameters.MinorVersion.HasValue ? contextParameters.MinorVersion.Value : 2,
                (int)WGLContextAttributes.Flags, 0,
                (int)WGLContextAttributes.ProfileMask, (int)WGLContextProfileMask.CoreProfileBit,
                0
            }
            );

            if(finalContext == IntPtr.Zero)
                throw new ContextCreationException("Unable to create OpenGL 3.0 context.", contextParameters);



            return finalContext;
        }

        public IntPtr BuildLegacyContext(ContextCreationParameters parameters)
        {
            if(parameters.Device == 0)
                throw new ContextCreationException("No device specified.", parameters);
            if(parameters.Window == 0)
                throw new ContextCreationException("No window specified.", parameters);

            var desc = new PixelFormatDescriptor
            {
                Version = 1,
                StencilBits = 8,
                DepthBits = 24,
                ColorBits = 32,
                PixelType = PixelType.Rgba,
                Size = (ushort)System.Runtime.InteropServices.Marshal.SizeOf(typeof(PixelFormatDescriptor)),
                Flags = PixelFormatFlags.DoubleBuffer | PixelFormatFlags.DrawToWindows | PixelFormatFlags.SupportOpenGL,
            };

            int pixelFormat = _wgl.ChoosePixelFormat((IntPtr)parameters.Device, ref desc);

            if (pixelFormat == 0)
                throw new PixelFormatException("Could not select an appropriate pixel format.", parameters, desc);

            if (!_wgl.SetPixelFormat((IntPtr)parameters.Device, pixelFormat, ref desc))
                throw new PixelFormatException("Could not set pixel format for HDC.", parameters, desc);

            var glptr = _wgl.wglCreateContext((IntPtr)parameters.Device);
            if (glptr == IntPtr.Zero)
                throw new ContextCreationException("Unable to create OpenGL context.", parameters);

            return glptr;
        }
    }
}
