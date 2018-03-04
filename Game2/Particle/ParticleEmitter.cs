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
    public abstract class ParticleEmitter
    {
        protected bool activated;
        public Vector2 pos;
        protected ParticleType particleType;
        public float pIlluminationStrength = 0;
        public Vector2 pSize = new Vector2(5, 5);
        public float pGravityFactor = 0.5f;
        public float pBounceFactor = 0.5f;
        public float pDampenFactor = 0.99f;
        public int pLifeTime = 100;
        public float pVelocity = 1;
        public bool pLoop = false;
        public int pFrameskip = -1;

        public int pPerUpdate = 1;

        protected Random random;

        public ParticleEmitter(ParticleType particleType, Vector2 pos)
        {
            this.particleType = particleType;
            this.pos = pos;
            random = new Random();
        }

        public virtual void start()
        {
            activated = true;
        }

        public abstract void update();

        public virtual void stop()
        {
            activated = false;
        }

    }
}
