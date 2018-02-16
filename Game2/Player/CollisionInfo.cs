using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class CollisionInfo
    {
        public bool collided;
        public Vector2 speed;
        public Object obj;
        public Vector2 pos;

        public bool isStatic = false;

        public CollisionInfo(bool collided, Vector2 speed)
        {
            this.collided = collided;
            this.speed = speed;
            this.obj = null;
        }

        public CollisionInfo(bool collided, Vector2 speed, bool isStatic)
        {
            this.collided = collided;
            this.speed = speed;
            this.obj = null;
            this.isStatic = isStatic;
        }

        public CollisionInfo(bool collided, Vector2 speed, Object obj, Vector2 pos)
        {
            this.collided = collided;
            this.speed = speed;
            this.obj = obj;
            this.pos = pos;
        }

        public float getFallSpeed()
        {
            Vector2 g = WorldInfo.gravity;
            if (g.Y < -0.5f)
                return -speed.Y;
            if (g.Y > 0.5f)
                return speed.Y;
            if (g.X < -0.5f)
                return -speed.X;
            if (g.X > 0.5f)
                return speed.X;
            return 0;
        }

        public float getRealSpeed()
        {
            Vector2 g = WorldInfo.gravity;
            if (g.Y < -0.5f)
                return -speed.X;
            if (g.Y > 0.5f)
                return +speed.X;
            if (g.X < -0.5f)
                return speed.Y;
            if (g.X > 0.5f)
                return -speed.Y;
            return 0;
        }

    }
}
