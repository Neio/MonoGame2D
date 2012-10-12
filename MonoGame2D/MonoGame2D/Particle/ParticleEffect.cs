using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MonoGame2D.Utils;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Particle {

    ///<summary>Defines particle effect</summary>
    public class ParticleEffect : IParticleProvider {
        private ParticleEffectParameters _params = new ParticleEffectParameters();
        private EmitterParameters _emitter = new EmitterParameters();

		private GradientStop[] _colorGradient;
		private GradientColorTable _gradientTable;

		#region Properties

		/// <summary>
		/// Gets or sets the sprite source to use for creating new particles.
		/// </summary>
		/// <value>The sprite source for new particles.</value>
        public Texture2D SpriteSource { get; set; }

        ///<summary>Parameters of particle generation</summary>
        public ParticleEffectParameters Params {
            get { return _params; }
            set { 
                if (null == value) throw new ArgumentNullException("Params");
                _params = value;
            }
        }

        ///<summary>Parameters of particle emitter</summary>
        public EmitterParameters Emitter {
            get { return _emitter; }
            set {
                if (null == value) throw new ArgumentNullException("Emitter");
                _emitter = value;
            }
        }

		/// <summary>
		/// Gets the particle gradient table for this effect.
		/// </summary>
		/// <value>The particle gradient table of this effect.</value>
		public GradientColorTable ParticleGradientTable {
			get {
				if (null == _colorGradient || _colorGradient != _params.ColorGradient) {
					_colorGradient = _params.ColorGradient;
					_gradientTable = new GradientColorTable(_colorGradient, 100);	//TODO adjust resolution
				}
				return _gradientTable;
			}
		}

		#endregion

		#region Constructor

		///<summary>Create default particle effect</summary>
        public ParticleEffect() {
        }

        ///<summary>Create default particle effect</summary>
        public ParticleEffect(Texture2D spriteProvider)
            : this()
        {
            SpriteSource = spriteProvider;
        }

        ///<summary>Create particle effect from file</summary>
        public ParticleEffect(Stream assetStream) {
//	            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
//
//	            ResourceFileInfo resFile = new ResourceFileInfo(fileName);
	            XmlSerializer serializer = new XmlSerializer(typeof(ParticleEffectFile));
//				var stream = resFile.OpenStream();
            ParticleEffectFile effectFileContent = (ParticleEffectFile)serializer.Deserialize(assetStream);

            Params = effectFileContent.Effect;
            Emitter = effectFileContent.Emitter;
        }

        public ParticleEffect(Stream assetStream, Texture2D sprite)
            : this(assetStream) {
                SpriteSource = sprite;
		}

		#endregion

		///<summary>Saves particle effect to file (includes emitter settings)</summary>
        public void SaveToFile(string fileName) {
            SaveToFile(fileName, null);
        }

        ///<summary>Saves particle effect to file (includes emitter settings)</summary>
        public void SaveToFile(string fileName, ParticleEditorSettings editorSettings) {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            ParticleEffectFile effectFileContent = new ParticleEffectFile();
            effectFileContent.Effect = Params;
            effectFileContent.Emitter = Emitter;
            effectFileContent.EditorSettings = editorSettings;

            using (FileStream stream = new FileStream(fileName, FileMode.Create)) {
                XmlSerializer serializer = new XmlSerializer(typeof(ParticleEffectFile));
                serializer.Serialize(stream, effectFileContent);
            }
        }

		/// <summary>
		/// Gets the random value range.
		/// </summary>
		/// <param name="range">The value in range.</param>
		/// <returns>New random value.</returns>
        private float GetRandomRange(Range range) {
            return range.PickRangeValue(RandomUtils.NextFloat());
        }

        ///<summary>Create particle new particle</summary>
        public virtual BaseParticle CreateParticle() {
            GenericParticle p = new GenericParticle(GetRandomRange(Params.Lifetime));

            p.SpriteProvider = SpriteSource;

            p.Size = new Vector2(GetRandomRange(Params.StartSize));
            p.DeltaSize = new Vector2(GetRandomRange(Params.EndSize)) - p.Size;

            p.Velocity = GetRandomRange(Params.Velocity);
            p.Acceleration = GetRandomRange(Params.Acceleration);

            p.Direction = GetRandomRange(Params.Direction) + GetRandomRange(Params.DirectionSpread) * RandomUtils.NextFloat(-0.5f, 0.5f);
            p.TangentialSpeed = GetRandomRange(Params.TangentialSpeed);
            p.TangentialAcceleration = GetRandomRange(Params.TangentialAcceleration);

            p.Spin = GetRandomRange(Params.Spin);
            p.SpinAcceleration = GetRandomRange(Params.SpinAcceleration);

            float gravityDirection = GetRandomRange(Params.GravityDirection);
            float gravity = GetRandomRange(Params.Gravity);
            p.Gravity = new Vector2((float)Math.Cos(gravityDirection) * gravity, (float)Math.Sin(gravityDirection) * gravity);

            p.Angle = 0.0f;

			//resolve gradient to table
			p.ColorTable = ParticleGradientTable;

            return p;
        }


        public ParticleSystem CreateParticleSystem() {
            return new ParticleSystem(Emitter, this);
        }

        public ParticleSystem CreateParticleSystem(float Direction)
        {
            return new ParticleSystem(Emitter, this, Direction);
        }
    }
}
