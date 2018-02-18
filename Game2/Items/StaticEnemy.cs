using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class StaticEnemy : Enemy
    {
        public StaticEnemy(ContentManager content, float X, float Y, float width, float height) : base(content, X, Y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/staticenemy");
        }
        public override void Update()
        {
            if (CollidesWithPlayer())
                Game1.getPlayer().die();
            if (speed.X == 0 && speed.Y == 0)
            setUpDirection();

            if (speed.Y != 0)
            {
                if ((getBlockSetArray()[0,0] && getBlockSetArray()[1,0]) || (!getBlockSetArray()[0,0] && !getBlockSetArray()[1,0]))
                    speed.Y = 1;
                if ((getBlockSetArray()[0,1] && getBlockSetArray()[1,1]) || (!getBlockSetArray()[0,1] && !getBlockSetArray()[1,1]))
                    speed.Y = -1;
            }

            if (speed.X != 0)
            {
                if ((getBlockSetArray()[0,0] && getBlockSetArray()[0,1]) || (!getBlockSetArray()[0,0] && !getBlockSetArray()[0,1]))
                    speed.X = 1;
                if ((getBlockSetArray()[1,0] && getBlockSetArray()[1,1]) || (!getBlockSetArray()[1,0] && !getBlockSetArray()[1,1]))
                    speed.X = -1;
            }
        }
    }
}
