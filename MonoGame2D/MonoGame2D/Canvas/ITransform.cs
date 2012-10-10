using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    public interface ITransform
    {
        Matrix Matrix { get; }
    }
}
