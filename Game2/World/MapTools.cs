using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class MapTools
    {
        public static float getXMultiplier()
        {
           
            return (float)Math.Round(Vector2.Transform(WorldInfo.gravity, Matrix.CreateRotationZ(MathHelper.Pi * 0.5f)).X * -1);
        }
        public static float getYMultiplier()
        {
            return (float)Math.Round(Vector2.Transform(WorldInfo.gravity, Matrix.CreateRotationZ(MathHelper.Pi * 0.5f)).Y * -1f);
        }

        public static Vector2 getMultiplierVec()
        {
            return new Vector2(getXMultiplier(), getYMultiplier());
        }
        public static float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }
        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static Vector2 mapToGridCoords(Vector2 pos)
        {
            return new Vector2((int)(pos.X / 16), (int)(pos.Y / 16));
        }
        public static float distanceOfVecs(Vector2 a, Vector2 b)
        {
            return (a - b).Length();
        }

        public static bool PointCollidesWithRect(Vector2 point, Vector2 rectPos, float width, float height, Vector2 rotationPoint, float rotation)
        {

            point -= rotationPoint;
            point = Vector2.Transform(point, Matrix.CreateRotationZ(rotation));
            point += rotationPoint;
            return PointCollidesWithRect(point, rectPos, width, height);

        }
        public static bool PointCollidesWithRect(Vector2 point, Vector2 rectPos, float width, float height)
        {

            if (point.X <= rectPos.X || point.X >= rectPos.X + width || point.Y <= rectPos.Y || point.Y >= rectPos.Y + height)
            {
                return false;
            }
            return true;


        }

        public static bool PointCollidesWithRect(Vector2 point, Vector2 upLeft, Vector2 downRight)
        {

            if(upLeft.X > downRight.X)
            {
                float temp = downRight.X;
                downRight.X = upLeft.X;
                upLeft.X = temp;
            }
            if (upLeft.Y > downRight.Y)
            {
                float temp = downRight.Y;
                downRight.Y = upLeft.Y;
                upLeft.Y = temp;
            }
            

            if (point.X > upLeft.X && point.X < downRight.X && point.Y > upLeft.Y && point.Y < downRight.Y)
                return true;

            return false;
        }
        // a1 is line1 start, a2 is line1 end, b1 is line2 start, b2 is line2 end
        static bool LineIntersectsLine(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersection)
        {
            intersection = Vector2.Zero;

            Vector2 b = a2 - a1;
            Vector2 d = b2 - b1;
            float bDotDPerp = b.X * d.Y - b.Y * d.X;

            // if b dot d == 0, it means the lines are parallel so have infinite intersection points
            if (bDotDPerp == 0)
                return false;

            Vector2 c = b1 - a1;
            float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
            if (t < 0 || t > 1)
                return false;

            float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
            if (u < 0 || u > 1)
                return false;

            intersection = a1 + t * b;

            return true;
        }
        public static CollisionInfo lineCollidesWithRect(Vector2 startPointLine, Vector2 endPointLine, Vector2 rectPos, float width, float height)
        {
            List<Vector2> collisionPoints = new List<Vector2>();

            Vector2 intersectionLine1;
            bool line1Intersected = false;

            line1Intersected = LineIntersectsLine(startPointLine, endPointLine, rectPos, rectPos + new Vector2(width, 0), out intersectionLine1);
            if (line1Intersected)
                collisionPoints.Add(intersectionLine1);

            Vector2 intersectionLine2;
            bool line2Intersected = false;

            line2Intersected = LineIntersectsLine(startPointLine, endPointLine, rectPos, rectPos + new Vector2(0, height), out intersectionLine2);
            if (line2Intersected)
                collisionPoints.Add(intersectionLine2);

            Vector2 intersectionLine3;
            bool line3Intersected = false;

            line3Intersected = LineIntersectsLine(startPointLine, endPointLine, rectPos + new Vector2(0, height), rectPos + new Vector2(width, height), out intersectionLine3);
            if (line3Intersected)
                collisionPoints.Add(intersectionLine3);

            Vector2 intersectionLine4;
            bool line4Intersected = false;

            line4Intersected = LineIntersectsLine(startPointLine, endPointLine, rectPos + new Vector2(width, 0), rectPos + new Vector2(width, height), out intersectionLine4);
            if (line4Intersected)
                collisionPoints.Add(intersectionLine4);

            if(collisionPoints.Count == 1)
            {
                return new CollisionInfo(true, Vector2.Zero, null, collisionPoints[0]);
            }

            if (collisionPoints.Count >=2)
            {
                if((startPointLine - collisionPoints[0]).LengthSquared() < (startPointLine - collisionPoints[1]).LengthSquared())
                {
                    return new CollisionInfo(true, Vector2.Zero, null, collisionPoints[0]);
                }
                else
                {
                    return new CollisionInfo(true, Vector2.Zero, null, collisionPoints[1]);
                }
            }
            return new CollisionInfo(false, Vector2.Zero);


            
        }

    }
}
