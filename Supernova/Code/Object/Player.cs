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

        private static bool _engine = false;
        private static bool _gifFrame = false;

        public static Vector2 DrawPosition { get; } = new Vector2(688, 648);
        
        public static Vector2 Dimensions { get; } = new Vector2(48, 48);
        
        public static float Angle { get; set; } = (float) Math.PI * 3 / 2;

        public static float Health { get; set; } = 100F;

        public static float Fuel { get; set; } = 100F;

        public static int Score { get; set; } = 0;

        private static int _interval = 20;

        private static Planet _landedPlanet;

        public static void Reset() {
            position = new Vector2(640, 620);
            velocity = new Vector2(.00001f, (float) Math.PI * 3 / 2);
            _engine = false;
            Angle = (float)Math.PI * 3 / 2;
            Health = 100f;
            Fuel = 100f;
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
        
        public static void AddToVelocity(float amount, float angle) {

            float CVX = (float) (velocity.X * Math.Cos(velocity.Y));
            float CVY = (float) (velocity.X * Math.Sin(velocity.Y));

            float NVX = (float)(amount * Math.Cos(angle));
            float NVY = (float)(amount * Math.Sin(angle));

            velocity.X = (float)Math.Sqrt(Math.Pow(CVX + NVX, 2) + Math.Pow(CVY + NVY, 2));
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

                if (IsCollisionAsteroid(WorldManager.Asteroids[i]) && !WorldManager.Asteroids[i].Dead) {

                    float x = position.X - WorldManager.Asteroids[i].X;
                    float y = position.Y - WorldManager.Asteroids[i].Y;

                    float asteroidAngle = (float)Math.Atan(y / x);

                    if (x <= 0 && y >= 0 || x <= 0 && y <= 0) {
                        asteroidAngle += (float)Math.PI;
                        asteroidAngle %= (float)Math.PI * 2;
                    }

                    AddToVelocity(1.5f * velocity.X * (float)Math.Abs(Math.Cos(asteroidAngle - velocity.Y)), asteroidAngle);

                    if (_invincibleTimer == 10) {
                        Health -= 5 * velocity.X;
                        _invincibleTimer = 0;
                    }

                    WorldManager.Asteroids[i].Dead = true;
                }

            }

            foreach (var chunk in WorldManager.Chunks.Values) {

                foreach (var planet in chunk.Planets) {

                    if (_landedPlanet == null || (Math.Sqrt(Math.Pow(planet.X - X, 2) +
                        Math.Pow(planet.Y - Y, 2)) < Math.Sqrt(Math.Pow(_landedPlanet.X - X, 2) + Math.Pow(_landedPlanet.Y - Y, 2))
                        && Y - planet.Y < 5))
                        _landedPlanet = planet;

                    if (IsCollisionPlanet(planet)) {

                        _landedPlanet = planet;

                        float x = position.X - planet.X;
                        float y = position.Y - planet.Y;

                        float planetAngle = (float)((Math.Atan(y / x) + 2 * Math.PI) % (Math.PI * 2));

                        if (x < 0 && y >= 0 || x <= 0 && y <= 0) {
                            planetAngle += (float)Math.PI;
                            planetAngle %= (float)Math.PI * 2;
                        }


                        while (IsCollisionPlanet(planet)) {

                            _interval = 0;
                            position.X += .01f * (float)Math.Cos(planetAngle);
                            position.Y += .01f * (float)Math.Sin(planetAngle);
                        }

                        if (velocity.X < 1.5 && ((planetAngle - Angle) % (Math.PI * 2) < .7 || (planetAngle - Angle) - (Math.PI * 2) < .7)) {


                            if (velocity.X <= .5) {

                                UpdateHealth(.025F);
                                UpdateFuel(.1F);

                                position.X = planet.X + (float)((planet.Radius + 10) * Math.Cos(planetAngle + planet.ChangeInAngle));
                                position.Y = planet.Y + (float)((planet.Radius + 10) * Math.Sin(planetAngle + planet.ChangeInAngle));
                            }

                            if (!_engine) {
                                Angle = planetAngle;
                                velocity.X *= .5f;
                            }

                        } else {

                            if (velocity.X * (float)Math.Abs(Math.Sin(planetAngle - velocity.Y)) >= .5 &&
                                velocity.X * (float)Math.Abs(Math.Cos(planetAngle - velocity.Y)) < 1)
                                velocity.X *= .5f;

                            AddToVelocity(1.4f * velocity.X * (float)Math.Abs(Math.Cos(planetAngle - velocity.Y)), planetAngle);

                            if (_invincibleTimer == 10 && velocity.X > 3) {
                                Health -= 7.5f * velocity.X * (float)Math.Abs(Math.Cos(planetAngle - velocity.Y));
                                _invincibleTimer = 0;
                            }
                        }
                    }
                }
            }
        }

        private static bool IsCollisionAsteroid(Asteroid asteroid) {

            float distance = Vector2.Distance(new Vector2(asteroid.X, asteroid.Y), position);
            return distance < asteroid.Radius + Dimensions.X / 2 - 9;
        }

        private static bool IsCollisionPlanet(Planet planet) {
            float distance = Vector2.Distance(new Vector2(planet.X, planet.Y) , position);
            return distance < planet.Radius + Dimensions.X / 2 - 12;
        }

        private static double distance(double x1, double y1, double x2, double y2) {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static void Tick() {

            Vector2 gravity = WorldManager.getGravityEffects(position);

            AddToVelocity(gravity.X, gravity.Y);

            velocity.X = Math.Min(velocity.X, 7f * WorldManager.AccelerationModifier);
            velocity.Y = velocity.Y < 0 ?(float) Math.PI * 2 + velocity.Y: (float)(velocity.Y % (Math.PI * 2));

            Angle = Angle < 0 ? (float)Math.PI * 2 + Angle : (float)(Angle % (Math.PI * 2));

            CheckCollision();

            position.X += (float) (velocity.X * Math.Cos(velocity.Y));
            position.Y += (float) (velocity.X * Math.Sin(velocity.Y));

            Camera.SetX(-(position.X - DrawPosition.X + Dimensions.X / 2));
            Camera.SetY(-(position.Y - DrawPosition.Y + Dimensions.Y / 2));

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
            WorldManager.Bullets.Add(new Bullet(new Vector2 (position.X, position.Y + 12), velocity, Angle));
        }


        public static void Render(SpriteBatch _spriteBatch) {

            if (!_engine)
                _spriteBatch.Draw(SpriteManager.playerRotationShade(sprite, _landedPlanet), destinationRectangle:
                    new Rectangle(
                        (int)(Camera.GetWidthScalar() * (DrawPosition.X - Dimensions.X / 2)),
                        (int)(Camera.GetHeightScalar() * (DrawPosition.Y - Dimensions.Y / 2)),
                        (int)(Camera.GetWidthScalar() * (Dimensions.X)),
                        (int)(Camera.GetHeightScalar() * Dimensions.Y)),
                    null, Color.White,
                    (Angle + (float)Math.PI / 2) % ((float)Math.PI * 2),
                    new Vector2(sprite.Width / 2F, sprite.Height / 2F), SpriteEffects.None,
                    0f);

            else if (_gifFrame) {
                _spriteBatch.Draw(SpriteManager.playerRotationShade(sprite2, _landedPlanet), destinationRectangle:
                    new Rectangle(
                        (int)(Camera.GetWidthScalar() * (DrawPosition.X - Dimensions.X / 2)),
                        (int)(Camera.GetHeightScalar() * (DrawPosition.Y - Dimensions.Y / 2)),
                        (int)(Camera.GetWidthScalar() * (Dimensions.X)),
                        (int)(Camera.GetHeightScalar() * Dimensions.Y)),
                    null, Color.White,
                    (Angle + (float)Math.PI / 2) % ((float)Math.PI * 2),
                    new Vector2(sprite2.Width / 2F, sprite2.Height / 2F), SpriteEffects.None,
                    0f);
            }
            else if (!_gifFrame) {
                _spriteBatch.Draw(SpriteManager.playerRotationShade(sprite3, _landedPlanet), destinationRectangle:
                    new Rectangle(
                        (int)(Camera.GetWidthScalar() * (DrawPosition.X - Dimensions.X / 2)),
                        (int)(Camera.GetHeightScalar() * (DrawPosition.Y - Dimensions.Y / 2)),
                        (int)(Camera.GetWidthScalar() * Dimensions.X),
                        (int)(Camera.GetHeightScalar() * Dimensions.Y)),
                    null, Color.White,
                    (Angle + (float)Math.PI / 2) % ((float)Math.PI * 2),
                    new Vector2(sprite3.Width / 2F, sprite3.Height / 2F), SpriteEffects.None,
                    0f);

            }

        }
    }
}
