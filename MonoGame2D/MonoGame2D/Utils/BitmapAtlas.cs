using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Vortex.IO;
using System.IO;
using Vortex.Debugging;
using Vortex.Resources;
using System.Threading;

namespace MonoGame2D.Utils {

	/// <summary>
	/// Collection of image source. Note, atlas is powered by System.Drawing.Bitmap and do not support extended DX files formats. Use png, bmp, jpg, gif or so for working with it
	/// </summary>
	public class BitmapAtlas : IDisposable {
		static readonly Size DEFAULT_BORDER_SIZE = new Size(4, 4);

		///<summary>Defines interface of collection image entry</summary>
		public interface AtlasEntry {
			/// <summary>
			/// Gets the size of the image.
			/// </summary>
			/// <value>The size of the image.</value>
			Size ImageSize { get; }

			/// <summary>
			/// Loads the image on specified surface.
			/// </summary>
			/// <param name="surface">The target surface.</param>
			/// <param name="region">The target surface region to fill with this entry.</param>
			void LoadOnSurface(Surface surface, Rectangle region);
		}

		private Dictionary<string, AtlasEntry> _map;

		#region Public Interface

		/// <summary>
		/// Initializes an empty instance of the <see cref="ImageCollection"/> class.
		/// </summary>
		public BitmapAtlas() {
			_map = new Dictionary<string, AtlasEntry>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageCollection"/> class populating it from other collection.
		/// </summary>
		/// <param name="otherCollection">The other collection.</param>
		public BitmapAtlas(BitmapAtlas otherCollection) {
			if (null == otherCollection) throw new ArgumentNullException("otherCollection");

			_map = new Dictionary<string, AtlasEntry>(otherCollection._map);
		}

		#endregion

		/// <summary>
		/// Checks is any entry with such name exists and removes it
		/// </summary>
		/// <param name="name">The name to check.</param>
		private void CheckRemoveNameKey(string name) {
			if (_map.ContainsKey(name)) {
				_map.Remove(name);
			}
		}

		#region Collection Interface

		/// <summary>
		/// Gets the number of atlas entries.
		/// </summary>
		/// <value>The count of atlas entries.</value>
		public int Count {
			get { return _map.Count; }
		}

		/// <summary>
		/// Clears atlas.
		/// </summary>
		public void Clear() {
			_map.Clear();
		}

		/// <summary>
		/// Removes atlas entry with specified name.
		/// </summary>
		/// <param name="name">The atlas entry name.</param>
		public void Remove(string name) {
			_map.Remove(name);
		}

		/// <summary>
		/// Adds the image into atlas.
		/// </summary>
		/// <param name="name">The name of image.</param>
		/// <param name="fileName">A valid name of the image file.</param>
		public void AddImage(string name, string fileName) {
			if (null == name) throw new ArgumentNullException("name");
			if (null == fileName) throw new ArgumentNullException("fileName");

			ResourceFileInfo file = new ResourceFileInfo(fileName);
			using (Stream stream = file.OpenStream()) {
				CheckRemoveNameKey(name);			
				Bitmap loadedBitmap = new Bitmap(stream);
				//add "loaded" implementation just for removing it at the end
				_map.Add(name, new LoadedBitmapAtlasEntry(loadedBitmap));
			}

			
		}

		/// <summary>
		/// Adds the bitmap into atlas.
		/// </summary>
		/// <param name="name">The name of image.</param>
		/// <param name="bitmap">The bitmap to add into atlas.</param>
		public void AddImage(string name, Bitmap bitmap) {
			if (null == name) throw new ArgumentNullException("name");
			if (null == bitmap) throw new ArgumentNullException("bitmap");
			CheckRemoveNameKey(name);

			_map.Add(name, new BitmapAtlasEntry(bitmap));
		}

		/// <summary>
		/// Gets the collection of atlas entry names.
		/// </summary>
		/// <value>The entry names.</value>
		public ICollection<string> Names {
			get { return _map.Keys; }
		}

		/// <summary>
		/// Gets the collection of entries.
		/// </summary>
		/// <value>The entries.</value>
		public ICollection<AtlasEntry> Entries {
			get { return _map.Values; }
		}

		/// <summary>
		/// Gets the <see cref="MonoGame2D.TextureAtlas.AtlasEntry"/> with the specified name.
		/// </summary>
		/// <value><see cref="MonoGame2D.TextureAtlas.AtlasEntry"/> with the specified name</value>
		public AtlasEntry this[string name] {
			get { return _map[name]; }
		}

		#endregion

		/// <summary>
		/// Create texture from images added into atlas.
		/// </summary>
		/// <returns><see cref="MonoGame2D.Texture"/> based on this texture atlas</returns>
		public Texture ToTexture() {
			return ToTexture(PixelFormat.DefaultAlpha, DEFAULT_BORDER_SIZE);
		}


		/// <summary>
		/// Create texture from images added into atlas.
		/// </summary>
		/// <returns><see cref="MonoGame2D.Texture"/> based on this texture atlas</returns>
		public Texture ToTexture(PixelFormat textureFormat) {
			return ToTexture(textureFormat, DEFAULT_BORDER_SIZE);
		}

		/// <summary>
		/// Create texture from images added into atlas.
		/// </summary>
		/// <param name="textureFormat">The texture format to use.</param>
		/// <param name="imageBorderSize">Size of the border around each image.</param>
		/// <returns>
		/// 	<see cref="MonoGame2D.Texture"/> based on this texture atlas
		/// </returns>
		public Texture ToTexture(PixelFormat textureFormat, Size imageBorderSize) {
			if (Count == 0) throw new InvalidOperationException("There are no images added into collection");

			using (Log.TraceUp("Assembing atlas texture, format={0}, border={1},{2}", textureFormat, imageBorderSize.Width, imageBorderSize.Height)) {
				//log into details all of images
				StringBuilder builder = new StringBuilder();
				builder.Append("Image ids:");
				foreach (string imageId in _map.Keys) {
					builder.AppendFormat("\n{0}", imageId);
				}
				Log.Details(builder.ToString());

				//create filler for atlas
				AtlasFiller atlasFiller = new AtlasFiller(this, imageBorderSize);

				//STEP 2. Define looking the best texture size if it is not specified
				List<ImageFillLocation> bestLocatedImages = null;
				float bestFillFactor = 0.0f;
				float bestSquareness = 0.0f;
				Size bestSize = Size.Empty;

				foreach (int height in AtlasFiller.GeneratePow2Array(64, 2048)) {
					foreach (int width in AtlasFiller.GeneratePow2Array(64, 2048)) {
						//List<ImageFillLocation> images = ImageLocator::GetImageFillInfoListFromCollection(collection, borderSize);
						List<ImageFillLocation> locatedImages;
						if (atlasFiller.Locate(new Rectangle(0, 0, width, height), out locatedImages)) {
							float fillFactor = (float)atlasFiller.TotalArea / (float)(width * height);
							float squareness = AtlasFiller.CalcSquareness(width, height);
							if (fillFactor > bestFillFactor || fillFactor == bestFillFactor && squareness > bestSquareness) {
								bestFillFactor = fillFactor;
								bestSquareness = squareness;
								bestLocatedImages = locatedImages;
								bestSize = new Size(width, height);
							}
							//Debug::Details("Option with size [{0}x{1}] - fill factor: {2:F2}%, squareness: {3:F2}", width, height, fillFactor * 100, squareness);
						}
					}
				}

				if (null == bestLocatedImages) {
					throw new Exception("Unable to locate all images onto texture");
				}

				Log.Success("Best case : [{0}x{1}] - fill factor: {2:F2}%, squareness: {3:F2}", bestSize.Width, bestSize.Height, bestFillFactor * 100, bestSquareness);


                if (!Game.IsMainThread)
                    ResourceLoadingLocker.Lock.AcquireWriterLock(-1);

				//STEP 3. Create texture with best found parameters
                Texture texture = TryCreateTexture(bestSize, 1);//try 10 times

                if (!Game.IsMainThread)
                    ResourceLoadingLocker.Lock.ReleaseWriterLock();

				//STEP 4. One-by-one load images on texture
				foreach (ImageFillLocation location in bestLocatedImages) {
					AtlasEntry entry = this[location.Name];
					entry.LoadOnSurface(texture.Surface, location.ImageRegion);
					texture.Sprites.Add(location.Name, new Rect(location.ImageRegion));

                    if (!Game.IsMainThread)
                        Thread.Sleep(1);
				}


				return texture;
			}
		}

        private Texture TryCreateTexture(Size bestSize, int tryTime)
        {
            if (tryTime == 0)
            { 
                //failed
                Log.Error("Create texture failed");
                throw new Exception("Create Texture Failed.");
            }
            Texture texture;
            try
            {
                texture = new Texture(bestSize.Width, bestSize.Height, PixelFormat.A8R8G8B8);
            }
            catch
            {
                //retry once
                Log.Error("Create texture failed, retry again");
                texture = TryCreateTexture(bestSize, tryTime--);
            }
            return texture;
        }

		#region IDisposable Members

		/// <summary>
		/// Performs freeing, releasing, or resetting unmanaged resources of atlas entries.
		/// </summary>
		public void Dispose() {
			foreach (AtlasEntry entry in Entries) {
				IDisposable dispEntry = entry as IDisposable;
				if (null != dispEntry) {
					dispEntry.Dispose();
				}
			}
		}

		#endregion
	}

