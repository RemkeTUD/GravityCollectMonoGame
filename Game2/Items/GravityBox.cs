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
    public class GravityBox : CollisionBox
    {
        bool horizontal = false;
        float fallHeight;

        public GravityBox(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            this.fallHeight = 100;
            this.horizontal = false;
        }

        public GravityBox(ContentManager content, float x, float y, float width, float height, int fallHeight = 100, bool horizontal = false) : base(content, x, y, width, height)
        {
            this.fallHeight = fallHeight;
            this.horizontal = horizontal;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!horizontal)
            {
                LineRenderer.DrawLine(spriteBatch, spawnPos, spawnPos + new Vector2(0, fallHeight), Color.Black, 3);
            }
            else
            {
                LineRenderer.DrawLine(spriteBatch, spawnPos, spawnPos + new Vector2(fallHeight, 0), Color.Black, 3);
            }
            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            if(!horizontal) {

                speed.Y += 0.8f * ((float)(WorldInfo.gravity.Y));

                if (speed.Y > 0 && pos.Y > spawnPos.Y + fallHeight)
                    speed.Y = 0;
                if (speed.Y < 0 && pos.Y < spawnPos.Y)
                    speed.Y = 0;
            }
            else
            {
                speed.X += 0.8f * ((float)(WorldInfo.gravity.X));

                if (speed.X > 0 && pos.X > spawnPos.X + fallHeight)
                    speed.X = 0;
                if (speed.X < 0 && pos.X < spawnPos.X)
                    speed.X = 0;
            }
            base.Update();
        }


        Button buttonLengthPlus;
        Button buttonHorizontalPlus;

        public override void drawParamMenu(SpriteBatch batch)
        {
            if (buttonLengthPlus == null)
                buttonLengthPlus = new Button(new Rectangle(1300, 700, 32, 32), delegate { fallHeight += 16f; }, "saw");
            if (buttonHorizontalPlus == null)
                buttonHorizontalPlus = new Button(new Rectangle(1200, 700, 32, 32), delegate { horizontal =!horizontal; }, "saw");

            buttonLengthPlus.Draw(batch);
            buttonLengthPlus.Update();
            batch.DrawString(font, ((int)(fallHeight /16)).ToString(), new Vector2(1300, 800), Color.Black);

            buttonHorizontalPlus.Draw(batch);
            buttonHorizontalPlus.Update();
            batch.DrawString(font, ((horizontal)).ToString(), new Vector2(1200, 800), Color.Black);

            base.drawParamMenu(batch);
        }

        public override bool isClickedParamMenu()
        {
            return base.isClickedParamMenu() || buttonLengthPlus.isClicked() || buttonHorizontalPlus.isClicked(); ;
        }

    }
}
