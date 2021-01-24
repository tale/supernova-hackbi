using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperNova.Code.Object  {
    
    public class Bullet  {
        
        public Boolean isVisible = false;
        private float timer, angle;
        private Vector2 position, size, velocity;
        private const float speed = -100f;
        private Texture2D sprite;

        public Bullet(Vector2 position, Vector2 size) {

            this.position = position;
            this.size = size;
            this.velocity = new Vector2((float)(speed * Math.Sin(angle)), (float)(speed * Math.Cos(angle)));
            this.sprite = MakeBulletTexture();
        }
        
        public void Tick() {

            position.X += velocity.X;
            position.Y += velocity.Y;
        }

        private void CheckCollision() {
            
        }
        
        private Boolean IsCollision(Astroid asteroid) {

            double bulletX = position.X + size.X / 2, bulletY = position.Y + size.Y / 2;
            double asteroidXTemp = Math.Cos(angle + 90) * (asteroid.GetX() - bulletX) -
                                   Math.Sin(angle + 90) * (asteroid.GetY() - bulletY) + bulletX;
            double asteroidYTemp = Math.Sin(angle + 90) * (asteroid.GetX() - bulletX) +
                                   Math.Cos(angle + 90) * (asteroid.GetY() - bulletY) + bulletY;
            double closestX, closestY;
            if (asteroidXTemp < position.X) closestX = position.X;
            else if (asteroidXTemp > position.X + size.X) closestX = position.X + size.X;
            else closestX = asteroidXTemp;
            if (asteroidYTemp < position.Y) closestY = position.Y;
            else if (asteroidYTemp > position.Y + size.Y) closestY = position.Y + size.Y;
            else closestY = asteroidYTemp;
            double distance = Math.Sqrt(Math.Pow(asteroidXTemp - closestX, 2) + Math.Pow(asteroidYTemp - closestY, 2));
            return distance < asteroid.GetRadius();
        }

        private static Texture2D MakeBulletTexture() {

            return null;
        }
    }
}