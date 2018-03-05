using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Penumbra;

namespace Game1
{
    public class Rocket : Item
    {
        public float angleSpeed = 0;
        public float travelSpeed;
        private RoundParticleEmitter particleEmitter;
        private ConeParticleEmitter smokeEmitter;
        private Vector2 oldPos;
        public Rocket(ContentManager content, float x, float y, float width, float height, Vector2 movingDirection) : base(content, x, y, width, height)
        {

            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/rocket");
            this.travelSpeed = 15f;
            angle = MapTools.VectorToAngle(movingDirection) ;
            particleEmitter = new RoundParticleEmitter(ParticleType.SPARK, new Vector2(0, 0));
            particleEmitter.pLifeTime = 100;
            particleEmitter.pMinLifeTime = 70;
            particleEmitter.pLoop = false;
            particleEmitter.pPerUpdate = 100;
            particleEmitter.pSize = new Vector2(5, 5);
            particleEmitter.pVelocity = 10;
            particleEmitter.pMinVelocity = 5;
            particleEmitter.pIlluminationStrength = 1;
            particleEmitter.pBounceFactor = 0.5f;
            particleEmitter.pDampenFactor = 0.97f;
            particleEmitter.pGravityFactor = 0.3f;

            smokeEmitter = new ConeParticleEmitter(ParticleType.ENGINE, new Vector2(0, 0), 0, 10);
            smokeEmitter.pLifeTime = 40;
            smokeEmitter.pMinLifeTime = 30;
            smokeEmitter.pLoop = false;
            smokeEmitter.pPerUpdate = 1;
            smokeEmitter.pSize = new Vector2(15, 15);
            smokeEmitter.pVelocity = 7;
            smokeEmitter.pMinVelocity = 5;
            smokeEmitter.pIlluminationStrength = 0.0005f;
            smokeEmitter.pBounceFactor = 0.0f;
            smokeEmitter.pDampenFactor = 0.97f;
            smokeEmitter.pGravityFactor = -0.02f;
            smokeEmitter.start();
            oldPos = pos;
        }

        public override void Update()
        {

            speed *= 1.0125f;
            Vector2 playerPos = Game1.getPlayer().getCenter();
            speed.Normalize();
            Vector2 pointRotatedUp = getCenter() + Vector2.Transform(speed, Matrix.CreateRotationZ(0.1f));
            Vector2 pointRotatedDown = getCenter() + Vector2.Transform(speed, Matrix.CreateRotationZ(-0.1f));

            if((playerPos - pointRotatedUp).Length() < (playerPos-pointRotatedDown).Length())
            {
                if (angleSpeed < 0)
                    angleSpeed = 0;
                angleSpeed += 0.001f;
            }
            if ((playerPos - pointRotatedUp).Length() > (playerPos - pointRotatedDown).Length())
            {
                if (angleSpeed > 0)
                    angleSpeed = 0;
                angleSpeed -= 0.001f;
            }
            angle += angleSpeed;
            speed = MapTools.AngleToVector(angle);
            speed *= travelSpeed;

            if(collidesWithMap().collided)
            {
                destroy = true;

                Light light = new PointLight
                {
                    Position = new Vector2(getCenter().X, getCenter().Y) - speed,
                    Scale = new Vector2(500), // Range of the light source (how far the light will travel)
                    Radius = 1f,
                    Intensity = 1f,

                    Color = new Color(1f, 0.5f, 0, 1),
                    CastsShadows = false,
                    ShadowType = ShadowType.Illuminated // Will not lit hulls themselves

                };

                ExplosionHandler.addExplosion(light);

            }
            if(CollidesWithPlayer())
            {
                Game1.getPlayer().die();

                Light light = new PointLight
                {
                    Position = new Vector2(getCenter().X, getCenter().Y) - speed,
                    Scale = new Vector2(1500), // Range of the light source (how far the light will travel)
                    Radius = 1f,
                    Intensity = 1f,

                    Color = new Color(5f, 0.5f, 0, 1),
                    ShadowType = ShadowType.Solid // Will not lit hulls themselves

                };

                ExplosionHandler.addExplosion(light);

            }
            smokeEmitter.pos = oldPos;
            smokeEmitter.Direction = angle + Math.PI;
            smokeEmitter.update();

            if(destroy == true)
            {
                particleEmitter.pos = oldPos;
                particleEmitter.start();
                particleEmitter.update();
            }
            oldPos = pos;
            base.Update();
        }
        public override void reset()
        {
            destroy = true;
            base.reset();
        }
    }
}
