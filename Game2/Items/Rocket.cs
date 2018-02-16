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
            }
            if(CollidesWithPlayer())
            {
                Game1.getPlayer().die();
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
