using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public static class MonoGame2DExtention
    {
        public static Vector2 Size(this Texture2D sprite)
        {
            return new Vector2(sprite.Width, sprite.Height);
        }


        public static Color Blank(this Color color)
        {
            return new Color(Color.White, 255);
        }

    }
}
