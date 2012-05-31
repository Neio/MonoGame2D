using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Particle {
   
    ///<summary>Particle collection is container of particles. It contains their life and order</summary>
    public class ParticleCollection {
        List<BaseParticle> m_Particles = new List<BaseParticle>();
        List<BaseParticle> m_ShadowParticles = new List<BaseParticle>();

        public ParticleCollection() {
            ;
        }

        ///<summary>Add particle to collection</summary>
        public void Add(BaseParticle particle) {
            m_Particles.Add(particle);
        }

        ///<summary>Number of particles</summary>
        public int Count {
            get { return m_Particles.Count; }
        }

        ///<summary>Enumeration of particles</summary>
        public IEnumerable<BaseParticle> Particles {
            get { return m_Particles; }
        }

		/// <summary>
		/// Clears this collection.
		/// </summary>
		public void Clear() {
			m_Particles.Clear();
		}

        ///<summary>Update particles in collection: move, rotate, scale etc</summary>
        public void Update(float timeDelta) {
            //iterate for all particles
            foreach (BaseParticle particle in m_Particles) {
                if (particle.Update(timeDelta)) {
                    m_ShadowParticles.Add(particle);
                }
            }

            //swap lists
            List<BaseParticle> tempList = m_ShadowParticles;
            m_ShadowParticles = m_Particles;
            m_Particles = tempList;

            //clear everything
            m_ShadowParticles.Clear();
        }

        ///<summary>Draws collection of particles in direct or reverse order</summary>
        public void Draw(SpriteBatch canvas, bool reverseOrder)
        {

            canvas.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //draw all particles
            foreach (BaseParticle particle in m_Particles) {

                Vector2 xy, size;
                float angle;
                Color colorTint;
                Texture2D sprite;

                particle.QueryParameters(out xy, out size, out angle, out colorTint, out sprite);

                Rectangle pos = new Rectangle((int)xy.X, (int)xy.Y, (int)size.X, (int)size.Y);
                canvas.Draw(sprite,pos, null, colorTint, angle, size/2 , SpriteEffects.None, 0f);
                //spriteBatch.Draw(sprite, , colorTint);

               // DrawSprite(xy, size, angle, sprite, colorTint);

            }



            canvas.End();
        }
    }
}
