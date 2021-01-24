using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperNova.Code.Object  {
    
    public class Bullet  {
        
        private float _timer, _angle;
        private Vector2 _position, _velocity;
        private const float _speed = -100f;
        private Texture2D _sprite;

        public Bullet(Vector2 position, float angle) {

            _position = position;
            _angle = angle;
            _velocity = new Vector2((float)(_speed * Math.Sin(_angle)), (float)(_speed * Math.Cos(_angle)));
            _sprite = MakeBulletTexture();
        }
        
        public bool IsVisible { get; set; }
        
        public Vector2 Size { get; } = new Vector2(3, 20);
        
        public float X {
            get { return _position.X; }
        }
        
        public float Y {
            get { return _position.Y; }
        }
        
        public void Tick() {

            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
        }

        private void CheckCollision() {
            
        }
        
        private bool IsCollision(Asteroid asteroid) {

            double bulletX = _position.X + Size.X / 2, bulletY = _position.Y + Size.Y / 2;
            double asteroidXTemp = Math.Cos(_angle + 90) * (asteroid.X - bulletX) -
                                   Math.Sin(_angle + 90) * (asteroid.Y - bulletY) + bulletX;
            double asteroidYTemp = Math.Sin(_angle + 90) * (asteroid.X - bulletX) +
                                   Math.Cos(_angle + 90) * (asteroid.Y - bulletY) + bulletY;
            
            double closestX, closestY;
            if (asteroidXTemp < _position.X) closestX = _position.X;
            else if (asteroidXTemp > _position.X + Size.X) closestX = _position.X + Size.X;
            else closestX = asteroidXTemp;
            if (asteroidYTemp < _position.Y) closestY = _position.Y;
            else if (asteroidYTemp > _position.Y + Size.Y) closestY = _position.Y + Size.Y;
            else closestY = asteroidYTemp;
            
            double distance = Math.Sqrt(Math.Pow(asteroidXTemp - closestX, 2) + Math.Pow(asteroidYTemp - closestY, 2));
            return distance < asteroid.Radius;
        }

        private static Texture2D MakeBulletTexture() {

            return null;
        }
    }
}