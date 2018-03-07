using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Game1
{
    [XmlInclude(typeof(MoveableItem))]
    [XmlInclude(typeof(Saw))]
    [XmlInclude(typeof(Bumper))]
    [XmlInclude(typeof(Enemy))]
    [XmlInclude(typeof(BoxDisapear))]
    [XmlInclude(typeof(ButtonDisapear))]
    [XmlInclude(typeof(Canon))]
    [XmlInclude(typeof(CanonBall))]
    [XmlInclude(typeof(CollisionBox))]
    [XmlInclude(typeof(FreeGravityBox))]
    [XmlInclude(typeof(GravityBox))]
    [XmlInclude(typeof(InversedGravityBox))]
    [XmlInclude(typeof(Laser))]
    [XmlInclude(typeof(Moving_Box))]
    [XmlInclude(typeof(OnGravityChangeBlock))]
    [XmlInclude(typeof(OpenWall))]
    [XmlInclude(typeof(RocketSpawner))]
    [XmlInclude(typeof(Sign))]
    [XmlInclude(typeof(Spike))]
    [XmlInclude(typeof(StaticEnemy))]
    [XmlInclude(typeof(Teleporter))]
    [XmlInclude(typeof(TeleporterDestination))]
    [XmlInclude(typeof(TractorBeam))]
    [XmlInclude(typeof(Trampoline))]
    [XmlInclude(typeof(Trapdoor))]
    [XmlInclude(typeof(Textbox))]
    [XmlInclude(typeof(TextDialog))]
    [XmlInclude(typeof(Raycast))]
    [XmlInclude(typeof(CollisionInfo))]
    [XmlInclude(typeof(Wolf))]
    [XmlInclude(typeof(Raven))]
    [XmlInclude(typeof(RotateSpike))]
    [XmlInclude(typeof(Ink))]
    [XmlInclude(typeof(Tree))]
    [XmlInclude(typeof(DonutBlock))]
    public abstract class Item
    {
        public Vector2 speed = new Vector2(0, 0);
        //public Vector2 movingDirection = new Vector2(0, 0);
        protected Texture2D textureTest;

        public Rectangle rect = new Rectangle(1000, 1000, 32, 32);
        public Vector2 pos = new Vector2(1000, 1000);
        public Vector2 size = new Vector2(1000, 1000);
        public Vector2 spawnPos = new Vector2(1000, 1000);
        public float angle;
        public int alpha = 255 ,r =255,g=255 ,b=255;

        public bool destroy = false;
        public bool hasIllumination = false;
        public float illuminationStrength = 1;

        public static SpriteFont font;
        public Rectangle sourceRect;

        public Item()
        {

        }

        public Item(ContentManager content, float x, float y, float width, float height)
        {
            pos.X = x;
            pos.Y = y;
            size.X = width;
            size.Y = height;
            textureTest = null;
            spawnPos = new Vector2(x, y);

            if (font == null)
                font = Game1.cManager.Load<SpriteFont>("font");
            

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
           
            if (sourceRect.Width == 0)
            {
                sourceRect = new Rectangle(0, 0, textureTest.Width, textureTest.Height);

            }
            spriteBatch.Draw(
                textureTest,
                position: pos,
                sourceRectangle: sourceRect,
                color: new Color(r,g,b, alpha),
                rotation: angle,
                origin: new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f),
                scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                effects: SpriteEffects.None,
                layerDepth: 1);
        }

        public virtual void drawOutlines(SpriteBatch spriteBatch)
        {

            if (sourceRect.Width == 0)
            {
                sourceRect = new Rectangle(0, 0, textureTest.Width, textureTest.Height);

            }
            spriteBatch.Draw(
                textureTest,
                position: pos + new Vector2(1f / Game1.getCam().Zoom, 0),
                sourceRectangle: sourceRect,
                color: new Color(0,0,0, alpha),
                rotation: angle,
                origin: new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f),
                scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                effects: SpriteEffects.None,
                layerDepth: 1);
            spriteBatch.Draw(
                textureTest,
                position: pos + new Vector2(-1f / Game1.getCam().Zoom, 0),
                sourceRectangle: sourceRect,
                color: new Color(0, 0, 0, alpha),
                rotation: angle,
                origin: new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f),
                scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                effects: SpriteEffects.None,
                layerDepth: 1);
            spriteBatch.Draw(
                textureTest,
                position: pos + new Vector2(0, 1f / Game1.getCam().Zoom),
                sourceRectangle: sourceRect,
                color: new Color(0, 0, 0, alpha),
                rotation: angle,
                origin: new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f),
                scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                effects: SpriteEffects.None,
                layerDepth: 1);
            spriteBatch.Draw(
                textureTest,
                position: pos + new Vector2(0, -1f / Game1.getCam().Zoom),
                sourceRectangle: sourceRect,
                color: new Color(0, 0, 0, alpha),
                rotation: angle,
                origin: new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f),
                scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                effects: SpriteEffects.None,
                layerDepth: 1);
        }

        public virtual void drawIllumination(SpriteBatch spriteBatch)
        {
            if (hasIllumination)
            {
                if (sourceRect.Width == 0)
                {
                    sourceRect = new Rectangle(0, 0, textureTest.Width, textureTest.Height);

                }
                spriteBatch.Draw(
                    textureTest,
                    position: pos,
                    sourceRectangle: sourceRect,
                    color: new Color(r * illuminationStrength, g * illuminationStrength, b * illuminationStrength, alpha),
                    rotation: angle,
                     origin: new Vector2((float)sourceRect.Width * 0.5f, (float)sourceRect.Height * 0.5f),
                    scale: new Vector2(1 * (size.X / (float)sourceRect.Width), 1 * (size.Y / (float)sourceRect.Height)),
                    effects: SpriteEffects.None,
                    layerDepth: 1);
            }
        }

        public virtual void Update()
        {

        }

        public void ApplyChanges()
        {
            pos.X += (float)speed.X;
            pos.Y += (float)speed.Y;

            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
            rect.Width = (int)size.X;
            rect.Height = (int)size.Y;
        }

        public virtual Vector2 getCenter()
        {
            return pos;
        }

        public virtual bool CollidesWithPlayer()
        {
            Vector2 playerPoint = Game1.getPlayer().getCenter();
            Vector2 boxUpLeft = getCenter() - size * 0.5f;
            Vector2 boxDownRight = getCenter() + size * 0.5f;
            return (boxUpLeft.X < playerPoint.X && boxUpLeft.Y < playerPoint.Y && boxDownRight.X > playerPoint.X && boxDownRight.Y > playerPoint.Y);
        }

        public virtual bool CollidesWithAnyCollisionBox()
        {

            Vector2 boxUpLeft = pos - size * 0.5f;
            Vector2 boxDownRight = pos + size * 0.5f;
            foreach (CollisionBox other in Game1.world.collisionBoxes)
            {
                Vector2 otherUpLeft = other.pos - size * 0.5f;
                Vector2 otherDownRight = other.pos + size * 0.5f;
                Rectangle rectThis = new Rectangle((int)boxUpLeft.X, (int)boxUpLeft.Y, (int)(boxDownRight - boxUpLeft).X, (int)(boxDownRight - boxUpLeft).Y);
                Rectangle rectOther = new Rectangle((int)otherUpLeft.X, (int)otherUpLeft.Y, (int)(otherDownRight - otherUpLeft).X, (int)(otherDownRight - otherUpLeft).Y);
                if (rectThis.Intersects(rectOther))
                    return (true);
            }
            return false;
        }

        public virtual CollisionInfo collidesDownWithMap()
        {
            return Game1.world.collidesWithPoints(downPoints());
        }
        public virtual CollisionInfo collidesUpWithMap()
        {
            return Game1.world.collidesWithPoints(upPoints());
        }
        public virtual CollisionInfo collidesLeftWithMap()
        {
            return Game1.world.collidesWithPoints(leftPoints());
        }
        public virtual CollisionInfo collidesRightWithMap()
        {
            return Game1.world.collidesWithPoints(rightPoints());
        }

        public List<Vector2> downPoints()
        {
            List<Vector2> points = new List<Vector2>();

            points.Add(getCenter() + WorldInfo.gravity * size.Y * 0.51f);
            points.Add(getCenter() + WorldInfo.gravity * size.Y * 0.51f + MapTools.getMultiplierVec() * size.X * 0.4f);
            points.Add(getCenter() + WorldInfo.gravity * size.Y * 0.51f - MapTools.getMultiplierVec() * size.X * 0.4f);

            return points;
        }

        public List<Vector2> upPoints()
        {
            List<Vector2> points = new List<Vector2>();

            points.Add(getCenter() - WorldInfo.gravity * size.Y * 0.51f);
            points.Add(getCenter() - WorldInfo.gravity * size.Y * 0.51f + MapTools.getMultiplierVec() * size.X * 0.4f);
            points.Add(getCenter() - WorldInfo.gravity * size.Y * 0.51f - MapTools.getMultiplierVec() * size.X * 0.4f);

            return points;
        }

        public List<Vector2> leftPoints()
        {
            List<Vector2> points = new List<Vector2>();

            points.Add(getCenter() - (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.51f);
            points.Add(getCenter() - (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.51f + WorldInfo.gravity * size.Y * 0.4f);
            points.Add(getCenter() - (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.51f - WorldInfo.gravity * size.Y * 0.4f);
            return points;
        }

        public List<Vector2> rightPoints()
        {
            List<Vector2> points = new List<Vector2>();

            points.Add(getCenter() + (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.51f);
            points.Add(getCenter() + (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.51f + WorldInfo.gravity * size.Y * 0.4f);
            points.Add(getCenter() + (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.51f - WorldInfo.gravity * size.Y * 0.4f);
            return points;
            //return getCenter() + (new Vector2(MapTools.getXMultiplier(), MapTools.getYMultiplier())) * size.X * 0.51f + WorldInfo.gravity;
        }

        public virtual CollisionInfo collidesWithMap()
        {
            Vector2 pos = MapTools.mapToGridCoords(getCenter());
            if (Game1.world.get((int)pos.X, (int)pos.Y).Type.Collision)
                return new CollisionInfo(true, Vector2.Zero);
            else
            {
                foreach (CollisionBox box in Game1.world.collisionBoxes)
                {
                    if (collidesWithCollisionBox(box))
                    {
                        return new CollisionInfo(true, speed); ;
                    }
                }
            }
            return new CollisionInfo(false, Vector2.Zero);
        }

        public Boolean collidesWithCollisionBox(CollisionBox box)
        {
            Vector2 playerPoint = getCenter();
            return box.collidesWithMovingPoint(getCenter(), speed);
            return (playerPoint.X > box.getCenter().X - box.size.X * 0.5f && playerPoint.X < box.getCenter().X + box.size.X * 0.5f &&
                playerPoint.Y > box.getCenter().Y - box.size.Y * 0.5f && playerPoint.Y < box.getCenter().Y + box.size.Y * 0.5f);
        }

        public virtual void reset()
        {
            this.pos = spawnPos;
            this.speed = new Vector2(0, 0);

        }

        public virtual bool collidesWithPoint(Vector2 point)
        {

            Vector2 boxUpLeft = getCenter() - size * 0.5f;
            Vector2 boxDownRight = getCenter() + size * 0.5f;
            return (boxUpLeft.X < point.X && boxUpLeft.Y < point.Y && boxDownRight.X > point.X && boxDownRight.Y > point.Y);
        }

        public float getFallSpeed()
        {
            Vector2 g = WorldInfo.gravity;
            if (g.Y < -0.5f)
                return -speed.Y;
            if (g.Y > 0.5f)
                return speed.Y;
            if (g.X < -0.5f)
                return -speed.X;
            if (g.X > 0.5f)
                return speed.X;
            return 0;
        }

        public float getRealSpeed()
        {
            Vector2 g = WorldInfo.gravity;
            if (g.Y < -0.5f)
                return -speed.X;
            if (g.Y > 0.5f)
                return +speed.X;
            if (g.X < -0.5f)
                return speed.Y;
            if (g.X > 0.5f)
                return -speed.Y;
            return 0;
        }

        public void setFallSpeed(float speed)
        {
            Vector2 g = WorldInfo.gravity;
            if (g.Y < -0.5f)
                this.speed.Y = -speed;
            if (g.Y > 0.5f)
                this.speed.Y = speed;
            if (g.X < -0.5f)
                this.speed.X = -speed;
            if (g.X > 0.5f)
                this.speed.X = speed;
        }

        public void setRealSpeed(float speed)
        {
            Vector2 g = WorldInfo.gravity;
            if (g.Y < -0.5f)
                this.speed.X = -speed;
            if (g.Y > 0.5f)
                this.speed.X = speed;
            if (g.X < -0.5f)
                this.speed.Y = speed;
            if (g.X > 0.5f)
                this.speed.Y = -speed;
        }

        public void setYSpeed(float speed)
        {
            Vector2 g = WorldInfo.gravity;
            if (g.Y < -0.5f)
                setFallSpeed(speed);
            if (g.Y > 0.5f)
                setFallSpeed(-speed);
            if (g.X < -0.5f)
                setRealSpeed(speed);
            if (g.X > 0.5f)
                setRealSpeed(-speed);
        }

        public void setXSpeed(float speed)
        {
            Vector2 g = WorldInfo.gravity;
            if (g.X < -0.5f)
                setFallSpeed(speed);
            if (g.X > 0.5f)
                setFallSpeed(-speed);
            if (g.Y < -0.5f)
                setRealSpeed(-speed);
            if (g.Y > 0.5f)
                setRealSpeed(speed);
        }

        Button widthUpButton;
        Button widthDownButton;

        public virtual void drawParamMenu(SpriteBatch batch)
        {
            if (font == null)
                font = Game1.cManager.Load<SpriteFont>("font");
            if (widthUpButton == null)
                widthUpButton = new Button(new Rectangle(1400, 700, 32, 32), delegate { size += new Vector2(16, 16); }, "saw");
            if (widthDownButton == null)
                widthDownButton = new Button(new Rectangle(1432, 700, 32, 32), delegate { size -= new Vector2(16, 16); }, "saw");

            widthUpButton.Draw(batch);
            widthDownButton.Draw(batch);
            widthUpButton.Update();
            widthDownButton.Update();

            batch.DrawString(font, pos.ToString(), new Vector2(1500, 800), Color.Black);
            batch.DrawString(font, size.ToString(), new Vector2(1400, 800), Color.Black);

            


        }

        public virtual bool isClickedParamMenu()
        {
            return (widthUpButton.isClicked() || widthDownButton.isClicked());
        }

        public virtual Dictionary<string, string> getAttributeList()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("X", pos.X.ToString());
            result.Add("Y", pos.Y.ToString());
            result.Add("Width", size.X.ToString());
            result.Add("Height", size.Y.ToString());

            return result;
        }

        public void reloadTexture(ContentManager content, string theme)
        {
            
            string texName = "themes/" + theme + "/items/" + Path.GetFileName(textureTest.Name);
            textureTest = content.Load<Texture2D>(texName);
        }

        public virtual void frameInit()
        {

        }

        public virtual void correctDownCollision()
        {

        }
        public virtual void correctRightCollision()
        {
        }

        public virtual void correctLeftCollision()
        {
        }
        public virtual void checkForWalls()
        {
        }
        }

}
