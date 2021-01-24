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
        
        public Chunk(float x, float y) {
            _position = new Vector2(x, y);
            _planets = GeneratePlanetMap(WorldManager.Generator);
        }

        private Planet[] GeneratePlanetMap(NoiseGenerator noiseGenerator) {
            // Noise Constants
            const int noiseMultiplier1 = 5000;
            const int noiseMultiplier2 = 125;
            const float radiusDivider = 2F;
            
            var array = new Planet[16];
            var (x, y) = _position;

            for (var iter = 0; iter < array.GetLength(0); iter++) {
                var iterMultiplier = iter * noiseMultiplier2;
                var noiseLevel = (float)noiseGenerator.evaluate(x * noiseMultiplier1 + iterMultiplier, y * noiseMultiplier1 + iterMultiplier) * noiseMultiplier1;

                if (noiseLevel > 10) {
                    array[iter] = new Planet(new Vector2(x * noiseMultiplier1 + iterMultiplier, y * noiseMultiplier1 + iterMultiplier), noiseLevel / radiusDivider, 100, 23);
                }
                else {
                    array[iter] = null;
                }
            }

            return array;
        }

        public void render(SpriteBatch _spritebatch) {

            foreach (var planet in _planets) {
                if (planet != null) planet.render(_spritebatch);
            }
        }
    }
}


        