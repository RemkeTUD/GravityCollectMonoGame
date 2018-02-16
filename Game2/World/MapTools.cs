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
            return new Vector2((int)(pos.X / 32), (int)(pos.Y / 32));
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

    }
}
