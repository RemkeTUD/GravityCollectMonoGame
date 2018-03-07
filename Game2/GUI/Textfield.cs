using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Textfield : GUIElement
    {
        public string text = "";
        public bool isActive = false;
        int framesLastKeyPressed = 0;
        public Textfield(Rectangle rect) : base(rect, null)
        {
            textureTest = Game1.cManager.Load<Texture2D>("textfield");

        }

        public override void Update()
        {

            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                isActive = false;
            if(isClicked())
            {
                isActive = true;
            }
            if (keys.Length == 0)
            {
                framesLastKeyPressed = 0;
            }
            if (keys.Length > 0 && isActive)
            {
                var keyValue = keys[0].ToString();
                
                if(framesLastKeyPressed == 0) {
                    if (text.Count() > 0 && keyValue == "Back")
                        text = text.Remove(text.Length - 1);
                    else
                        text += keyValue;
                }
                framesLastKeyPressed = 2;
            }

            if (framesLastKeyPressed > 0)
                framesLastKeyPressed--;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, text, rect.Location.ToVector2(), Color.Black);
        }
    }
}
