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
    public class Enemy : Item
    {

        float travelSpeed = 1f;
        public Enemy(ContentManager content, float X, float Y, float width, float height) : base(content, X, Y, width, height)
        {
            textureTest = content.Load<Texture2D>("themes/" + Game1.world.currentTheme + "/items/enemy");
            
        }

        public override void Update()
        {
            if (CollidesWithPlayer())
                Game1.getPlayer().die();
            if(pos == spawnPos)
                setUpDirection();
            updateDirection();
            base.Update();
        }

        public void setUpDirection()
        {
            bool[,] setArray = getBlockSetArray();

            if (setArray[0,0] && setArray[1,0] && !setArray[0,1] && !setArray[1,1])
            {
                speed.X = 1; speed.Y = 0;
            }
            if (setArray[1,0] && setArray[1,1] && !setArray[0,1] && !setArray[0,0])
            {
                speed.X = 0; speed.Y = 1;
            }
            if (setArray[0,1] && setArray[1,1] && !setArray[1,0] && !setArray[0,0])
            {
                speed.X = -1; speed.Y = 0;
            }
            if (setArray[0,0] && setArray[0,1] && !setArray[1,0] && !setArray[1,1])
            {
                speed.X = 0; speed.Y = -1;
            }

            updateDirection();

        }

        public void updateDirection()
        {
            bool[,] setArray = getBlockSetArray();



            if (setArray[0,0] && !setArray[0,1] && !setArray[1,0] && !setArray[1,1])
            {
                speed.X = 0; speed.Y = -1;
            }
            if (setArray[1,0] && !setArray[0,1] && !setArray[0,0] && !setArray[1,1])
            {
                speed.X = 1; speed.Y = 0;
            }
            if (setArray[0,1] && !setArray[0,0] && !setArray[1,0] && !setArray[1,1])
            {
                speed.X = -1; speed.Y = 0;
            }
            if (setArray[1,1] && !setArray[0,1] && !setArray[1,0] && !setArray[0,0])
            {
                speed.X = 0; speed.Y = 1;
            }

            if (setArray[0,0] && setArray[0,1] && setArray[1,0] && !setArray[1,1])
            {
                speed.X = 1; speed.Y = 0;
            }
            if (setArray[1,0] && setArray[0,1] && !setArray[0,0] && setArray[1,1])
            {
                speed.X = -1; speed.Y = 0;
            }
            if (setArray[0,1] && setArray[0,0] && !setArray[1,0] && setArray[1,1])
            {
                speed.X = 0; speed.Y = -1;
            }
            if (setArray[1,1] && !setArray[0,1] && setArray[1,0] && setArray[0,0])
            {
                speed.X = 0; speed.Y = 1;
            }
            speed.Normalize();
            speed *= travelSpeed;

        }

        public bool[,] getBlockSetArray()
        {
            bool[,] setArray = new bool[2,2];

            setArray[0, 0] = Game1.world.get((int)MapTools.mapToGridCoords(getCenter() + new Vector2(-1, -1)).X, (int)MapTools.mapToGridCoords(getCenter() + new Vector2(-1, -1)).Y).Type != BlockType.AIR;
            setArray[0,1] = Game1.world.get((int)MapTools.mapToGridCoords(getCenter() + new Vector2(-1, +1)).X, (int)MapTools.mapToGridCoords(getCenter() + new Vector2(-1, +1)).Y).Type != BlockType.AIR;
            setArray[1,0] = Game1.world.get((int)MapTools.mapToGridCoords(getCenter() + new Vector2(+1, -1)).X, (int)MapTools.mapToGridCoords(getCenter() + new Vector2(+1, -1)).Y).Type != BlockType.AIR;
            setArray[1,1] = Game1.world.get((int)MapTools.mapToGridCoords(getCenter() + new Vector2(+1, +1)).X, (int)MapTools.mapToGridCoords(getCenter() + new Vector2(+1, +1)).Y).Type != BlockType.AIR;

            return setArray;

        }

        Button buttonSpeedPlus, buttonSpeedMinus;

        public override void drawParamMenu(SpriteBatch batch)
        {
            if (buttonSpeedPlus == null)
                buttonSpeedPlus = new Button(new Rectangle(1300, 700, 32, 32), delegate { travelSpeed += 0.1f; }, "saw");
            buttonSpeedPlus.Draw(batch);
            buttonSpeedPlus.Update();
            batch.DrawString(font, ((int)(travelSpeed * 10f)).ToString(), new Vector2(1300, 800), Color.Black);

            if (buttonSpeedMinus == null)
                buttonSpeedMinus = new Button(new Rectangle(1300 - 32, 700, 32, 32), delegate { travelSpeed -= 0.1f; }, "saw");
            buttonSpeedMinus.Draw(batch);
            buttonSpeedMinus.Update();

            base.drawParamMenu(batch);
        }

        public override bool isClickedParamMenu()
        {
            return base.isClickedParamMenu() || buttonSpeedPlus.isClicked() || buttonSpeedMinus.isClicked();
        }
    }
}
