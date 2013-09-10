using System;

namespace ModGL
{
    [Serializable]
    public class ContextCreationParameters
    {
        public IntPtr Display { get; set; }
        public IntPtr Window { get; set; }
        public IntPtr Device { get; set; }

        public int ColorBits { get; set; }
        public int DepthBits { get; set; }
        public int StencilBits { get; set; }

        public int MultiSamples { get; set; }
        public int MultiSampleQuality { get; set; }

        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int Revision { get; set; }
    }
}