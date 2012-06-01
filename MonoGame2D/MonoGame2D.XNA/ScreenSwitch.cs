using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

//Source Code modified from Vortex2D.NET

namespace MonoGame2D.Effects
{
    /// <summary>
	/// Provides domain switch effect context
	/// </summary>
	public interface ISceneSwitchContext {
		/// <summary>
		/// Gets the new domain sprite.
		/// </summary>
		/// <value>The new domain sprite.</value>
		Texture2D NewSceneSprite { get; }

		/// <summary>
		/// Gets the old domain sprite.
		/// </summary>
		/// <value>The old domain sprite.</value>
		Texture2D OldSceneSprite { get; }

		/// <summary>
		/// Gets the total effect time.
		/// </summary>
		/// <value>The total effect time.</value>
		float TotalEffectTime { get; }

		/// <summary>
		/// Gets the effect ellapsed time.
		/// </summary>
		/// <value>The effect ellapsed time.</value>
		float EllapsedTime { get; }

		/// <summary>
		/// Gets the progress of effect.
		/// </summary>
		/// <value>The effect progress. Value in range 0..1.</value>
		float Progress { get; }
	}

    /// <summary>
    /// Defines service interface for domain switching effects
    /// </summary>
    public interface ISceneSwitchEffect
    {
        /// <summary>
        /// Begins the switching effect. Usefull for storing effect contexts and initializing internal effect data.
        /// </summary>
        /// <param name="effectContext">The effect context.</param>
        void BeginEffect(SceneSwitchEffectPlayer effectContext);

        /// <summary>
        /// Ends the effect. It is proper place to free all of resource if some of them were acquired.
        /// </summary>
        void EndEffect();

        /// <summary>
        /// Updates the effect for with a specified time delta.
        /// </summary>
        /// <param name="timeDelta">The time delta in seconds.</param>
        void Update(float timeDelta);

        /// <summary>
        /// Draws the domain switching effect on the specified canvas.
        /// </summary>
        /// <param name="canvas">The canvas to use for effect drawing.</param>
        void Draw(SpriteBatch spriteBatch);
    }

    /// <summary>
    /// Contains collection of predefined domain switch effects
    /// </summary>
    public static class SceneSwitchEffects
    {

        /// <summary>Simple fade-out / fade-in switch effect</summary>
        public static readonly ISceneSwitchEffect Fade = new FadeSceneSwitchEffect();

        /// <summary>The slide left effect </summary>
        public static readonly ISceneSwitchEffect SlideLeft = new SlideSceneSwitchEffect(Vector2.UnitX);

        /// <summary>The slide right effect </summary>
        public static readonly ISceneSwitchEffect SlideRight = new SlideSceneSwitchEffect(-Vector2.UnitX);

        /// <summary>The slide down effect </summary>
        public static readonly ISceneSwitchEffect SlideDown = new SlideSceneSwitchEffect(Vector2.UnitY);

        /// <summary>The slide up effect </summary>
        public static readonly ISceneSwitchEffect SlideUp = new SlideSceneSwitchEffect(-Vector2.UnitY);

    }
    /// <summary>
	/// Implementation of <see cref="ISceneSwitchEffectContext"/> for domain graph subsystem
	/// </summary>
	public class SceneSwitchEffectPlayer : ISceneSwitchContext, IDisposable {
		private ISceneSwitchEffect _switchEffect;
		private RenderTarget2D _newSceneTexture;
		private RenderTarget2D _oldSceneTexture;
		private float _totalEffectTime;
		private float _invertedTotalEffectTime;
		private float _ellapsedTime = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="SceneSwitchEffectContext"/> class.
		/// </summary>
		/// <param name="newSceneSprite">The new domain sprite.</param>
		/// <param name="oldSceneSprite">The old domain sprite.</param>
		/// <param name="totalEffectTime">The total effect time.</param>
		public SceneSwitchEffectPlayer(ISceneSwitchEffect switchEffect, float totalEffectTime, RenderTarget2D newSceneTexture, RenderTarget2D oldSceneTexture) {
			if (null == switchEffect) throw new ArgumentNullException("switchEffect");
			if (null == newSceneTexture) throw new ArgumentNullException("newSceneTexture");
			if (null == oldSceneTexture) throw new ArgumentNullException("oldSceneTexture");

			_switchEffect = switchEffect;
			_newSceneTexture = newSceneTexture;
			_oldSceneTexture = oldSceneTexture;
			_totalEffectTime = totalEffectTime;
			_invertedTotalEffectTime = 1 / totalEffectTime;

			//invoke effect begin
			switchEffect.BeginEffect(this);
		}

