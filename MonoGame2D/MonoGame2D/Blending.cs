using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class Blending :BlendState
    {
        public readonly static BlendState None = BlendState.Opaque;
        public readonly static BlendState Normal = BlendState.AlphaBlend;

        public readonly static BlendState Add = BlendState.Additive;
    }
}
