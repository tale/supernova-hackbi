using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperNova.Code.Object {

    public class Supernova {

        private const double radius = 10;
        private double _yPosition, _yVelocity, _yAcceleration;

        private Texture2D _sprite;

        public Supernova(double yPosition, double yVelocity, double yAcceleration) {

            _yPosition = yPosition;
            _yVelocity = yVelocity;
            _yAcceleration = yAcceleration;
            _sprite = MakeSupernovaTexture();
        }

        public void Tick() {

            _yPosition += _yVelocity;
            _yVelocity += _yAcceleration;
        }

        public void Render() {



        }
        

        private static Texture2D MakeSupernovaTexture() {

            return null;

        }
    }

}
