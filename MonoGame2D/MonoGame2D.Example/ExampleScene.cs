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

            eNode.Scale = new Vector2(2.0f);
            TimeLine.Repeat(20.0f, t =>
            {
                eNode.Location = new Microsoft.Xna.Framework.Vector2(500f * t.Progress);
                eNode.Scale = new Vector2(1.0f + 0.5f * (float)Math.Sin(Math.PI * 20 * t.Progress));
            });

            var eNode2 = new ExampleNode();
            var dyNode = new DynamicElement(eNode2);
            dyNode.Location = new Vector2(500, 300);
            AddChild(dyNode);

            List<Texture2D> list = new List<Texture2D>();
            for (int i = 0; i < 9; i++)
            {
                var l = Context.Load<Texture2D>(String.Format("Fish\\c{0:0000}", i));
                list.Add(l);
            }
            for (int i = 9; i > 0; i--)
            {
                var l = Context.Load<Texture2D>(String.Format("Fish\\c{0:0000}", i));
                list.Add(l);
            }


            fish = new Animation(new SpriteList(list), true, 30);
            AddChild(fish);
            fish.Location = new Vector2(100, 100);
            
        }

        Animation fish;
        public override void DrawForeground(Canvas2D canvas)
        {
            using (canvas <= new Translation(100, 100) <= new Rotation(delta))
            {
                canvas.DrawLine(new Vector2(0, 0), new Vector2(100, 0), new LineStyle(2, LineStroke.Smooth), Color.Blue);
            }
        }

        float delta = 0;
        public override void Update(float gameTime)
        {
            base.Update(gameTime);
            delta += gameTime;
            fish.Rotation = delta;
        }

    }
}
