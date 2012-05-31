using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D
{
    public interface INode
    {
        void AddChild(Node Node);
        void RemoveChild(Node Node);
        void RemoveAllChildren();
    }
}
