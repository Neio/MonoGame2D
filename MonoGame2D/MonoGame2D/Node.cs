using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class Node
    {

        public Node Parent { get; internal set; }

        List<Node> _children = new List<Node>();

        public void AddChild(Node Node)
        {
            if (Node.Parent != null)
            {
                Node.Parent.RemoveChild(Node);
            }

            _children.Add(Node);
        }

        public void RemoveChild(Node Node)
        {
            if (_children.Contains(Node))
            {
                Node.Parent = null;
                _children.Remove(Node);
            }
        }

        public virtual void Draw(GraphicsDevice graphic, GameTime TimeDelta)
        { 
            
        }

        public virtual void DrawContent(GraphicsDevice graphic, GameTime TimeDelta)
        { 
        
        }

        public virtual void DrawChildren(GraphicsDevice graphic, GameTime TimeDelta)
        { 
        
        }

        public virtual void Update(GameTime TimeDelta)
        { 
            
        }
    }



}
