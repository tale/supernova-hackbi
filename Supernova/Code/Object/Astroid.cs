using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperNova.Code.Object {

    public class Astroid {

        static Random rand = new Random();
        
        public Boolean isVisible = false;

        public float radius;
        private float changeInRotation, rotation;
        private Vector2 position, velocity, accleration;

        private Texture2D sprite;

        public Astroid(Vector2 position, Vector2 velocity, float radius, float changeInRotation) {

            this.position = position;
            this.velocity = velocity;
            this.accleration = new Vector2();
            this.radius = radius;
            this.sprite = MakeAstroidTexture();
            this.changeInRotation = changeInRotation;
            this.rotation = (float)(rand.NextDouble() * Math.PI * 2);
        }

        public float GetX() {
            return position.X;
        }
        
        public float GetY() {
            return position.Y;
        }

        public float GetRadius() {
            return radius;
        }
        
        public void Tick() {

            rotation = (float)((rotation + changeInRotation) % (Math.PI * 2));

            position.X += velocity.X;
            position.Y += velocity.Y;
        }

        public void Render() {

        }
        

        private static Texture2D MakeAstroidTexture() {

            int type = (int)(rand.Next(1));

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
