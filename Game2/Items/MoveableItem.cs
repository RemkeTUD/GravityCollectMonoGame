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
    public class MoveableItem : Item, isTraveling
    {
        
        public Vector2 startPoint, endPoint;
        public float spawnSpeed;
        public float travelSpeed;
        public bool toEndPoint = true;

        public MoveableItem() : base()
        {

        }

        public MoveableItem(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            this.endPoint = new Vector2(x,y);
            startPoint = new Vector2(x, y);
            this.travelSpeed = 1;
            spawnSpeed = 1;
        }
        public MoveableItem(ContentManager content, float x, float y, float width, float height, float speed, Vector2 endPoint) : base(content, x, y, width, height)
        {
            this.endPoint = endPoint;
            startPoint = new Vector2(x, y);
            this.travelSpeed = speed;
            spawnSpeed = speed;
        }

        public override void Update()
        {
            if ((endPoint - startPoint).Length() < (pos - startPoint).Length() && toEndPoint)
            {
                toEndPoint = false;
            }
            else if (travelSpeed > (pos - startPoint).Length() && !toEndPoint)
            {
                toEndPoint = true;
            }
            if(travelSpeed != 0 && endPoint != startPoint) {
                speed = (endPoint - startPoint);
                speed.Normalize();
                speed *= travelSpeed * 0.1f;
                if (!toEndPoint)
                    speed *= -1;
            }
            base.Update();
        }

        public Vector2 getEndPoint()
        {
            return endPoint;
        }

        public void SetEndPoint(Vector2 endpoint)
        {
            this.endPoint = endpoint;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            startPoint = spawnPos;
            LineRenderer.DrawLine(spriteBatch, startPoint, endPoint, Color.Black, 3);
            base.Draw(spriteBatch);
        }

        public override void reset()
        {
            toEndPoint = true;
            //travelSpeed = spawnSpeed;
            base.reset();

        }

        Button buttonSpeedPlus, buttonSpeedMinus;

        public override void drawParamMenu(SpriteBatch batch)
        {
            if (buttonSpeedPlus == null)
                buttonSpeedPlus = new Button(new Rectangle(1360, 850, 16, 16), delegate { travelSpeed += 1f; }, "gui/plus");
            buttonSpeedPlus.Draw(batch);
            buttonSpeedPlus.Update();


            if (buttonSpeedMinus == null)
                buttonSpeedMinus = new Button(new Rectangle(1336, 850, 16, 16), delegate { travelSpeed -= 1f; }, "gui/minus");
            buttonSpeedMinus.Draw(batch);
            buttonSpeedMinus.Update();

            batch.DrawString(font, ((travelSpeed)).ToString(), new Vector2(1350, 870), Color.Black);
            base.drawParamMenu(batch);
        }
        public override bool isClickedParamMenu()
        {
            return base.isClickedParamMenu() || buttonSpeedPlus.isClicked() || buttonSpeedMinus.isClicked() ;
        }

        public override Dictionary<string, string> getAttributeList()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("Speed", speed.ToString());
            result.Add("EndpointX", endPoint.X.ToString());
            result.Add("EndpointY", endPoint.Y.ToString());

            return base.getAttributeList().Concat(result).GroupBy(p => p.Key).ToDictionary(g => g.Key, g => g.Last().Value);
        }

    }
}
