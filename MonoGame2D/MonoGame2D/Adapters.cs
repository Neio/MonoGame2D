using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    public static class Adapters {
        public static float Dot(this Vector2 v, Vector2 x)
        {
            return Vector2.Dot(v, x);
        }
    }
}
