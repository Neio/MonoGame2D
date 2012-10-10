using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace MonoGame2D
{
    /// <summary>
    /// Defines line stroke pattern. There are some prepared strokes but you can define own pattern. 
    /// Only one restriction for correct custom pattern display: it should be placed from left to right across whole texture!
    /// Note, you can put even animated pattern, because sprite provider resolves required sprite runtime
    /// </summary>
    public class LineStroke
    {

        public static void Init(GraphicsDevice device)
        {
            {
                Texture2D blank = new Texture2D(device,1, 1, false, SurfaceFormat.Color);
                blank.SetData(new[] { Color.White.ToArgb() });
                Solid = new LineStroke(blank);
                Dashed = new LineStroke(blank);

                CombinedDashed = new LineStroke(blank);
                DotDashed = new LineStroke(blank);
                Dotted = new LineStroke(blank);
            }

            {
                Texture2D blank = new Texture2D(device, 1, 3, false, SurfaceFormat.Color);
                blank.SetData(new[] { Color.White.ToArgb(), Color.Transparent.ToArgb(), Color.White.ToArgb() });
                Dual = new LineStroke(blank);
            }

            {
                Texture2D blank = new Texture2D(device, 1, 5, false, SurfaceFormat.Color);
                blank.SetData(new[] { Color.FromArgb(20, 255, 255, 255).ToArgb(), Color.White.ToArgb(), Color.White.ToArgb(), Color.White.ToArgb(), Color.FromArgb(20, 255, 255, 255).ToArgb() });
                Smooth = new LineStroke(blank);
            }

            {
                Texture2D blank = new Texture2D(device, 4, 1, false, SurfaceFormat.Color);
                blank.SetData(new[] { Color.White.ToArgb(), Color.Transparent.ToArgb(), Color.White.ToArgb(), Color.Transparent.ToArgb() });
                TinyDashed = new LineStroke(blank);
            }
        }

        private Texture2D _patternProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LineStroke"/> class.
        /// </summary>
        /// <param name="patternProvider">The pattern provider.</param>
        public LineStroke(Texture2D patternProvider)
        {
            if (null == patternProvider) throw new ArgumentException("patternProvider");
            _patternProvider = patternProvider;
        }

        /// <summary>
        /// Gets the pattern provider for this stroke.
        /// </summary>
        /// <value>The pattern provider.</value>
        public Texture2D PatternProvider
        {
            get { return _patternProvider; }
        }

        #region Presets

        ///<summary>Solid line stroking</summary>
        public static LineStroke Solid { get; internal set; }

        ///<summary>Dual line stroking</summary>
        public static LineStroke Dual { get; internal set; }

        ///<summary>Smooth line stroking</summary>
        public static LineStroke Smooth { get; internal set; }

        ///<summary>Tiny dashed line stroking</summary>
        public static LineStroke TinyDashed { get; internal set; }

        ///<summary>Dashed line stroking</summary>
        public static LineStroke Dashed { get; internal set; }

        ///<summary>Combined dashed stroking</summary>
        public static LineStroke CombinedDashed { get; internal set; }

        ///<summary>Dot-dashed stroking</summary>
        public static LineStroke DotDashed { get; internal set; }

        ///<summary>Dotted stroking</summary>
        public static LineStroke Dotted { get; internal set; }

        #endregion

    }
}
