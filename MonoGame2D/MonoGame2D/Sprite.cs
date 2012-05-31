using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public struct Sprite
    {
        public Vector2 Origin;
        public Texture2D Texture;
        public float Rotation;
        public float Scale;
        public Vector2 Position;

        public Sprite(Texture2D texture, Vector2 origin, float rotation)
        {
            this.Texture = texture;
            this.Origin = origin;
            this.Rotation = rotation;
            this.Scale = 1.0f;
            this.Position = new Vector2(0, 0);
        }

        public Sprite(Texture2D texture, Vector2 origin)
        {
            this.Texture = texture;
            this.Origin = origin;
            this.Rotation = 0; this.Scale = 1.0f; this.Position = new Vector2(0, 0);
        }

        public Sprite(Texture2D sprite)
        {
            Texture = sprite;
            Origin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            Rotation = 0;
            this.Scale = 1.0f; this.Position = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch canvas)
        {
            canvas.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
