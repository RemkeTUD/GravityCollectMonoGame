using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Textbox
    {
        string text;
        Texture2D textureTest;
        Rectangle rect;

        public static SpriteFont font;

        public Textbox(string text)
        {
            this.text = text;
            rect = new Rectangle(100, 500, 1400, 300);
            textureTest = Game1.cManager.Load<Texture2D>("textfield");
            if (font == null)
                font = Game1.cManager.Load<SpriteFont>("font");
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureTest, rect, Color.White);
            spriteBatch.DrawString(font, text, rect.Location.ToVector2() + new Vector2(40,40), Color.Black);
        }

    }
}
