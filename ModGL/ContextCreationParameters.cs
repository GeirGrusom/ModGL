using System;

namespace ModGL
{
    [Serializable]
    public class ContextCreationParameters
    {
        /// <summary>
        /// Specifies the display used.
        /// </summary>
        /// <remarks>This property is not used by Windows.</remarks>
        public long Display { get; set; }

        /// <summary>
        /// Specifies the window handle used.
        /// </summary>
        /// <remarks>On Windows, this field should be a HWND.</remarks>
        public long Window { get; set; }

        /// <summary>
        /// Specifies the graphics device used.
        /// </summary>
        /// <remarks>
        /// On Windows, this field should be a HDC.</remarks>
        public long Device { get; set; }

        /// <summary>
        /// Specifies number of colors bits. If not seet, this will default to 32.
        /// </summary>
        public int? ColorBits { get; set; }
        /// <summary>
        /// Specifies number of depth bits. If not set, this will default to 24.
        /// </summary>
        public int? DepthBits { get; set; }
        /// <summary>
        /// Specifies number of stencil bits. If not set, this will default to 8.
        /// </summary>
        public int? StencilBits { get; set; }

        /// <summary>
        /// Specifies number of multisamples. If not set, this will default to 0.
        /// </summary>
        public int? MultiSamples { get; set; }
        /// <summary>
        /// Specifies multisample quality. If not set, this will default to 0.
        /// </summary>
        public int? MultiSampleQuality { get; set; }

        /// <summary>
        /// Specifies minimum major OpenGL version. If not set, this will default to 3.
        /// </summary>
        public int? MajorVersion { get; set; }
        /// <summary>
        /// Specifies minimum minor OpenGL version. If not set, this will default to 0.
        /// </summary>
        public int? MinorVersion { get; set; }

        /// <summary>
        /// Specifies that this context should be a stereo rendering context.
        /// </summary>
        public bool StereoRendering { get; set; }

        /// <summary>
        /// Specifies color buffer swap interval. Set this to 1 to enable vertical sync.
        /// </summary>
        public int? SwapInterval { get; set; }
    }
}