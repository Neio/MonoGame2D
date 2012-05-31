﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame2D.Script;

namespace MonoGame2D
{
   public class ScriptNode : Node
    {
        private TimeLine _timeLine = new TimeLine();

        protected TimeLine TimeLine
        {
            get
            {
                return _timeLine;

            }
            set
            {
                _timeLine = value;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _timeLine.Update(GetTimeDelta(gameTime));
            base.Update(gameTime);
        }
    }
}
