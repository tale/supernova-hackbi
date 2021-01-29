using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public class Planet {

        static Random rand = new Random();

        private Texture2D _sprite;

        private const double _gravityStrength = .001;
        private float _mass, _changeInRotation, _rotation;

        public Planet(Vector2 position, float radius, float mass, float changeInRotation) {

            IsVisible = true;
            this.Radius = radius;
            Position = position;
            _mass = mass;
            _sprite = MakePlanetTexture();
            _changeInRotation = changeInRotation;
            _rotation = (float)(rand.NextDouble() * Math.PI * 2);
        }
        
        private Vector2 Position { get; }
        
        public bool IsVisible { get; set; }
     
        public float Radius { get; set; }

        public float X => Position.X;

        public float Y => Position.Y;

        public void Tick() {

            _rotation = (float)((_rotation + _changeInRotation) % (Math.PI * 2));
        }

        public Vector2 Gravity(Vector2 objectPosition) {

            var (x, y) = objectPosition;

            if (Math.Sqrt(Math.Pow(Position.X - x, 2) + Math.Pow(Position.Y - y, 2)) > 1000)
                return Vector2.Zero;

            float acceleration = (float)(_gravityStrength * _mass / (Math.Pow((Position.X - x) / 180, 2) + Math.Pow((Position.Y - y) / 180, 2)));
            float angle = (float)(Math.Atan((Position.Y - y) / (Position.X - x)));

            if (Position.X - x < 0 && Position.Y - y > 0 || Position.X - x < 0 && Position.Y - y < 0) {

                angle += (float)Math.PI;
                angle %= (float)Math.PI * 2;
            }

            return new Vector2((float)(acceleration * Math.Cos(angle)), (float)(acceleration * Math.Sin(angle)));
        }    

        public static Texture2D MakePlanetTexture() {

            int type = rand.Next(4);

            switch (type) {

                case 0:
                    return SpriteManager.GetTexture("EARTH_PLANET");

                case 1:
                    return SpriteManager.GetTexture("MARS_PLANET");

                case 2:
                    return SpriteManager.GetTexture("SAND_PLANET");

                case 3:
                    return SpriteManager.GetTexture("ROCK_PLANET");

                default:
                    return SpriteManager.GetTexture("PLANET");
            }
        }

        public void Render(SpriteBatch _spriteBatch) {

            if (Camera.IsOnScreen(Position, new Vector2(Radius * 3, Radius * 3)))
                _spriteBatch.Draw(_sprite, new Rectangle(
                        (int)(Camera.GetWidthScalar() * (Position.X - Radius * 1.3 + Camera.GetX())),
                    (int)(Camera.GetHeightScalar() * (Position.Y - Radius * 1.3 + Camera.GetY())),
                        (int)(Camera.GetWidthScalar() * Radius * 2),
                    (int)(Camera.GetHeightScalar() * Radius * 2)),
                    null, Color.White);
        }
    }
}
