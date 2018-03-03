using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Raven : Item
    {
        Animation animation = new Animation(32, 32, 5, true, 3);
        public Raven()
        {
            textureTest = Game1.cManager.Load<Texture2D>("raven");
        }

        public Raven(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = Game1.cManager.Load<Texture2D>("raven");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.update();
            sourceRect = animation.getSourceRectange();
            
            base.Draw(spriteBatch);
        }
    }
}
