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
    public class Camera2d
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation

        protected float targetZoom = 1;

        public Camera2d()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = new Vector2(1000,1000);
        }
        public float Zoom
        {
            get { return _zoom * 1920f / Game1.graphics.PreferredBackBufferWidth; }
            set { _zoom = value * Game1.graphics.PreferredBackBufferWidth / 1920f; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float TargetZoom
        {
            get { return targetZoom * 1920f / Game1.graphics.PreferredBackBufferWidth; }
            set { targetZoom = value * Game1.graphics.PreferredBackBufferWidth / 1920f; if (targetZoom < 0.1f) targetZoom = 0.1f; } // Negative zoom will flip image
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

            if (_zoom < targetZoom) {
                _zoom *= 1.1f;
                if(_zoom > targetZoom)
                {
                    _zoom = targetZoom;
                }
            }
            if (_zoom > targetZoom)
            {
                _zoom /= 1.1f;
                if (_zoom < targetZoom)
                {
                    _zoom = targetZoom;
                }
            }

            if (Game1.running)
            {
                Pos = Game1.getPlayer().getCenter();
                _pos = Pos;
            } else if(!EditorGui.saveText.isActive)
            {
                //Pos = Game1.getPlayer().getCenter() + offset;

                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.D))
                    Pos -= new Vector2(-8, 0);
                if (state.IsKeyDown(Keys.A))
                    Pos -= new Vector2(+8, 0);
                if (state.IsKeyDown(Keys.S))
                    Pos -= new Vector2(0,-8);
                if (state.IsKeyDown(Keys.W))
                    Pos -= new Vector2(0,+8);


            }

            if (Pos.X < 0 + Game1.graphics.PreferredBackBufferWidth / 2 + 32)
                _pos.X = Game1.graphics.PreferredBackBufferWidth / 2 + 32;

            if (Pos.Y < 0 + Game1.graphics.PreferredBackBufferHeight / 2 + 32)
                _pos.Y = Game1.graphics.PreferredBackBufferHeight / 2 + 32;

            if (Pos.X > Game1.world.width * 16 - Game1.graphics.PreferredBackBufferWidth / 2 - 32)
                _pos.X = Game1.world.width * 16 - Game1.graphics.PreferredBackBufferWidth / 2 - 32;

            if (Pos.Y > Game1.world.height * 16 - Game1.graphics.PreferredBackBufferHeight / 2-32)
                _pos.Y = Game1.world.height * 16 - Game1.graphics.PreferredBackBufferHeight / 2-32;


            Rotation = -MapTools.VectorToAngle(WorldInfo.gravity) + MathHelper.Pi * 0.5f;
        }
        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3((float)graphicsDevice.Viewport.Width * 0.5f, (float)graphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }
    }
}
