using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{

    /// <summary>
    /// Makes a graph snapshot for element update to avoid graph modification conflict
    /// </summary>
    class NodeShadow
    {
        const int DEFAULT_CAPACITY = 256;

        List<Node> _elements = new List<Node>(DEFAULT_CAPACITY);

        /// <summary>
        /// Enumeration of all elements are currently in snapshot
        /// </summary>
        /// <value>The elements.</value>
        public IEnumerable<Node> Elements
        {
            get { return _elements; }
        }

        /// <summary>
        /// Clears snapshot elements collection
        /// </summary>
        public void Clear()
        {
            _elements.Clear();
        }

        /// <summary>
        /// Adds all elements starting specified one to snapshot
        /// </summary>
        /// <param name="element">The element.</param>
        public void AddRoot(Node element)
        {
            if (null != element)
            {
                _elements.Add(element);
                foreach (Node el in element.Children)
                {
                    AddRoot(el);
                }
            }
        }

        /// <summary>
        /// Invokes update for all items in snapshot
        /// </summary>
        /// <param name="timeDelta">The time delta.</param>
        public void Update(GameTime gameTime)
        {
            foreach (Node element in _elements)
            {
                element.Update(gameTime);
            }
        }

    }
}
