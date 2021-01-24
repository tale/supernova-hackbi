using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public class Asteroid {

        static Random rand = new Random();
        
        private bool _isVisible = false;

        private float _changeInRotation, _rotation, _radius;
        private Vector2 _position, _velocity, _accleration;
        private Texture2D _sprite;

        public Asteroid(Vector2 position, Vector2 velocity, float radius, float changeInRotation) {

            _position = position;
            _velocity = velocity;
            _accleration = new Vector2();
            _radius = radius;
            _sprite = MakeAstroidTexture();
            _changeInRotation = changeInRotation;
            _rotation = (float)(rand.NextDouble() * Math.PI * 2);
        }

        public float GetX() {
            return _position.X;
        }
        
        public float GetY() {
            return _position.Y;
        }

        public float GetRadius() {
            return _radius;
        }
        
        public void Tick() {

            _rotation = (float)((_rotation + _changeInRotation) % (Math.PI * 2));

            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
        }

        public void Render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(_sprite, new Rectangle((int)(Camera.GetWidthScalar() * (_position.X - _radius + Camera.GetX())), (int)(Camera.GetHeightScalar() * (_position.Y - _radius + Camera.GetY())), (int)(Camera.GetWidthScalar() * _radius * 2), (int)(Camera.GetHeightScalar() * _radius * 2)), Color.White);
        }
        

        private static Texture2D MakeAstroidTexture() {

            int type = (int)(rand.Next(1));

            return SpriteManager.GetTexture("NULL");

            switch (type) {

                case 0:
                    return null;

                case 1:
                    return null;

                default:
                    return null;

            }
        }
    }

}
