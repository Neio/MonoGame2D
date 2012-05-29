using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGame2D
{
    public class Node
    {

        #region Childen

        public Node Parent { get; internal set; }

        ContentManager _context = null;
        List<Node> _children = new List<Node>();

        public void AddChild(Node Node)
        {
            if (Node.Parent != null)
            {
                Node.Parent.RemoveChild(Node);
            }
            _children.Add(Node);
            Node.Parent = this;
            Node.LoadContents(_context);
        }

        public void RemoveChild(Node Node)
        {
            if (_children.Contains(Node))
            {
                Node.Parent = null;
                Node.UnloadContents();
                _children.Remove(Node);
            }
        }

        public void RemoveAllChildren()
        {
            foreach (Node node in _children.ToArray())
            {
                RemoveChild(node);
            }
        }
        #endregion

        public virtual void LoadContents(ContentManager Context)
        {
            _context = Context;
        }

        public virtual void UnloadContents()
        { 
            
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
