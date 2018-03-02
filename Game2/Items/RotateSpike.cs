using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    public class RotateSpike : Item
    {
        public RotateSpike()
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/rotateSpike");

        }

        public RotateSpike(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/rotateSpike");

        }

        public override bool collidesWithPoint(Vector2 point)
        {
            return MapTools.PointCollidesWithRect(point, pos -new Vector2(size.X, size.X) * 0.5f, size.X, size.Y, pos, -angle);

            return base.collidesWithPoint(point);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (sourceRect.Width == 0)
            {
                sourceRect = new Rectangle(0, 0, textureTest.Width, textureTest.Height);

            }

            angle = MapTools.VectorToAngle( WorldInfo.gravity) - (float)Math.PI * 0.5f;

            if(Game1.running && collidesWithPoint(Game1.getPlayer().getCenter()))
            {
                //Console.WriteLine("hit");
                Game1.getPlayer().die();
            }

            spriteBatch.Draw(
                textureTest,
                position: pos,
                sourceRectangle: sourceRect,
                color: new Color(r, g, b, alpha),
                rotation: angle,
                origin: new Vector2(sourceRect.Width * 0.5f, sourceRect.Width * 0.5f),
                scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                effects: SpriteEffects.None,
                layerDepth: 1);
            //LineRenderer.DrawLine(spriteBatch, pos, pos - new Vector2(size.X, size.X) * 0.5f, Color.Red, 3f);
        }
    }
}
