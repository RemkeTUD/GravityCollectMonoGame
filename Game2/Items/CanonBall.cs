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
    public class CanonBall : Item
    {
        public CanonBall(ContentManager content, float x, float y, float width, float height, Vector2 speed) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("canonBall");
            speed.Normalize();
            Console.WriteLine(speed);
            this.speed = speed * 35f;
        }

        public override void Update()
        {
            if (!collidesDownWithMap().collided)
                setFallSpeed(getFallSpeed() + 0.8f);
            else
                destroy = true;
            if (CollidesWithPlayer())
                Game1.getPlayer().die();
            base.Update();
        }

        public override void reset()
        {
            destroy = true;
            base.reset();
        }
    }
}
