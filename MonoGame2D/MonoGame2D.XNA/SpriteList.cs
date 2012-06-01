using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    /// <summary>
    /// Implementation of sprite list based on System.Collections.Generic.List
    /// </summary>
    public class SpriteList : List<Texture2D>, ISpriteCollection
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteList"/> class.
        /// </summary>
        public SpriteList() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteList"/> class.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="collection"/> is null.</exception>
        public SpriteList(IEnumerable<Texture2D> collection) : base(collection) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity of new list.</param>
        public SpriteList(int capacity) : base(capacity) { }



    }
}
