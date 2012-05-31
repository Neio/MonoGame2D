using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    /// <summary>
    /// Sprite entity is stateful sprite container based on ISpriteProvider. Sprite entity has location, size and color tint.
    /// </summary>
    public class SpriteEntity : ISpriteEntity
    {
        protected Vector2 _xy;
        protected Vector2 _size;
        protected float _angle;
        protected Color _colorTint;
        protected Texture2D _spriteProvider;
        protected bool _originalSize;

        #region ISpriteEntity Members

        /// <summary>
        /// Gets or sets the location of sprite entity
        /// </summary>
        /// <value>The location of entity.</value>
        public Vector2 XY
        {
            get { return _xy; }
            set { _xy = value; }
        }

        /// <summary>
        /// Gets or sets the size of sprite entity.
        /// </summary>
        /// <value>The size of entity.</value>
        public Vector2 Size
        {
            get { return _originalSize ? _spriteProvider.Size() : _size; }
            set { _size = value; _originalSize = false; }
        }

        /// <summary>
        /// Gets or sets the rotation angle of sprite entity in radians.
        /// </summary>
        /// <value>The angle in radians.</value>
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        /// <summary>
        /// Gets or sets the color tint of entity.
        /// </summary>
        /// <value>The color tint.</value>
        public Color ColorTint
        {
            get { return _colorTint; }
            set { _colorTint = value; }
        }

        /// <summary>
        /// Gets the sprite source objects.
        /// </summary>
        /// <value>The <see cref="ISpriteProvider"/> instance.</value>
        public Texture2D SpriteProvider
        {
            get { return _spriteProvider; }
            set
            {
                if (null == value) throw new ArgumentNullException("SpriteProvider");
                _spriteProvider = value;
            }
        }

        /// <summary>
        /// Queries parameters of sprite entity.
        /// </summary>
        /// <param name="xy">The location of xy.</param>
        /// <param name="size">The size of sprite entity.</param>
        /// <param name="angle">The angle of sprite entity.</param>
        /// <param name="colorTint">The color tint of entity.</param>
        /// <param name="sprite">The source sprite.</param>
        public void QueryParameters(out Vector2 xy, out Vector2 size, out float angle, out Color colorTint, out Texture2D sprite)
        {
            xy = _xy;
            size = Size;
            angle = _angle;
            colorTint = _colorTint;
            sprite = _spriteProvider;
        }

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="SpriteEntity"/> class.
        /// </summary>
        /// <param name="sprite">The sprite provider.</param>
        public SpriteEntity(Texture2D sprite)
            : this(Vector2.Zero, Vector2.Zero, 0, Color.TransparentWhite, sprite)
        {
            _originalSize = true;
        }


        /// <summary>
        /// Creates a new instance of the <see cref="SpriteEntity"/> class with specified parameters.
        /// </summary>
        /// <param name="xy">The sprite entity location.</param>
        /// <param name="sprite">The sprite provider.</param>
        public SpriteEntity(Vector2 xy, Texture2D sprite)
            : this(xy, Vector2.Zero, 0, Color.TransparentWhite, sprite)
        {
            _originalSize = true;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SpriteEntity"/> class with all of parameters.
        /// </summary>
        /// <param name="xy">The sprite entity location.</param>
        /// <param name="angle">The rotation angle in radians.</param>
        /// <param name="sprite">The sprite provider.</param>
        public SpriteEntity(Vector2 xy, float angle, Texture2D sprite)
            : this(xy, Vector2.Zero, angle, Color.TransparentWhite, sprite)
        {
            _originalSize = true;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SpriteEntity"/> class with specified parameters.
        /// </summary>
        /// <param name="xy">The sprite entity location.</param>
        /// <param name="size">The size of sprite entity.</param>
        /// <param name="sprite">The sprite provider.</param>
        public SpriteEntity(Vector2 xy, Vector2 size, Texture2D sprite)
            : this(xy, Vector2.Zero, 0, Color.TransparentWhite, sprite)
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="SpriteEntity"/> class with all of parameters.
        /// </summary>
        /// <param name="xy">The sprite entity location.</param>
        /// <param name="angle">The rotation angle in radians.</param>
        /// <param name="colorTint">The color tint.</param>
        /// <param name="sprite">The sprite provider.</param>
        public SpriteEntity(Vector2 xy, float angle, Color colorTint, Texture2D sprite)
            : this(xy, Vector2.Zero, angle, colorTint, sprite)
        {
            _originalSize = true;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SpriteEntity"/> class with specified parameters.
        /// </summary>
        /// <param name="xy">The sprite entity location.</param>
        /// <param name="size">The size of sprite entity.</param>
        /// <param name="colorTint">The color tint.</param>
        /// <param name="sprite">The sprite provider.</param>
        public SpriteEntity(Vector2 xy, Vector2 size, Color colorTint, Texture2D sprite)
            : this(xy, size, 0, colorTint, sprite)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SpriteEntity"/> class with all of parameters.
        /// </summary>
        /// <param name="xy">The sprite entity location.</param>
        /// <param name="size">The size of sprite entity.</param>
        /// <param name="angle">The rotation angle in radians.</param>
        /// <param name="colorTint">The color tint.</param>
        /// <param name="sprite">The sprite provider.</param>
        public SpriteEntity(Vector2 xy, Vector2 size, float angle, Color colorTint, Texture2D sprite)
        {
            _spriteProvider = sprite;
            _xy = xy;
            _size = size;
            _angle = angle;
            _colorTint = colorTint;
        }

        /// <summary>
        /// Gets or sets a value indicating whether original size of sprite is forced.
        /// </summary>
        /// <value><c>true</c> if sprite is forced to use original size; otherwise, <c>false</c>.</value>
        public bool OriginalSize
        {
            get { return _originalSize; }
            set { _originalSize = value; }

        }

    }
}
