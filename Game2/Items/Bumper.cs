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
    public class Bumper : Item
    {
        Animation animation = new Animation(48, 48, 10, false, 2);
        public Bumper()
        {
            textureTest = Game1.cManager.Load<Texture2D>("Bumper");
        }

        public Bumper(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            if (textureTest == null)
                textureTest = content.Load<Texture2D>("Bumper");
        }

        public override void Update()
        {
            if(CollidesWithPlayer())
            {
                Vector2 direction = (Game1.getPlayer().getCenter() - getCenter());
                direction.Normalize();
                direction *= 10;
                Game1.getPlayer().setXSpeed(direction.X);
                Game1.getPlayer().setYSpeed(-direction.Y);
                animation.startPlay();
            }

            base.Update();
        }

        public override bool CollidesWithPlayer()
        {
            return (MapTools.distanceOfVecs(Game1.getPlayer().getCenter(), getCenter()) < size.X * 0.5f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sourceRect = animation.getSourceRectange();
            animation.update();
            base.Draw(spriteBatch);
        }

    }
}