	//------------------------------------------------------------------------------------------------

	/// <summary>
	/// Implements atlas entry for bitmap images
	/// </summary>
	class BitmapAtlasEntry : BitmapAtlas.AtlasEntry {
		protected Bitmap _bitmap;

		public BitmapAtlasEntry(Bitmap bitmap) {
			_bitmap = bitmap;
		}

		#region AtlasEntry Members

		/// <summary>
		/// Gets the size of the bitmap image.
		/// </summary>
		/// <value>The size of the bitmap image.</value>
		public Size ImageSize {
			get { return _bitmap.Size; }
		}

		/// <summary>
		/// Loads the bitmap image on specified surface.
		/// </summary>
		/// <param name="surface">The target surface.</param>
		/// <param name="region">The target surface region to fill with this entry.</param>
		public void LoadOnSurface(Surface surface, Rectangle region) {
			if (null == surface) throw new ArgumentNullException("surface");

			surface.SetBitmapData(region, _bitmap);
		}

		#endregion
	}

	/// <summary>
	/// Bitmap atlas entry loaded in atlas from file
	/// </summary>
	class LoadedBitmapAtlasEntry : BitmapAtlasEntry, IDisposable {
		public LoadedBitmapAtlasEntry(Bitmap bitmap) : base (bitmap) {
		}

		#region IDisposable Members

		///<summary>Disposes loaded bitmap resource</summary>
		public void Dispose() {
			_bitmap.Dispose();
		}

		#endregion
	}
}
