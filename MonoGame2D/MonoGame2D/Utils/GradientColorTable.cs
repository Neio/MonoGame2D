using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D.Utils {

	/// <summary>
	/// Implements gradient color table. It is useful for optimized heavy picking of color values from complex gradients.
	/// </summary>
	public class GradientColorTable {
		///<summary>Color sample values</summary>
		private Color[] _values;
		///<summary>Resolution of color table</summary>
		private float _resolution;
		///<summary>Duration of elementary sample</summary>
		private float _sampleLength;

		public GradientColorTable(GradientStop[] gradientPoints, int resolution) {
			if (null == gradientPoints) throw new ArgumentNullException("gradientPoints");			
			if (resolution < 1) throw new ArgumentException("Resolution should be at least 1", "resolution");

			_sampleLength = 1f / resolution;
			_resolution = (float)resolution;

			_values = new Color[(int)(resolution)];

			FillSamples(gradientPoints);
		}

		/// <summary>
		/// Fills the color sample values.
		/// </summary>
		/// <param name="gradientPoints">The gradient points.</param>
		private void FillSamples(GradientStop[] gradientPoints) {
			if (gradientPoints.Length < 2) throw new ArgumentException("Gradient points array should be at least 2 poitns length", "gradientPoints");

			for (int n = 0; n < gradientPoints.Length - 1; ++n) {
				int fromOffset = GetSampleOffsetFromPosition(gradientPoints[n].Position), toOffset = GetSampleOffsetFromPosition(gradientPoints[n + 1].Position);
				if (toOffset == fromOffset) {
					_values[toOffset] = gradientPoints[n].Color;
				} else {
					Color color1 = gradientPoints[n].Color;
					Color color2 = gradientPoints[n + 1].Color;
                    color2 = new Color( color2.R - color1.R, color2.G - color1.G, color2.B - color1.B, color2.A - color1.A);
					//Color.Multiply(ref color2, 1f / (toOffset - fromOffset), out color2);
                    color2 = color2 * (1f / (toOffset - fromOffset));
					//fill colors from/to offset
					for (int m = fromOffset; m <= toOffset; ++m) {
						_values[m] = color1;
                        color1 = new Color(color2.R + color1.R, color2.G + color1.G, color2.B + color1.B, color2.A + color1.A);
					}
				}
			}
		}

		/// <summary>
		/// Gets the sample offset from value position.
		/// </summary>
		/// <param name="position">The value position.</param>
		/// <returns>Table offset count</returns>
		private int GetSampleOffsetFromPosition(float position) {
			int offset = (int)(position * _resolution);
			return Math.Max(Math.Min(offset, _values.Length - 1), 0);
		}

		#region Public Inteface

		/// <summary>
		/// Gets the sample count in table.
		/// </summary>
		/// <value>The sample count in table.</value>
		public int SampleCount {
			get { return _values.Length; }
		}

		/// <summary>
		/// Gets the length of the value sample.
		/// </summary>
		/// <value>The length of the value sample.</value>
		public float SampleLength {
			get { return _sampleLength; }
		}

		/// <summary>
		/// Gets the <see cref="MonoGame2D.ColorU"/> from the specified index.
		/// </summary>
		/// <value>Color value of the specified color sample in table</value>
		public Color this[int index] {
			get { return _values[index]; }
			private set {
				_values[index] = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="MonoGame2D.ColorU"/> at the specified position.
		/// </summary>
		/// <value>Color value at the specified position</value>
		public Color this[float position] {
			get {
				return _values[GetSampleOffsetFromPosition(position)];
			}
		}

		#endregion
	}

}
