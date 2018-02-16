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
    public class Camera2d
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation
        Vector2 offset = new Vector2(0,0);
        public Camera2d()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
        public void update()
        {
            if (Game1.running)
            {
                Pos = Game1.getPlayer().getCenter();
                offset = new Vector2(0, 0);
            } else
            {
                Pos = Game1.getPlayer().getCenter() + offset;

                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.D))
                    offset -= new Vector2(-8, 0);
                if (state.IsKeyDown(Keys.A))
                    offset -= new Vector2(+8, 0);
                if (state.IsKeyDown(Keys.S))
                    offset -= new Vector2(0,-8);
                if (state.IsKeyDown(Keys.W))
                    offset -= new Vector2(0,+8);

            }


            Rotation = -MapTools.VectorToAngle(WorldInfo.gravity) + MathHelper.Pi * 0.5f;
        }
        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3((float)graphicsDevice.Viewport.Width * 0.5f, (float)graphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }
    }
}
