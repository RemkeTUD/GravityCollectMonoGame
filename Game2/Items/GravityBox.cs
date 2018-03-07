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
        public bool horizontal = false;
        public float fallHeight;
        public GravityBox()
        {
            //textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/enemy");
        }
        public GravityBox(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            this.fallHeight = 16;
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
            calcDirection();
            base.Update();
        }


        public virtual void calcDirection()
        {
            if (!horizontal)
            {

                speed.Y += 0.4f * ((float)(WorldInfo.gravity.Y));

                if (speed.Y > 0 && pos.Y > spawnPos.Y + fallHeight)
                    speed.Y = 0;
                if (speed.Y < 0 && pos.Y < spawnPos.Y)
                    speed.Y = 0;
            }
            else
            {
                speed.X += 0.4f * ((float)(WorldInfo.gravity.X));

                if (speed.X > 0 && pos.X > spawnPos.X + fallHeight)
                    speed.X = 0;
                if (speed.X < 0 && pos.X < spawnPos.X)
                    speed.X = 0;
            }
        }


        Button buttonLengthPlus, buttonLengthMinus;
        Button buttonHorizontalToggle;

        public override void drawParamMenu(SpriteBatch batch)
        {
            if (buttonLengthPlus == null)
                buttonLengthPlus = new Button(new Rectangle(1360, 850, 16, 16), delegate { fallHeight += 16f; }, "gui/plus");
            if (buttonLengthMinus == null)
                buttonLengthMinus = new Button(new Rectangle(1336, 850, 16, 16), delegate { fallHeight -= 16f; }, "gui/minus");
            if (buttonHorizontalToggle == null)
                buttonHorizontalToggle = new Button(new Rectangle(1290, 850, 16, 16), delegate { horizontal =!horizontal; }, "gui/toggle");

            buttonLengthPlus.Draw(batch);
            buttonLengthPlus.Update();
            buttonLengthMinus.Draw(batch);
            buttonLengthMinus.Update();
            batch.DrawString(font, ((int)(fallHeight /16)).ToString(), new Vector2(1350, 870), Color.Black);

            buttonHorizontalToggle.Draw(batch);
            buttonHorizontalToggle.Update();
            batch.DrawString(font, ((horizontal)).ToString(), new Vector2(1290, 870), Color.Black);

            base.drawParamMenu(batch);
        }

        public override bool isClickedParamMenu()
        {
            return base.isClickedParamMenu() || buttonLengthPlus.isClicked() || buttonLengthMinus.isClicked() || buttonHorizontalToggle.isClicked(); ;
        }

    }
}
