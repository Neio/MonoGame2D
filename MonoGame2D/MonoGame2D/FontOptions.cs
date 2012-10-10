using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D
{
    /// <summary>
    /// Defines additional font options for drawing text
    /// </summary>
    public class FontOptions
    {
        private float _letterSpacing;
        private float _lineSpacing;

        /// <summary>
        /// Gets the letter spacing.
        /// </summary>
        /// <value>The letter spacing.</value>
        public float LetterSpacing
        {
            get { return _letterSpacing; }
        }

        /// <summary>
        /// Gets the line spacing.
        /// </summary>
        /// <value>The line spacing.</value>
        public float LineSpacing
        {
            get { return _lineSpacing; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FontOptions"/> class.
        /// </summary>
        /// <param name="letterSpacing">The letter spacing.</param>
        /// <param name="lineSpacing">The line spacing.</param>
        public FontOptions(float letterSpacing, float lineSpacing)
        {
            _letterSpacing = letterSpacing;
            _lineSpacing = lineSpacing;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FontOptions"/> class.
        /// </summary>
        /// <param name="lineSpacing">The line spacing.</param>
        public FontOptions(float lineSpacing)
            : this(0, lineSpacing)
        {
        }

        /// <summary>
        /// Default FontOptions, no extra spacings and scalings
        /// </summary>
        public static readonly FontOptions Default = new FontOptions(0, 0);
    }
}
