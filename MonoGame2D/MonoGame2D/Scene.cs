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

       

        public virtual void Activate()
        { }

        public virtual void Deactivate() 
        { }
    }
}
