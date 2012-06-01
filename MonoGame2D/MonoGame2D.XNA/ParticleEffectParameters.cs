using System;
using System.Collections.Generic;
using System.Text;
using MonoGame2D.Utils;
using Microsoft.Xna.Framework;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Particle {

    ///<summary>Settings of particle effect</summary>
    public class ParticleEffectParameters {
        ///<summary>Defines how long particle should live</summary>
        public Range Lifetime;
        ///<summary>Defines initial size of particle</summary>
        public Range StartSize;
        ///<summary>Defines final size of particle at the die time</summary>
        public Range EndSize;
        ///<summary>Defines initial linear velocity of particle</summary>
        public Range Velocity;
        ///<summary>Defines acceleration for particle</summary>
        public Range Acceleration;
        ///<summary>Defines initial direction of particle in radians</summary>
        public Range Direction;
        ///<summary>Defines spread around initial direction (it is correction)</summary>
        public Range DirectionSpread;
        ///<summary>Defines initial tangential speed</summary>
        public Range TangentialSpeed;
        ///<summary>Defines tangential acceleration which will increase/decrease tangential speed</summary>
        public Range TangentialAcceleration;
        ///<summary>Defines initial spin speed of particle</summary>
        public Range Spin;
        ///<summary>Defines spin acceleration for particle</summary>
        public Range SpinAcceleration;
        ///<summary>Defines direction of gravity which will interact with particle</summary>
        public Range GravityDirection;
        ///<summary>Defines initial particle spin</summary>
        public Range Gravity;
		///<summary>Particle color gradient </summary>
		public GradientStop[] ColorGradient;

        public ParticleEffectParameters() {
            Lifetime = new Range(1);
            StartSize = EndSize = new Range(16);
            Velocity = new Range(200);
            DirectionSpread = new Range(1);
			ColorGradient = new GradientStop[] {
				new GradientStop(0, Color.White),
				new GradientStop(1, Color.White)
			};
        }

        ///<summary>Clones emitter parameters</summary>
        public EmitterParameters Clone() {
            return (EmitterParameters)this.MemberwiseClone();
        }

    }
}
