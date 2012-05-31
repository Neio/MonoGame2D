using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Particle {

    ///<summary>Contains parameters of emitter</summary>
    public class EmitterParameters {
        ///<summary>Defines emitter location</summary>
        public Vector2 Location;
        ///<summary>Defines emitter distribution range (inner/outer radius or rectangle sides length)</summary>
        public Range DistributionRange;
        ///<summary>Rate - how many particles will be generated during one second</summary>
        public float Rate;
        ///<summary>Defines pulse accumulation amount</summary>
        public float PulseAmount;
        ///<summary>Emitter direction radial offset</summary>
        public float Direction;
        ///<summary>Defines type of how points should be created around specified point</summary>
        public EmitterRegionType RegionType;

        public EmitterParameters() {
            RegionType = EmitterRegionType.Radial;
            DistributionRange = new Range(0, 50);
            PulseAmount = 1;
            Rate = 500;
        }

        ///<summary>Clones emitter parameters</summary>
        public EmitterParameters Clone() {
            return (EmitterParameters)this.MemberwiseClone();
        }
    }
}
