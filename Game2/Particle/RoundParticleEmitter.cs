﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class RoundParticleEmitter : ParticleEmitter
    {
        public RoundParticleEmitter(ParticleType particleType, Vector2 pos) : base(particleType, pos) {}

        public override void update()
        {
            if (activated)
            {
                for(int i = 0; i < pPerUpdate; i++)
                {
                    double angle = random.NextDouble()* Math.PI * 2;
                    float v = pVelocity;
                    if(pMinVelocity != -1)
                        v = (float)random.NextDouble() * (pVelocity - pMinVelocity) + pMinVelocity;
                    int lifeTime = pLifeTime;
                    if (pMinLifeTime != -1)
                        lifeTime = (int)(random.NextDouble() * (pLifeTime - pMinLifeTime) + pMinLifeTime);
                    Vector2 velocity = MapTools.AngleToVector((float)angle) * v;
                    Particle particle = new Particle(particleType, pos, pSize, velocity, lifeTime, pLoop, pFrameskip);
                    particle.illuminationStrength = pIlluminationStrength;
                    particle.gravityFactor = pGravityFactor;
                    particle.bounceFactor = pBounceFactor;
                    particle.dampenFactor = pDampenFactor;
                    Game1.world.particles.Add(particle);

                }
            }
        }
    }
}
