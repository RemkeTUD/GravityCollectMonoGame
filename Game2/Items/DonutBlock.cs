using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class DonutBlock : CollisionBox
    {

        int framesScinePlayerStepOn = 0;
        bool playerStepOn = false;

        public DonutBlock()
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/box");

        }

        public DonutBlock(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/box");

        }

        public override void Update() { 
        
           if(collidesWithPoint(Game1.getPlayer().downPoint()))
            {
                playerStepOn = true;
            }
            if (playerStepOn)
                framesScinePlayerStepOn++;

            if (framesScinePlayerStepOn == 30)
                speed = WorldInfo.gravity * 3;

            base.Update();
        }
    }
}
