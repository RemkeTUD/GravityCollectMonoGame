using Microsoft.Xna.Framework;
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

        static GUIElement bgRight;
        public static void init()
        {
            int buttonSize = Game1.graphics.PreferredBackBufferHeight / 30;

            int y = 0;

            bgRight = new GUIElement(new Rectangle(Game1.graphics.PreferredBackBufferWidth - 330, Game1.graphics.PreferredBackBufferHeight - 60, 330, 60), "gui/bgright");

            foreach(BlockType bType in BlockType.Values)
            {
                guiElements.Add(new Button(new Rectangle(0, y * (buttonSize + 10), buttonSize, buttonSize), bType.setCurrentBlockType, bType.TexName));
                y++;
            }
            y = 0;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Saw), Game1.cManager, 32, 32); }, "saw"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(StaticEnemy), Game1.cManager, 32, 32); }, "Enemy"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(FreeGravityBox), Game1.cManager, 32, 32); }, "box"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Laser), Game1.cManager, 32, 32); }, "laser"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(RocketSpawner), Game1.cManager, 32, 32); }, "rocketspawner"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Trapdoor), Game1.cManager, 32, 32); }, "openwall"));
            y++;
<<<<<<< HEAD
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(OpenWall), Game1.cManager, 32, 32); }, "openwall"));
=======
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(OpenWall), Game1.cManager, 32, 32); }, "halfopend"));
>>>>>>> cb78b7248243b04f442c0b1dd7a883ee2d1776f1
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Moving_Box), Game1.cManager, 32, 32); }, "box"));
            y++;
<<<<<<< HEAD
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(GravityBox), Game1.cManager, 32, 32); }, "box"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(InversedGravityBox), Game1.cManager, 32, 32); }, "box"));
=======
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(GravityBox), Game1.cManager, 32, 32); }, "fallstone"));
            y++;
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(InversedGravityBox), Game1.cManager, 32, 32); }, "fallstone"));
>>>>>>> cb78b7248243b04f442c0b1dd7a883ee2d1776f1
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(TractorBeam), Game1.cManager, 32, 32); }, "TractorBeam"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Spike), Game1.cManager, 32, 32); }, "Spike"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Canon), Game1.cManager, 32, 32); }, "Canon"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate {
                ButtonDisapear button = (ButtonDisapear)Game1.world.createInstanceAtMouse(typeof(ButtonDisapear), Game1.cManager, 32, 32);
                currentDragItem = Game1.world.createInstanceAtMouse(typeof(BoxDisapear), Game1.cManager, 32, 32);
                ((BoxDisapear)currentDragItem).button = button;
            }, "Button"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(OnGravityChangeBlock), Game1.cManager, 32, 32); }, "Box"));
            y++;
<<<<<<< HEAD
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Trampoline), Game1.cManager, 32, 32); }, "OpenWall"));
=======
            guiElements.Add(new Button(new Rectangle(1600 - 30, y * 40, 30, 30), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Trampoline), Game1.cManager, 32, 32); }, "trampoline"));
