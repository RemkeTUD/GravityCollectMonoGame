using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Trapdoor : CollisionBox
    {
        int rotation;
        bool isOpen = true;

        public Trapdoor(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/openwall");
            this.rotation = 0;
            angle = (float)Math.PI * 0.5f * (rotation + 1);
        }

        public Trapdoor(ContentManager content, float x, float y, float width, float height, int rotation = 0) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/openwall");
            this.rotation = rotation;
            angle = (float)Math.PI * 0.5f * (rotation + 1);
        }
        public override void Update()
        {
            if(rotation == 0)
            {
                if (WorldInfo.gravity.Y > 0.5f)
                    isOpen = false;
                if (WorldInfo.gravity.Y < -0.5f)
                    isOpen = true;
            }
            if (rotation == 3)
            {
                if (WorldInfo.gravity.X > 0.5f)
                    isOpen = false;
                if (WorldInfo.gravity.X < -0.5f)
                    isOpen = true;
            }
            if (rotation == 2)
            {
                if (WorldInfo.gravity.Y > 0.5f)
                    isOpen = true;
                if (WorldInfo.gravity.Y < -0.5f)
                    isOpen = false;
            }
            if (rotation == 1)
            {
                if (WorldInfo.gravity.X > 0.5f)
                    isOpen = true;
                if (WorldInfo.gravity.X < -0.5f)
                    isOpen = false;
            }
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isOpen)
                alpha = 255;
            else
                alpha = 100;
            base.Draw(spriteBatch);
        }

        public override bool collidesWithMovingPoint(Vector2 point, Vector2 direction)
        {

            if (isOpen)
                return false;

            

            return base.collidesWithMovingPoint(point, direction);
        }
    }
}
