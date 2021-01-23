using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Object;

//@author Peter Downey

namespace Supernova.Code.World {
    public class Chunk {

        private static Random rand = new Random();

      
        protected Vector2 position = Vector2.Zero;
        protected Planet[] planets;


        public Chunk(float x, float y) {

            position.X = x;
            position.Y = y;

            planets = new Planet[0];
      
        }



        public void render(SpriteBatch _spritebatch) {

            for (int n = 0; n < planets.Length; n++) {

                planets[n].render(_spritebatch);
            }
        }
    }
}


        