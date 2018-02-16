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
    public class BoxDisapear : CollisionBox
    {
        public ButtonDisapear button;
        public BoxDisapear(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            if (textureTest == null)
                textureTest = content.Load<Texture2D>("boxdisapear");
            this.button = null;
        }
        public BoxDisapear(ContentManager content, float x, float y, float width, float height, ButtonDisapear button = null) : base(content, x, y, width, height)
        {
            if(textureTest == null)
            textureTest = content.Load<Texture2D>("boxdisapear");
            this.button = button;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (button.isActivated())
            {
                alpha = 100;
            }
            else
                alpha = 255;
            base.Draw(spriteBatch);
        }

        public override bool collidesWithMovingPoint(Vector2 point, Vector2 direction)
        {
            if(button != null && button.isActivated())
            {
                return false;
            }
            return base.collidesWithMovingPoint(point, direction);
        }
    }
}
