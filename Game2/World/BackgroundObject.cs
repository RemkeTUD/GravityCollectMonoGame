using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Game1
{
    
    [XmlInclude(typeof(Vector2))]
    public class BackgroundObject
    {
        public Vector2 pos;
        public float distance;
        public string texPath;
        public Vector2 speed;
        Texture2D tex;
        float scale;

        public BackgroundObject()
        {

        }
        public BackgroundObject(Vector2 pos, float distance, string texPath, Vector2 speed, float scale = 1)
        {
            this.pos = pos;
            this.distance = distance;
            this.texPath = texPath;
            this.speed = speed;
            this.scale = scale;
        }
        Vector2 movedPos = new Vector2();
        public void draw(SpriteBatch batch)
        {
            if (tex == null)
                tex = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/background/" + texPath);
            movedPos.X = (float)(pos.X + ((Game1.getCam().Pos.X - 1000) * 0.1f) * distance);
            movedPos.Y = (float)(pos.Y + ((Game1.getCam().Pos.Y - 1000) * 0.1f) * distance);
            

            pos += speed;
            if(speed.LengthSquared() != 0) {
                if (pos.X > Game1.world.width * 16)
                pos.X -= Game1.world.width * 16+tex.Width * scale;
                if (pos.X < -tex.Width * scale)
                pos.X += Game1.world.width * 16 + tex.Width * scale;

                if (pos.Y > Game1.world.height * 16)
                    pos.Y -= Game1.world.height * 16 + tex.Height * scale;
                if (pos.Y < -tex.Height * scale)
                    pos.Y += Game1.world.height * 16 + tex.Height * scale;
            }




            batch.Draw(tex,
                    position : movedPos,
                    scale: new Vector2(scale,scale),
                    color: Color.White);
        }

        public void reloadTexture()
        {
            tex = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/background/" + texPath);
        }

    }
}
