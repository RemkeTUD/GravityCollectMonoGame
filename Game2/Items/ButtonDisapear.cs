using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class ButtonDisapear : Item
    {
        public ButtonDisapear()
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/button");
        }

        public ButtonDisapear(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            if (textureTest == null)
                textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/button");
        }

        public bool isActivated()
        {
            return CollidesWithPlayer() || CollidesWithAnyCollisionBox();
        }
    }
}
