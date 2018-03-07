using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Button : GUIElement
    {
        MouseState mState, mPrevState;

        BAction action;
        public Button(Rectangle rect, BAction action, string texture) : base(rect, texture)
        {
            this.action = action;
            //textureTest = Game1.cManager.Load<Texture2D>(texture);
        }

        public override void Update()
        {
            mState = Mouse.GetState();
            if (rect.X < mState.X &&
                rect.X + rect.Width > mState.X &&
                rect.Y < mState.Y &&
                rect.Y + rect.Height > mState.Y &&
                mState.LeftButton == ButtonState.Pressed && mPrevState.LeftButton == ButtonState.Released)
            {
                
                action();
            }

            base.Update();


            mPrevState = mState;
        }

        public delegate void BAction();
    }
}
