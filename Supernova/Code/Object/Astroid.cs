using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperNova.Code.Object {

    public class Astroid {

        static Random rand = new Random();

        private float radius, changeInRotation, rotation;
        private Vector2 position, velocity, accleration;

        private Texture2D sprite;

        public Astroid(Vector2 position, Vector2 velocity, float radius, float changeInRotation) {

            this.position = position;
            this.velocity = velocity;
            this.accleration = new Vector2();
            this.radius = radius;
            this.sprite = makeAstroidTexture();
            this.changeInRotation = changeInRotation;
            this.rotation = (float)(rand.NextDouble() * Math.PI * 2);
        }

        public void tick() {

            rotation = (float)((rotation + changeInRotation) % (Math.PI * 2));

            position.X += velocity.X;
            position.Y += velocity.Y;
        }

        public void render() {



        }




        private static Texture2D makeAstroidTexture() {

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
