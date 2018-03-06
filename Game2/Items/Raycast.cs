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
            Vector2 res = new Vector2(pos.X, pos.Y);
            List<CollisionInfo> collisionInfos = new List<CollisionInfo>();
            dir.Normalize();
            Vector2 posOnGrid = MapTools.mapToGridCoords(pos);
            Vector2 endPointOnGrid = MapTools.mapToGridCoords(pos) + dir * length / 16f;
            CollisionInfo raytraversalInfo = setLine(pos, dir, length);
            if (raytraversalInfo.collided)
            {
                collisionInfos.Add( new CollisionInfo(true, Vector2.Zero, null, raytraversalInfo.pos));
            }
            Player player = Game1.getPlayer();
            if (MapTools.lineCollidesWithRect(pos, pos + dir * length, player.pos - player.size * 0.5f, player.size.X, player.size.Y).collided)
            {
                res = MapTools.lineCollidesWithRect(pos, pos + dir * length, player.pos - player.size * 0.5f, player.size.X, player.size.Y).pos;
                collisionInfos.Add(new CollisionInfo(true, Vector2.Zero, player, res));
            }
            foreach (CollisionBox box in Game1.world.collisionBoxes)
            {
                if(MapTools.lineCollidesWithRect(pos, pos + dir * length, box.pos - box.size * 0.5f, box.size.X, box.size.Y).collided)
                {
                    res = MapTools.lineCollidesWithRect(pos, pos + dir * length, box.pos - box.size * 0.5f, box.size.X, box.size.Y).pos;
                    collisionInfos.Add(new CollisionInfo(true, Vector2.Zero, box, res));
                }
            }

            if(collisionInfos.Count == 0)
                return new CollisionInfo(true, Vector2.Zero, null, pos + dir * length);

            while(collisionInfos.Count > 1)
            {
                if((pos-collisionInfos[0].pos).LengthSquared() > (pos - collisionInfos[1].pos).LengthSquared())
                {
                    collisionInfos.RemoveAt(0);
                }
                else
                    collisionInfos.RemoveAt(1);
            }
            return collisionInfos[0];

            /*
            while (!Game1.world.collidesWithPoint(res).collided && !Game1.getPlayer().collidesWithMovingPoint(res, Vector2.Zero))
            {
                res += dir;
                if ((pos - res).Length() > length)
                    break;
            }

            

            if (Game1.getPlayer().collidesWithMovingPoint(res, Vector2.Zero))
                return new CollisionInfo(true, Vector2.Zero, Game1.getPlayer(), res);*/


        }

        public CollisionInfo setLine(Vector2 pos, Vector2 dir, float length)
        {

            int x = (int)MapTools.mapToGridCoords(pos).X;
            int y = (int)MapTools.mapToGridCoords(pos).Y;

            int stepX = 1;
            int stepY = 1;
            if (dir.X < 0)
                stepX = -1;
            if (dir.Y < 0)
                stepY = -1;
            float tMaxXVal = pos.X % 16;
            float tMaxYVal = pos.Y % 16;

            if (stepX == 1)
                tMaxXVal = 16f - tMaxXVal;
            if (stepY == 1)
                tMaxYVal = 16f - tMaxYVal;

            float tMaxX = tMaxXVal / dir.X * stepX;
            float tMaxY = tMaxYVal / dir.Y * stepY;

            float tDeltaX = 16f / dir.X * stepX;
            float tDeltaY = 16f / dir.Y * stepY;
            while(true) {
                bool delX = false;
                if (tMaxX < tMaxY)
                {
                    tMaxX = tMaxX + tDeltaX;
                    x = x + stepX;
                    delX = true;
                }
                else
                {
                    tMaxY = tMaxY + tDeltaY;
                    y = y + stepY;
                    delX = false;
                }
                if (x < 0 || y < 0 || x >= Game1.world.width || y >= Game1.world.height)
                    break;
                if (Game1.world.blocks[x, y].Type.Collision)
                {
                    float min;
                    if (delX)
                        min = Math.Min(tMaxX - tDeltaX, tMaxY);
                    else
                        min = Math.Min(tMaxX, tMaxY - tDeltaY);
                    Vector2 hitPos = pos + min * dir;
                    //hitPos = new Vector2((hitPos.X - hitPos.X % 16) + hitPos.Y % 16, (hitPos.Y - hitPos.Y % 16) + hitPos.X % 16);
                    return new CollisionInfo(true, Vector2.Zero, null, hitPos);

                }
            }

            return new CollisionInfo(false, Vector2.Zero, null, dir * length);
        }


    }
}
