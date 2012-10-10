using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class GameFont
    {
        private static SpriteFont _default;
        internal static void Init(ContentManager content)
        {
            _default = content.Load<SpriteFont>("Default");
        }

        public static SpriteFont Default { get { return _default; } }
    }
}
