using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1
{
    public class Particle
    {
        Vector2 pos;
        public float illuminationStrength = 0;
        Animation animation;
        private Vector2 deltaMove = new Vector2(0, 0);
        Texture2D texture = Game1.cManager.Load<Texture2D>("spark");
        public int alpha = 255, r = 255, g = 255, b = 255;
        float angle;
        Vector2 velocity;
        Vector2 size;
        public float gravityFactor = 0.5f;
        public float bounceFactor = 0.5f;
        public float dampenFactor = 1f;
        public int lifeTime = 100;

        public Particle(ParticleType particleType, Vector2 pos, Vector2 size, Vector2 velocity, int lifeTime, bool aLoop, int aFrameskip = -1)
        {
            if(aFrameskip == -1)
            {
                aFrameskip = (int)(lifeTime / (particleType.MaxFrame - 1));
            }
            animation = new Animation(particleType.Width, particleType.Height, particleType.MaxFrame, aLoop, 34);
            animation.startPlay();
            this.lifeTime = lifeTime;
            this.pos = pos;
            this.size = size;
            this.velocity = velocity;
        }

        public void update()
        {
            CollisionInfo ci;
            animation.update();
            velocity *= dampenFactor;
            velocity.X += gravityFactor * (float)(Math.Round(WorldInfo.gravity.X));
            pos.X += velocity.X;
            ci = Game1.world.collidesWithPoint(pos);
            deltaMove.X = velocity.X;
            if (ci.collided)
            {
                deltaMove.X = 0;
                pos.X -= velocity.X - ci.speed.X;
                velocity.X *= -bounceFactor;
                velocity.X += ci.speed.X;
                velocity.Y *= dampenFactor;
            }
            velocity.Y += gravityFactor * (float)(Math.Round(WorldInfo.gravity.Y));
            pos.Y += velocity.Y;
            ci = Game1.world.collidesWithPoint(pos);
            deltaMove.Y = velocity.Y;
            if (ci.collided)
            {
                deltaMove.Y = 0;
                pos.Y -= velocity.Y - ci.speed.Y;
                velocity.Y *= -bounceFactor;
                velocity.Y += ci.speed.Y;
                velocity.X *= dampenFactor;
            }
            angle = (float)Math.Atan2(deltaMove.X, -deltaMove.Y);
            lifeTime--;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = animation.getSourceRectange();
            
            spriteBatch.Draw(
                texture,
                position: pos,
                sourceRectangle: sourceRect,
                color: new Color(r, g, b, alpha),
                rotation: angle,
                origin: new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f),
                scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                effects: SpriteEffects.None,
                layerDepth: 1);
        }

        public void drawIllumination(SpriteBatch spriteBatch)
        {
            if(illuminationStrength != 0)
            {
                Rectangle sourceRect = animation.getSourceRectange();

                spriteBatch.Draw(
                    texture,
                    position: pos,
                    sourceRectangle: sourceRect,
                    color: new Color(r * illuminationStrength, g * illuminationStrength, b * illuminationStrength, alpha),
                    rotation: angle,
                    origin: new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f),
                    scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                    effects: SpriteEffects.None,
                    layerDepth: 1);
            }

        }


    }
}
