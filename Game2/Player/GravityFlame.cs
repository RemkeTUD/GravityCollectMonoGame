using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{

    

    public class GravityFlame
    {
        Texture2D textureTest;
        public Vector2 size = new Vector2(8, 8);
        public Rectangle rect = new Rectangle(1000, 1000, 8, 8);
        public Vector2 pos = new Vector2(999, 999);

        public Vector2 goalPos = new Vector2(1000, 1000);

        bool flipped = false;

        long frame;
        int frame2 = 0;
        Vector2 offset = new Vector2(0,0);
        Vector2 offsetGoal = new Vector2(0, 0);
        static Random rnd = new Random();
        public GravityFlame()
        {
            
            frame = rnd.Next(10000);
            textureTest = Game1.cManager.Load<Texture2D>("player");
        }

        public void update(int id)
        {
            frame++;
            frame2++;
            Vector2 vecToPlayer;
            //if (id == 0 || frame2 < 60)
            vecToPlayer = (Game1.getPlayer().getCenter() - pos);
            //else
            //    vecToPlayer = (Game1.getPlayer().flames[id-1].getCenter() - pos);
            vecToPlayer.Normalize();
            vecToPlayer *= 16 * (id+1);

            
           // if((pos.X > Game1.getPlayer().getCenter().X && !Game1.getPlayer().flipped) || (pos.X < Game1.getPlayer().getCenter().X && Game1.getPlayer().flipped)) {
                //vecToPlayer.Y *= 0.3f;
           //     goalPos = Game1.getPlayer().getCenter() - vecToPlayer * 0.5f;

           // }
          //  else
                goalPos = Game1.getPlayer().getCenter() - vecToPlayer;

            pos += (goalPos - pos) * 0.2f;

            if (WorldInfo.gravity.X == 0)
            {
                offsetGoal.Y = (float)Math.Sin(frame * 0.03f) * 5;
                offsetGoal.X = 0;
            }
            else {
                offsetGoal.X = (float)Math.Sin(frame * 0.03f) * 5;
                offsetGoal.Y = 0;

            }

            offset += (offsetGoal - offset) * 0.3f;

        }

        public void draw(SpriteBatch batch)
        {
            rect.X = (int)(pos.X + offset.X);
            rect.Y = (int)(pos.Y + offset.Y);
            rect.Width = (int)size.X;
            rect.Height = (int)size.Y;
            SpriteEffects effect;
            if (flipped)
                effect = SpriteEffects.FlipHorizontally;
            else
                effect = SpriteEffects.None;
            batch.Draw(textureTest, destinationRectangle: rect, color: Color.White, rotation: MapTools.VectorToAngle(WorldInfo.gravity) - (float)Math.PI * 0.5f, origin: new Vector2(textureTest.Width * 0.5f, textureTest.Height * 0.5f), effects: effect);

        }
        public Vector2 getCenter()
        {
            return pos;
        }

    }
}
