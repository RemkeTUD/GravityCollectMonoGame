﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1
{
    public class World
    {
        public int width, height;
        public Block[,] blocks;
        ContentManager content;
        public List<Item> items;
        public List<CollisionBox> collisionBoxes;

        public static string[] themeNames;

        Texture2D background;

        public string currentTheme = "Normal";

        BlockType currentBlockType = BlockType.GREEN;

        public Vector2 playerSpawn = new Vector2(1000,1000);

        public World(int width, int height, ContentManager content)
        {
            this.width = width; this.height = height;
            this.content = content;
            foreach(BlockType block in BlockType.Values)
            {
                block.reloadTexture(content, currentTheme);
            }
            blocks = new Block[width, height];
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    blocks[x, y] = new Block(BlockType.AIR, x, y);
                }
            }
            setNeighboursOfBlocks();

            items = new List<Item>();
            collisionBoxes = new List<CollisionBox>();

            //ButtonDisapear button = new ButtonDisapear(content, 1024, 1024, 64, 64);
            //BoxDisapear box = new BoxDisapear(content, 704, 704, 128, 128,button);



            //addItem(box);
            //addItem(button);

            //addItem(new Saw(content, 1024, 704, 128, 1, new Vector2(1200, 900)));
            addItem(new RocketSpawner(content, 1024, 1024, 64, 64, 1, new Vector2(1024, 1500)));

            reloadBackgroundTexture(Game1.cManager, currentTheme);

            

            //CollisionBox box = new GravityBox(content, 700, 700, 128, 128, 100, true);
            //items.Add(box);
            //collisionBoxes.Add(box);

        }

        public void reloadBackgroundTexture(ContentManager content, string theme)
        {

            string texName = "themes/" + theme + "/background/test";
            background = content.Load<Texture2D>(texName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                background,
                position: Game1.getPlayer().pos * 0.5f - new Vector2(1100,1000),
                scale: new Vector2(4,4),
                effects: SpriteEffects.None,
                layerDepth: 1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(blocks[x,y].Type.Name != "Air")
                    {
                        
                        
                        blocks[x, y].draw(spriteBatch, x, y);
                        
                    }
                }
            }

            foreach(Item item in items)
            {
                item.Draw(spriteBatch);
            }
            

        }
        public void applyChanges(GraphicsDevice graphicsDevice)
        {

            for (int i = 0; i < items.Count; i++)
            {
                items.ElementAt(i).ApplyChanges();
            }
        }
            public void update(GraphicsDevice graphicsDevice)
        {

            for (int i = 0; i < items.Count; i++)
            {
                items.ElementAt(i).Update();
            }

            

            
        }

        public int fill(int x, int y, BlockType type, BlockType startType,int fills)
        {
            if (startType == type)
                return 0;
            fills++;
            if(get(x,y).Type != startType)
            {
                return fills;
            }
            if (fills > 100)
                return fills;
            if (x >= 0 && x < width && y >= 0 && y < height)
                blocks[x, y] = new Block(type, x , y);
            else
                return fills;
            fill(x + 1, y, type, startType, fills);
            fill(x - 1, y, type, startType, fills);
            fill(x, y + 1, type, startType, fills);
            fill(x, y - 1, type, startType, fills);
            return fills;
        }

        public Block get(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return blocks[x, y];
            }
            else
                return new Block(BlockType.AIR, x , y);
        }

        public void setLine(int x0, int y0, int x1, int y1, BlockType type)
        {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = -Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx + dy, e2; /* error value e_xy */

            while (true)
            {
                blocks[x0, y0] = new Block(type, x0, y0);
                if (x0 == x1 && y0 == y1) break;
                e2 = 2 * err;
                if (e2 > dy) { err += dy; x0 += sx; } /* e_xy+e_x > 0 */
                if (e2 < dx) { err += dx; y0 += sy; } /* e_xy+e_y < 0 */
            }
        }

        public void setNeighboursOfBlocks()
        {
            for (int x = 1; x < width - 2; x++)
            {
                for (int y = 1; y < height - 2; y++)
                {
                    if(blocks[x, y].Type.Name!="Air") {
                        blocks[x,y].Neighbours["Left"] = blocks[x - 1,y].Type.Name;
                        blocks[x,y].Neighbours["Right"] = blocks[x + 1,y].Type.Name;
                        blocks[x,y].Neighbours["Up"] = blocks[x,y - 1].Type.Name;
                        blocks[x,y].Neighbours["Down"] = blocks[x, y + 1].Type.Name;

                        blocks[x,y].Neighbours["UpLeft"] = blocks[x - 1, y - 1].Type.Name;
                        blocks[x,y].Neighbours["UpRight"] = blocks[x + 1, y - 1].Type.Name;
                        blocks[x,y].Neighbours["DownLeft"] = blocks[x - 1, y + 1].Type.Name;
                        blocks[x,y].Neighbours["DownRight"] = blocks[x + 1, y + 1].Type.Name;
                    }
                }
            }
            for (int x = 1; x < width - 2; x++)
            {
                for (int y = 1; y < height - 2; y++)
                {
                    if (blocks[x, y].Type.Name != "Air")
                    {
                        blocks[x, y].setTexRegions();
                        
                    }
                }
            }
            
        }

        public void setNeighboursOfBlock(int xc, int yc)
        {
            for (int x = xc-2; x < xc+2; x++)
            {
                for (int y = yc-2; y < yc+2; y++)
                {
                    if (blocks[x, y].Type.Name != "Air")
                    {
                        blocks[x, y].Neighbours["Left"] = blocks[x - 1, y].Type.Name;
                        blocks[x, y].Neighbours["Right"] = blocks[x + 1, y].Type.Name;
                        blocks[x, y].Neighbours["Up"] = blocks[x, y - 1].Type.Name;
                        blocks[x, y].Neighbours["Down"] = blocks[x, y + 1].Type.Name;

                        blocks[x, y].Neighbours["UpLeft"] = blocks[x - 1, y - 1].Type.Name;
                        blocks[x, y].Neighbours["UpRight"] = blocks[x + 1, y - 1].Type.Name;
                        blocks[x, y].Neighbours["DownLeft"] = blocks[x - 1, y + 1].Type.Name;
                        blocks[x, y].Neighbours["DownRight"] = blocks[x + 1, y + 1].Type.Name;
                    }
                }
            }
            for (int x = xc - 2; x < xc + 2; x++)
            {
                for (int y = yc - 2; y < yc + 2; y++)
                {
                    if (blocks[x, y].Type.Name != "Air")
                    {
                        blocks[x, y].setTexRegions();

                    }
                }
            }

        }


        public void cleanDestroyedItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items.ElementAt(i).destroy)
                {
                    items.Remove(items.ElementAt(i));
                    i--;

                }
            }
            for (int i = 0; i < collisionBoxes.Count; i++)
            {
                if (collisionBoxes.ElementAt(i).destroy)
                {
                    collisionBoxes.Remove(collisionBoxes.ElementAt(i));
                    i--;

                }
            }
        }

        public void addItem(Item item)
        {
            items.Add(item);
            if(item is CollisionBox)
            {
                collisionBoxes.Add((CollisionBox)item);
            }
        }

        public void correctDownCollisions()
        {
            
            for (int i = 0; i < items.Count; i++)
            {
                items.ElementAt(i).correctDownCollision();
            }
        }
        public void correctLeftCollisions()
        {

            for (int i = 0; i < items.Count; i++)
            {
                items.ElementAt(i).correctLeftCollision();
            }
        }
        public void correctRightCollisions()
        {

            for (int i = 0; i < items.Count; i++)
            {
                items.ElementAt(i).correctRightCollision();
            }
        }

        public void checkForWalls()
        {

            for (int i = 0; i < items.Count; i++)
            {
                items.ElementAt(i).checkForWalls();
            }
            for (int i = 0; i < items.Count; i++)
            {
                items.ElementAt(items.Count - 1 - i).checkForWalls();
            }
        }

        public void frameInit()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items.ElementAt(i).frameInit();
            }
        }

        public CollisionInfo collidesWithPoint(Vector2 pos)
        {
            Vector2 gridPos = MapTools.mapToGridCoords(pos);
            if (Game1.world.get((int)gridPos.X, (int)gridPos.Y).Type.Collision)
                return new CollisionInfo(true, Vector2.Zero, true);
            else
            {
                foreach (CollisionBox box in collisionBoxes)
                {
                    if(box.collidesWithMovingPoint(pos, Vector2.Zero))
                    {
                        return new CollisionInfo(true, box.speed);
                    }
                }
            }
            return new CollisionInfo(false, Vector2.Zero);
            
        }

        public CollisionInfo collidesWithPoints(List<Vector2> points)
        {
            foreach(Vector2 pos in points) {
                Vector2 gridPos = MapTools.mapToGridCoords(pos);
                if (Game1.world.get((int)gridPos.X, (int)gridPos.Y).Type.Collision)
                    return new CollisionInfo(true, Vector2.Zero, true);
                else
                {
                    foreach (CollisionBox box in collisionBoxes)
                    {
                        if (box.collidesWithMovingPoint(pos, Vector2.Zero))
                        {
                            return new CollisionInfo(true, box.speed);
                        }
                    }
                }
            }
            return new CollisionInfo(false, Vector2.Zero);
        }


        public Item createInstanceAtMouse(Type type, ContentManager content, int width, int height)
        {
            MouseState state = Mouse.GetState();
            Vector2 pos = Vector2.Transform(new Vector2(state.X - 0, state.Y - 0), Matrix.Invert(Game1.getCam().get_transformation(Game1.graphicsDevice)));
            Item item = (Item)Activator.CreateInstance(type, content, pos.X, pos.Y , width, height);
            addItem(item);
            return item;
        }

        public void loadFromXML()
        {

            items.Clear();
            collisionBoxes.Clear();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    blocks[x, y] = new Block(BlockType.AIR,x ,y);
                }
            }

                    using (XmlReader reader = XmlReader.Create(EditorGui.saveText.text + ".xml"))
            {
                reader.MoveToContent();
                while(reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if(reader.Name == "Block")
                        {
                            reader.Read();
                            reader.Read();
                            int x = Int32.Parse(reader.Value);
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            int y = Int32.Parse(reader.Value);
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            string type =   reader.Value;
                            blocks[x, y] = new Block(BlockType.getBlockTypeByName(type),x ,y);

                        }
                        if (reader.Name == "Item")
                        {

                            List<Object> param = new List<object>();
                            param.Add(Game1.cManager);
                            reader.Read();
                            reader.Read();
                            Type type = Type.GetType(reader.Value);

                            while (reader.Read())
                            {
                                if (reader.Name == "Item")
                                    break;
                                if (reader.NodeType == XmlNodeType.Text)
                                    param.Add(Int32.Parse(reader.Value));
                                
                            }
                            Console.WriteLine(param);
                            addItem((Item)Activator.CreateInstance(type, param.ToArray()));

                        }
                    }
                    


                }
            }

            setNeighboursOfBlocks();

        }

        public void saveAsXML()
        {
            using (XmlWriter writer = XmlWriter.Create(EditorGui.saveText.text + ".xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("World");
                    writer.WriteElementString("Height", height.ToString());
                    writer.WriteElementString("Width", width.ToString());

                    writer.WriteStartElement("Blocks");

                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            if(blocks[x,y].Type != BlockType.AIR) {
                                writer.WriteStartElement("Block");
                                writer.WriteElementString("X", x.ToString());
                                writer.WriteElementString("Y", y.ToString());
                                writer.WriteElementString("Type", blocks[x, y].Type.Name);
                                writer.WriteEndElement();
                            }
                        }
                    }
                    writer.WriteEndElement();

                writer.WriteStartElement("Items");

                foreach(Item item in items)
                {
                    writer.WriteStartElement("Item");
                    writer.WriteElementString("Type", item.GetType().FullName);
                    foreach (string key in item.getAttributeList().Keys)
                    {
                        writer.WriteElementString(key, item.getAttributeList()[key]);
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();

            }
        }
        public void changeTheme()
        {

            int currentThemeIndex = 0;
            for(int i = 0; i< themeNames.Length; i++)
            {
                if (themeNames[i] == currentTheme)
                    currentThemeIndex = i;
            }
            currentThemeIndex += 1;
            currentThemeIndex %= themeNames.Length;

            currentTheme = themeNames[currentThemeIndex];

            reloadBackgroundTexture(Game1.cManager, currentTheme);

            foreach (BlockType block in BlockType.Values)
            {
                block.reloadTexture(content, currentTheme);
            }
            foreach (Item item in items)
            {
                item.reloadTexture(content, currentTheme);
            }
        }
    }


}
