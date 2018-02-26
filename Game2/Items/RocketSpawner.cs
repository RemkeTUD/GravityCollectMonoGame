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
    public class RocketSpawner : MoveableItem
    {
        ContentManager content;
        public int frames = 0;
        public RocketSpawner()
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/rocketlauncher");
        }
        public RocketSpawner(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height, 0, new Vector2(x,y))
        {
            this.content = content;
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/rocketlauncher");

        }

        public RocketSpawner(ContentManager content, float x, float y, float width, float height, float speed, Vector2 endPoint) : base(content, x, y, width, height, speed, endPoint)
        {
            this.content = content;
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/rocketlauncher");

        }

        public override void Update()
        {
            
            angle = MapTools.VectorToAngle(Game1.getPlayer().getCenter() - getCenter());
            
            if (frames % 120 == 119)
            {
                Game1.world.addItem(new Rocket(content, pos.X, pos.Y, size.X, size.Y * 0.5f, Game1.getPlayer().getCenter() - getCenter()));
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
