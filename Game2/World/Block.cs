using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Block
    {
       

       

        Dictionary<string, string> neighbours;
        static Rectangle rect;

        int texRegionX, texRegionY;
        
        BlockType type;

        Hull hull;

        public Block(BlockType type,  int x, int y)
        {
            this.type = type;
            neighbours = new Dictionary<string, string>();
            rect = new Rectangle(0, 0, 16, 16);
            setDefaultNeighbours();

            if(type != BlockType.AIR)
            {
                hull = new Hull(new Vector2(1.0f), new Vector2(-1.0f, 1.0f), new Vector2(-1.0f), new Vector2(1.0f, -1.0f))
                {
                    Position = new Vector2(x * 16 + 8, y* 16 + 8),
                    Scale = new Vector2(8)
                };
                Game1.penumbra.Hulls.Add(hull);
            }

        }
        
        public void removeHull()
        {
            if(type != BlockType.AIR) {
                //Game1.penumbra.Hulls.CollectionChanged += delegate { Console.WriteLine("Test"); };
            Game1.penumbra.Hulls.Remove(hull);
                hull = null;
                
            }

        }

        public void draw(SpriteBatch spriteBatch, int x, int y)
        {
            rect.X = x * 16;
            rect.Y = y * 16;
            //setTexRegions();
            int texWidth = type.Texture.Width / 4; int texHeight = type.Texture.Height / 4;
            spriteBatch.Draw(type.Texture,
                destinationRectangle: rect,
                sourceRectangle: new Rectangle(texRegionX * texWidth, texRegionY * texHeight, texWidth, texHeight),
                color: Color.White);
        }

        public void drawIllumination(SpriteBatch spriteBatch, int x, int y)
        {
            if (type.Illumination)
            {
                rect.X = x * 16;
                rect.Y = y * 16;
                //setTexRegions();
                int texWidth = type.Texture.Width / 4; int texHeight = type.Texture.Height / 4;
                spriteBatch.Draw(type.Texture,
                    destinationRectangle: rect,
                    sourceRectangle: new Rectangle(texRegionX * texWidth, texRegionY * texHeight, texWidth, texHeight),
                    color: Color.White);
            }

        }


        public Dictionary<string, string> Neighbours { get { return neighbours; } }
        public BlockType Type { get { return type; } }

        public void setDefaultNeighbours()
        {
            string air = "Air";
            Neighbours["Left"] = air;
            Neighbours["Right"] = air;
            Neighbours["Up"] = air;
            Neighbours["Down"] = air;

            Neighbours["UpLeft"] = air;
            Neighbours["UpRight"] = air;
            Neighbours["DownLeft"] = air;
            Neighbours["DownRight"] = air;
        }

        public void setTexRegions()
        {
            if (type.isConnectsToSelf)
            {
                if (Neighbours["Left"] == type.Name && Neighbours["Right"] != type.Name)
                {
                    texRegionX = 2;
                    texRegionY = 1;
                }
                if (Neighbours["Right"] == type.Name && Neighbours["Left"] != type.Name)
                {
                    texRegionX = 0;
                    texRegionY = 1;
                }
                if (Neighbours["Up"] == type.Name && Neighbours["Down"] != type.Name)
                {
                    texRegionX = 1;
                    texRegionY = 2;
                }
                if (Neighbours["Down"] == type.Name && Neighbours["Up"] != type.Name)
                {
                    texRegionX = 1;
                    texRegionY = 0;


                }
                if (Neighbours["Down"] == type.Name && Neighbours["Up"] != type.Name && Neighbours["Left"] == type.Name && Neighbours["Right"] != type.Name)
                {
                    texRegionX = 2;
                    texRegionY = 0;
                }
                if (Neighbours["Down"] == type.Name && Neighbours["Up"] != type.Name && Neighbours["Left"] != type.Name && Neighbours["Right"] == type.Name)
                {
                    texRegionX = 0;
                    texRegionY = 0;
                }
                if (Neighbours["Down"] != type.Name && Neighbours["Up"] == type.Name && Neighbours["Left"] == type.Name && Neighbours["Right"] != type.Name)
                {
                    texRegionX = 2;
                    texRegionY = 2;
                }
                if (Neighbours["Down"] != type.Name && Neighbours["Up"] == type.Name && Neighbours["Left"] != type.Name && Neighbours["Right"] == type.Name)
                {
                    texRegionX = 0;
                    texRegionY = 2;



                }

                if (Neighbours["Left"] == type.Name && Neighbours["Right"] == type.Name && Neighbours["Up"] == type.Name && Neighbours["Down"] == type.Name)
                {
                    texRegionX = 1;
                    texRegionY = 1;
                }

                if (Neighbours["Down"] == type.Name && Neighbours["Up"] == type.Name && Neighbours["Left"] == type.Name && Neighbours["Right"] == type.Name &&
                        Neighbours["DownLeft"] == type.Name && Neighbours["DownRight"] == type.Name && Neighbours["UpRight"] != type.Name && Neighbours["UpLeft"] == type.Name)
                {
                    texRegionX = 2;
                    texRegionY = 3;
                }
                if (Neighbours["Down"] == type.Name && Neighbours["Up"] == type.Name && Neighbours["Left"] == type.Name && Neighbours["Right"] == type.Name &&
                     Neighbours["DownLeft"] == type.Name && Neighbours["DownRight"] != type.Name && Neighbours["UpRight"] == type.Name && Neighbours["UpLeft"] == type.Name)
                {
                    texRegionX = 1;
                    texRegionY = 3;
                }
                if (Neighbours["Down"] == type.Name && Neighbours["Up"] == type.Name && Neighbours["Left"] == type.Name && Neighbours["Right"] == type.Name &&
                     Neighbours["DownLeft"] != type.Name && Neighbours["DownRight"] == type.Name && Neighbours["UpRight"] == type.Name && Neighbours["UpLeft"] == type.Name)
                {
                    texRegionX = 0;
                    texRegionY = 3;
                }
                if (Neighbours["Down"] == type.Name && Neighbours["Up"] == type.Name && Neighbours["Left"] == type.Name && Neighbours["Right"] == type.Name &&
                     Neighbours["DownLeft"] == type.Name && Neighbours["DownRight"] == type.Name && Neighbours["UpRight"] == type.Name && Neighbours["UpLeft"] != type.Name)
                {
                    texRegionX = 3;
                    texRegionY = 3;
                }
            }
            else
            {

                if (Neighbours["Right"] != type.Name && Neighbours["Right"] != BlockType.AIR.Name)
                {
                    texRegionX = 0;
                    texRegionY = 1;
                }
                if (Neighbours["Left"] != type.Name && Neighbours["Left"] != BlockType.AIR.Name)
                {
                    texRegionX = 2;
                    texRegionY = 1;
                }
                if (Neighbours["Down"] != type.Name && Neighbours["Down"] != BlockType.AIR.Name)
                {
                    texRegionX = 1;
                    texRegionY = 0;
                }
                if (Neighbours["Up"] != type.Name && Neighbours["Up"] != BlockType.AIR.Name)
                {
                    texRegionX = 1;
                    texRegionY = 2;


                }
            }
        }
    }
}
