using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Object;
using Supernova.Code.Util;

//@author Peter Downey

namespace Supernova.Code.World {
    public class Chunk {

        private static Random rand = new Random();

      
        protected Vector2 position = Vector2.Zero;
        protected Planet[,] planets;


        public Chunk(float x, float y) {

            position.X = x;
            position.Y = y;

            planets = GeneratePlanetMap(new NoiseGenerator(69));
        }

        private Planet[,] GeneratePlanetMap(NoiseGenerator noiseGenerator) {
            var array = new Planet[16, 16];

            for (var i = 0; i < array.GetLength(0); i++) {
                for (var j = 0; j < array.GetLength(1); j++) {
                    const int multiplier = 125;
                    const float divider = 2f;

                    var prefix = (float)noiseGenerator.evaluate(i * multiplier, j * multiplier) * multiplier;
                    // -10 and +10 are from Perlin
                    if (prefix > 10 || prefix < -10) {
                        array[i, j] = new Planet(new Vector2(i * prefix, j * prefix), prefix / divider, 100, 23);
                    }
                    else {
                        array[i, j] = null;
                    }
                    
                }
            }

            return array;
        }

        public void render(SpriteBatch _spritebatch) {

            for (var i = 0; i < planets.GetLength(0); i++) {
                for (var j = 0; j < planets.GetLength(1); j++) {
                    if (planets[i, j] != null) planets[i, j].render(_spritebatch);
                }
            }
        }
    }
}


        