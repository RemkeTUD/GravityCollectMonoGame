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
    public class Trampoline : Item
    {
        public int rotation;
        Animation animation;
        public Trampoline()
        {
            textureTest = Game1.cManager.Load<Texture2D>("trampoline");
            animation = new Animation(16, 16, 9, false, 2);
        }
        public Trampoline(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("trampoline");
            animation = new Animation(16, 16, 9, false,2);
            this.rotation = 3;
            angle = (float)Math.PI * 0.5f * (rotation + 2);
        }

        public override void Update()
        {
            if (CollidesWithPlayer())
            {
                if(rotation == 0) {
                    Game1.getPlayer().setYSpeed(-20);
                    Game1.getPlayer().framesSpacePressed = 0;
                }
                if (rotation == 2)
                {
                    Game1.getPlayer().setYSpeed(20);
                    Game1.getPlayer().framesSpacePressed = 0;
                }
                if (rotation == 1)
                {
                    Game1.getPlayer().setXSpeed(-20);
                    Game1.getPlayer().framesSpacePressed = 0;
                }
                if (rotation == 3)
                {
                    Game1.getPlayer().setXSpeed(20);
                    Game1.getPlayer().framesSpacePressed = 0;
                }
                animation.startPlay();
            }
            base.Update();
        }
        Button buttonRotationPlus;

        public override void Draw(SpriteBatch spriteBatch)
        {
            sourceRect = animation.getSourceRectange();
            animation.update();
            base.Draw(spriteBatch);
        }

        public override void drawParamMenu(SpriteBatch batch)
        {
            if (buttonRotationPlus == null)
                buttonRotationPlus = new Button(new Rectangle(1300, 700, 32, 32), delegate { rotation += 1; rotation %= 4; angle = (float)Math.PI * 0.5f * (rotation + 2); }, "saw");
            buttonRotationPlus.Draw(batch);
            buttonRotationPlus.Update();
            batch.DrawString(font, ((int)(rotation)).ToString(), new Vector2(1300, 800), Color.Black);
            
            base.drawParamMenu(batch);
        }
        public override bool isClickedParamMenu()
        {
            return base.isClickedParamMenu() || buttonRotationPlus.isClicked();
        }
    }
}
