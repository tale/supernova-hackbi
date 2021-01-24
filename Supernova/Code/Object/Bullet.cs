using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperNova.Code.Object  {
    
    public class Bullet  {
        
        private bool _isVisible = false;
        private float _timer, _angle;
        private Vector2 _position, _size, _velocity;
        private const float _speed = -100f;
        private Texture2D _sprite;

        public Bullet(Vector2 position, Vector2 size) {

            _position = position;
            _size = size;
            _velocity = new Vector2((float)(_speed * Math.Sin(_angle)), (float)(_speed * Math.Cos(_angle)));
            _sprite = MakeBulletTexture();
        }
        
        public void Tick() {

            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
        }

        private void CheckCollision() {
            
        }
        
        private bool IsCollision(Astroid asteroid) {

            double bulletX = _position.X + _size.X / 2, bulletY = _position.Y + _size.Y / 2;
            double asteroidXTemp = Math.Cos(_angle + 90) * (asteroid.GetX() - bulletX) -
                                   Math.Sin(_angle + 90) * (asteroid.GetY() - bulletY) + bulletX;
            double asteroidYTemp = Math.Sin(_angle + 90) * (asteroid.GetX() - bulletX) +
                                   Math.Cos(_angle + 90) * (asteroid.GetY() - bulletY) + bulletY;
            double closestX, closestY;
            if (asteroidXTemp < _position.X) closestX = _position.X;
            else if (asteroidXTemp > _position.X + _size.X) closestX = _position.X + _size.X;
            else closestX = asteroidXTemp;
            if (asteroidYTemp < _position.Y) closestY = _position.Y;
            else if (asteroidYTemp > _position.Y + _size.Y) closestY = _position.Y + _size.Y;
            else closestY = asteroidYTemp;
            double distance = Math.Sqrt(Math.Pow(asteroidXTemp - closestX, 2) + Math.Pow(asteroidYTemp - closestY, 2));
            return distance < asteroid.GetRadius();
        }

        private static Texture2D MakeBulletTexture() {

            return null;
        }
    }
}