using System;

namespace ModGL.NativeGL
{
    public class GLVersionAttribute : Attribute
    {
        public int Major;
        public int Minor;
        public int Revision;

        public GLVersionAttribute(int major, int minor, int revision)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
        }

        public GLVersionAttribute(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", Major, Minor, Revision);
        }

    }
}