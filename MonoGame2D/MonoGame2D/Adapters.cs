using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public static class Adapters {
        public static float Dot(this Vector2 v, Vector2 x)
        {
            return Vector2.Dot(v, x);
        }

        public static Vector2 Half(this Vector2 v) {
            return new Vector2(v.X * 0.5f, v.Y * 0.5f);
        }

        public static Color MultiplyAlpha(this Color a, float Alpha)
        { 
            return Color.FromNonPremultiplied((int)Alpha*255, a.G,a.B, a.A);
        }

    }
}
