using System;
using System.Collections.Generic;
using System.Text;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Particle {

    ///<summary>Container for particle effect for serialization</summary>
    public class ParticleEffectFile {
        public ParticleEffectParameters Effect { get; set; }
        public EmitterParameters Emitter { get; set; }
        public ParticleEditorSettings EditorSettings { get; set; }
    }
}
