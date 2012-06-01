using System;
using System.Collections.Generic;
using System.Text;
using MonoGame2D;
using Microsoft.Xna.Framework;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Particle {

    public class ParticleEditorSettings {
        ///<summary>Background color of particle test environment</summary>
        public Color BackgroundColor = Color.Black;
        ///<summary>Index of best sprite image to render some particle effect</summary>
        public int SpriteIndex;
        ///<summary>Index of blending mode to render some particle effect</summary>
        public int BlendingMode = 1;
    }

}
