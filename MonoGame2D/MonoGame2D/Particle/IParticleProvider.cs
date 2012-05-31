using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame2D.Particle {

    ///<summary>Defines interface of particle provider which can be plugged to ParticlySystem</summary>
    public interface IParticleProvider {
        ///<summary>Creates particle derrived from BaseParticle</summary>
        BaseParticle CreateParticle();
    }
}
