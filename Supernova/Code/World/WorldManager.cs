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
        
        private static (int, int)[] loaded = new (int, int)[9];
        
        private static Dictionary<(int, int), Chunk> Chunks { get; } = new Dictionary<(int, int), Chunk>();
        private static List<Asteroid> Asteroids { get; } = new List<Asteroid>();

        //private static Planet planet = new Planet(new Vector2(600, 100), 128, 50, 1);


        public static void WorldTick() {


            int cordX = -Camera.GetX();
            int cordY = -Camera.GetY();

            cordX = (cordX < 0 ? cordX -= 5000 : cordX) / 5000;
            cordY = (cordY < 0 ? cordY -= 5000 : cordY) / 5000;

            for (int j = -1; j <= 1; j++) {

                for (int n = -1; n <= 1; n++) {

                    if (!Chunks.ContainsKey((cordX + n, cordY + j)))
                        Chunks.Add((cordX + n, cordY + j), new Chunk((cordX + n) * 5000, (cordY + j) * 5000));

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
                            spawnPoint = new Vector2(cordX * -1 * spawnLocation * 30 - Camera.GetX(), cordY * -1 * spawnLocation * 30 - Camera.GetY());
                            break;
                        case 1:
                            spawnPoint = new Vector2(cordX * 1 * spawnLocation * 30 - Camera.GetX(), cordY * -1 * spawnLocation * 30 - Camera.GetY());
                            break;
                        case 2:
                            spawnPoint = new Vector2(cordX * 1 * spawnLocation * 30 + -Camera.GetX(), cordY * -1 * spawnLocation * 30 - Camera.GetY());
                            break;
                    }
                    Asteroids.Add(new Asteroid(spawnPoint, new Vector2((float)rand / 2, (float)rand) / 2, 10, 23));
                }
            }

            foreach (var asteroid in Asteroids) {
                asteroid.Tick();

                if (Vector2.Distance(new Vector2(asteroid.X, asteroid.Y), Player.GetPosition()) > 50000) {
                    Asteroids.Remove(asteroid);
                }
            }

            for (int n = 0; n < 9; n++) {

                Chunks[loaded[n]].Tick();
            }



        }

        public static Vector2 getGravityEffects(Vector2 positition) {

            Vector2 acceleration = Vector2.Zero;

            for (int n = 0; n < 9; n++) {

                acceleration += Chunks[loaded[n]].getGravityEffects(positition);
            }
            //acceleration = planet.Gravity(positition);

            float mangnitude = Math.Min((float)Math.Sqrt(Math.Pow(acceleration.X, 2) + Math.Pow(acceleration.Y, 2)), .0425f);

            float angle = 0;

            if (acceleration.X > 0 && acceleration.Y > 0)
                angle = (float)Math.Atan(acceleration.Y / (acceleration.X));

            else if (acceleration.X < 0 && acceleration.Y > 0)
                angle = (float)(Math.Atan(acceleration.Y / (acceleration.X)) + Math.PI);

            else if (acceleration.X < 0 && acceleration.Y < 0)
                angle = (float)(Math.Atan(acceleration.Y / (acceleration.X)) + Math.PI);

            else if (acceleration.X > 0 && acceleration.Y < 0)
                angle = (float)(Math.Atan(acceleration.Y / (acceleration.X)) + 2 * Math.PI);

            angle %= (float)Math.PI * 2;

            return new Vector2(mangnitude, angle);

        }

        public static void WorldRender(SpriteBatch _spriteBatch) {
            for (int n = 0; n < 9; n++) {

                Chunks[loaded[n]].Render(_spriteBatch);
            }

            //planet.Render(_spriteBatch);

            foreach (var asteroid in Asteroids) {
                asteroid?.Render(_spriteBatch);
            }
        }
    }
}
