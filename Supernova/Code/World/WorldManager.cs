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
        
        public static Dictionary<(int, int), Chunk> Chunks { get; } = new Dictionary<(int, int), Chunk>();
        public static List<Asteroid> Asteroids { get; } = new List<Asteroid>();
        public static List<Bullet> Bullets { get; } = new List<Bullet>();

        //private static Planet planet = new Planet(new Vector2(600, 100), 128, 50, 1);


        public static void WorldTick() {


            int cordX = -Camera.GetX();
            int cordY = -Camera.GetY();

            cordX = (cordX < 0 ? cordX -= 2000 : cordX) / 2000;
            cordY = (cordY < 0 ? cordY -= 2000 : cordY) / 2000;

            for (int j = -1; j <= 1; j++) {

                for (int n = -1; n <= 1; n++) {

                    if (!Chunks.ContainsKey((cordX + n, cordY + j)))
                        Chunks.Add((cordX + n, cordY + j), new Chunk((cordX + n) * 2000, (cordY + j) * 2000));

                    loaded[(j + 1) * 3 + (n + 1)] = (cordX + n, cordY + j);

                }
            }

            var rand = _random.RandomGauss();
            if (rand > 2.8) {

                int spawnAmount = (int)(rand * 1.75f);

                for (int n = 0; n < spawnAmount; n++) {
         


                    rand = _random.RandomGauss();

                    var x = _random.RandomGauss();
                    var y = Math.Abs(_random.RandomGauss());

                    Vector2 spawnPoint = new Vector2((float)rand * 360 - Camera.GetX(), -100 - Camera.GetY());

                    Asteroids.Add(new Asteroid(spawnPoint, new Vector2((float)x / 2, (float)y) / 2, 10, 23));
                }
            }
            
            for (int n = 0; n < Bullets.Count; n++) {
                Bullets[n].Tick();

                if (Vector2.Distance(new Vector2(Bullets[n].X, Bullets[n].Y), Player.GetPosition()) > 1000) {
                    Bullets.Remove(Bullets[n]);
                }
            }

            for (int n = 0; n < Asteroids.Count; n++) {
                Asteroids[n].Tick();

                if (Vector2.Distance(new Vector2(Asteroids[n].X, Asteroids[n].Y), Player.GetPosition()) > 2000) {
                    Asteroids.Remove(Asteroids[n]);
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
            foreach (var bullet in Bullets) {
                bullet.Render(_spriteBatch);
            }
        }
    }
}
