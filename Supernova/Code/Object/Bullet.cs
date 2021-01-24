using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code.World;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object  {
    
    public class Bullet  {
        
        private float _timer, _angle;
        private Vector2 _position, _velocity;
        private const float _speed = 20f;
        private Texture2D _sprite;

        public Bullet(Vector2 position, float angle) {

            _position = position;
            _angle = angle;
            _velocity = new Vector2((float)(_speed * Math.Cos(_angle)), (float)(_speed * Math.Sin(_angle)));
            _sprite = MakeBulletTexture();
        }
        
        public bool IsVisible { get; set; }
        
        public Vector2 Size { get; } = new Vector2(5, 30);
        
        public float X => _position.X;

        public float Y => _position.Y;
        
        public void Tick() {

            CheckCollision();
            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
        }

        private void CheckCollision() {
            
            for (int i = WorldManager.Asteroids.Count - 1; i >= 0; i--) {

                if (IsCollision(WorldManager.Asteroids[i])) {

                    Console.WriteLine("BULLET-ASTEROID COLLISION");
                    
                    WorldManager.Asteroids.Remove(WorldManager.Asteroids[i]);
                }

            }
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

        public void Render(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(_sprite, new Rectangle((int)(Camera.GetWidthScalar() * (_position.X - Size.X / 2 + Camera.GetX())), (int)(Camera.GetHeightScalar() * (_position.Y - Size.Y / 2 + Camera.GetY())), (int)(Camera.GetWidthScalar() * Size.X),
                (int)(Camera.GetHeightScalar() * Size.Y)), 
                null, Color.White, 
                (float)(_angle + Math.PI / 2), 
                new Vector2(_sprite.Width / 2F, _sprite.Height / 2F), SpriteEffects.None, 
                0f);
        }

        private static Texture2D MakeBulletTexture() {
            
            return SpriteManager.GetTexture("NULL");
        }
    }
}