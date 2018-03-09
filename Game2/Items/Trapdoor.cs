using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Trapdoor : CollisionBox
    {
        public int rotation;
        public Trapdoor()
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/openwall");
        }
        public Trapdoor(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/openwall");
            this.rotation = 0;
            angle = (float)Math.PI * 0.5f * (rotation + 1);
        }

        public Trapdoor(ContentManager content, float x, float y, float width, float height, int rotation = 0) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/openwall");
            this.rotation = rotation;
            angle = (float)Math.PI * 0.5f * (rotation + 1);
        }
        public override void Update()
        {
            if(rotation == 0)
            {
                if (WorldInfo.gravity.Y > 0.5f)
                    active = false;
                if (WorldInfo.gravity.Y < -0.5f)
                    active = true;
            }
            if (rotation == 3)
            {
                if (WorldInfo.gravity.X > 0.5f)
                    active = false;
                if (WorldInfo.gravity.X < -0.5f)
                    active = true;
            }
            if (rotation == 2)
            {
                if (WorldInfo.gravity.Y > 0.5f)
                    active = true;
                if (WorldInfo.gravity.Y < -0.5f)
                    active = false;
            }
            if (rotation == 1)
            {
                if (WorldInfo.gravity.X > 0.5f)
                    active = true;
                if (WorldInfo.gravity.X < -0.5f)
                    active = false;
            }
            angle = (float)Math.PI * 0.5f * (rotation + 1);
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            angle = (float)Math.PI * 0.5f * (rotation + 1);
            if (active)
                alpha = 255;
            else
                alpha = 100;
            base.Draw(spriteBatch);
        }

        Button buttonRotationPlus;
        public override void drawOutlines(SpriteBatch spriteBatch)
        {
        }
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

    }
}
