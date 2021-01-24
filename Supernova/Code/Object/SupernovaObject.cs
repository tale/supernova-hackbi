﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code.World;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public static class SupernovaObject {

        private const float _radius = 10;
        private static float _yPosition = 900, _yVelocity = -2.25f;

        private static Texture2D _sprite = SpriteManager.GetTexture("EXPANDING_SUPERNOVA");


        public static float Radius {
            get { return _radius; }
        }
        
        public static float Y {
            get { return _yPosition; }
        }
        
        public static float VY {
            get { return _yVelocity; }
        }

        
        public static void Tick() {

            _yPosition += _yVelocity;

            if (Player.Y > _yPosition) {
                Player.Health -= 1;
            }
        }

        public static void Render(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(_sprite, destinationRectangle: new Rectangle(0, (int)(Camera.GetHeightScalar() * (_yPosition + Camera.GetY())), (int)(Camera.GetWidthScalar() * 1280), (int)(Camera.GetHeightScalar() * 800)), Color.White);


        }
        
    }

}
