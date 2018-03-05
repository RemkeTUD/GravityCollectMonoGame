using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class ConeParticleEmitter : ParticleEmitter
    {

        private double direction;
        private float coneRadius;
        public ConeParticleEmitter(ParticleType particleType, Vector2 pos, Vector2 direction, float coneRadius) : base(particleType, pos) {
            this.direction = MapTools.VectorToAngle(direction);
            this.coneRadius = coneRadius;
        }

        public ConeParticleEmitter(ParticleType particleType, Vector2 pos, double direction, float coneRadius) : base(particleType, pos)
        {
            this.direction = direction;
            this.coneRadius = coneRadius;
        }

        public override void update()
        {
            if (activated)
            {
                for(int i = 0; i < pPerUpdate; i++)
                {
                    double coneRadians = (coneRadius * Math.PI) / 180.0;
                    double angle = (random.NextDouble() * coneRadians * 2) + direction - coneRadians;
                    float v = pVelocity;
                    if (pMinVelocity != -1)
                        v = (float)random.NextDouble() * (pVelocity - pMinVelocity) + pMinVelocity;
                    int lifeTime = pLifeTime;
                    if(pMinLifeTime != -1)
                        lifeTime = (int) (random.NextDouble() * (pLifeTime - pMinLifeTime) + pMinLifeTime);
                    Vector2 velocity = MapTools.AngleToVector((float) angle) * v;
                    Particle particle = new Particle(particleType, pos, pSize, velocity, lifeTime, pLoop, pFrameskip);
                    particle.illuminationStrength = pIlluminationStrength;
                    particle.gravityFactor = pGravityFactor;
                    particle.bounceFactor = pBounceFactor;
                    particle.dampenFactor = pDampenFactor;
                    Game1.world.particles.Add(particle);

                }
            }
        }

        public Vector2 DirectionVec
        {
            get
            {
                return new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction));
            }
            set
            {
                this.direction = Math.Atan2(value.Y, value.X);
            }
        }

        public double Direction
        {
            get
            {
                return direction;
            }
            set
            {
                this.direction = value;
            }
        }

    }
}
