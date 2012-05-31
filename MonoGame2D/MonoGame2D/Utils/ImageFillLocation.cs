using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MonoGame2D.Utils {

	/// <summary>
	/// Information about fill location of entry
	/// </summary>
	class ImageFillLocation : IComparable<ImageFillLocation> {
		private string _entryName;
		private Rectangle _region;
		private Rectangle _imageRegion;

		public ImageFillLocation(string entryName, Size imageSize, Size borderSize) {
			_entryName = entryName;
			_region = new Rectangle(0, 0, imageSize.Width + borderSize.Width, imageSize.Height + borderSize.Height);
			_imageRegion = new Rectangle(borderSize.Width / 2, borderSize.Height / 2, imageSize.Width, imageSize.Height);
		}

		/// <summary>
		/// Gets the name of entry.
		/// </summary>
		/// <value>The entry name.</value>
		public string Name {
			get { return _entryName; }
		}

		/// <summary>
		/// Gets the region occupied by this entry.
		/// </summary>
		/// <value>The region occupied by this entry.</value>
		public Rectangle Region {
			get { return _region; }
		}

		/// <summary>
		/// Gets the actual image region inside in Region.
		/// </summary>
		/// <value>The actual image region.</value>
		public Rectangle ImageRegion {
			get { return _imageRegion; }
		}


		/// <summary>
		/// Offsets the specified entry location.
		/// </summary>
		/// <param name="offsetX">The offset X.</param>
		/// <param name="offsetY">The offset Y.</param>
		public void Offset(int offsetX, int offsetY) {
			_region.Offset(offsetX, offsetY);
			_imageRegion.Offset(offsetX, offsetY);
		}

		/// <summary>
		/// Gets the left of entry region.
		/// </summary>
		/// <value>The left of entry region.</value>
		public int Left {
			get { return _region.Left; }
		}

		/// <summary>
		/// Gets the top of entry region.
		/// </summary>
		/// <value>The top of entry region.</value>
		public int Top {
			get { return _region.Top; }
		}

		/// <summary>
		/// Gets the right of entry region.
		/// </summary>
		/// <value>The right of entry region.</value>
		public int Right {
			get { return _region.Right; }
		}

		/// <summary>
		/// Gets the bottom of entry region.
		/// </summary>
		/// <value>The bottom of entry region.</value>
		public int Bottom {
			get { return _region.Bottom; }
		}

		/// <summary>
		/// Gets the width of entry region.
		/// </summary>
		/// <value>The width of entry region.</value>
		public int Width {
			get { return _region.Width; }
		}

		/// <summary>
		/// Gets the height of entry region.
		/// </summary>
		/// <value>The height of entry region.</value>
		public int Height {
			get { return _region.Height; }
		}


		#region IComparable<EntryLocation> Members

		/// <summary>
		/// Compares the current entry location with another object of the same type.
		/// </summary>
		/// <param name="other">An image location to compare with this object.</param>
		/// <returns>
		/// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>.
		/// </returns>
		public int CompareTo(ImageFillLocation other) {
			int compareValue = _region.Height.CompareTo(other._region.Height);
			if (compareValue != 0) {
				return compareValue;
			} else {
				return _region.Width.CompareTo(other._region.Width);
			} 
		}

		#endregion
	}
}
