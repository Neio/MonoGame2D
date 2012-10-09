using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    public class Canvas2D
    {
        public Canvas2D(Microsoft.Xna.Framework.Graphics.SpriteBatch Batch, ref Matrix Matrix)
        {
            this.Batch = Batch;
            this.Matrix = Matrix;
        }

        public SpriteBatch Batch { get; private set; }

        public Matrix Matrix { get; private set; }

        public void DrawSprite(float X, float Y, Texture2D Sprite, Color Color)
        {
            
            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix);
            Batch.Draw(Sprite, new Vector2(X, Y) - Sprite.Size() / 2, Color);
            Batch.End();
        }

        public void DrawSprite(Vector2 spritePoint, Vector2 sizeOverride, Texture2D Sprite, Color colorTint)
        {
            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix);
            Batch.Draw(Sprite, spritePoint - Sprite.Size() / 2, colorTint);
            Batch.End();
        }

        public void Clear(Color color)
        {
            Batch.GraphicsDevice.Clear(color);
        }
    }
}
