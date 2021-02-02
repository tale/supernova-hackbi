using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;
using Supernova.Code.World;

namespace SuperNova.Code.Object {

    public class Asteroid {

        private static readonly Random rand = new Random();

        private static Texture2D _exp = SpriteManager.GetTexture("EXPLOSION");
        private readonly Texture2D _sprite;

        private Vector2 _position, _velocity;

        public Asteroid(Vector2 position, Vector2 velocity, float radius, float changeInAngle) {

            Radius = radius;
            _position = position;
            _velocity = velocity;
            _sprite = SpriteManager.MakeAstroidTexture();
            ChangeInAngle = changeInAngle;
            Angle = (float)(rand.NextDouble() * Math.PI * 2 - Math.PI);
        }

        public Boolean Dead { get; set; } = false;

        public float Radius { get; }

        public float X => _position.X;
        public float Y => _position.Y;

        public float Angle { get; set; }
        public float ChangeInAngle { get; }

        public void Tick() {

            Vector2 gravityAcceleration = WorldManager.getGravityEffects(_position);

            _velocity.X += gravityAcceleration.X * (float)Math.Cos(gravityAcceleration.Y);
            _velocity.Y += gravityAcceleration.X * (float)Math.Sin(gravityAcceleration.Y);

            _position.X += _velocity.X;
            _position.Y += _velocity.Y;

            Angle += ChangeInAngle;
            Angle = Angle < 0 ? (float)Math.PI * 2 + Angle : (float)(Angle % (Math.PI * 2));

        }

        public void Render(SpriteBatch _spriteBatch) {

            if (Camera.IsOnScreen(_position, new Vector2(Radius * 2, Radius * 2))) {

                if (!Dead)
                    _spriteBatch.Draw(SpriteManager.rotationShade( _sprite, Angle), new Rectangle(
                            (int)(Camera.GetWidthScalar() * (_position.X + Camera.GetX())),
                            (int)(Camera.GetHeightScalar() * (_position.Y + Camera.GetY())),
                            (int)(Camera.GetWidthScalar() * Radius * 2),
                            (int)(Camera.GetHeightScalar() * Radius * 2)), null, Color.White,
                            (Angle) % ((float)Math.PI * 2),
                            new Vector2(_sprite.Width / 2F, _sprite.Height / 2F), SpriteEffects.None, 0f);
  

                else
                    _spriteBatch.Draw(_exp, new Rectangle(
                        (int)(Camera.GetWidthScalar() * (_position.X - Radius + Camera.GetX())),
                        (int)(Camera.GetHeightScalar() * (_position.Y - Radius + Camera.GetY())),
                        (int)(Camera.GetWidthScalar() * Radius * 2),
                        (int)(Camera.GetHeightScalar() * Radius * 2)), Color.White);
            }
        }


        public Boolean HitPlanet(Planet[] planets) {

            for (int n = 0; n < planets.Length; n++) {

                float distance = Vector2.Distance(new Vector2(planets[n].X, planets[n].Y), _position);

                if (distance < planets[n].Radius + Radius)
                    return true;
            }
            return false;
        }
    }
}
