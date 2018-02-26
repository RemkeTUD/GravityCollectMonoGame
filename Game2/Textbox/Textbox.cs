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
        public string text;
        Texture2D textureTest;
        public Rectangle rect;

        public static SpriteFont font;
        public Textbox()
        {
            textureTest = Game1.cManager.Load<Texture2D>("textfield");

        }
        public Textbox(string text)
        {
            this.text = text;
            rect = new Rectangle(100, 500, 1400, 300);
            textureTest = Game1.cManager.Load<Texture2D>("textfield");
            if (font == null)
                font = Game1.cManager.Load<SpriteFont>("textboxfont");
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureTest, rect, Color.White);
            spriteBatch.DrawString(font, parseText(text), rect.Location.ToVector2() + new Vector2(40,40), Color.Black);
        }

        private String parseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > 1350)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
        }

    }
}
