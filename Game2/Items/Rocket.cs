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
        float angleSpeed = 0;
        float travelSpeed;
        public Rocket(ContentManager content, float x, float y, float width, float height, Vector2 movingDirection) : base(content, x, y, width, height)
        {

            textureTest = content.Load<Texture2D>("rocket");
            this.travelSpeed = 15f;
            angle = MapTools.VectorToAngle(movingDirection) ;

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
                    Scale = new Vector2(1500), // Range of the light source (how far the light will travel)
                    Radius = 1f,
                    Intensity = 1f,

                    Color = new Color(1f, 0.1f, 0, 1),
                    ShadowType = ShadowType.Solid // Will not lit hulls themselves

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

            base.Update();
        }
        public override void reset()
        {
            destroy = true;
            base.reset();
        }
    }
}
