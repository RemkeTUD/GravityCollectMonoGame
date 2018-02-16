﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class EditorGui
    {
        public static BlockType currentBlockType = BlockType.GREEN;

        public static List<GUIElement> guiElements = new List<GUIElement>();
        public static Textfield saveText;
        public static Item currentDragItem = null;
        public static Item currentSelectedItem = null;
        static MouseState state, prevState;
        static KeyboardState kstate, kprevState;
        static Vector2 lastIndexClicked = new Vector2(-1, -1);
        public static void init()
        {
            int y = 0;
            foreach(BlockType bType in BlockType.Values)
            {
                guiElements.Add(new Button(new Rectangle(0, y * 40, 30, 30), bType.setCurrentBlockType, bType.TexName));
                y++;
            }
            y = 0;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Saw), Game1.cManager, 64, 64); }, "saw"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Enemy), Game1.cManager, 64, 64); }, "enemy"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(FreeGravityBox), Game1.cManager, 64, 64); }, "box"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Laser), Game1.cManager, 64, 64); }, "laser"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(RocketSpawner), Game1.cManager, 64, 64); }, "rocketspawner"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Trapdoor), Game1.cManager, 64, 64); }, "openwall"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(OpenWall), Game1.cManager, 64, 64); }, "openwall"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Moving_Box), Game1.cManager, 64, 64); }, "box"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(GravityBox), Game1.cManager, 64, 64); }, "box"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(TractorBeam), Game1.cManager, 64, 64); }, "TractorBeam"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Spike), Game1.cManager, 64, 64); }, "Spike"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Canon), Game1.cManager, 64, 64); }, "Canon"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate {
                ButtonDisapear button = (ButtonDisapear)Game1.world.createInstanceAtMouse(typeof(ButtonDisapear), Game1.cManager, 64, 64);
                currentDragItem = Game1.world.createInstanceAtMouse(typeof(BoxDisapear), Game1.cManager, 64, 64);
                ((BoxDisapear)currentDragItem).button = button;
            }, "Button"));
            y++;

            saveText = new Textfield(new Rectangle(1600 - 266, 900 - 20, 256, 16));
            guiElements.Add(saveText);
            guiElements.Add(new Button(new Rectangle(1600 - 290, 900 - 20, 16, 16), delegate { Game1.world.loadFromXML(); }, "saw"));
            guiElements.Add(new Button(new Rectangle(1600 - 290, 900 - 40, 16, 16), delegate { Game1.world.saveAsXML(); }, "box"));
            

            y++;


        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(GUIElement elem in guiElements)
            {
                elem.Draw(spriteBatch);
            }

            if (!Game1.running && currentSelectedItem != null)
                currentSelectedItem.drawParamMenu(spriteBatch);

        }

        public static bool isGuiClicked()
        {
            foreach (GUIElement element in guiElements)
                if (element.isClicked())
                    return true;
            if (currentSelectedItem == null)
                return false;
            return currentSelectedItem.isClickedParamMenu();
        }

        public static void Update(GraphicsDevice graphicsDevice)
        {

            kstate = Keyboard.GetState();
            if(Keyboard.GetState().IsKeyDown(Keys.H) && kprevState.IsKeyUp(Keys.H))
            {
                Game1.world.changeTheme();
            }

            foreach (GUIElement elem in guiElements)
            {
                elem.Update();
            }

            state = Mouse.GetState();
            World world = Game1.world;

            if (state.ScrollWheelValue > 0)
                Game1.getCam().Zoom *= 1.01f;
            if (state.ScrollWheelValue < 0)
                Game1.getCam().Zoom /= 1.01f;


            Vector2 pos = Vector2.Transform(new Vector2(state.X - 0, state.Y - 0), Matrix.Invert(Game1.getCam().get_transformation(graphicsDevice)));
            if (state.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
            {
                foreach(Item item in Game1.world.items)
                {
                    if(item.collidesWithPoint(pos))
                    {
                        currentDragItem = item;
                        currentSelectedItem = item;
                        break;
                    }
                }
            }
            if (state.RightButton == ButtonState.Pressed && prevState.RightButton == ButtonState.Released)
            {
                foreach (Item item in Game1.world.items)
                {
                    if (item.collidesWithPoint(pos))
                    {
                        item.destroy = true;
                        break;
                    }
                }
            }
            if (state.LeftButton == ButtonState.Pressed)
            {
                if (currentDragItem != null && !Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                {

                    if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt)) {
                        pos.X = pos.X - pos.X%32;
                        pos.Y = pos.Y - pos.Y % 32;
                    }

                    currentDragItem.pos = pos;
                    currentDragItem.spawnPos = currentDragItem.pos;
                }
                else if(!isGuiClicked())
                {
                    if (!Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                    {
                        currentSelectedItem = null;
                        int xCoord = ((int)pos.X / 32);
                        int yCoord = ((int)pos.Y / 32);
                        if (xCoord >= 0 && xCoord < world.width && yCoord >= 0 && yCoord < world.height)
                        {
                            if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift)) {
                                world.blocks[((int)pos.X / 32), (int)(pos.Y / 32)] = new Block(currentBlockType, xCoord, yCoord);
                                if(lastIndexClicked.X != -1)
                                {
                                    world.setLine((int)lastIndexClicked.X, (int)lastIndexClicked.Y ,((int)pos.X / 32), (int)(pos.Y / 32), currentBlockType);
                                }
                                lastIndexClicked = new Vector2(((int)pos.X / 32), (int)(pos.Y / 32));
                            }
                            else
                                world.fill(((int)pos.X / 32), (int)(pos.Y / 32), currentBlockType, world.get((int)(pos.X / 32), (int)(pos.Y / 32)).Type, 0);
                            world.setNeighboursOfBlocks();
                        }
                    }
                    else
                    {
                        pos.X = pos.X - pos.X % 32;
                        pos.Y = pos.Y - pos.Y % 32;
                        if (currentSelectedItem is isTraveling)
                            ((isTraveling)currentSelectedItem).SetEndPoint(pos);
                    }

                }

            }
            else {
                currentDragItem = null;
                lastIndexClicked = new Vector2(-1, -1);
            }
            if (state.RightButton == ButtonState.Pressed)
            {
                int xCoord = ((int)pos.X / 32);
                int yCoord = ((int)pos.Y / 32);
                if (xCoord >= 0 && xCoord < world.width && yCoord >= 0 && yCoord < world.height)
                {
                    world.blocks[((int)pos.X / 32), (int)(pos.Y / 32)].removeHull();
                    world.blocks[((int)pos.X / 32), (int)(pos.Y / 32)] = new Block(BlockType.AIR, xCoord, yCoord);
                    world.setNeighboursOfBlocks();
                }
            }

            kprevState = kstate ;
            prevState = state;
        }
        
    }
}
