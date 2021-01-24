using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public class Planet {

        static Random rand = new Random();

        private const double _gravityStrength = .00001;
        private float _radius, _mass, _changeInRotation, _rotation;
        private Vector2 _position;

        private Texture2D _sprite;

        public Planet(Vector2 position, float radius, float mass, float changeInRotation) {

            _position = position;
            _radius = radius;
            _mass = mass;
            _sprite = MakePlanetTexture();
            _changeInRotation = changeInRotation;
            _rotation = (float)(rand.NextDouble() * Math.PI * 2);
        }

        public void Tick() {

            _rotation = (float)((_rotation + _changeInRotation) % (Math.PI * 2));

        }

        public void Render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(_sprite, new Rectangle((int)(Camera.GetWidthScalar() * (_position.X - _radius + Camera.GetX())), (int)(Camera.GetHeightScalar() * (_position.Y - _radius + Camera.GetY())), (int)(Camera.GetWidthScalar() * _radius * 2), (int)(Camera.GetHeightScalar() * _radius * 2)), Color.White);


        }

        public Vector2 Gravity(Vector2 objectPosition) {

            float accelerationX = (float)(_gravityStrength * _mass / Math.Pow(_position.X - objectPosition.X, 2));

            float accelerationY = (float)(_gravityStrength * _mass / Math.Pow(_position.Y - objectPosition.Y, 2));

            if (_position.X < objectPosition.X)
                accelerationX *= -1;

            if (_position.Y < objectPosition.Y)
                accelerationY *= -1;



            return new Vector2(accelerationX, accelerationY);

        }

        private static float Hypot(float a, float b) {

            return (float)Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }
            
            
            
            

        private static Texture2D MakePlanetTexture() {

            int type = (int)(rand.Next(1));

            switch (type) {

                case 0:
                    return SpriteManager.GetTexture("PLANET");

                case 1:
                    return SpriteManager.GetTexture("PLANET");

                default:
                    return SpriteManager.GetTexture("PLANET");

            }



        }


    }
}
