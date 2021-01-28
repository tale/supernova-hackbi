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
        private static Random _random = new Random();
        public Planet[] Planets { get; }
        
        public Chunk(float x, float y) {
            _position = new Vector2(x, y);
            Planets = GeneratePlanetMap();
        }

        private Planet[] GeneratePlanetMap() {
            
            var array = new Planet[10];

            int iteration = 1000000;
            int number = 0;

            List<(int, int)> previous = new List<(int, int)>();

            do {

                var x = _random.Next(1800) + 100;
                var y = _random.Next(1800) + 100;

                if (checkPrevious(x, y, previous)) {
                    array[number] = new Planet(new Vector2((x + _position.X), (y + _position.Y)), 84, 100, (float)_random.NextDouble() / 50 );
                    previous.Add((x, y));
                    number++;
                }

                iteration--;

            } while (iteration > 0 && number < 10);                

            return array;
        }



        private Boolean checkPrevious(int x, int y, List<(int, int)> previous) {

            foreach ((int, int) prev in previous) {

                var (x2, y2) = prev;

                if (Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2)) < 300)
                    return false;
            }
            return true;
        }

        public Vector2 getGravityEffects(Vector2 positition) {

            Vector2 acceleration = Vector2.Zero;

            for (int n = 0; n < Planets.Length; n++) {

                Vector2 temp = Planets[n].Gravity(positition);

                acceleration.X += temp.X;
                acceleration.Y += temp.Y;
            }
            return acceleration;
        }

        public void Tick() {

            foreach (var planet in Planets) {
                planet?.Tick();
            }
        }

        public void Render(SpriteBatch _spritebatch) {

            foreach (var planet in Planets) {
                if (planet != null) planet.Render(_spritebatch);
            }
        }
    }
}


        