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
    public class TractorBeam : Item
    {
        public float length = 32 * 30;

        public TractorBeam()
        {
            textureTest = Game1.cManager.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/tractorBeam");
        }
        public TractorBeam(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/tractorBeam");
        }

        public override void Update()
        {
            Player player = Game1.getPlayer();

            if (beamCollidesWithPlayer())
            {
                player.fallSpeed -= player.fallAcceleration * 1.7f;
            }
        }
        public bool beamCollidesWithPlayer()
        {
            Player player = Game1.getPlayer();
            return MapTools.PointCollidesWithRect(player.getCenter(),
                getUpLeftBeamPoint(),
                getDownRightBeamPoint()) ||
                MapTools.PointCollidesWithRect(player.leftPoints()[0],
                getUpLeftBeamPoint(),
                getDownRightBeamPoint()) ||
                MapTools.PointCollidesWithRect(player.rightPoints()[0],
                getUpLeftBeamPoint(),
                getDownRightBeamPoint());
        }

        public Vector2 getUpLeftBeamPoint()
        {
            return getCenter() - new Vector2(MapTools.getXMultiplier() * size.X * 0.5f, MapTools.getYMultiplier() * size.Y * 0.5f);
        }

        public Vector2 getDownRightBeamPoint()
        {
            return getCenter() + new Vector2(MapTools.getXMultiplier() * size.X * 0.5f, MapTools.getYMultiplier() * size.Y * 0.5f) + WorldInfo.gravity * length;
        }

        public override void Draw(SpriteBatch batch)
        {
                if(WorldInfo.angle == 0) {
                Vector2 upLeft = getUpLeftBeamPoint();
                Vector2 downRight = getDownRightBeamPoint();
                if (upLeft.X > downRight.X)
                {
                    float temp = downRight.X;
                    downRight.X = upLeft.X;
                    upLeft.X = temp;
                }
                if (upLeft.Y > downRight.Y)
                {
                    float temp = downRight.Y;
                    downRight.Y = upLeft.Y;
                    upLeft.Y = temp;
                }
                
                batch.Draw(textureTest, new Rectangle(upLeft.ToPoint(), (downRight - upLeft).ToPoint()), Color.White);
            }
            base.Draw(batch);
        }

    }
}
