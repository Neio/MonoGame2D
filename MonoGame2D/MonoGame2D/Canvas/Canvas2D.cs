using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    public class Canvas2D :StateScope
    {
        ///<summary>Maximal number of pushed transforms for canvas</summary>
        private const int TRANSFORM_STACK_MAX_SIZE = 64;
        private Matrix[] _transformStack = new Matrix[TRANSFORM_STACK_MAX_SIZE];
        private Matrix _transform = Matrix.Identity;
        ///<summary>Size of transformation stack</summary>
        private int _transformStackSize = 0;
        private BlendState _blending;
        private bool _begined ;

        public Canvas2D(Microsoft.Xna.Framework.Graphics.SpriteBatch Batch, GameTime GameTime, ref Matrix Matrix)
        {
            this.Batch = Batch;
            this.Matrix = Matrix;
            this.Time = GameTime;
            _blending = BlendState.AlphaBlend;
            _begined = false;
            Begin();
        }

        public SpriteBatch Batch { get; private set; }

        public GameTime Time { get; private set; }


        public BlendState Blending { get { return _blending; } set { _blending = value; } }



        public Matrix Matrix
        {
            get
            {
                return _transform;
            }
            private set
            {
                _transform = value;
            }
        }

        protected void Apply()
        {
            if (_begined)
            {
                End();
                Begin();
            }
        }

        /// <summary>
        /// Pushes the current transform onto transformation stack.
        /// </summary>
        public void PushTransform()
        {
            if (_transformStackSize >= TRANSFORM_STACK_MAX_SIZE)
            {
                throw new Exception("Transformation stack overflow");
            }
            _transformStack[_transformStackSize++] = _transform;
            Apply();
        }

        /// <summary>
        /// Pops the transform. Retrieves it from stack
        /// </summary>
        public void PopTransform()
        {
            if (_transformStackSize > 0)
            {
                _transformStackSize -= 1;
                _transform = _transformStack[_transformStackSize];
                Apply();
            }
            else
            {
                throw new Exception("Can't pop. Transformation stack is empty");
            }

        }

        /// <summary>
        /// Sets (overrides) the specified transform for canvas.
        /// </summary>
        /// <param name="transform">The transform for override.</param>
        public void SetTransform(ref Matrix transform)
        {
            _transform = transform;
            Apply();
        }

        /// <summary>
        /// Applies the specified transform after current canvas transform.
        /// </summary>
        /// <param name="transform">The transform to apply.</param>
        public void ApplyTransform(ref Matrix transform)
        {
            Matrix current = _transform;
            Matrix.Multiply(ref current, ref transform, out _transform);
            Apply();
        }

        /// <summary>
        /// Applies the specified transform before current canvas transform.
        /// </summary>
        /// <param name="transform">The transform to apply.</param>
        public void ApplyTransformBefore(ref Matrix transform)
        {
            Matrix current = _transform;
            Matrix.Multiply(ref transform, ref current, out _transform);
            Apply();
        }

        public void DrawSprite(float X, float Y, Texture2D Sprite, Color Color)
        {
            
            Batch.Draw(Sprite, new Vector2(X, Y) - Sprite.Size() / 2, Color);
        }

        public void DrawSprite(Vector2 spritePoint, Vector2 sizeOverride, Texture2D Sprite, Color colorTint)
        {
            Batch.Draw(Sprite, spritePoint - Sprite.Size() / 2, colorTint);

        }

        public void DrawText(SpriteFont font, Vector2 location, string text, FontOptions options, Color colorTint) 
        { 
            if (null == font) throw new ArgumentNullException("font");
			if (null == text) throw new ArgumentNullException("text");

            Batch.DrawString(font, text, location, colorTint);

	
        }

        public void Clear(Color color)
        {
            Batch.GraphicsDevice.Clear(color);
        }


        private void Begin()
        {
            
            Batch.Begin(SpriteSortMode.Immediate, _blending, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, _transform);
            _begined = true;
        }

        private void End()
        {
            Batch.End();
            _begined = false;
        }
      

        /// <summary>
        /// For canvas : does nothing
        /// </summary>
        protected override void CloseScope()
        {
            ; //nothing
            if (_begined)
            {
                End();
            }
        }

        /// <summary>
        /// Returns self, because it is root of scope chain.
        /// </summary>
        /// <value>The canvas instance.</value>
        protected override Canvas2D ReferredCanvas
        {
            get { return this; }
        }
    }
}
