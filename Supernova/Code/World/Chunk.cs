using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Object;
using Supernova.Code.Util;

//@author Peter Downey

namespace Supernova.Code.World {
    public class Chunk {
        private Vector2 _position;
        private Planet[] _planets;
        private static Random random = new Random();
        
        public Chunk(float x, float y) {
            _position = new Vector2(x, y);
            _planets = GeneratePlanetMap(WorldManager.Generator);
        }

        private Planet[] GeneratePlanetMap(NoiseGenerator noiseGenerator) {
            // Noise Constants
            const int noiseMultiplier1 = 5000;
            const int noiseMultiplier2 = 125;
            const float radiusDivider = 2F;
            
            var array = new Planet[300];

            for (var iter = 0; iter < array.Length; iter++) {

                var x = random.Next(5000);
                var y = random.Next(5000);

                var iterMultiplier = iter * noiseMultiplier2;
                var noiseLevel = (float)noiseGenerator.evaluate((x + _position.X) * noiseMultiplier1, (y + _position.Y)) * noiseMultiplier1;

                array[iter] = new Planet(new Vector2((x + _position.X), (y + _position.Y)), 50, 100, 23);

                /*if (noiseLevel > 5) {
                    
                }
                else {
                    array[iter] = null;
                }*/
            }

            return array;
        }

        public void Render(SpriteBatch _spritebatch) {

            foreach (var planet in _planets) {
                if (planet != null) planet.Render(_spritebatch);
            }
        }
    }
}


        