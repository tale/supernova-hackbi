using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperNova.Code.Object {

    public class Supernova {

        private const double radius = 10;
        private double yPosition, yVelocity, yAcceleration;

        private Texture2D sprite;

        public Supernova(double yPosition, double yVelocity, double yAcceleration) {

            this.yPosition = yPosition;
            this.yVelocity = yVelocity;
            this.yAcceleration = yAcceleration;
            this.sprite = makeSupernovaTexture();
        }

        public void Tick() {

            yPosition += yVelocity;
            yVelocity += yAcceleration;
        }

        public void Render() {



        }
        

        private static Texture2D makeSupernovaTexture() {

            return null;

        }
    }

}
