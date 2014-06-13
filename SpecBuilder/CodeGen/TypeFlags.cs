using System;

namespace SpecBuilder.CodeGen
{
    [Flags]
    public enum TypeFlags
    {
        None = 0x0,
        Out = 0x1,
        In = 0x2,
        Ref = 0x4
    }
}