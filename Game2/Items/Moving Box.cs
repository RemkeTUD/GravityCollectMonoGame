﻿using System;
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

        public Vector2 startPoint, endPoint;
        public bool toEndPoint = true;
        public float travelSpeed;

        public Moving_Box()
        {
           
        }

        public Moving_Box(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            this.endPoint = new Vector2(x,y);
            startPoint = new Vector2(x, y);
            this.travelSpeed = 1;
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
            speed *= travelSpeed * 0.1f;
            if (!toEndPoint)
                speed *= -1;
            

            base.Update();
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
            return base.isClickedParamMenu() || buttonSpeedPlus.isClicked() || buttonSpeedMinus.isClicked();
        }

    }
}
