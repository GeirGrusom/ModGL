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
                                [In]int[]                   piAttribIList,
                                [In]float[]                 pfAttribFList,
                                uint                    nMaxFormats,
                                [In]int[]                   piFormats,
                                [In]uint[]                  nNumFormats
    );

    public delegate HGLRC wglCreateContextAttribsARB(HDC hDC, HGLRC hshareContext, [In]int[] attribList);

    public interface IWGL
    {
        HGLRC wglCreateContext(HDC hdc);

        IntPtr wglGetProcAddress([In]string procName);

        bool wglMakeCurrent(HDC dc, HGLRC glrc);

        int GetPixelFormat(HDC hdc);

        bool SetPixelFormat(HDC hdc, int iPixelFormat, ref PixelFormatDescriptor ppfd);

        int DescribePixelFormat(HDC hdc, int pixelFormat, uint bytes, out PixelFormatDescriptor ppfd);

        int ChoosePixelFormat(HDC hdc, ref PixelFormatDescriptor ppfd);

        bool wglDeleteContext(HGLRC hglrc);

        bool SwapBuffers(IntPtr hdc);
    }

    public interface IWGL3
    {
        bool ChoosePixelFormatARB(
            HDC hdc,
            [In] int[] piAttribIList,
            [In] float[] pfAttribFList,
            uint nMaxFormats,
            [In] int[] piFormats,
            [In] uint[] nNumFormats);
        HGLRC CreateContextAttribsARB(HDC hDC, HGLRC hshareContext, [In]int[] attribList);
    }
}
