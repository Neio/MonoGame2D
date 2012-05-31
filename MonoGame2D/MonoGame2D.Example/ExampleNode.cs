using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame2D.Example
{
    class ExampleNode : ScriptNode
    {
        bool _load=false;

        public override void LoadContents(Microsoft.Xna.Framework.Content.ContentManager Context)
        {
            base.LoadContents(Context);
            if (!_load )
            {
                var lava = Context.Load<Texture2D>("lava");
                var sp = new Sprite(lava);
                AddSprite(sp);
                //TimeLine.Repeat(20.0f, t =>
                //{
                //    sp.Position = new Microsoft.Xna.Framework.Vector2(0,300f * t.Progress);
                //});
                _load = true;
            }
        }

        

    }
}
