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
                var sp = new Sprite(Context.Load<Texture2D>("lava"));
                AddSprite(sp);
                _load = true;
            }
        }

        

    }
}
