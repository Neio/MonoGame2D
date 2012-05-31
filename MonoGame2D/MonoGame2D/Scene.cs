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
        
        }

        public virtual void Activate()
        { }

        public virtual void Deactivate() 
        { }
    }
}
