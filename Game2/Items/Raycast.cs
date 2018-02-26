using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Raycast
    {
        public Vector2 pos;
        public Vector2 dir;
        public float length;
        public Raycast()
        {

        }
            public Raycast(Vector2 pos, Vector2 dir, float length = 1000f)
        {
            this.pos = pos;
            this.dir = dir;
            this.length = length;
            this.dir.Normalize();
        }

        public CollisionInfo getHit()
        {
            Vector2 res = new Vector2(pos.X, pos.Y) ;
            dir.Normalize();
            while (!Game1.world.collidesWithPoint(res).collided && !Game1.getPlayer().collidesWithMovingPoint(res, Vector2.Zero))
            {
                res += dir;
                if ((pos - res).Length() > length)
                    break;
            }
            if (Game1.getPlayer().collidesWithMovingPoint(res, Vector2.Zero))
                return new CollisionInfo(true, Vector2.Zero, Game1.getPlayer(), res);
            return new CollisionInfo(true, Vector2.Zero, null, res);

        }
        

    }
}
