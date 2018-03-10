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
    public class OnGravityChangeBlock : CollisionBox
    {
        
        public bool activeSet = true;
        KeyboardState state, prevState;

        public OnGravityChangeBlock(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
        }

        public OnGravityChangeBlock()
        {

        }

        

        public override void Update()
        {
            state = Keyboard.GetState();
            prevState = Keyboard.GetState();
            if (Game1.world.gravityChanged)
                activeSet = !activeSet;

            if(!activeSet || (activeSet && !CollidesWithPlayer()))
            {
                active = activeSet;
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active)
                alpha = 255;
            else
                alpha = 120;
            base.Draw(spriteBatch);
        }

    }
}
