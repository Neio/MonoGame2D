using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    /// <summary>
    /// Interface for sprite entity. Sprite entity is extension of <see cref="Vortex.Drawing.Sprite"/> which contains location, size, color tint and rotation angle.
    /// </summary>
    public interface ISpriteEntity
    {
        /// <summary>
        /// Gets the location of sprite entity
        /// </summary>
        /// <value>The location of entity.</value>
        Vector2 XY { get; }

        /// <summary>
        /// Gets the size of sprite entity.
        /// </summary>
        /// <value>The size of entity.</value>
        Vector2 Size { get; }

        /// <summary>
        /// Gets the rotation angle of sprite entity in radians.
        /// </summary>
        /// <value>The angle in radians.</value>
        float Angle { get; }

        /// <summary>
        /// Gets the color tint of entity.
        /// </summary>
        /// <value>The color tint.</value>
        Color ColorTint { get; }

        /// <summary>
        /// Gets the sprite source objects.
        /// </summary>
        /// <value>The <see cref="ISpriteProvider"/> instance.</value>
        ISpriteSource SpriteProvider { get; }

        /// <summary>
        /// Queries parameters of sprite entity.
        /// </summary>
        /// <param name="xy">The location of xy.</param>
        /// <param name="size">The size of sprite entity.</param>
        /// <param name="angle">The angle of sprite entity.</param>
        /// <param name="colorTint">The color tint of entity.</param>
        /// <param name="sprite">The source sprite.</param>
        void QueryParameters(out Vector2 xy, out Vector2 size, out float angle, out Color colorTint, out Texture2D sprite);

    }
}
