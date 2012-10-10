using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D
{
    /// <summary>
    /// Contains information about line drawing style
    /// </summary>
    public class LineStyle
    {
        private float _width;
        private LineStroke _stroke;

        /// <summary>
        /// Initializes a new instance of the <see cref="LineStyle"/> class. Stroke is Solid.
        /// </summary>
        /// <param name="width">The width of line.</param>
        public LineStyle(float width)
            : this(width, LineStroke.Solid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineStyle"/> class.
        /// </summary>
        /// <param name="width">The width of line.</param>
        /// <param name="stroke">The stroke with line shading pattern.</param>
        public LineStyle(float width, LineStroke stroke)
        {
            if (null == stroke) throw new ArgumentNullException("stroke");

            _width = width;
            _stroke = stroke;
        }

        /// <summary>
        /// Gets the width of line.
        /// </summary>
        /// <value>The width of line.</value>
        public float Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the stroke to use for line shading.
        /// </summary>
        /// <value>The line stroke.</value>
        public LineStroke Stroke
        {
            get { return _stroke; }
        }
    }
}
