using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    public class Translation : ITransform
    {
        public Vector2 Translate;

        public Translation(float x, float y)
        {
            Translate = new Vector2(x, y);
        }

        public Translation(Vector2 dx)
        {
            Translate = dx;
        }

        public Matrix Matrix
        {
            get { return Matrix.CreateTranslation(Translate.X, Translate.Y, 0); }
        }
    }
}
