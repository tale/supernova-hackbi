using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperNova.Code.Object {

    public class Supernova {

        private const float _radius = 10;
        private float _yPosition, _yVelocity, _yAcceleration;

        private Texture2D _sprite;

        public Supernova(float yPosition, float yVelocity, float yAcceleration) {

            _yPosition = yPosition;
            _yVelocity = yVelocity;
            _yAcceleration = yAcceleration;
            _sprite = MakeSupernovaTexture();
        }

        public float Radius {
            get { return _radius; }
        }
        
        public float Y {
            get { return _yPosition; }
        }
        
        public float VY {
            get { return _yVelocity; }
        }
        
        public float AY {
            get { return _yAcceleration; }
        }
        
        public void Tick() {

            _yPosition -= _yVelocity;
            _yVelocity += _yAcceleration;
        }

        public void Render() {



        }
        

        private static Texture2D MakeSupernovaTexture() {

            return null;

        }
    }

}
