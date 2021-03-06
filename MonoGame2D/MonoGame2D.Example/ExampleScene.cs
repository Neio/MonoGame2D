﻿using System;
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
#if WINDOWS
		   UseEmbeddedResouce = true;
           resourceManager = ExampleSceneResource.ResourceManager;
#else
			this.RootDirectory = "Resources";
			UseEmbeddedResouce = false;
           //UseEmbeddedResouce = true;
           //resourceManager = ExampleSceneResource.ResourceManager;
#endif

        }

        
        public override void LoadContents(IResourceCollection Context)
        {
            base.LoadContents(Context);

         


            var eNode = new ExampleNode("teddy_bear_toy");

            AddChild(eNode);

            eNode.Scale = new Vector2(2.0f);
            TimeLine.Repeat(20.0f, t =>
            {
                eNode.Location = new Microsoft.Xna.Framework.Vector2(500f * t.Progress);
                eNode.Scale = new Vector2(1.0f + 0.5f * (float)Math.Sin(Math.PI * 20 * t.Progress));
            });

            var eNode2 = new ExampleNode("mouse");
            var dyNode = new DynamicElement(eNode2);
            dyNode.Location = new Vector2(500, 300);
			dyNode.Scale = new Vector2(0.4f);
            AddChild(dyNode);

            List<Texture2D> list = new List<Texture2D>();
            for (int i = 0; i < 9; i++)
            {
                var l = Context.Get<Texture2D>(String.Format("c{0:0000}", i));
                list.Add(l);
            }
            for (int i = 9; i > 0; i--)
            {
                var l = Context.Get<Texture2D>(String.Format("c{0:0000}", i));
                list.Add(l);
            }


            fish = new Animation(new SpriteList(list), true, 30);
            AddChild(fish);
            fish.Location = new Vector2(100, 100);
            
        }

        Animation fish;
        public override void DrawForeground(Canvas2D canvas)
        {
            canvas.DrawText(GameFont.Default, new Vector2(20, 20), "Hello", Color.Yellow);

            using (canvas <= new Translation(250, 100) <= new Rotation(delta))
            {
                
                canvas.DrawLine(new Vector2(0, 0), new Vector2(100, 0), new LineStyle(2, LineStroke.Smooth), Color.Blue);
            }

            using (canvas <= new Translation(500, 100) <= new Rotation(delta))
            {
                canvas.DrawFilledRect(new Rect(-100, -100, 100, 100), Color.Yellow);
                //canvas.DrawRect(new Rect(0, 0, 100, 100), new LineStyle(1), Color.Yellow);
                
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
