using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame2D.Utils;

namespace MonoGame2D.Example
{
    class ExampleScene : Scene
    {
        public ExampleScene() {
        }

        public override void LoadContents(Microsoft.Xna.Framework.Content.ContentManager Context)
        {
            base.LoadContents(Context);

            var eNode = new ExampleNode();

            AddChild(eNode);

            TimeLine.Repeat(20.0f, t =>
            {
                eNode.Location = new Microsoft.Xna.Framework.Vector2(500f * t.Progress);
                //eNode.Rotation = (float)Math.PI * 5 * t.Progress;
            });

            List<Texture2D> list = new List<Texture2D>();
            for (int i = 5; i < 17; i++)
            {
                var l = Context.Load<Texture2D>(String.Format("Fish\\c{0:0000}", i));
                list.Add(l);
            }
            for (int i = 0; i < 5; i++)
            {
                var l = Context.Load<Texture2D>(String.Format("Fish\\c{0:0000}", i));
                list.Add(l);
            }
            var fish = new Animation(new SpriteList(list), true, 30);
            AddChild(fish);
            fish.Location = new Vector2(100, 100);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch graphic, Microsoft.Xna.Framework.GameTime GameTime, ref Microsoft.Xna.Framework.Matrix Transform)
        {
            graphic.GraphicsDevice.Clear(Color.Black);
            base.Draw(graphic, GameTime, ref Transform);
        }

    }
}
