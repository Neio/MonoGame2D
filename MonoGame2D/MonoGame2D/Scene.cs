using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    public class Scene : Game
    {
        List<Node> _children = new List<Node>();

        public void AddChild(Node Node)
        {
            if (Node.Parent != null)
            {
                Node.Parent.RemoveChild(Node);
            }
            _children.Add(Node);
            Node.LoadContents(Content);
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

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var child in _children)
            {
                child.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (var child in _children)
            {
                child.DrawContent(GraphicsDevice, gameTime);
            }
        }

    }
}
