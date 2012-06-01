using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    /// <summary>
    /// Defines interface of sprite read-only collection
    /// </summary>
    public interface ISpriteCollection : IEnumerable<Texture2D>
    {

        /// <summary>
        /// Gets the count of sprites in collection.
        /// </summary>
        /// <value>The count of sprites.</value>
        int Count { get; }

        /// <summary>
        /// Gets the <see cref="Vortex.Drawing.Sprite"/> at the specified index.
        /// </summary>
        /// <value><see cref="Vortex.Drawing.Sprite"/> at specified index</value>
        Texture2D this[int index] { get; }

        /// <summary>
        /// Looks for sprite and returns its index in collection
        /// </summary>
        /// <param name="sprite">The sprite to find.</param>
        /// <returns>Index of sprite in collection or -1</returns>
        int IndexOf(Texture2D sprite);

    }
}
