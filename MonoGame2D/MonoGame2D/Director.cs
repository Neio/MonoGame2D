﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame2D.Effects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame2D.Script;

namespace MonoGame2D
{
    public class Director : Game
    {
        
        NodeShadow shadow = new NodeShadow();
        GraphicsDeviceManager graphics;

        private RenderTarget2D _effectTarget;
        private RenderTarget2D _oldEffectTarget;

        private SceneSwitchEffectPlayer _switchEffectPlayer;
        SpriteBatch canvas = null;

        //private GraphSnapshot _graphSnapshot = new GraphSnapshot();
        private Stack<Scene> _sceneStack = new Stack<Scene>();
        private List<Scene> _sceneList = new List<Scene>();
        private List<Scene> _modalSceneList = new List<Scene>();

        TimeLine _script = new TimeLine();


        #region Game

        public Director(int Width, int Height, bool FullScreen = false)
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.IsFullScreen = FullScreen;
            graphics.PreferredBackBufferHeight = Height;
            graphics.PreferredBackBufferWidth = Width;
            
            Content.RootDirectory = "Content";
            FileSystem.RootDirectory = Content.RootDirectory;

            Window.Title = "MonoGame2D";
            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Game script
        /// </summary>
        public TimeLine Script { get {
            return _script;
        } }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            PresentationParameters pp = graphics.GraphicsDevice.PresentationParameters;
            _effectTarget = new RenderTarget2D(graphics.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight,
                true, pp.BackBufferFormat, DepthFormat.Depth24);
            //canvas = new Canvas(graphics.GraphicsDevice);
            canvas = new SpriteBatch( graphics.GraphicsDevice);
            blank = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
        }
        Texture2D blank;
        public Texture2D BlankTexture { get {
            return blank;
        } }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);

            _script.Update((float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000));

            if (CurrentScene != null)
            {
                shadow.AddRoot(CurrentScene);
                shadow.Update(gameTime);
                shadow.Clear();
            }


            if (null != _switchEffectPlayer)
            {
                if (!_switchEffectPlayer.Update((float)(gameTime.ElapsedGameTime.TotalMilliseconds/1000)))
                {
                    _switchEffectPlayer.Dispose();
                    _switchEffectPlayer = null;
                }
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            var device = graphics.GraphicsDevice;

            device.SetRenderTarget(_effectTarget);
            DrawContent(device, gameTime);
            device.SetRenderTarget(null);

            //draw domain through effect or some else...
            if (_switchEffectPlayer != null)
            {
                //device.SetRenderTarget(_effectTarget);
                _switchEffectPlayer.Draw(canvas);
                //device.SetRenderTarget(null);
            }
            else
            {
                canvas.Begin();
                canvas.Draw(_effectTarget, _effectTarget.Bounds, Color.White);
                canvas.End();

            }

        }

        /// <summary>
        /// Draws the domain content.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        private void DrawContent(GraphicsDevice device, GameTime gametime)
        {
            
            //draw background
            var identity =Matrix.Identity;
            
            Scene scene = CurrentScene;
            if (null == scene)
            {
                device.Clear(Color.Black);
            }

            if (scene != null)
                scene.DrawContent(canvas, gametime, ref identity);

          
        }


        #endregion



        #region Scene Managemnet




        /// <summary>
        /// Gets the current active background domain.
        /// </summary>
        /// <value>The current background domain.</value>
        public Scene CurrentScene
        {
            get
            {
                if (_sceneStack.Count > 0)
                {
                    return _sceneStack.Peek();
                }
                else
                {
                    return null;
                }
            }
            set { SetScene(value); }
        }

        /// <summary>
        /// Gets the size of the domain stack.
        /// </summary>
        /// <value>The size of the domain stack.</value>
        public int SceneStackSize
        {
            get { return _sceneStack.Count; }
        }

        /// <summary>
        /// Immediately sets the specified domain as current.
        /// </summary>
        /// <param name="domain">The current domain. May be null.</param>
        public void SetScene(Scene scene)
        {
            SetScene(scene, null, 0);
        }

        /// <summary>
        /// Switches the domain with specified domain switch effect.
        /// </summary>
        /// <param name="domain">The new domain.</param>
        /// <param name="switchEffect">The switch effect.</param>
        /// <param name="timeToSwitch">The time to switch.</param>
        public void SetScene(Scene scene, ISceneSwitchEffect switchEffect, float timeToSwitch)
        {
            if (null == scene) throw new ArgumentNullException("scene");
            if (null != switchEffect)
            {
                StartEffect(switchEffect, timeToSwitch);
            }

            if (_sceneStack.Count > 0)
            {
                _sceneStack.Pop().Deactivate();
            }

            //change domain
            _sceneStack.Push(scene);
            
            //invoke activate for new domain...
            if (null != scene)
            {
                scene.LoadContents(Content);
                scene.Activate();
            }
        }

        /// <summary>
        /// Switches the domain with specified domain switch effect placing it on top of domain stack
        /// </summary>
        /// <param name="domain">The new top domain.</param>
        public void PushScene(Scene scene)
        {
            PushScene(scene, null, 0);
        }

        /// <summary>
        /// Switches the domain with specified domain switch effect placing it on top of domain stack
        /// </summary>
        /// <param name="domain">The new top domain.</param>
        /// <param name="switchEffect">The switch effect.</param>
        /// <param name="timeToSwitch">The time to switch.</param>
        public void PushScene(Scene scene, ISceneSwitchEffect switchEffect, float timeToSwitch)
        {
            if (null == scene) throw new ArgumentNullException("scene");
            if (null != switchEffect)
            {
                StartEffect(switchEffect, timeToSwitch);
            }

            if (_sceneStack.Count > 0)
            {
                _sceneStack.Peek().Deactivate();
            }

            //change domain
            _sceneStack.Push(scene);

            //invoke activate for new domain...
            scene.LoadContents(Content);
            scene.Activate();
        }

        /// <summary>
        /// Pops the domain. Activating previous one removing top.
        /// </summary>
        /// <param name="switchEffect">The switch effect.</param>
        /// <param name="timeToSwitch">The time to switch.</param>
        public void PopScene()
        {
            PopScene(null, 0f);
        }

        /// <summary>
        /// Pops the domain. Activating previous one removing top.
        /// </summary>
        /// <param name="switchEffect">The switch effect.</param>
        /// <param name="timeToSwitch">The time to switch.</param>
        public void PopScene(ISceneSwitchEffect switchEffect, float timeToSwitch)
        {
            if (_sceneStack.Count > 0)
            {
                _sceneStack.Pop().Deactivate();

                if (null != switchEffect)
                {
                    StartEffect(switchEffect, timeToSwitch);
                }

                if (_sceneStack.Count > 0)
                {
                    _sceneStack.Peek().Activate();
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to pop scene. Scene stack is empty.");
            }
        }

        /// <summary>
        /// Starts the effect.
        /// </summary>
        /// <param name="switchEffect">The switch effect.</param>
        /// <param name="timeToSwitch">The time to switch.</param>
        private void StartEffect(ISceneSwitchEffect switchEffect, float timeToSwitch)
        {
            if (null == switchEffect) throw new ArgumentNullException("switchEffect");
            if (timeToSwitch < 0.05f) throw new ArgumentException("Scene switch effect should be at least 50 ms length", "timeToSwitch");

            //
            var _oldOne = _oldEffectTarget;
            _oldEffectTarget = _effectTarget;
            if (_oldOne == null)
            {
                PresentationParameters pp = graphics.GraphicsDevice.PresentationParameters;
                _effectTarget = new RenderTarget2D(graphics.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight,
                    true, pp.BackBufferFormat, DepthFormat.Depth24);
            }
            else
            {
                PresentationParameters pp = graphics.GraphicsDevice.PresentationParameters;
                _effectTarget = new RenderTarget2D(graphics.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight,
                    true, pp.BackBufferFormat, DepthFormat.Depth24);
            }
            //2. Create player targeted this effect...
            _switchEffectPlayer = new SceneSwitchEffectPlayer(switchEffect, timeToSwitch, _effectTarget, _oldEffectTarget);
        }

        #endregion


       
    }
}
