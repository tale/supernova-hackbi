﻿using System;
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

            
            var array = new Planet[30];

            int iteration = 1000000;
            int number = 0;

            List<(int, int)> previous = new List<(int, int)>();

            do {
                var x = random.Next(5000);
                var y = random.Next(5000);

                //var noiseLevel = (float)noiseGenerator.evaluate((x + _position.X) * noiseMultiplier1, (y + _position.Y)) * noiseMultiplier1;

                if (checkPrevious(x, y, previous)) {
                    array[number] = new Planet(new Vector2((x + _position.X), (y + _position.Y)), 64, 100, 23);
                    previous.Add((x, y));
                    number++;
                }
                iteration--;
            } while (iteration > 0 && number < 30);                



            return array;
        }

        private Boolean checkPrevious(int x, int y, List<(int, int)> previous) {

            foreach ((int, int) prev in previous) {

                var (x2, y2) = prev;

                if (Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2)) < 700) {
                    return false;
                }

            }

            return true;

        }

        public void Render(SpriteBatch _spritebatch) {

            foreach (var planet in _planets) {
                if (planet != null) planet.Render(_spritebatch);
            }
        }
    }
}


        