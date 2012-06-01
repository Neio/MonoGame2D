using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D
{
    [Flags]
    public enum TextLayout
    {
        Default = 0,
        Left = 1,
        Right = 2,
        Center = 3,
        Top = 256,
        Bottom = 512,
        Middle = 768,
    }
}
