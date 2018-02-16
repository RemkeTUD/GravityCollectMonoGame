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
    public class Spike : MoveableItem
    {
        

        public Spike(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("spike");
            
        }

        public override void Update()
        {
            if(collidesWithPoint(Game1.getPlayer().getCenter()))
            {
                Game1.getPlayer().die();
            }
            base.Update();
        }

        public override bool collidesWithPoint(Vector2 point)
        {
            return MapTools.PointCollidesWithRect(point, spawnPos - new Vector2(size.X, 0) * 0.5f, size.X, size.Y * 2 + (pos - spawnPos).Length(),spawnPos, -angle + (float)(Math.PI * 0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            angle = MapTools.VectorToAngle(endPoint-startPoint);
            
            startPoint = spawnPos;
            LineRenderer.DrawLine(spriteBatch, startPoint, endPoint, Color.Black, 3);
            spriteBatch.Draw(
               textureTest,
               position: spawnPos,
               sourceRectangle: new Rectangle(0, 0, (int)(textureTest.Width * 0.51f), textureTest.Height + 1+(int)((pos - spawnPos).Length() * ((float)textureTest.Height/size.Y))),
               color: new Color(255, 255, 255, alpha),
               rotation: angle - (float)(Math.PI * 0.5f),
               origin: new Vector2(textureTest.Width * 0.25f, 0),
               scale: new Vector2(2f * (size.X / (float)textureTest.Width), 1f * ((size.Y) / (float)textureTest.Height)),
               effects: SpriteEffects.FlipVertically,
               layerDepth: 1f);

            spriteBatch.Draw(
               textureTest,
               position: pos,
               sourceRectangle: new Rectangle((int)(textureTest.Width * 0.51f), 0, (int)(textureTest.Width * 0.51f), textureTest.Height),
               color: new Color(255, 255, 255, alpha),
               rotation: angle - (float)(Math.PI * 0.5f),
               origin: new Vector2(textureTest.Width * 0.25f, -textureTest.Height * 1f),
               scale: new Vector2(2f * (size.X / (float)textureTest.Width), 1 * (size.Y / (float)textureTest.Height)),
               effects: SpriteEffects.None,
               layerDepth: 1f);
        }

    }
}
