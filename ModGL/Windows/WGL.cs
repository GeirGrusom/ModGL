using System;
using System.Runtime.InteropServices;
using HDC = System.IntPtr;
using HGLRC = System.IntPtr;

namespace ModGL.Windows
{

    public struct PixelFormatDescriptor
    {
        public ushort Size;
        public ushort Version;
        public uint Flags;
        public byte PixelType;
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

    public delegate bool wglChoosePixelFormatARB(HDC hdc,
                                int[] piAttribIList,
                                float[] pfAttribFList,
                                uint nMaxFormats,
                                int[] piFormats,
                                uint[] nNumFormats);

    public delegate HGLRC wglCreateContextAttribsARB(HDC hDC, HGLRC hshareContext, int[] attribList);

    public interface IWGL
    {
        HGLRC wglCreateContext(HDC hdc);
        
        TDelegate wglGetProcAddress<TDelegate>(string procName);
        
        bool wglMakeCurrent(HDC dc, HGLRC glrc);

        int GetPixelFormat(HDC hdc);

        bool SetPixelFormat(HDC hdc, int iPixelFormat, ref PixelFormatDescriptor ppfd);        

        int DescribePixelFormat(HDC hdc, int pixelFormat, uint bytes, out PixelFormatDescriptor ppfd);

        int ChoosePixelFormat(HDC hdc, ref PixelFormatDescriptor ppfd);

        bool wglDeleteContext(HGLRC hglrc);
    }



    public class WGL : IWGL
    {
        public const string WGLLibraryName = "opengl32";

        public const string GDILibraryName = "gdi32";

        TDelegate IWGL.wglGetProcAddress<TDelegate>(string procName)
        {
            return  (TDelegate)Convert.ChangeType(Marshal.GetDelegateForFunctionPointer(wglGetProcAddress(procName), typeof(TDelegate)), typeof(TDelegate));
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

        [DllImport(GDILibraryName)]
        public static extern bool SetPixelFormat(HDC hdc, int iPixelFormat, ref PixelFormatDescriptor ppfd);

        [DllImport(WGLLibraryName)]
        public static extern HGLRC wglCreateContext(HDC hdc);

        [DllImport(WGLLibraryName)]
        public static extern IntPtr wglGetProcAddress(string procName);

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
    }


}
