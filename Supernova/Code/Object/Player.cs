using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Supernova.Code.World;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public static class Player {

        private static Texture2D sprite = SpriteManager.GetTexture("PLAYER");
        private static Texture2D sprite2 = SpriteManager.GetTexture("PLAYER1");
        private static Texture2D sprite3 = SpriteManager.GetTexture("PLAYER2");

        private static Vector2 position = new Vector2(640, 620);
        private static Vector2 velocity = new Vector2(.00001f, (float) Math.PI * 3 / 2);

        private static int _fuelTimer = 0, _gifTimer = 0, _invincibleTimer = 0;

        private static Boolean _engine = false;
        private static Boolean _gifFrame = false;

        public static Vector2 DrawPosition { get; } = new Vector2(640, 620);
        
        public static Vector2 Size { get; } = new Vector2(48, 48);
        
        public static float Angle { get; set; } = (float) Math.PI * 3 / 2;

        public static float Health { get; set; } = 100F;

        public static float Fuel { get; set; } = 100F;

        public static int Score { get; set; } = 0;

        private static int _interval = 20;

        public static void Reset() {
            position = new Vector2(640, 620);
            velocity = new Vector2(.00001f, (float) Math.PI * 3 / 2);
            _engine = false;
            Angle = (float)Math.PI * 3 / 2;
            Health = 100F;
            Fuel = 100F;
            Score = 0;

            _fuelTimer = 0;
            _gifTimer = 0;
            _invincibleTimer = 0;
        }

        public static Vector2 GetPosition() {
            return position;
        }
        
        public static float X {
            get { return position.X; }
        }
        
        public static float Y {
            get { return position.Y; }
        }
        
        public static float VX {
            get { return velocity.X; }
        }
        
        public static float VY {
            get { return velocity.Y; }
        }
        
        public static void addToVelocity(float amount, float angle) {

            float CVX = (float) (velocity.X * Math.Cos(velocity.Y));
            float CVY = (float) (velocity.X * Math.Sin(velocity.Y));

            float NVX = (float)(amount * Math.Cos(angle));
            float NVY = (float)(amount * Math.Sin(angle));

            velocity.X = (float) Math.Sqrt(Math.Pow(CVX + NVX, 2) + Math.Pow(CVY + NVY, 2));

            velocity.Y = (float)Math.Atan((CVY + NVY) / (CVX + NVX));


            if (CVX + NVX <= 0 && CVY + NVY >= 0 || CVX + NVX <= 0 && CVY + NVY <= 0) {
                velocity.Y += (float)Math.PI;
                velocity.Y %= (float)Math.PI * 2;

            }
        }
        
         private static void UpdateHealth(float health) {
             while (Health <= 100 && _interval <= 20) {
                 Health += health;
                 _interval++;
             }
         }
        
         private static void UpdateFuel(float fuel) {
             while (Fuel <= 100 && _interval <= 20) {
                 Fuel += fuel;
             }
         }

        private static void CheckCollision() {

            for (int i = WorldManager.Asteroids.Count - 1; i >= 0; i--) {

                if (IsCollisionAsteroid(WorldManager.Asteroids[i])) {

                    velocity.X *= 0.5f;
                    velocity.Y += (float)Math.PI;

                    if (_invincibleTimer == 10) {
                        Health -= 5 * velocity.X;
                        _invincibleTimer = 0;
                    }

                    WorldManager.Asteroids.Remove(WorldManager.Asteroids[i]);
                }

            }

            foreach (var chunk in WorldManager.Chunks.Values) {

                foreach (var planet in chunk.Planets) {

                    if (IsCollisionPlanet(planet)) {

                        float x = position.X - planet.X;
                        float y = position.Y - planet.Y;

                        float planetAngle = (float)Math.Atan(y / x);

                        if (x <= 0 && y >= 0 || x <= 0 && y <= 0) {
                            planetAngle += (float)Math.PI;
                            planetAngle %= (float)Math.PI * 2;
                        }


                        if (velocity.X < 1.5 && Math.Abs(planetAngle - Angle) < .7) {

                            while (IsCollisionPlanet(planet)) {

                                _interval = 0;
                                position.X += .01f * (float)Math.Cos(planetAngle);
                                position.Y += .01f * (float)Math.Sin(planetAngle);
                            }

                            if (!_engine)
                                Angle = planetAngle;

                            if (velocity.X <= .5) {
                                UpdateHealth(.025F);
                                UpdateFuel(.1F);
                            }

                        } else {

                            addToVelocity(3, planetAngle);

                            if (_invincibleTimer == 10 && velocity.X > 3) {
                                Health -= 5 * velocity.X;
                                _invincibleTimer = 0;
                            }

                        }

                        if (!_engine)
                            velocity.X *= .5f;
                    }
                }
            }
        }

        private static bool IsCollisionAsteroid(Asteroid asteroid) {
            
            return IsCollisionBody(asteroid.X, asteroid.Y, asteroid.Radius);
        }

        private static bool IsCollisionPlanet(Planet planet) {
            float distance = Vector2.Distance(new Vector2(planet.X, planet.Y) , position);
            return distance < planet.Radius + Size.X / 2 - 8;
        }
        
        private static bool IsCollisionBody(double bodyX, double bodyY, double bodyRadius)
        {

            double pointX1 = position.X,
                pointY1 = position.Y - Size.Y / 2,
                pointX2 = position.X - Size.X / 2,
                pointY2 = position.Y + Size.Y / 2,
                pointX3 = position.X + Size.X / 2,
                pointY3 = position.Y + Size.Y / 2;
            return isCollisionLineCircle(pointX1, pointY1, pointX2, pointY2, bodyX, bodyY, bodyRadius)
                   || isCollisionLineCircle(pointX1, pointY1, pointX3, pointY3, bodyX, bodyY, bodyRadius)
                   || isCollisionLineCircle(pointX2, pointY2, pointX3, pointY3, bodyX, bodyY, bodyRadius);
        }

        private static bool isCollisionLineCircle(double lineX1, double lineY1, double lineX2, double lineY2, double circleX,
            double circleY, double radius) {

            double lineLen = distance(lineX1, lineY1, lineX2, lineY2);
            double dotProduct = ((circleX - lineX1) * (lineX2 - lineX1) + (circleY - lineY1) * (lineY2 - lineY1)) /
                                Math.Pow(lineLen, 2);
            double closestX = dotProduct * (lineX2 - lineX1) + lineX1;
            double closestY = dotProduct * (lineY2 - lineY1) + lineY1;
            double distLineClosest1 = distance(lineX1, lineY1, closestX, closestY);
            double distLineClosest2 = distance(lineX2, lineY2, closestX, closestY);
            if (Math.Abs(distLineClosest1 + distLineClosest2 - lineLen) > 0.5)
                return false;
            double distCircleClosest = distance(circleX, circleY, closestX, closestY);
            return distCircleClosest < radius * 1.3;
        }

        private static double distance(double x1, double y1, double x2, double y2) {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static void Tick() {

            Vector2 gravity = WorldManager.getGravityEffects(position);

            addToVelocity(gravity.X, gravity.Y);

            velocity.X = Math.Min(velocity.X, 7f);

            if (velocity.Y < 0)
                velocity.Y = (float) Math.PI * 2 + velocity.Y;

            velocity.Y = (float) (velocity.Y % (Math.PI * 2));

            if (Angle < 0)
                Angle = (float) Math.PI * 2 + Angle;

            Angle = (float) (Angle % (Math.PI * 2));

            CheckCollision();

            position.X += (float) (velocity.X * Math.Cos(velocity.Y));
            position.Y += (float) (velocity.X * Math.Sin(velocity.Y));

            Camera.SetX(-(position.X - 640));
            Camera.SetY(-(position.Y - 620));

            KeyboardState keyboardState = Keyboard.GetState();

            _fuelTimer += 1;

            if (keyboardState.IsKeyDown(Keys.Space) && _fuelTimer > 20) {
                Shoot();
                _fuelTimer = 0;
            }

            _engine = keyboardState.IsKeyDown(Keys.W) && Fuel > 0;

            if (_gifTimer == 4) {
                _gifFrame = !_gifFrame;
                _gifTimer = 0;

            } else
                _gifTimer++;

            _invincibleTimer = Math.Min(_invincibleTimer + 1, 10);

            Score++;
        }
        
        
        private static void Shoot() {
            WorldManager.Bullets.Add(new Bullet(position - Size / 2, Angle));
        }


        public static void Render(SpriteBatch _spriteBatch) {

            if (!_engine)
                _spriteBatch.Draw(sprite, destinationRectangle:
                    new Rectangle(
                        (int)(Camera.GetWidthScalar() * (DrawPosition.X - Size.X / 2)),
                        (int)(Camera.GetHeightScalar() * (DrawPosition.Y - Size.Y / 2)),
                        (int)(Camera.GetWidthScalar() * (Size.X)),
                        (int)(Camera.GetHeightScalar() * Size.Y)),
                    null, Color.White,
                    (Angle + (float)Math.PI / 2) % ((float)Math.PI * 2),
                    new Vector2(sprite.Width / 2F, sprite.Height / 2F), SpriteEffects.None,
                    0f);

            else if (_gifFrame) {
                _spriteBatch.Draw(sprite2, destinationRectangle:
                    new Rectangle(
                        (int)(Camera.GetWidthScalar() * (DrawPosition.X - Size.X / 2)),
                        (int)(Camera.GetHeightScalar() * (DrawPosition.Y - Size.Y / 2)),
                        (int)(Camera.GetWidthScalar() * (Size.X)),
                        (int)(Camera.GetHeightScalar() * Size.Y)),
                    null, Color.White,
                    (Angle + (float)Math.PI / 2) % ((float)Math.PI * 2),
                    new Vector2(sprite2.Width / 2F, sprite2.Height / 2F), SpriteEffects.None,
                    0f);
            }
            else if (!_gifFrame) {
                _spriteBatch.Draw(sprite3, destinationRectangle:
                    new Rectangle(
                        (int)(Camera.GetWidthScalar() * (DrawPosition.X - Size.X / 2)),
                        (int)(Camera.GetHeightScalar() * (DrawPosition.Y - Size.Y / 2)),
                        (int)(Camera.GetWidthScalar() * (Size.X)),
                        (int)(Camera.GetHeightScalar() * Size.Y)),
                    null, Color.White,
                    (Angle + (float)Math.PI / 2) % ((float)Math.PI * 2),
                    new Vector2(sprite3.Width / 2F, sprite3.Height / 2F), SpriteEffects.None,
                    0f);

            }

        }
    }
}
