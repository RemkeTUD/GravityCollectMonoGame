using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class WorldInfo
    {
        public static Vector2 gravity = new Vector2(0,1);
        public static float angle;

        public static void setAngle(float angle)
        {
            WorldInfo.angle = angle;
        }

        public static void updateGravity()
        {
            if (angle != 0)
            {
                if (angle > 0)
                {
                    gravity = Vector2.Transform(gravity, Matrix.CreateRotationZ(MathHelper.ToRadians(5)));
                    angle-=5;
                }
                if (angle < 0)
                {
                    gravity = Vector2.Transform(gravity, Matrix.CreateRotationZ(MathHelper.ToRadians(-5)));
                    angle+=5;
                }
            }
        }

    }
}
