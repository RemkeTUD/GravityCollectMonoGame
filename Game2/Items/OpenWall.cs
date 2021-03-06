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
    public class OpenWall : CollisionBox
    {
        public int rotation;
        public OpenWall()
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/halfOpend");
        }
        public OpenWall(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/halfOpend");
            this.rotation = 0;
            angle = (float)Math.PI * 0.5f * (rotation);
        }

        public OpenWall(ContentManager content, float x, float y, float width, float height, int rotation = 0) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/halfOpend");
            this.rotation = rotation;
            angle = (float)Math.PI * 0.5f * (rotation);
        }

        

        public override void frameInit()
        {
            if (
                rotation == 0 && Game1.getPlayer().getCenter().Y + 16 < (getCenter() - size * 0.5f).Y + 2 && Game1.getPlayer().getYSpeed() >= 0 ||
                rotation == 3 && Game1.getPlayer().getCenter().X + 8 < (getCenter() - size * 0.5f).X + 2 && Game1.getPlayer().getXSpeed() >= 0 ||
                rotation == 2 && Game1.getPlayer().getCenter().Y - 16 > (getCenter() + size * 0.5f).Y - 2 && Game1.getPlayer().getYSpeed() <= 0 ||
                rotation == 1 && Game1.getPlayer().getCenter().X - 8 > (getCenter() + size * 0.5f).X - 2 && Game1.getPlayer().getXSpeed() <= 0
               )
                active = true;
            else
                active = false;
            base.frameInit();
        }

        public override void drawOutlines(SpriteBatch spriteBatch)
        {
        }

        Button buttonRotationPlus;

        public override void drawParamMenu(SpriteBatch batch)
        {
            if (buttonRotationPlus == null)
                buttonRotationPlus = new Button(new Rectangle(1360, 850, 16, 16), delegate { rotation += 1; rotation %= 4; }, "gui/plus");
            buttonRotationPlus.Draw(batch);
            buttonRotationPlus.Update();
            batch.DrawString(font, ((int)(rotation)).ToString(), new Vector2(1360, 870), Color.Black);
            base.drawParamMenu(batch);
        }
        public override bool isClickedParamMenu()
        {
            return base.isClickedParamMenu() || buttonRotationPlus.isClicked();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            angle = (float)Math.PI * 0.5f * (rotation);

            base.Draw(spriteBatch);
        }
    }
}
