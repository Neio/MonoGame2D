using System;
using System.Collections.Generic;
using System.Text;
using MonoGame2D;

namespace MonoGame2D.Particle {

    ///<summary>Base particle class which can be processed by stock particle system implementation</summary>
    public abstract class BaseParticle : SpriteEntity {

        //lifetime variables
        float _timeToLive;
        float _lifeTime;
        float _lifeProgress;

        ///<summary>Creates particle with specified lifetime</summary>
        public BaseParticle(ISpriteSource provider, float timeToLive) : base(provider) {
            _timeToLive = timeToLive;
            _lifeProgress = 0;
        }

        ///<summary>True if particle still alive</summary>
        public bool IsAlive {
            get { return _lifeProgress < 1; }
        }

        ///<summary>Life progress. Value from 0 ... 1. 0 - just born, 1 - dead</summary>
        public float LifeProgress {
            get { return _lifeProgress; }
        }

        ///<summary>Time to live for particle in seconds</summary>
        public float TimeToLive {
            get { return _timeToLive; }
        }

        ///<summary>Updates particle with some frame time. If particle is not alive anymore false will be returned</summary>
        public virtual bool Update(float timeDelta) {
            _lifeTime += timeDelta;
            _lifeProgress = _lifeTime / _timeToLive;
            return _lifeProgress < 1;
        }

    }
}
