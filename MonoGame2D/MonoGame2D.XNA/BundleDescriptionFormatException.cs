using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame2D.Utils {

	/// <summary>
	/// Defines special exception of collection description format parsing
	/// </summary>
	class BundleDescriptionFormatException : Exception {

		public BundleDescriptionFormatException()
			: base() {
		}

		public BundleDescriptionFormatException(string message, object arg0)
			: base(string.Format(message, arg0)) {

		}

		public BundleDescriptionFormatException(string message, params object[] args)
			: base(string.Format(message, args)) {
		}

	}
}
