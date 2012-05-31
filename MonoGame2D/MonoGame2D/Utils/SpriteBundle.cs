using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using OpenTK.Graphics.OpenGL;

namespace MonoGame2D.Utils {

	/// <summary>
	/// Implements sprite/sprite animaiton collection.
	/// Bundle is easy to use resource container, which allows groups multiple resources. It simplifies their lifetime management.
	/// Access to collection element is provided by string key: id.
	/// </summary>
	public class SpriteBundle : IBundle<Texture2D> {
		
		///<summary>Contains information about texture source name (required for unloading parts of texture)</summary>
		class TextureContainer {
			public string SourceName { get; private set; }
			public Texture Texture { get; private set; }

			public TextureContainer(string sourceName, Texture texture) {
				this.SourceName = sourceName;
				this.Texture = texture;
			}
		}

        private Dictionary<String, Texture2D> _spriteMap = new Dictionary<string, Texture2D>();
		private List<TextureContainer> _textures = new List<TextureContainer>();

		/// <summary>
		/// Initializes an empty instance of the <see cref="SpriteBundle"/> class.
		/// </summary>
		public SpriteBundle() {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SpriteBundle"/> class with specified source file.
		/// </summary>
		/// <param name="descriptionFileName">Name of the sprite description source file.</param>
		public SpriteBundle(string descriptionFileName) {
			LoadContent(descriptionFileName);
		}

		/// <summary>
		/// Removes all sprites with maps specified texture.
		/// </summary>
		/// <param name="texture">The texture instance.</param>
		private void RemoveAllSpritesOfTexture(Texture texture) {
			List<string> keys = new List<string>(_spriteMap.Keys);

			foreach (string key in keys) {
                Texture2D sprite = _spriteMap[key];
				if (sprite.Texture == texture) {
					_spriteMap.Remove(key);
				}
			}
		}


		#region IBundle<Sprite> Members
		
		/// <summary>
		/// Gets the <see cref="MonoGame2D.Sprite"/> with the specified id.
		/// </summary>
		/// <value><see cref="MonoGame2D.Sprite"/> instance if sprite exists, otherwise null</value>
        public Texture2D this[string id]
        {
			get {
				if (id == null) throw new ArgumentNullException("id");
                Texture2D sprite;
				_spriteMap.TryGetValue(id, out sprite);
				return sprite;
			}
		}

		/// <summary>
		/// Loads sprite content into collection by description.
		/// </summary>
		/// <param name="descriptionFileName">Name of the descrition file.</param>
		public void LoadContent(string descriptionFileName) {
			Watcher.AssertFileName(descriptionFileName);

			string directoryName = System.IO.Path.GetDirectoryName(descriptionFileName);

			ResourceFileInfo file = new ResourceFileInfo(descriptionFileName);

			using (Stream fileStream = file.OpenStream()) {
				XmlSerializer serializer = new XmlSerializer(typeof(AtlasNode));
				AtlasNode atlas = serializer.Deserialize(fileStream) as AtlasNode;

				foreach (TextureNode textureNode in atlas.Textures) {
					if (textureNode.Images.Count > 0) {
						Texture texture;
						if (textureNode.Images.Count == 1) {
							textureNode.Images[0].Validate();
							texture = new Texture(MakePath(directoryName, textureNode.Images[0].File), textureNode.Format);
						} else {
							//assemble texture using bitmap atlas
							BitmapAtlas atlasAssembler = new BitmapAtlas();

							foreach (ImageNode imageNode in textureNode.Images) {
								//check node's consistency and validate it
								imageNode.Validate();
								atlasAssembler.AddImage(imageNode.Id, MakePath(directoryName, imageNode.File));
							}

							//add texture to resource list
							texture = atlasAssembler.ToTexture(textureNode.Format);
						}
						_textures.Add(new TextureContainer(descriptionFileName, texture));

						//add sprites into collection map
						foreach (ImageNode imageNode in textureNode.Images) {
							Sprite imageSprite = texture.Sprites[imageNode.Id];
							Vector2 origin;
							imageNode.GetOrigin(imageSprite, out origin);
							imageSprite = imageSprite.MoveOriginTo(origin);
							Add(imageNode.Id, imageSprite);

							//check to reffer sprites
							foreach (SpriteNode spriteNode in imageNode.Sprites) {
								spriteNode.Validate();
								//prepare sprite
								Sprite subSprite = imageSprite.Cut(spriteNode.Region);
								subSprite = subSprite.MoveOriginTo(spriteNode.Origin);
								Add(spriteNode.Id, subSprite);
							}
						}
					}
				}
			}
		}

		private string MakePath(string directoryName, string fileName) {
			if (fileName.StartsWith(@"\") || fileName.StartsWith(@"/")) {
				return fileName.Substring(1);
			} else {
				return string.IsNullOrEmpty(directoryName) ? fileName : directoryName + @"\" + fileName;
			}
		}

		/// <summary>
		/// Unloads the content loaded previously with that description file.
		/// </summary>
		/// <param name="descriptionFileName">Name of the description file.</param>
		public void UnloadContent(string descriptionFileName) {
			//iterate through
			foreach (TextureContainer container in _textures.ToArray()) {
				if (container.SourceName.Equals(descriptionFileName)) {
					RemoveAllSpritesOfTexture(container.Texture);
					_textures.Remove(container);
					container.Texture.Dispose();
				}
			}
		}

		/// <summary>
		/// Determines whether this collection contains element with the specified id.
		/// </summary>
		/// <param name="id">The id of element.</param>
		/// <returns>
		/// 	<c>true</c> if collection contains element with the specified id; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(string id) {
			return _spriteMap.ContainsKey(id);
		}

		/// <summary>
		/// Adds the specified sprite element and binds it with specified id.
		/// </summary>
		/// <param name="id">The id of sprite element.</param>
		/// <param name="element">The sprite element.</param>
        public void Add(string id, Texture2D element)
        {
			_spriteMap.Add(id, element);
		}

		/// <summary>
		/// Removes from collection the sprite element with specified id.
		/// </summary>
		/// <param name="id">The id of element.</param>
		public void Remove(string id) {
			_spriteMap.Remove(id);
		}

		/// <summary>
		/// Clears this collection. All resources will be removed.
		/// </summary>
		public void Clear() {
			foreach (TextureContainer container in _textures) {
				container.Texture.Dispose();
			}

			_textures.Clear();
			_spriteMap.Clear();
		}

		/// <summary>
		/// Gets the enumeration of collection elements ids.
		/// </summary>
		/// <value>Elements ids.</value>
		public IEnumerable<string> Ids {
			get { return _spriteMap.Keys; }
		}

		#endregion

		#region Desc format nodes

		/// <summary>
		/// Represents Sprite node of SpriteBundle content XML
		/// </summary>
		[XmlType("Sprite")]
		public class SpriteNode { 
			[XmlAttribute] 
			public string Id;
			[XmlAttribute]
			public int X;
			[XmlAttribute]
			public int Y;
			[XmlAttribute]
			public int Width;
			[XmlAttribute]
			public int Height;
			[XmlAttribute]
			public int OriginX = int.MinValue;
			[XmlAttribute]
			public int OriginY = int.MinValue;

			public Rect Region {
				get { return Rect.FromBox(X, Y, Width, Height); }
			}

			public Vector2 Origin {
				get {
					return new Vector2(
						OriginX == int.MinValue ? (float)Width / 2 : OriginX,
						OriginY == int.MinValue ? (float)Height / 2 : OriginY
					);
				}
			}

			public  void Validate() {
				if (string.IsNullOrEmpty(Id)) {
					throw new BundleDescriptionFormatException("Sprite Id should be not empty string");
				}
			}
		}

		/// <summary>
		/// Represents Image node of SpriteBundle content XML
		/// </summary>
		[XmlType("Image")]
		public class ImageNode {
			[XmlAttribute]
			public string Id;
			[XmlAttribute]
			public string File;
			[XmlAttribute]
			public int OriginX = int.MinValue;
			[XmlAttribute]
			public int OriginY = int.MinValue;

			public List<SpriteNode> Sprites;

            public void GetOrigin(Texture2D sprite, out Vector2 result)
            {
				result = new Vector2(
					OriginX == int.MinValue ? sprite.Origin.X : OriginX,
					OriginY == int.MinValue ? sprite.Origin.Y : OriginY
				);
			}

			public void Validate() {
				if (string.IsNullOrEmpty(File)) {
					throw new BundleDescriptionFormatException("Image File should be valid relative path to image file");
				}

				if (string.IsNullOrEmpty(Id)) {
					Id = File;
				}
			}
		}

		/// <summary>
		/// Represents texture node of SpriteBundle content XML
		/// </summary>
		[XmlType("Texture")]
		public class TextureNode {
			[XmlAttribute]
			public string Id;
			[XmlAttribute]
			public PixelFormat Format = PixelFormat.A8R8G8B8;
			
			public List<ImageNode> Images { get; set; }
		}

		/// <summary>
		/// Represents root node of SpriteBundle content XML
		/// </summary>
		[XmlRoot]
		[XmlType("Atlas")]
		public class AtlasNode {
			public List<TextureNode> Textures { get; set; }
		}

		#endregion
	}
}
