using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    public class Scaling : ITransform
    {
        public Vector2 Scale;

        public Scaling(Vector2 scale)
        {
            this.Scale = scale;
        }

        public Scaling(float scale)
        {
            this.Scale = new Vector2(scale);
        }

        public Microsoft.Xna.Framework.Matrix Matrix
        {
            get { return Microsoft.Xna.Framework.Matrix.CreateScale(Scale.X, Scale.Y, 1); }
        }
    }

}
