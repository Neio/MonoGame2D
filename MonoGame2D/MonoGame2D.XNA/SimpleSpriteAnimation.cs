using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame2D.Script;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    /// <summary>
    /// Simple animation complete callback handler
    /// </summary>
    public delegate void SimpleAnimationCompleteCallback(SimpleSpriteAnimation sender);

    /// <summary>
    /// Provides simple per-frame animation with specified frame rate. Sprite source - sprite collection.
    /// More complex animation could be provided by <see cref="SpriteAnimation"/> create in SpriteVortex.
    /// </summary>
    public class SimpleSpriteAnimation :  IUpdatable
    {
        private ISpriteCollection _sprites;
        private float _frameRate;
        private float _frameDuration;
        private int _firstFrame;
        private int _currentFrame;
        private float _frameTime;
        bool _paused;
        bool _repeatable;

        /// <summary>
        /// Occurs when animation complete - last frame is reached regardless animation is repeatable or not
        /// </summary>
        public event SimpleAnimationCompleteCallback AnimationComplete;


        /// <summary>
        /// Creates a new instance of the <see cref="SimpleSpriteAnimation"/> class.
        /// </summary>
        /// <param name="spriteCollection">The sprite collection.</param>
        /// <param name="frameRate">The frame rate.</param>
        public SimpleSpriteAnimation(ISpriteCollection spriteCollection, float frameRate)
            : this(spriteCollection, frameRate, true)
        {
        }

        public SimpleSpriteAnimation(ISpriteCollection spriteCollection, float frameRate, bool repeatable)
        {
            if (null == spriteCollection)
            {
                throw new ArgumentNullException("spriteCollection");
            }
            if (spriteCollection.Count < 1)
            {
                throw new ArgumentException("Sprite collection is empty. Animated collection should have at least one sprite", "spriteCollection");
            }
            if (frameRate <= 0.0f)
            {
                throw new ArgumentException("Frame rate should be greater than 0", "frameRate");
            }

            _sprites = spriteCollection;
            _frameRate = frameRate;
            _frameDuration = 1 / frameRate;
            _firstFrame = 0;
            _currentFrame = 0;
            _repeatable = repeatable;
        }

        /// <summary>
        /// Current sprite of animated sequence
        /// </summary>
        /// <value><see cref="Sprite"/> instance.</value>
        public Texture2D CurrentSprite
        {
            get { return _sprites[_currentFrame]; }
        }

        #region ISpriteProvider Members

        /// <summary>
        /// Returns current frame sprite
        /// </summary>
        /// <returns><see cref="Sprite"/> instance.</returns>
        public Texture2D ToSprite()
        {
            return _sprites[_currentFrame];
        }

        #endregion

        #region IUpdatable Members

        /// <summary>
        /// Updates the specified animation with frame time delta.
        /// </summary>
        /// <param name="timeDelta">The time delta in seconds.</param>
        public void Update(float timeDelta)
        {
            if (!_paused)
            {
                _frameTime += timeDelta;
                if (_frameTime >= _frameDuration)
                {
                    if (_currentFrame + 1 < _sprites.Count)
                    {
                        _currentFrame++;
                    }
                    else
                    {
                        if (AnimationComplete != null)
                        {
                            AnimationComplete(this);
                        }
                        if (_repeatable)
                        {
                            _currentFrame = _firstFrame;
                        }
                        else
                        {
                            _paused = true;
                        }
                    }
                    _frameTime -= _frameDuration;
                }
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets frame rate of animation expressed in frames/sec
        /// </summary>
        /// <value>The frame rate (frames/sec).</value>
        public float FrameRate
        {
            get { return _frameRate; }
            set
            {
                if (value <= 0.0f)
                {
                    throw new ArgumentException("Frame rate should be greater than 0", "FrameRate");
                }
                _frameRate = value;
                _frameDuration = 1 / value;
                _frameTime = 0;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the frame in seconds (inverted to framerate).
        /// </summary>
        /// <value>The duration of the frame (seconds).</value>
        public float FrameDuration
        {
            get { return _frameDuration; }
            set
            {
                if (value <= 0.0f)
                {
                    throw new ArgumentException("Frame duration should be greater than 0", "FrameDuration");
                }
                _frameDuration = value;
                _frameRate = 1 / value;
                _frameTime = 0;
            }
        }

        /// <summary>
        /// Gets or sets the first frame sprite index.
        /// </summary>
        /// <value>The first frame sprite index.</value>
        public int FirstFrame
        {
            get { return _firstFrame; }
            set
            {
                if (value < 0 || value >= _sprites.Count)
                {
                    throw new IndexOutOfRangeException(string.Format("First frame '{0}' is not valid index for animation sprite collection", value));
                }
                _firstFrame = value;
                _frameTime = 0;
            }
        }

        /// <summary>
        /// Gets or sets the index of current frame in animation sequence.
        /// </summary>
        /// <value>The current frame index.</value>
        public int CurrentFrame
        {
            get { return _currentFrame; }
            set
            {
                if (value < 0 || value >= _sprites.Count)
                {
                    throw new IndexOutOfRangeException(string.Format("Current frame '{0}'is  not valid index for animation sprite collection", value));
                }
                _currentFrame = value;
                _frameTime = 0;
            }
        }

        /// <summary>
        /// Resets animation to default frame
        /// </summary>
        public void Replay()
        {
            _currentFrame = _firstFrame;
            _frameTime = 0;
            _paused = false;
        }

        /// <summary>
        /// Gets or sets paused state of animation
        /// </summary>
        /// <value><c>true</c> if animation paused; otherwise, <c>false</c>.</value>
        public bool Paused
        {
            get { return _paused; }
            set { _paused = value; }
        }

        /// <summary>
        /// Gets or sets repeatable state (if true then animation will start from beginning after last frame)
        /// </summary>
        /// <value><c>true</c> if animation is repeatable; otherwise, <c>false</c>.</value>
        public bool Repeatable
        {
            get { return _repeatable; }
            set { _repeatable = value; }
        }
    }
}
