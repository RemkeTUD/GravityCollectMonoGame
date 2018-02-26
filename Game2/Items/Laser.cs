using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Penumbra;

namespace Game1
{
    public class Laser : Item
    {
        float angleSpeed;
        Raycast raycast;
        CollisionInfo hitOfRaycast;

        Light light = new PointLight
        {
            Scale = new Vector2(3000), // Range of the light source (how far the light will travel)
            Radius = 0.1f,
            Intensity = 0.1f,
            
            Color = new Color(1f, 0, 0, 1),
            ShadowType = ShadowType.Solid // Will not lit hulls themselves
            
        };

        public Laser(ContentManager content, float x, float y, float width, float height) : base(content, x, y, width, height)
        {
            this.angleSpeed = 0;
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/laser");
            Game1.penumbra.Lights.Add(light);
            raycast = new Raycast(pos, new Vector2(0, 0), 3000);
        }

        public Laser(ContentManager content, float x, float y, float width, float height, float angleSpeed = 0) : base(content, x, y, width, height)
        {
            this.angleSpeed = angleSpeed;
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/laser");
            //Game1.penumbra.Lights.Add(light);
            raycast = new Raycast(pos, new Vector2(0,0), 3000);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            

            if(Game1.running) {
            LineRenderer.DrawLine(spriteBatch, pos, hitOfRaycast.pos, Color.Red, 3f);
            light.Position = hitOfRaycast.pos - raycast.dir * 5;
            }
            base.Draw(spriteBatch);
        }

        public override void drawIllumination(SpriteBatch spriteBatch)
        {


            if (Game1.running)
            {
                LineRenderer.DrawLine(spriteBatch, pos, hitOfRaycast.pos, new Color(0.5f, 0, 0, 1), 3f);
                light.Position = hitOfRaycast.pos - raycast.dir * 5;
            }
            base.drawIllumination(spriteBatch);
        }
        public override void Update()
        {


            angle += angleSpeed;

            raycast.dir = MapTools.AngleToVector(angle-MathHelper.Pi * 0.5f);
            raycast.pos = pos;
            hitOfRaycast = raycast.getHit();
            if (hitOfRaycast.obj is Player)
                Game1.getPlayer().die();
            
            base.Update();
        }


        Button buttonAngleSpeedPlus, buttonAngleSpeedMinus;

        public override void drawParamMenu(SpriteBatch batch)
        {
            if (buttonAngleSpeedPlus == null)
                buttonAngleSpeedPlus = new Button(new Rectangle(1300, 700, 32, 32), delegate { angleSpeed += 0.001f; }, "saw");
            buttonAngleSpeedPlus.Draw(batch);
            buttonAngleSpeedPlus.Update();
            batch.DrawString(font, ((int)(angleSpeed * 1000f)).ToString(), new Vector2(1300, 800), Color.Black);

            if (buttonAngleSpeedMinus == null)
                buttonAngleSpeedMinus = new Button(new Rectangle(1300 - 32, 700, 32, 32), delegate { angleSpeed -= 0.001f; }, "saw");
            buttonAngleSpeedMinus.Draw(batch);
            buttonAngleSpeedMinus.Update();


            base.drawParamMenu(batch);
        }

        public override bool isClickedParamMenu()
        {
            return base.isClickedParamMenu() || buttonAngleSpeedPlus.isClicked() || buttonAngleSpeedMinus.isClicked();
        }

    }
}
