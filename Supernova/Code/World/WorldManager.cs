using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Object;
using Supernova.Code.Util;
using SuperNova.Code.Util;

namespace Supernova.Code.World {
    public static class WorldManager {
        public static readonly NoiseGenerator Generator = new NoiseGenerator(new Random().Next(10000, 1000000));
        private static readonly GaussianRandom _random = new GaussianRandom();
        
        
        private static Dictionary<(int, int), Chunk> chunks = new Dictionary<(int, int), Chunk>();
        private static List<Asteroid> _asteroids = new List<Asteroid>();
        private static (int, int)[] loaded = new (int, int)[9];


        public static void WorldTick() {


            int cordX = -Camera.GetX();
            int cordY = -Camera.GetY();

            cordX = (cordX < 0 ? cordX -= 5000 : cordX) / 5000;
            cordY = (cordY < 0 ? cordY -= 5000 : cordY) / 5000;

            for (int j = -1; j <= 1; j++) {

                for (int n = -1; n <= 1; n++) {

                    if (!chunks.ContainsKey((cordX + n, cordY + j)))
                        chunks.Add((cordX + n, cordY + j), new Chunk((cordX + n) * 5000, (cordY + j) * 5000));

                    loaded[(j + 1) * 3 + (n + 1)] = (cordX + n, cordY + j);

                }
            }

            var rand = _random.RandomGauss();
            if (rand > 2.2) {
                int spawnAmount = new Random().Next(10, 20);

                for (int n = 0; n < spawnAmount; n++) {
                    Vector2 spawnPoint = Vector2.Zero;
                    int spawnLocation = new Random().Next(0, 2);

                    switch (spawnLocation) {
                        case 0:
                            spawnPoint = new Vector2(cordX * -1 * spawnLocation * 30, cordY * -1 * spawnLocation * 30);
                            break;
                        case 1:
                            spawnPoint = new Vector2(cordX * 1 * spawnLocation * 30, cordY * -1 * spawnLocation * 30);
                            break;
                        case 2:
                            spawnPoint = new Vector2(cordX * 1 * spawnLocation * 30 + Camera.GetWidthScalar(), cordY * -1 * spawnLocation * 30);
                            break;
                    }
                    _asteroids.Add(new Asteroid(spawnPoint, new Vector2((float)rand / 2, (float)rand) / 2, 10, 23));
                }
            }

            foreach (var asteroid in _asteroids) {
                asteroid.Tick();

                if (Vector2.Distance(new Vector2(asteroid.GetX(), asteroid.GetY()), Player.GetPosition()) > 50000) {
                    _asteroids.Remove(asteroid);
                }
            }

  
        }

        public static void WorldRender(SpriteBatch _spriteBatch) {
            for (int n = 0; n < 9; n++) {

                chunks[loaded[n]].Render(_spriteBatch);
            }

            foreach (var asteroid in _asteroids) {
                asteroid?.Render(_spriteBatch);
            }
        }
    }
}
