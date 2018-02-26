using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    public class InversedGravityBox : GravityBox
    {
        public InversedGravityBox()
        {
        }
        public InversedGravityBox(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
        }

        public override void calcDirection()
        {
            if (!horizontal)
            {

                speed.Y -= 0.8f * ((float)(WorldInfo.gravity.Y));

                if (speed.Y > 0 && pos.Y > spawnPos.Y + fallHeight)
                    speed.Y = 0;
                if (speed.Y < 0 && pos.Y < spawnPos.Y)
                    speed.Y = 0;
            }
            else
            {
                speed.X -= 0.8f * ((float)(WorldInfo.gravity.X));

                if (speed.X > 0 && pos.X > spawnPos.X + fallHeight)
                    speed.X = 0;
                if (speed.X < 0 && pos.X < spawnPos.X)
                    speed.X = 0;
            }
        }

    }
}