>>>>>>> cb78b7248243b04f442c0b1dd7a883ee2d1776f1
            y++;

            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate {
                currentDragItem = Game1.world.createInstanceAtMouse(typeof(Sign), Game1.cManager, 32, 32);
                List<Textbox> boxes = new List<Textbox>();
                boxes.Add(new Textbox("Dies ist ein Text zum ausprobieren der soeben eingefuegten Funktion. Felix suckt. Remke ist der Beste. Es macht Spass sich dumme Texte auszudenken, insbesondere wenn sie Felix beleidigen. Eisbaeren sind keine Rudeltiere, sie jagen Robben zum ueberleben und Leben in Australien."));
                boxes.Add(new Textbox("Test2"));
                ((Sign)currentDragItem).setDialog(new TextDialog(boxes));
            }, "sign"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Bumper), Game1.cManager, 48, 48); }, "Bumper"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Ink), Game1.cManager, 16, 16); }, "ink"));
            y++;
            

            int x = 2;
            y = 0;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - x * buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Wolf), Game1.cManager, 48, 48); }, "wolf"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - x * buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Tree), Game1.cManager, 128, 128); }, "tree"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - x * buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(DonutBlock), Game1.cManager, 32, 32); }, "box"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - x * buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate {
                TeleporterDestination teleporterDest = (TeleporterDestination)Game1.world.createInstanceAtMouse(typeof(TeleporterDestination), Game1.cManager, 32, 32);
                currentDragItem = Game1.world.createInstanceAtMouse(typeof(Teleporter), Game1.cManager, 32, 32);
                ((Teleporter)currentDragItem).dest = teleporterDest;
            }, "teleporter"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - x * buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(RotateSpike), Game1.cManager, 32, 128); }, "rotateSpike"));
            y++;
            guiElements.Add(new Button(new Rectangle(Game1.graphics.PreferredBackBufferWidth - x * buttonSize, y * (buttonSize + 10), buttonSize, buttonSize), delegate { currentDragItem = Game1.world.createInstanceAtMouse(typeof(Raven), Game1.cManager, 32, 32); }, "raven"));
            y++;

            saveText = new Textfield(new Rectangle(20, Game1.graphics.PreferredBackBufferHeight - 20, 256, 16));
            guiElements.Add(saveText);
            guiElements.Add(new Button(new Rectangle(0, Game1.graphics.PreferredBackBufferHeight - 20, 16, 16), delegate { Game1.world = Game1.world.loadFromXML(EditorGui.saveText.text + ".xml"); Game1.world.updateWorldAfterLoad(); }, "gui/save"));
            guiElements.Add(new Button(new Rectangle(0, Game1.graphics.PreferredBackBufferHeight - 40, 16, 16), delegate { Game1.world.saveAsXML(EditorGui.saveText.text + ".xml"); }, "gui/load"));

            

            y++;


        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (currentSelectedItem != null)
                bgRight.Draw(spriteBatch);

            foreach (GUIElement elem in guiElements)
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

            foreach (GUIElement elem in guiElements)
            {
                elem.Update();
            }
            if(!saveText.isActive) {
            if (Keyboard.GetState().IsKeyDown(Keys.H) && kprevState.IsKeyUp(Keys.H))
            {
                Game1.world.changeTheme();
            }

                if (Keyboard.GetState().IsKeyDown(Keys.G) && !kprevState.IsKeyDown(Keys.G))
                {
                    Game1.world.saveAsXML(EditorGui.saveText.text + ".xml");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.F) && !kprevState.IsKeyDown(Keys.F))
                {

                    Game1.world = Game1.world.loadFromXML(EditorGui.saveText.text + ".xml");
                    Game1.world.updateWorldAfterLoad();
                }

                state = Mouse.GetState();
            World world = Game1.world;

            if (state.ScrollWheelValue > prevState.ScrollWheelValue)
                Game1.getCam().targetZoom *= 1.2f;
            if (state.ScrollWheelValue < prevState.ScrollWheelValue)
                Game1.getCam().targetZoom /= 1.2f;
            


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
                if(kstate.IsKeyDown(Keys.P))
                {
                    pos.X = pos.X - pos.X % 16;
                    pos.Y = pos.Y - pos.Y % 16;
                    Game1.world.playerSpawn = pos;
                    Game1.getPlayer().pos = pos;
                }
            }
            if (state.RightButton == ButtonState.Pressed && prevState.RightButton == ButtonState.Released)
            {
                foreach (Item item in Game1.world.items)
                {
                    if (item.collidesWithPoint(pos))
                    {
                        item.destroy = true;
                            currentSelectedItem = null;
                        break;
                    }
                }
            }
            if (state.LeftButton == ButtonState.Pressed)
            {
                if (currentDragItem != null && !Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                {

                    if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
                    {
                        pos.X = pos.X - pos.X % 16;
                        pos.Y = pos.Y - pos.Y % 16;
                    }

                    currentDragItem.pos = pos;
                        if (currentDragItem is MoveableItem)
                            ((MoveableItem)currentDragItem).endPoint = pos;
                    currentDragItem.spawnPos = currentDragItem.pos;
                }
                else if (!isGuiClicked() && Keyboard.GetState().IsKeyUp(Keys.P)) 
                {
                    if (!Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                    {
                        currentSelectedItem = null;
                        int xCoord = ((int)pos.X / 16);
                        int yCoord = ((int)pos.Y / 16);
                        if (xCoord >= 0 && xCoord < world.width && yCoord >= 0 && yCoord < world.height)
                        {
                            if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift)) {
                                if(world.blocks[((int)pos.X / 16), (int)(pos.Y / 16)].Type != currentBlockType) {
                                world.blocks[((int)pos.X / 16), (int)(pos.Y / 16)] = new Block(currentBlockType, xCoord, yCoord);
                                if(lastIndexClicked.X != -1)
                                {
                                    world.setLine((int)lastIndexClicked.X, (int)lastIndexClicked.Y ,((int)pos.X / 16), (int)(pos.Y / 16), currentBlockType);
                                }
                                lastIndexClicked = new Vector2(((int)pos.X / 16), (int)(pos.Y / 16));
                                }
                            }
                            else
                                world.fill(((int)pos.X / 16), (int)(pos.Y / 16), currentBlockType, world.get((int)(pos.X / 16), (int)(pos.Y / 16)).Type, 0);
                            world.setNeighboursOfBlock(((int)pos.X / 16), (int)(pos.Y / 16));
                        }
                    }
                    else
                    {
                        pos.X = pos.X - pos.X % 16;
                        pos.Y = pos.Y - pos.Y % 16;
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
                int xCoord = ((int)pos.X / 16);
                int yCoord = ((int)pos.Y / 16);
                if (xCoord >= 0 && xCoord < world.width && yCoord >= 0 && yCoord < world.height)
                {
                    world.blocks[((int)pos.X / 16), (int)(pos.Y / 16)].removeHull();
                    world.blocks[((int)pos.X / 16), (int)(pos.Y / 16)] = Game1.world.airDefault;
                    if(prevState.RightButton == ButtonState.Released)
                    world.setNeighboursOfBlocks();
                }
            }
            }
            kprevState = kstate ;
            prevState = state;
        }
        
    }
}
