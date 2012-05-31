using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D.Utils {

	/// <summary>
	/// Gradient stop point. It consists of position and color value
	/// </summary>
	public struct GradientStop {
		/// <summary>
		/// Position of gradient stop
		/// </summary>
		public float Position;

		/// <summary>
		/// Value of gradient color stop
		/// </summary>
		public Color Color;

		/// <summary>
		/// Initializes a new instance of the <see cref="GradientStop"/> struct.
		/// </summary>
		/// <param name="position">The position.</param>
		/// <param name="color">The color.</param>
		public GradientStop(float position, Color color) {
			Position = position;
			Color = color;
		}

	}
}
