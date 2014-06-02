using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Numerics
{
    public struct Quaternion
    {
        private readonly Vector4f value;

        public Quaternion(Vector4f value)
        {
            this.value = value;
        }

        public Quaternion Normalize()
        {
            return new Quaternion(value.Normalize());
        }
    }
}
