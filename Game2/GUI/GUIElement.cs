using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class GUIElement
    {
        protected Rectangle rect;
        protected Texture2D textureTest;
        MouseState mState, mPrevState;
        public static SpriteFont font;
        public GUIElement(Rectangle rect)
        {
            this.rect = rect;
            if (font == null)
                font = Game1.cManager.Load<SpriteFont>("font");
        }

        public bool isClicked()
        {
            mState = Mouse.GetState();
            return rect.X < mState.X &&
                rect.X + rect.Width > mState.X &&
                rect.Y < mState.Y &&
                rect.Y + rect.Height > mState.Y &&
                mState.LeftButton == ButtonState.Pressed;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureTest, rect, Color.White);
        }

        public virtual void Update()
        {

        }

    }
}
