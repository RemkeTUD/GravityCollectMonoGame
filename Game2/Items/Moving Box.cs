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
    public class Moving_Box : CollisionBox, isTraveling
    {

        Vector2 startPoint, endPoint;
        bool toEndPoint = true;
        float travelSpeed;
        public Moving_Box(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            this.endPoint = new Vector2(x,y);
            startPoint = new Vector2(x, y);
            this.travelSpeed = 2;
        }
        public Moving_Box(ContentManager content, float x, float y, float width, float height, float speed, Vector2 endPoint) : base(content, x, y, width, height)
        {
            this.startPoint = new Vector2(x,y);
            this.endPoint = endPoint;
            this.travelSpeed = speed;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            startPoint = spawnPos;
            LineRenderer.DrawLine(spriteBatch, startPoint, endPoint, Color.Black, 3);
            base.Draw(spriteBatch);
        }

        public Vector2 getEndPoint()
        {
            return endPoint;
        }

        public void SetEndPoint(Vector2 endpoint)
        {
            this.endPoint = endpoint;
        }

        public override void Update()
        {
            if ((endPoint - startPoint).Length() < (pos - startPoint).Length() && toEndPoint) {
                toEndPoint = false;
            } else if (travelSpeed > (pos - startPoint).Length() && !toEndPoint)
            {
                toEndPoint = true;
            }

            speed = (endPoint - startPoint);
            speed.Normalize();
            speed *= travelSpeed;
            if (!toEndPoint)
                speed *= -1;

            

            base.Update();
        }

    }
}
