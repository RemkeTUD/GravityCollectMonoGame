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
    public class CollisionBox : Item
    {
        public CollisionBox()
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/box");
        }
        public CollisionBox(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            speed = new Vector2(0,0);
            if (textureTest == null)
                textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/box");



        }

        public override void Update()
        {

            if (collidesWithPoints(Game1.getPlayer().leftPoints()) && getRealSpeed() > 0.01f)
                Game1.getPlayer().speed = getRealSpeed();
            if (collidesWithPoints(Game1.getPlayer().rightPoints()) && getRealSpeed() < -0.01f)
                Game1.getPlayer().speed = getRealSpeed();

            base.Update();
        }
        public override void reset()
        {
            speed = new Vector2(0, 0);
            base.reset();

        }
        public virtual bool collidesWithMovingPoint(Vector2 point, Vector2 direction)
        {
            
            Vector2 boxUpLeft = pos - size * 0.5f;
            Vector2 boxDownRight = pos + size * 0.5f;
            return (boxUpLeft.X < point.X && boxUpLeft.Y < point.Y && boxDownRight.X > point.X && boxDownRight.Y > point.Y);
        }
        public virtual bool collidesWithPoints(List<Vector2> points)
        {

            Vector2 boxUpLeft = pos - size * 0.5f;
            Vector2 boxDownRight = pos + size * 0.5f;
            foreach(Vector2 point in points)
                if (boxUpLeft.X < point.X && boxUpLeft.Y < point.Y && boxDownRight.X > point.X && boxDownRight.Y > point.Y)
                    return true;
            return false;
        }
    }
}
