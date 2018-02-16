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
    public class Saw : MoveableItem
    {
        public Saw(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, width, 1, new Vector2(x,y))
        {
            textureTest = content.Load<Texture2D>("saw");

        }
        public Saw(ContentManager content, float x, float y, float width, float speed, Vector2 endPoint) : base(content, x, y, width, width, speed, endPoint)
        {
            textureTest = content.Load<Texture2D>("saw");
            
        }

        public override void Update()
        {
            if(CollidesWithPlayer())
            {
                Game1.getPlayer().die();
            }
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            angle += .1f;
            base.Draw(spriteBatch);
        }

        public override bool CollidesWithPlayer()
        {
            return (MapTools.distanceOfVecs(Game1.getPlayer().getCenter(), getCenter()) < size.X * 0.5f) ;
        }
    }
}
