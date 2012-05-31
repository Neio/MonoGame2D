using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MonoGame2D.Utils {

	/// <summary>
	/// Implements Atlas filling algorithm
	/// </summary>
	class AtlasFiller {
		private static int[] SideValues = new int[] { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192 };
		private BitmapAtlas _atlas;
		private Size _safeBorder;

		public int Count { get; private set; }
		public int MaxWidth { get; private set; }
		public int MaxHeight { get; private set; }
		public int MinWidth { get; private set; }
		public int MinHeight { get; private set; }
		public int MaxArea { get; private set; }
		public int MinArea { get; private set; }
		public int TotalArea { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AtlasFiller"/> class.
		/// </summary>
		/// <param name="atlas">The source atlas.</param>
		/// <param name="safeBorder">The safe border to use.</param>
		public AtlasFiller(BitmapAtlas atlas, Size safeBorder) {
			_atlas = atlas;
			_safeBorder = safeBorder;

			foreach(BitmapAtlas.AtlasEntry entry in atlas.Entries) {
				Size size = entry.ImageSize + safeBorder;
				int area = size.Width * size.Height, width = size.Width, height = size.Height;

				if (area > MaxArea) MaxArea = area;		
				if (size.Width > MaxWidth) MaxWidth = size.Width;
				if (size.Height > MaxHeight) MaxHeight = size.Height;

				if (area < MinArea || MinArea==0) MinArea = area;
				if (size.Width < MinWidth || MinWidth==0) MinWidth = size.Width;
				if (size.Height < MinHeight || MinHeight==0) MinHeight = size.Height;

				TotalArea = TotalArea + area;
				Count = Count + 1;
			}
		}

		/// <summary>
		/// Calculates the squareness.
		/// </summary>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>Squarenes ratio</returns>
		public static float CalcSquareness(int width, int height) {
			float fpWidth = (float)width, fpHeight = (float)height;
			if (fpWidth > fpHeight) {
				return fpHeight / fpWidth;
			} else {
				if (fpHeight != 0) {
					return fpWidth / fpHeight;
				} else {
					return 0.0f;
				}
			}
		}

		/// <summary>
		/// Generates the pow 2 size array.
		/// </summary>
		/// <param name="from">From value.</param>
		/// <param name="to">To value.</param>
		/// <returns>Array of sizes</returns>
		public static int[] GeneratePow2Array(int from, int to) {
			List<int> list = new List<int>();
			foreach (int pow2value in SideValues) {
				if (pow2value >= from && pow2value <= to) {
					list.Add(pow2value);
				}
			}
			return list.ToArray();
		}

		/// <summary>
		/// Picks the pow2 value.
		/// </summary>
		/// <param name="value">The real value.</param>
		/// <returns>Nearest pow2 value</returns>
		public static int PickPow2Value(int value) {
			foreach (int pow2value in SideValues) {
				if (pow2value >= value) {
					return pow2value;
				}
			}
			throw new Exception("Unexpected failure of PickPow2Value function");
		}


		/// <summary>
		/// Locates the images from atlas in specified free rect.
		/// </summary>
		/// <param name="freeRect">The free rect.</param>
		/// <param name="resultList">The result list of properly located images.</param>
		/// <returns><c>true</c> if all of images were located properly; otherwise <c>false</c></returns>
		public bool Locate(Rectangle freeRect, out List<ImageFillLocation> resultList) {
			List<ImageFillLocation> sourceList = GetImageFillInfoListFromCollection();
			resultList = LocateImagesInRegion(sourceList, freeRect);
			return sourceList.Count == 0;
		}

		/// <summary>
		/// Locates the specified images in given free region.
		/// </summary>
		/// <param name="sourceList">The list of source image locations.</param>
		/// <param name="freeRect">The free rect.</param>
		/// <returns></returns>
		private List<ImageFillLocation> LocateImagesInRegion(List<ImageFillLocation> sourceList, Rectangle freeRect) {
			List<ImageFillLocation> resultList = new List<ImageFillLocation>();

			if (sourceList.Count == 0) {
				return resultList;
			}

			ImageFillLocation currentRect = RejectBestFitRect(sourceList, freeRect);
			if (currentRect == null) {
				return resultList;
			}

			currentRect.Offset(freeRect.Left, freeRect.Top);
			resultList.Add(currentRect);


			//now lets divide free space on two stripes: vertical/horizontal	
			Rectangle stripe1 = new Rectangle(currentRect.Right, currentRect.Top, freeRect.Width - currentRect.Width, currentRect.Height);
			if (stripe1.Width > 0 && stripe1.Height > 0) {
				resultList.AddRange(LocateImagesInRegion(sourceList, stripe1));
			}

			Rectangle stripe2 = new Rectangle(freeRect.Left, currentRect.Bottom, freeRect.Width, freeRect.Height - currentRect.Height);
			if (stripe2.Width > 0 && stripe2.Height > 0) {
				resultList.AddRange(LocateImagesInRegion(sourceList, stripe2));
			}

			return resultList;
		}

		/// <summary>
		/// Generates the image fill info list from collection.
		/// </summary>
		/// <returns>List of image fill info</returns>
		private List<ImageFillLocation> GetImageFillInfoListFromCollection() {
			List<ImageFillLocation> list = new List<ImageFillLocation>();

			foreach (String name in _atlas.Names) {
				BitmapAtlas.AtlasEntry entry = _atlas[name];
				list.Add(new ImageFillLocation(name, entry.ImageSize, _safeBorder));
			}

			list.Sort();
			list.Reverse();

			return list;
		}

		/// <summary>
		/// Rejects the best fit rect from list.
		/// </summary>
		/// <param name="sourceList">The source list (modified).</param>
		/// <param name="freeRect">The free rect.</param>
		/// <returns></returns>
		private ImageFillLocation RejectBestFitRect(List<ImageFillLocation> sourceList, Rectangle freeRect) {
			for (int n = 0; n < sourceList.Count; ++n) {
				if (sourceList[n].Height <= freeRect.Height && sourceList[n].Width <= freeRect.Width) {
					ImageFillLocation fillInfo = sourceList[n];
					sourceList.RemoveAt(n);
					return fillInfo;
				}
			}
			return null;
		}
	}

}
