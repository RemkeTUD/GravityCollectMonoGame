using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Sign : Item
    {
        public TextDialog dialog;
        public Sign()
        {
            textureTest = Game1.cManager.Load<Texture2D>("sign");
        }
        public Sign(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = Game1.cManager.Load<Texture2D>("sign");
            this.dialog = null;
        }
        public Sign(ContentManager content, float x, float y, float width, float height, TextDialog dialog) : base(content, x, y, width, height)
        {
            textureTest = Game1.cManager.Load<Texture2D>("sign");
            this.dialog = dialog;
        }


        public void setDialog(TextDialog dialog)
        {
            this.dialog = dialog;
        }

        public override void Update()
        {
            if(CollidesWithPlayer() && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                TextDialog.setUpDialog(dialog);
            }
            base.Update();
        }

    }
}
