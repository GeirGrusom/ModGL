using System;
using System.Runtime.InteropServices;

using ModGL.NativeGL;

using HDC = System.IntPtr;
using HGLRC = System.IntPtr;

namespace ModGL.Windows
{

    [Flags]
    public enum PixelFormatFlags : uint
    {
        DoubleBuffer = 0x00000001,
        Stereo = 0x00000002,
        DrawToWindows = 0x00000004,
        DrawToBitmap = 0x00000008,
        SupportGDI = 0x00000010,
        SupportOpenGL = 0x00000020,
        GenericFormat = 0x00000040,
        NeedPalett = 0x00000080,
        NeedSystemPalett = 0x00000100,
        SwapExhange = 0x00000200,
        SwapCopy = 0x00000400,
        SwapLayerBuffers = 0x00000800,
        GenericAccelerated = 0x00001000,
        SupportDirectDraw = 0x00002000
    }

    public enum PixelType : byte
    {
        Rgba = 0,
        ColorIndex = 1
    }

    public enum Plane
    {
        MainPlane = 0,
        OverlayPlane = 1,
        UnderlyingPlane = -1
    }

    public struct PixelFormatDescriptor
    {
        public ushort Size;
        public ushort Version;
        public PixelFormatFlags Flags;
        public PixelType PixelType;
        public byte ColorBits;
        public byte RedBits;
        public byte RedShift;
        public byte GreenBits;
        public byte GreenShift;
        public byte BlueBits;
        public byte BlueShift;
        public byte AlphaBits;
        public byte AlphaShift;
        public byte AccumBits;
        public byte AccumRedBits;
        public byte AccumGreenBits;
        public byte AccumBlueBits;
        public byte AccumAlphaBits;
        public byte DepthBits;
        public byte StencilBits;
        public byte AuxBuffers;
        public byte LayerType;
        public byte Reserved;
        public uint LayerMask;
        public uint VisibleMask;
        public uint DamageMask;
    }
                                                       
    public delegate bool wglChoosePixelFormatARB(HDC    hdc,
                                int[]                   piAttribIList,
                                float[]                 pfAttribFList,
                                uint                    nMaxFormats,
                                int[]                   piFormats,
                                uint[]                  nNumFormats
    );

    public delegate HGLRC wglCreateContextAttribsARB(HDC hDC, HGLRC hshareContext, int[] attribList);

    public interface IWGL
    {
        HGLRC wglCreateContext(HDC hdc);

        Delegate wglGetProcAddress(string procName, Type delegateType);

        TDelegate wglGetProcAddress<TDelegate>(string procName);

        bool wglMakeCurrent(HDC dc, HGLRC glrc);

        int GetPixelFormat(HDC hdc);

        bool SetPixelFormat(HDC hdc, int iPixelFormat, ref PixelFormatDescriptor ppfd);

        int DescribePixelFormat(HDC hdc, int pixelFormat, uint bytes, out PixelFormatDescriptor ppfd);

        int ChoosePixelFormat(HDC hdc, ref PixelFormatDescriptor ppfd);

        bool wglDeleteContext(HGLRC hglrc);

        bool SwapBuffers(IntPtr hdc);
    }



    public class WGL : IWGL
    {
        public const string WGLLibraryName = "opengl32";

        public const string GDILibraryName = "gdi32";

        Delegate IWGL.wglGetProcAddress(string procName, Type delegateType)
        {
            var ptr = wglGetProcAddress(procName);

            if (ptr == IntPtr.Zero)
            {
                var error = GL.glGetError();
                return null;
            }
            return Marshal.GetDelegateForFunctionPointer(ptr, delegateType);
        }

        TDelegate IWGL.wglGetProcAddress<TDelegate>(string procName)
        {
            return (TDelegate)Convert.ChangeType((this as IWGL).wglGetProcAddress(procName, typeof(TDelegate)), typeof(TDelegate));
        }

        bool IWGL.wglMakeCurrent(IntPtr dc, IntPtr glrc)
        {
            return wglMakeCurrent(dc, glrc);
        }

        int IWGL.GetPixelFormat(IntPtr hdc)
        {
            return GetPixelFormat(hdc);
        }

        int IWGL.DescribePixelFormat(IntPtr hdc, int pixelFormat, uint bytes, out PixelFormatDescriptor ppfd)
        {
            return DescribePixelFormat(hdc, pixelFormat, bytes, out ppfd);
        }

        int IWGL.ChoosePixelFormat(IntPtr hdc, ref PixelFormatDescriptor ppfd)
        {
            return ChoosePixelFormat(hdc, ref ppfd);
        }

        bool IWGL.wglDeleteContext(IntPtr hglrc)
        {
            return wglDeleteContext(hglrc);
        }

        IntPtr IWGL.wglCreateContext(IntPtr hdc)
        {
            return wglCreateContext(hdc);
        }

        bool IWGL.SetPixelFormat(HDC hdc, int iPixelFormat, ref PixelFormatDescriptor ppfd)
        {
            return SetPixelFormat(hdc, iPixelFormat, ref ppfd);
        }

        bool IWGL.SwapBuffers(HDC hdc)
        {
            return SwapBuffers(hdc);
        }

        [DllImport(GDILibraryName)]
        public static extern bool SetPixelFormat(HDC hdc, int iPixelFormat, ref PixelFormatDescriptor ppfd);

        [DllImport(WGLLibraryName)]
        public static extern HGLRC wglCreateContext(HDC hdc);

        [DllImport(WGLLibraryName)]
        public static extern IntPtr wglGetProcAddress([MarshalAs(UnmanagedType.LPStr)]string procName);

        [DllImport(WGLLibraryName)]
        public static extern bool wglMakeCurrent(HDC dc, HGLRC glrc);

        [DllImport(WGLLibraryName)]
        public static extern bool wglDeleteContext(HGLRC hglrc);

        [DllImport(GDILibraryName)]
        public static extern int GetPixelFormat(HDC hdc);

        [DllImport(GDILibraryName)]
        public static extern int DescribePixelFormat(HDC hdc, int pixelFormat, uint bytes, out PixelFormatDescriptor ppfd);

        [DllImport(GDILibraryName)]
        public static extern int ChoosePixelFormat(HDC hdc, ref PixelFormatDescriptor ppfd);

        [DllImport(GDILibraryName)]
        public static extern bool SwapBuffers(HDC hdc);
    }


}
