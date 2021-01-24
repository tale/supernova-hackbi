using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public class Asteroid {

        static Random rand = new Random();

        private float _changeInRotation, _rotation;
        private Vector2 _position, _velocity, _acceleration;
        private Texture2D _sprite;

        public Asteroid(Vector2 position, Vector2 velocity, float radius, float changeInRotation) {

            IsVisible = true;
            Radius = radius;
            _position = position;
            _velocity = velocity;
            _acceleration = new Vector2();
            _sprite = MakeAstroidTexture();
            _changeInRotation = changeInRotation;
            _rotation = (float)(rand.NextDouble() * Math.PI * 2);
        }

        public bool IsVisible { get; set; }
        
        public float Radius { get; }

        public float X {
            get { return _position.X; }
        }
        
        public float Y {
            get { return _position.Y; }
        }

        
        public void Tick() {

            _rotation = (float)((_rotation + _changeInRotation) % (Math.PI * 2));

            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
        }

        public void Render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(_sprite, new Rectangle((int)(Camera.GetWidthScalar() * (_position.X - Radius + Camera.GetX())), (int)(Camera.GetHeightScalar() * (_position.Y - Radius + Camera.GetY())), (int)(Camera.GetWidthScalar() * Radius * 2), (int)(Camera.GetHeightScalar() * Radius * 2)), Color.White);
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
