﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class Animation : ScriptNode
    {
        protected Animation() { }
        public Animation(ISpriteCollection anim, bool Repeat = false, int FPS = 30)
        {
            fps = FPS;
            _anim = new SimpleSpriteAnimation(anim, fps, false);
            _anim.Repeatable = Repeat;
            _anim.AnimationComplete += new SimpleAnimationCompleteCallback(_anim_AnimationComplete);
        }

        protected void _anim_AnimationComplete(SimpleSpriteAnimation sender)
        {

            if (OnAnimationComplete != null)
                OnAnimationComplete(sender);
        }

        protected float fps;
        public event SimpleAnimationCompleteCallback OnAnimationComplete;

        protected SimpleSpriteAnimation _anim = null;

        public override void Draw(Canvas2D canvas)
        {
            if (_anim != null)
            {
                //graphic.Begin();
               // canvas.Begin();
                //canvas.Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, canvas.Matrix);
                  //  Matrix.CreateTranslation(Location.X, Location.Y, 0) * Matrix.CreateRotationZ(Rotation));
                //canvas.Batch.Draw(_anim.ToSprite(), new Vector2(0, 0), Color.White);
                canvas.DrawSprite(0, 0, _anim.ToSprite(), Color.White);
                //canvas.Batch.End();
                //canvas.End();
            }
        }


        public override void Update(float gameTime)
        {
            base.Update(gameTime);

            if (_anim != null)
            {
                _anim.Update(gameTime);
            }
        }
        public void Replay()
        {
            if (_anim != null)
                _anim.Replay();
        }

        public void Pause()
        {
            if (_anim != null)
                _anim.Repeatable = false;
        }

        public void Continue()
        {
            if (_anim != null)
            {
                _anim.Repeatable = true;
                _anim.Replay();
            }

        }


        public void ChangeSpeed(float TargetSpeedRate)
        {
            var _fps = TargetSpeedRate * fps;
            if (_anim.FrameRate != _fps)
                _anim.FrameRate = _fps;
        }


    }
}
