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
    public class GravityBox : CollisionBox
    {
        bool horizontal = false;
        float fallHeight;

        public GravityBox(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            this.fallHeight = 100;
            this.horizontal = false;
        }

        public GravityBox(ContentManager content, float x, float y, float width, float height, int fallHeight = 100, bool horizontal = false) : base(content, x, y, width, height)
        {
            this.fallHeight = fallHeight;
            this.horizontal = horizontal;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!horizontal)
            {
                LineRenderer.DrawLine(spriteBatch, spawnPos, spawnPos + new Vector2(0, fallHeight), Color.Black, 3);
            }
            else
            {
                LineRenderer.DrawLine(spriteBatch, spawnPos, spawnPos + new Vector2(fallHeight, 0), Color.Black, 3);
            }
            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            if(!horizontal) {

                speed.Y += 0.8f * ((float)(WorldInfo.gravity.Y));

                if (speed.Y > 0 && pos.Y > spawnPos.Y + fallHeight)
                    speed.Y = 0;
                if (speed.Y < 0 && pos.Y < spawnPos.Y)
                    speed.Y = 0;
            }
            else
            {
                speed.X += 0.8f * ((float)(WorldInfo.gravity.X));

                if (speed.X > 0 && pos.X > spawnPos.X + fallHeight)
                    speed.X = 0;
                if (speed.X < 0 && pos.X < spawnPos.X)
                    speed.X = 0;
            }
            base.Update();
        }

        
    }
}
