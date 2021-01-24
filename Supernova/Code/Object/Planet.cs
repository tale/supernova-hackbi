﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public class Planet {

        static Random rand = new Random();

        private const double gravityStrength = 0.000000000000001;
        private float radius, mass, changeInRotation, rotation;
        public Vector2 position;

        private Texture2D sprite;

        public Planet(Vector2 position, float radius, float mass, float changeInRotation) {

            this.position = position;
            this.radius = radius;
            this.mass = mass;
            this.sprite = MakePlanetTexture();
            this.changeInRotation = changeInRotation;
            this.rotation = (float)(rand.NextDouble() * Math.PI * 2);
        }

        public void Tick() {

            rotation = (float)((rotation + changeInRotation) % (Math.PI * 2));

        }

        public void Render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(sprite, new Rectangle((int)(Camera.GetWidthScalar() * (position.X - radius + Camera.GetX())), (int)(Camera.GetHeightScalar() * (position.Y - radius + Camera.GetY())), (int)(Camera.GetWidthScalar() * radius * 2), (int)(Camera.GetHeightScalar() * radius * 2)), Color.White);


        }

        public Vector2 Gravity(Vector2 ObjectPosition) {

            float acceleration = (float)(gravityStrength * mass / Hypot(position.X - ObjectPosition.X, position.Y - ObjectPosition.Y));

            float angle = (float)Math.Atan((position.Y - ObjectPosition.Y) / (position.X - ObjectPosition.X));

            return new Vector2((float)(acceleration * Math.Cos(angle)), (float)(acceleration * Math.Sin(angle)));

        }

        private static float Hypot(float a, float b) {

            return (float)Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }
            
            
            
            

        private static Texture2D MakePlanetTexture() {

            int type = (int)(rand.Next(1));

            switch (type) {

                case 0:
                    return SpriteManager.GetTexture("PLANET");

                case 1:
                    return SpriteManager.GetTexture("PLANET");

                default:
                    return SpriteManager.GetTexture("PLANET");

            }



        }


    }
}
