using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D
{
    public class Rotation : ITransform
    {
        public float Rotate;

        public Rotation(float Rotate)
        {
            this.Rotate = Rotate;
        }

        public Microsoft.Xna.Framework.Matrix Matrix
        {
            get { return Microsoft.Xna.Framework.Matrix.CreateRotationZ(Rotate); }
        }
    }
}
