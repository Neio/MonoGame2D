using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class Scene : ScriptNode, INode
    {
        public Scene()
        {
            //BackgroundColor = Color.Black;
        }

       // protected Color BackgroundColor { get; set; }

        protected virtual void Draw(Canvas2D canvas)
        {
            //canvas.Batch.GraphicsDevice.Clear(BackgroundColor);
        }

        public override void Draw(SpriteBatch graphic, GameTime GameTime, ref Matrix Transform)
        {
            base.Draw(graphic, GameTime, ref Transform);
            Draw(new Canvas2D(graphic, ref Transform));
        }

        public virtual void Activate()
        { }

        public virtual void Deactivate() 
        { }
    }
}
