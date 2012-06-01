using System;
using System.Collections.Generic;
using System.Text;
using MonoGame2D.Utils;
using Microsoft.Xna.Framework;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Particle {

    ///<summary>Implementation of highly tunable multipurpose particle object</summary>
    public class GenericParticle : BaseParticle {
        public Vector2 DeltaSize;

        public Vector2 Gravity;
        public Vector2 VelocityVector;

        public float Direction;
        public float Velocity;
        public float Spin;
        public float TangentialSpeed;

        public float Acceleration;
        public float SpinAcceleration;
        public float TangentialAcceleration;

		///<summary>Color gradient table</summary>
		public GradientColorTable ColorTable;

        ///<summary>Creates stock particle with specified time to live</summary>
        public GenericParticle(float timeToLive) : base(null, timeToLive) {
        }

        public override bool Update(float timeDelta) {
            if (base.Update(timeDelta)) {
                Size += DeltaSize * (timeDelta / TimeToLive);
				ColorTint = ColorTable[LifeProgress];

                XY += new Vector2((float)Math.Cos(Direction) * Velocity * timeDelta, (float)Math.Sin(Direction) * Velocity * timeDelta) + VelocityVector * timeDelta;
                Angle += Spin * timeDelta;
                Direction += TangentialSpeed * timeDelta;
                
                Velocity += Acceleration * timeDelta;
                Spin += SpinAcceleration * timeDelta; 
                TangentialSpeed += TangentialAcceleration * timeDelta;
                VelocityVector += Gravity * timeDelta;

                return true;
            }
            else {
                return false;
            }
        }



    }

}
