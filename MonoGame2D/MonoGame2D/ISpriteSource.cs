using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    /// <summary>
    /// A class which exposes this interface should be able to return sprite.
    /// It is very useful to use sprite source instead of direct sprite reference.
    /// </summary>
    public interface ISpriteSource
    {

        /// <summary>
        /// Returns the sprite which describes object.
        /// </summary>
        /// <returns><see cref="Sprite"/> instance.</returns>
        Texture2D ToSprite();
    }
}
