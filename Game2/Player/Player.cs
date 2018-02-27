using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{

   public class Player
    {
        Texture2D textureTest;
        public float speed, fallSpeed;
        public float acceleration = 0.9f, fallAcceleration = 0.4f;
        float maxSpeed = 4.5f, maxFallSpeed = 17.5f;

        public Vector2 size = new Vector2(14, 32);
        public Rectangle rect = new Rectangle(1000, 1000, 16, 32);
        public Vector2 pos = new Vector2(1000,1000);
        public KeyboardState state, prevState;
        public bool flipped;

        public int framesInAir = 0, framesSpacePressed = 0;

        long frames = 0;

        bool showPlayerPositions = false;

        List<PlayerPositionShow> playerPositions = new List<PlayerPositionShow>();

        public List<GravityFlame> flames = new List<GravityFlame>();
        public Player(ContentManager content)
        {
            textureTest = content.Load<Texture2D>("player");
            state = Keyboard.GetState();
            prevState = Keyboard.GetState();

            flames.Add(new GravityFlame());
            flames.Add(new GravityFlame());
            flames.Add(new GravityFlame());
            flames.Add(new GravityFlame());

        }

        public Vector2 getCenter()
        {
            return pos ;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
            SpriteEffects effect;
            if (flipped)
                effect = SpriteEffects.FlipHorizontally;
            else
                effect = SpriteEffects.None;
            spriteBatch.Draw(textureTest, destinationRectangle: rect, color: Color.White, rotation: MapTools.VectorToAngle(WorldInfo.gravity) - (float)Math.PI * 0.5f, origin: new Vector2(textureTest.Width * 0.5f, textureTest.Height * 0.5f), effects: effect);

            int flameId = 0;
            foreach(GravityFlame flame in flames)
            {
                
                flame.update(flameId);
                
                flame.draw(spriteBatch);
                flameId++;

            }

            int playerPosID = 30;
            if(!Game1.running && showPlayerPositions)
            foreach (PlayerPositionShow pl in playerPositions) {

                    pl.Draw(spriteBatch, playerPosID);
                    playerPosID--;
                }

        }

        public void drawIllumination(SpriteBatch spriteBatch)
        {
            int flameId = 0;
            foreach (GravityFlame flame in flames)
            {

                flame.draw(spriteBatch);
                flameId++;

            }
        }
        public void Input(float delta)
        {
            
            


            state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.I) && prevState.IsKeyUp(Keys.I))
                showPlayerPositions = !showPlayerPositions;

            inputJump();
            

            inputMovement();

            if(frames % 10 == 0)
                playerPositions.Add(new PlayerPositionShow(pos, size, flipped, WorldInfo.gravity));
            if (playerPositions.Count > 30)
                playerPositions.RemoveAt(0);

            frames++;


        }

        public void update(float delta)
        {
            applySpeed();
            applyFallSpeed(delta);
           Vector2 gridCoords = MapTools.mapToGridCoords(getCenter());
            if (Game1.world.get((int)gridCoords.X,(int) gridCoords.Y).Type.Killing)
            {
                die();
            }
            

        }

        public void inputJump()
        {
            if (!state.IsKeyDown(Keys.Space) && !isGrounded().collided)
            {
                framesSpacePressed = 2000;
            }
            if (!isGrounded().collided)
            {
                if(fallSpeed < maxFallSpeed)
                if (framesSpacePressed >= 30)
                    fallSpeed += fallAcceleration * 1f;
                else
                    fallSpeed += fallAcceleration * 0.3f;
                framesInAir++;
            }
            else
            {
                fallSpeed = isGrounded().getFallSpeed();

            }

            if (state.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space) && isGrounded().collided && !TextDialog.isInDialog)
            {
                fallSpeed += -5;
                framesSpacePressed++;
            }
            if (!state.IsKeyDown(Keys.Space) && isGrounded().collided)
            {
                framesSpacePressed = 0;
            }
            if (!state.IsKeyDown(Keys.Space) && !isGrounded().collided)
            {
                framesSpacePressed = 2000;
            }
            if (state.IsKeyDown(Keys.Space))
            {
                framesSpacePressed++;
            }

            

        }
        public void inputMovement()
        {
            if (state.IsKeyDown(Keys.A) && !TextDialog.isInDialog)
            {
                flipped = true;
                if (speed > -maxSpeed + isGrounded().getRealSpeed())
                {
                    if (isGrounded().collided)
                        speed -= acceleration;
                    else
                        speed -= acceleration * 0.35f;
                }
            }
            else if (state.IsKeyDown(Keys.D) && !TextDialog.isInDialog)
            {
                flipped = false;
                if (speed < maxSpeed + isGrounded().getRealSpeed())
                {
                    if (isGrounded().collided)
                        speed += acceleration;
                    else
                        speed += acceleration * 0.35f;
                }
            }
            else
            {
                if (speed > 0 + isGrounded().getRealSpeed())
                {
                    if (speed > acceleration)
                    {
                        if (isGrounded().collided)
                            speed -= acceleration;
                        else
                            speed -= acceleration * 0.25f;
                    }
                    else
                        speed = 0 + isGrounded().getRealSpeed();
                }
                if (speed < 0 + isGrounded().getRealSpeed())
                {
                    if (speed < -acceleration)
                    {
                        if (isGrounded().collided)
                            speed += acceleration;
                        else
                            speed += acceleration * 0.25f;
                    }
                    else
                        speed = 0 + isGrounded().getRealSpeed();
                }
            }



            if (!isGrounded().collided && collidesRight() && !flipped)
            {
                if (fallSpeed > 5)
                    fallSpeed -= fallAcceleration + 0.3f;
                if (state.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
                {
                    speed = -7.5f;
                    fallSpeed += -4;
                    if (fallSpeed <= 0)
                        fallSpeed = -6;
                    if (fallSpeed > 0)
                        fallSpeed = -5;
                        framesSpacePressed = 10;
                }
            }

            if (!isGrounded().collided && collidesLeft() && flipped)
            {
                if (fallSpeed > 5)
                    fallSpeed -= fallAcceleration + 0.3f;
                if (state.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
                {
                    speed = +7.5f;
                    fallSpeed += -4;
                    if (fallSpeed <= 0)
                        fallSpeed = -6;
                    if (fallSpeed > 0)
                        fallSpeed = -5;
                    framesSpacePressed = 10;
                }
            }
        }
        public void inputGravityChange()
        {
            if (state.IsKeyDown(Keys.Right) && prevState.IsKeyUp(Keys.Right))
            {
                WorldInfo.setAngle(90);
                float tempSpeed = speed;
                speed = fallSpeed;
                fallSpeed = -tempSpeed;
                //flames.RemoveAt(0);
                Game1.world.gravityChanged = true;
            }
            if (state.IsKeyDown(Keys.Left) && prevState.IsKeyUp(Keys.Left))
            {
                WorldInfo.setAngle(-90);


                float tempSpeed = speed;
                speed = -fallSpeed;
                fallSpeed = tempSpeed;
                //flames.RemoveAt(0);
                Game1.world.gravityChanged = true;
            }
            if (state.IsKeyDown(Keys.Up) && prevState.IsKeyUp(Keys.Up))
            {
                WorldInfo.setAngle(180);
                fallSpeed *= -1;
                speed *= -1;
                //flames.RemoveAt(0);
                Game1.world.gravityChanged = true;
            }
            prevState = state;
        }

        public void die()
        {
            this.speed = 0;
            this.fallSpeed = 0;
            this.pos = Game1.world.playerSpawn;
            foreach (Item item in Game1.world.items)
                item.reset();
            WorldInfo.gravity = new Vector2(0, 1);
        }

        public CollisionInfo isGrounded()
        {
            
            Vector2 pos = MapTools.mapToGridCoords(downPoint());
            if (Game1.world.get((int)pos.X, (int)pos.Y).Type.Collision)
                return new CollisionInfo(true, Vector2.Zero) ;
            else
            {
                foreach(CollisionBox box in Game1.world.collisionBoxes)
                {
                    if (collidesDownWithCollisionBox(box)) {
                        return new CollisionInfo(true, box.speed); ;
                    }
                }
            }
            return new CollisionInfo(false, Vector2.Zero) ;
        }

        public CollisionInfo collidesUp()
        {

            Vector2 pos = MapTools.mapToGridCoords(upPoint());
            if (Game1.world.get((int)pos.X, (int)pos.Y).Type.Collision)
                return new CollisionInfo(true, Vector2.Zero);
            else
            {
                foreach (CollisionBox box in Game1.world.collisionBoxes)
                {
                    if (collidesUpWithCollisionBox(box))
                        return new CollisionInfo(true, box.speed);
                }
            }
            return new CollisionInfo(false, Vector2.Zero);
        }

        public Boolean collidesRight()
        {

            Vector2 pos;
            List<Vector2> points = rightPoints();
            foreach (Vector2 point in points)
            {
                pos = MapTools.mapToGridCoords(point);
                if (Game1.world.get((int)pos.X, (int)pos.Y).Type.Collision)
                    return true;

            }

            foreach (CollisionBox box in Game1.world.collisionBoxes)
            {
                if (!(box is FreeGravityBox) && collidesRightWithCollisionBox(box))
                    return true;
            }

            return false;
        }

        public Boolean collidesLeft()
        {
            Vector2 pos;
            List<Vector2> points = leftPoints();
            foreach(Vector2 point in points) {
                pos = MapTools.mapToGridCoords(point);
                if (Game1.world.get((int)pos.X, (int)pos.Y).Type.Collision)
                    return true;

            }

            foreach (CollisionBox box in Game1.world.collisionBoxes)
            {
                 if (!(box is FreeGravityBox) && collidesLeftWithCollisionBox(box))
                     return true;
            }
            
            return false;
        }

        public Boolean collidesDownWithCollisionBox(CollisionBox box)
        {
            Vector2 playerPoint = downPoint();
            return box.collidesWithMovingPoint(playerPoint, getMovingVector());
        }
        public Boolean collidesRightWithCollisionBox(CollisionBox box)
        {
            foreach (Vector2 point in rightPoints())
            {
                Vector2 playerPoint = point;
                if (box.collidesWithMovingPoint(playerPoint, getMovingVector()))
                    return true;
            }
            return false;
        }
        public Boolean collidesLeftWithCollisionBox(CollisionBox box)
        {
            foreach(Vector2 point in leftPoints()) {
            Vector2 playerPoint = point;
            if( box.collidesWithMovingPoint(playerPoint, getMovingVector()))
                return true;
            }
            return false;
        }
        public Boolean collidesUpWithCollisionBox(CollisionBox box)
        {
            Vector2 playerPoint = upPoint();
            return box.collidesWithMovingPoint(playerPoint, getMovingVector());
        }

        public Vector2 downPoint()
        {
            return getCenter() + WorldInfo.gravity * size.Y * 0.501f;
        }

        public Vector2 upPoint()
        {
            return getCenter() - WorldInfo.gravity * size.Y * 0.501f;
        }

        public List<Vector2> leftPoints()
        {
            List<Vector2> points = new List<Vector2>();
            points.Add(getCenter() - (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.501f);
            points.Add(getCenter() - (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.501f - WorldInfo.gravity * size.Y * 0.4f);
            points.Add(getCenter() - (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.501f + WorldInfo.gravity * size.Y * 0.4f);

            return points;
        }

        public List<Vector2> rightPoints()
        {
            List<Vector2> points = new List<Vector2>();
            points.Add(getCenter() + (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.501f);
            points.Add(getCenter() + (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.501f - WorldInfo.gravity * size.Y * 0.4f);
            points.Add(getCenter() + (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.501f + WorldInfo.gravity * size.Y * 0.4f);

            return points;
        }

        public void setYSpeed(float speed)
        {
            Vector2 g = WorldInfo.gravity;
            if (g.Y < -0.5f)
                fallSpeed = speed;
            if (g.Y > 0.5f)
                fallSpeed = -speed;
            if (g.X < -0.5f)
                this.speed = -speed;
            if (g.X > 0.5f)
                this.speed = speed;
        }

        public void setXSpeed(float speed)
        {
            Vector2 g = WorldInfo.gravity;
            if (g.X < -0.5f)
                fallSpeed = -speed;
            if (g.X > 0.5f)
                fallSpeed = speed;
            if (g.Y < -0.5f)
                this.speed = -speed;
            if (g.Y > 0.5f)
                this.speed = speed;
        }

        public float getXSpeed()
        {
            Vector2 g = WorldInfo.gravity;
            if (g.X < -0.5f)
                return -fallSpeed;
            if (g.X > 0.5f)
                return fallSpeed;
            if (g.Y < -0.5f)
                return -speed;
            if (g.Y > 0.5f)
              return speed;
            return 0;
        }

        public float getYSpeed()
        {
            Vector2 g = WorldInfo.gravity;
            if (g.X < -0.5f)
                return speed;
            if (g.X > 0.5f)
                return -speed;
            if (g.Y < -0.5f)
                return -fallSpeed;
            if (g.Y > 0.5f)
                return fallSpeed;
            return 0;
        }

        public Vector2 getMovingVector()
        {
            return new Vector2((int)(speed * MapTools.getXMultiplier()) + (int)(fallSpeed * (float)(WorldInfo.gravity.X)),
                (int)(speed * MapTools.getYMultiplier()) + (int)(fallSpeed * (float)(WorldInfo.gravity.Y)));
        }

        public void applySpeed()
        {
            if ((speed > 0 && !collidesRight()) || (speed < 0 && !collidesLeft()))
            {
                pos.X += (float)(speed * Math.Round(MapTools.getXMultiplier()));
                pos.Y += (float)(speed * Math.Round(MapTools.getYMultiplier()));
            }
            else
                speed = 0;
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
        }

        public virtual bool collidesWithMovingPoint(Vector2 point, Vector2 direction)
        {

            Vector2 boxUpLeft = getCenter() - size * 0.5f;
            Vector2 boxDownRight = getCenter() + size * 0.5f;
            return (boxUpLeft.X < point.X && boxUpLeft.Y < point.Y && boxDownRight.X > point.X && boxDownRight.Y > point.Y);
        }

        public void correctDownCollision()
        {
            while(isGrounded().collided) {
                pos.X -= (float)(WorldInfo.gravity.X);
                pos.Y -= (float)(WorldInfo.gravity.Y);
            }
            pos.X += (float)Math.Round(WorldInfo.gravity.X);
            pos.Y += (float)Math.Round(WorldInfo.gravity.Y);

            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
        }
        public void correctUpCollision()
        {
            while (collidesUp().collided)
            {
                pos.X += (float)(WorldInfo.gravity.X);
                pos.Y += (float)(WorldInfo.gravity.Y);
            }
            pos.X -= (float)Math.Round(WorldInfo.gravity.X);
            pos.Y -= (float)Math.Round(WorldInfo.gravity.Y);

            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
        }

        public void correctRightCollision()
        {
            if(collidesRight()) { 
                while (collidesRight())
                {
                    pos.X -= (float)(WorldInfo.gravity.Y);
                    pos.Y += (float)(WorldInfo.gravity.X);
                }
                pos.X += (float)Math.Round(WorldInfo.gravity.Y);
                pos.Y -= (float)Math.Round(WorldInfo.gravity.X);

                rect.X = (int)pos.X;
                rect.Y = (int)pos.Y;
            }
        }

        public void correctLeftCollision()
        {
            if (collidesLeft())
            {
                while (collidesLeft())
                {
                    pos.X += (float)(WorldInfo.gravity.Y);
                    pos.Y -= (float)(WorldInfo.gravity.X);
                }
                pos.X -= (float)Math.Round(WorldInfo.gravity.Y);
                pos.Y += (float)Math.Round(WorldInfo.gravity.X);

                rect.X = (int)pos.X;
                rect.Y = (int)pos.Y;
            }
        }


        public void applyFallSpeed(float delta)
        {
            if(fallSpeed > 0) {
                pos.X += (int)(fallSpeed * (float)(Math.Round(WorldInfo.gravity.X)));
                pos.Y += (int)(fallSpeed * (float)(Math.Round(WorldInfo.gravity.Y)));
            }
            else if (fallSpeed < 0)
            {
                for (int i = 0; i > fallSpeed; i--)
                {

                    if (collidesUp().collided)
                    {
                        framesSpacePressed = 30;
                        fallSpeed = 1 + MathHelper.Clamp(collidesUp().getFallSpeed(),0,10000);
                        //break;
                    }
                    else
                    {
                        pos.X -= (float)(WorldInfo.gravity.X);
                        pos.Y -= (float)(WorldInfo.gravity.Y);
                    }
                }
            }

            /*
            if(fallSpeed > 0) {
                for(int i = 0; i < fallSpeed; i++) {
                    if (isGrounded().collided)
                    {
                        fallSpeed = isGrounded().getFallSpeed() + 1;
                        //pos += isGrounded().speed;
                        if (!state.IsKeyDown(Keys.Space))
                            framesSpacePressed = 0;
                        framesInAir = 0;
                        //break;
                    } else {
                        pos.X += (float)(WorldInfo.gravity.X);
                        pos.Y += (float)(WorldInfo.gravity.Y);
                    }
                }
            }
            else if (fallSpeed < 0)
            {
                for (int i = 0; i > fallSpeed; i--)
                {

                    if (collidesUp())
                    {
                        framesSpacePressed = 30;
                        fallSpeed = 3;
                        //break;
                    }
                    else
                    {
                        pos.X -= (float)(WorldInfo.gravity.X);
                        pos.Y -= (float)(WorldInfo.gravity.Y);
                    }
                }
            }
        */
        }
    }
}
