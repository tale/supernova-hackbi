using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;
using Supernova.Code.World;

namespace SuperNova.Code.Object {

    public class Asteroid {

        private static readonly Random rand = new Random();

        private readonly float _changeInRotation;
        private float _rotation;
        private Vector2 _position, _acceleration;
        private readonly Texture2D _sprite;

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
        public Vector2 _velocity;

        public float X => _position.X;

        public float Y => _position.Y;


        public void Tick() {


            var accel = WorldManager.getGravityEffects(_position);

            _velocity.X += accel.X * (float)Math.Cos(accel.Y);
            _velocity.Y += accel.X * (float)Math.Sin(accel.Y);

            _rotation = (float)((_rotation + _changeInRotation) % (Math.PI * 2));

            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
        }

        public void Render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(_sprite,
                new Rectangle((int)(Camera.GetWidthScalar() * (_position.X - Radius + Camera.GetX())), (int)(Camera.GetHeightScalar() * (_position.Y - Radius + Camera.GetY())), (int)(Camera.GetWidthScalar() * Radius * 2),
                    (int)(Camera.GetHeightScalar() * Radius * 2)), Color.White);
        }


        public Boolean hitPlanet(Planet[] planets) {

            for (int n = 0; n < planets.Length; n++) {

                float distance = Vector2.Distance(new Vector2(planets[n].X, planets[n].Y), _position);

                if (distance < planets[n].Radius + Radius) {

                    return true;
                }

            }

            return false;

        }

        private static Texture2D MakeAstroidTexture() {

            var type = rand.Next(1);

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
