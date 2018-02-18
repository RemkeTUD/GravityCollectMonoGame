using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Canon : MoveableItem
    {
        int frames;
        public Canon(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/Canon");
        }

        public override void Update()
        {
            

            if (frames % 120 == 119)
            {
                Game1.world.addItem(new CanonBall(Game1.cManager, pos.X, pos.Y, size.X, size.Y, MapTools.AngleToVector(angle - (float)Math.PI * 0.5f)));
                frames = 0;
            }


            frames++;
            base.Update();
        }
        public override void reset()
        {
            frames = 0;
            base.reset();
        }
    }
}
