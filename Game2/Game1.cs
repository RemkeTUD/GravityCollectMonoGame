using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System;
using System.IO;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch, guiBatch;
        static Player player;
        public static World world;
        static Camera2d cam;
        RenderTarget2D rt;

        RenderTarget2D preBloomTarget;
        BlurPass blurPass, blurPass1, blurPass2;
        Mipmap bloomMipmap;
        bool bloomEnabled = true;

        public static bool running = false;
        KeyboardState state, prevState;
        public static ContentManager cManager;
        public static GraphicsDevice graphicsDevice;

        public static PenumbraComponent penumbra;

        Light light = new PointLight
        {
            Scale = new Vector2(1000), // Range of the light source (how far the light will travel)
            Radius = 10f,
            Intensity = 0.3f,
            ShadowType = ShadowType.Illuminated // Will not lit hulls themselves
        };

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1920;
            //graphics.SynchronizeWithVerticalRetrace = true;
            //graphics.ToggleFullScreen();

            Content.RootDirectory = "Content";
            cManager = Content;
            penumbra = new PenumbraComponent(this);
            penumbra.Lights.Add(light);
            penumbra.AmbientColor = new Color(0.93f,0.93f,0.93f,1);
            //penumbra.AmbientColor = new Color(0.2f, 0.2f, 0.2f, 1);
            cam = new Camera2d();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            world = new World(256, 256, Content);
            EditorGui.init();
            player = new Player(Content);

            World.themeNames = new string[Directory.GetDirectories(Content.RootDirectory + "/themes").Length];
            int i = 0;
            foreach (string folder in Directory.GetDirectories(Content.RootDirectory + "/themes"))
            {

                World.themeNames[i] = Path.GetFileName(folder);
                i++;

            }

            cam.Zoom = 1f;
            rt = new RenderTarget2D(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            preBloomTarget = new RenderTarget2D(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            blurPass = new BlurPass(GraphicsDevice, Content, (int)Math.Round(graphics.PreferredBackBufferWidth / 32.0f), (int)Math.Round(graphics.PreferredBackBufferHeight / 32.0f), 1.5f);
            blurPass1 = new BlurPass(GraphicsDevice, Content, (int)Math.Round(graphics.PreferredBackBufferWidth / 16.0f), (int)Math.Round(graphics.PreferredBackBufferHeight / 16.0f), 2.5f);
            blurPass2 = new BlurPass(GraphicsDevice, Content, (int)Math.Round(graphics.PreferredBackBufferWidth / 4.0f), (int)Math.Round(graphics.PreferredBackBufferHeight / 4.0f), 1.2f);
            graphicsDevice = graphics.GraphicsDevice;
            bloomMipmap = new Mipmap(preBloomTarget, 5, GraphicsDevice);
            penumbra.Initialize();


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            guiBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.O))
                penumbra.AmbientColor = new Color(penumbra.AmbientColor.R+1, penumbra.AmbientColor.G+1, penumbra.AmbientColor.B+1);
            if (state.IsKeyDown(Keys.I))
                penumbra.AmbientColor = new Color(penumbra.AmbientColor.R - 1, penumbra.AmbientColor.G - 1, penumbra.AmbientColor.B - 1);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

                // TODO: Add your update logic here

                if (Keyboard.GetState().IsKeyDown(Keys.T) && !prevState.IsKeyDown(Keys.T) && !EditorGui.saveText.isActive)
                {
                    running = !running;
                }

            

            if (!running)
            {
                EditorGui.Update(GraphicsDevice);
            }
            if (running)
            {
                WorldInfo.updateGravity();
                if (WorldInfo.angle == 0)
                {


                    if (WorldInfo.angle == 0)
                    {

                        world.frameInit();

                        //if (!TextDialog.isInDialog)
                        player.Input((float)gameTime.ElapsedGameTime.TotalSeconds);
                        
                        
                        //if (!TextDialog.isInDialog)
                        
                        if (!TextDialog.isInDialog)
                            world.update(GraphicsDevice);
                        FreeGravityBox.setAllSpeedChains();

                        world.checkForWalls();
                        player.update(0);
                        if (!TextDialog.isInDialog)
                            world.applyChanges(GraphicsDevice);


                        world.correctDownCollisions();
                        world.correctRightCollisions();
                        world.correctLeftCollisions();
                       




                        //if(Math.Abs(player.fallSpeed) >= Math.Abs(player.speed)) {
                        
                        player.correctUpCollision();
                        player.correctDownCollision();
                        player.correctRightCollision();
                        player.correctLeftCollision();
                       /* if (player.collidesLeft(true) && player.isGrounded().collided)
                            player.decideCollisionCorrection(true);
                        else if (player.collidesRight(true) && player.isGrounded().collided)
                            player.decideCollisionCorrection(false);
                        
                        if (player.collidesRight(true))
                            player.correctRightCollision();
                        else if (player.collidesLeft(true))
                            player.correctLeftCollision();
                        if (player.isGrounded().collided)
                            player.correctDownCollision();
                        if (player.collidesUp().collided)
                            player.correctUpCollision();*/
                        //player.correctDownCollision();
                        //  player.correctRightCollision();
                        //player.correctLeftCollision();
                        //} else
                        /*{
                            player.correctRightCollision();
                            player.correctLeftCollision();

                            player.correctUpCollision();
                            player.correctDownCollision();
                            

                        }*/
                        if (!TextDialog.isInDialog)
                        player.inputGravityChange();

                    }

                }
            }

            world.cleanDestroyedItems();

            ExplosionHandler.update();


            if (state.IsKeyDown(Keys.Z) && prevState.IsKeyUp(Keys.Z))
            {
                penumbra.Visible = !penumbra.Visible;
            }

            if(state.IsKeyDown(Keys.Y) && prevState.IsKeyUp(Keys.Y))
            {
                bloomEnabled = !bloomEnabled;
            }

            cam.update();
            base.Update(gameTime);

            prevState = state;

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            light.Position = player.getCenter();
            GraphicsDevice.SetRenderTarget(rt);
            penumbra.Transform = cam.get_transformation(GraphicsDevice);
            penumbra.BeginDraw();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: cam.get_transformation(GraphicsDevice));
            world.drawBackground(spriteBatch);
            world.drawOutlines(spriteBatch);
            player.drawOutlines(spriteBatch);
            world.Draw(spriteBatch);
            
            player.Draw(spriteBatch);
            
            spriteBatch.End();
            penumbra.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(null);

            /* Bloom Pass Begin */
            GraphicsDevice.SetRenderTarget(preBloomTarget);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.LinearClamp, transformMatrix: cam.get_transformation(GraphicsDevice));
            world.drawIllumination(spriteBatch);
            player.drawIllumination(spriteBatch);
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            bloomMipmap.generate(spriteBatch);
            //            blurPass.blur(spriteBatch, bloomMipmap.getLevel(4), bloomMipmap.getLevel(4));
            blurPass2.blur(spriteBatch, bloomMipmap.getLevel(1), bloomMipmap.getLevel(1));
            blurPass1.blur(spriteBatch, bloomMipmap.getLevel(3), bloomMipmap.getLevel(3));
            blurPass.blur(spriteBatch, bloomMipmap.getLevel(4), bloomMipmap.getLevel(4));
            /* Bloom Pass End */

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(rt, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), new Color(0.8f, 0.8f, 0.8f, 1));
            spriteBatch.End();

            if (bloomEnabled)
            {
                spriteBatch.Begin(blendState: BlendState.Additive, samplerState: SamplerState.LinearClamp);
                spriteBatch.Draw(bloomMipmap.getLevel(1), new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.Draw(bloomMipmap.getLevel(3), new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.Draw(bloomMipmap.getLevel(4), new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.End();
            }



            guiBatch.Begin();
            EditorGui.Draw(guiBatch);
            TextDialog.draw(guiBatch);
            guiBatch.End();
            
            base.Draw(gameTime);
        }

        public static Camera2d getCam()
        {
            return cam;
        }

        public static Player getPlayer()
        {
            return player;
        }

    }
}