		/// <summary>
		/// Updates the context with the specified delta time.
		/// </summary>
		/// <param name="deltaTime">The delta time.</param>
		/// <returns><c>true</c> if effect can be continued; otherwise <c>false</c></returns>
		public bool Update(float deltaTime) {
			_ellapsedTime += deltaTime;
			if (_ellapsedTime < _totalEffectTime) {
				_switchEffect.Update(deltaTime);
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Draws the specified effect on a target.
		/// </summary>
		/// <param name="canvas">The canvas.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
			//resend to effect...
            _switchEffect.Draw(spriteBatch);
		}

		#region ISceneSwitchContext Members

		/// <summary>
		/// Gets the new domain sprite.
		/// </summary>
		/// <value>The new domain sprite.</value>
		public Texture2D NewSceneSprite {
			get { return _newSceneTexture; }
		}

		/// <summary>
		/// Gets the old domain sprite.
		/// </summary>
		/// <value>The old domain sprite.</value>
		public Texture2D OldSceneSprite {
			get { return _oldSceneTexture; }
		}

		/// <summary>
		/// Gets the total effect time.
		/// </summary>
		/// <value>The total effect time.</value>
		public float TotalEffectTime {
			get { return _totalEffectTime; }
		}

		/// <summary>
		/// Gets the effect ellapsed time.
		/// </summary>
		/// <value>The effect ellapsed time.</value>
		public float EllapsedTime {
			get { return _ellapsedTime; }
		}

		/// <summary>
		/// Gets the progress of effect.
		/// </summary>
		/// <value>The effect progress. Value in range 0..1.</value>
		public float Progress {
			get { return _ellapsedTime * _invertedTotalEffectTime; }
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Ends the effect playing
		/// </summary>
		public void Dispose() {
			_switchEffect.EndEffect();
		}

		#endregion
	}

    /// <summary>
	/// Base domain switch effect
	/// </summary>
	public abstract class BaseSceneSwitchEffect : ISceneSwitchEffect {
		/// <summary>
		/// Gets the context of effect.
		/// </summary>
		/// <value>The context of effect.</value>
		protected ISceneSwitchContext Context { get; private set; }


		#region ISceneSwitchEffect Members

		/// <summary>
		/// Begins the switching effect. Usefull for storing effect contexts and initializing internal effect data.
		/// </summary>
		/// <param name="effectContext">The effect context.</param>
		public void BeginEffect(SceneSwitchEffectPlayer effectContext) {
			Context = effectContext;
		}

		/// <summary>
		/// Ends the effect. It is proper place to free all of resource if some of them were acquired.
		/// </summary>
		public void EndEffect() {
			;
		}

		/// <summary>
		/// Updates the effect for with a specified time delta.
		/// </summary>
		/// <param name="timeDelta">The time delta in seconds.</param>
		public void Update(float timeDelta) {
			;
		}

		/// <summary>
		/// Draws the domain switching effect on the specified canvas.
		/// </summary>
		/// <param name="canvas">The canvas to use for effect drawing.</param>
		public abstract void Draw(SpriteBatch spriteBatch);

		#endregion
	}

    /// <summary>
    /// Implements flexible sliding domain switching effect
    /// </summary>
    public class SlideSceneSwitchEffect : BaseSceneSwitchEffect
    {
        private Vector2 _direction;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideSceneSwitchEffect"/> class.
        /// </summary>
        /// <param name="direction">The direction of sliding.</param>
        public SlideSceneSwitchEffect(Vector2 direction)
        {
            _direction = direction;
        }

        /// <summary>
        /// Draws the domain switching effect on the specified canvas.
        /// </summary>
        /// <param name="canvas">The canvas to use for effect drawing.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            float progress = Context.Progress * Context.Progress;

            var pp = spriteBatch.GraphicsDevice.PresentationParameters;
            var size = new Vector2(pp.BackBufferWidth, pp.BackBufferHeight);

            //drawing old domain
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null,
                Matrix.CreateTranslation(_direction.X * progress * size.X, _direction.Y * progress * size.Y, 0));
            spriteBatch.Draw(Context.OldSceneSprite, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            progress = 1 - progress;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null,
                Matrix.CreateTranslation(-_direction.X * progress * size.X, -_direction.Y * progress * size.Y, 0));
            spriteBatch.Draw(Context.NewSceneSprite, new Vector2(0, 0), Color.White);
            

            spriteBatch.End();
        }
    }

    /// <summary>
    /// Implements simpe fade switch effect
    /// </summary>
    public class FadeSceneSwitchEffect : BaseSceneSwitchEffect
    {

        /// <summary>
        /// Draws the domain fade effect on the specified canvas.
        /// </summary>
        /// <param name="canvas">The canvas to use for effect drawing.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            float progress = Context.Progress;
            if (progress < 0.5f)
            {
                progress = (0.5f - progress) * 2;
                //draw fadeout

                spriteBatch.Draw(Context.OldSceneSprite, new Vector2(0, 0), new Color(progress,  progress, progress, 1.0f));

                //canvas.DrawSprite(
                //    canvas.Region,
                //    Context.OldSceneSprite,
                //    new Color(Color.White * progress, 255)
                //);
            }
            else
            {
                progress = (progress - 0.5f) * 2;
                //draw fade in
                spriteBatch.Draw(Context.NewSceneSprite, new Vector2(0, 0), new Color(progress, progress,  progress, 1.0f));
                //canvas.DrawSprite(
                //    canvas.Region,
                //    Context.NewSceneSprite,
                //    new Color(Color.White * progress, 255)
                //);
            }

            spriteBatch.End();
        }
    }
}
