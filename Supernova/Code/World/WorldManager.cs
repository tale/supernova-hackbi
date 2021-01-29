using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Object;
using Supernova.Code.Util;
using SuperNova.Code.Util;

namespace Supernova.Code.World {
    public static class WorldManager {

        public static readonly GaussianRandom Random = new GaussianRandom();
        
        private static (int, int)[] _loaded = new (int, int)[9];
        private static (int, int)[] _parallaxLoaded1 = new (int, int)[9];
        private static (int, int)[] _parallaxLoaded2 = new (int, int)[9];
        private static (int, int)[] _parallaxLoaded3 = new (int, int)[9];
        private static (int, int)[] _starParallaxLoaded = new (int, int)[9];

        public static Dictionary<(int, int), Chunk> Chunks { get; } = new Dictionary<(int, int), Chunk>();
        public static Dictionary<(int, int), ParallaxChunk> ParallaxChunks1 { get; } = new Dictionary<(int, int), ParallaxChunk>();
        public static Dictionary<(int, int), ParallaxChunk> ParallaxChunks2 { get; } = new Dictionary<(int, int), ParallaxChunk>();
        public static Dictionary<(int, int), ParallaxChunk> ParallaxChunks3 { get; } = new Dictionary<(int, int), ParallaxChunk>();
        public static Dictionary<(int, int), ParallaxChunk> StarParallaxChunks { get; } = new Dictionary<(int, int), ParallaxChunk>();
        public static List<Asteroid> Asteroids { get; } = new List<Asteroid>();
        public static List<Bullet> Bullets { get; } = new List<Bullet>();

        public static void Reset() {
            Asteroids.Clear();
            Bullets.Clear();
            Chunks.Clear();
        }

        public static void WorldTick() {

            int cordX = -Camera.GetX();
            int cordY = -Camera.GetY();

            cordX = (cordX < 0 ? cordX -= 2000 : cordX) / 2000;
            cordY = (cordY < 0 ? cordY -= 2000 : cordY) / 2000;

            for (int j = -1; j <= 1; j++) {

                for (int n = -1; n <= 1; n++) {

                    if (!Chunks.ContainsKey((cordX + n, cordY + j)))
                        Chunks.Add((cordX + n, cordY + j), new Chunk((cordX + n) * 2000, (cordY + j) * 2000));

                    _loaded[(j + 1) * 3 + (n + 1)] = (cordX + n, cordY + j);
                }
            }

            ParallaxLayerTick(StarParallaxChunks, _starParallaxLoaded, 32, 160, true);
            ParallaxLayerTick(ParallaxChunks1, _parallaxLoaded1, 8, 40);
            ParallaxLayerTick(ParallaxChunks2, _parallaxLoaded2, 4, 20);
            ParallaxLayerTick(ParallaxChunks3, _parallaxLoaded3, 2, 10);

            var rand = Random.RandomGauss();

            if (rand > 2.2) {

                int spawnAmount = (int)(rand * 4f);

                for (int n = 0; n < spawnAmount; n++) {

                    rand = Random.RandomGauss();

                    var x = Random.RandomGauss();
                    var y = Math.Abs(Random.RandomGauss());

                    Vector2 spawnPoint = new Vector2((float)rand * 1280 + 640 - Camera.GetX(), -600 - Camera.GetY());

                    Asteroids.Add(new Asteroid(spawnPoint, new Vector2((float)x / 2, (float)y) / 2, 23));
                }
            }

            for (int n = 0; n < 9; n++) {
                Chunks[_loaded[n]].Tick();
            }
        }

        private static void ParallaxLayerTick(Dictionary<(int, int), ParallaxChunk> ParallaxChunks, (int, int)[] _parallaxLoaded, int scaler, int density, bool starChunk = false) {

            int cordX = -Camera.GetX() / scaler;
            int cordY = -Camera.GetY() / scaler;

            cordX = (cordX < 0 ? cordX -= 2000 : cordX) / 2000;
            cordY = (cordY < 0 ? cordY -= 2000 : cordY) / 2000;

            for (int j = -1; j <= 1; j++) {

                for (int n = -1; n <= 1; n++) {

                    if (!ParallaxChunks.ContainsKey((cordX + n, cordY + j)))
                        ParallaxChunks.Add((cordX + n, cordY + j), new ParallaxChunk((cordX + n) * 2000, (cordY + j) * 2000, scaler, density, starChunk));

                    _parallaxLoaded[(j + 1) * 3 + (n + 1)] = (cordX + n, cordY + j);
                }
            }
        }

        public static void WorldTick2() {

            for (int n = 0; n < Asteroids.Count; n++) {

                if (Asteroids[n].Dead) {
                    Asteroids.Remove(Asteroids[n]);
                    n -= 1;
                }
            }

            for (int n = Bullets.Count - 1; n >= 0; n--) {
                Bullets[n].Tick();

                if (Vector2.Distance(new Vector2(Bullets[n].X, Bullets[n].Y), Player.GetPosition()) > 1000 || Bullets[n].isPlanetCollision())
                    Bullets.Remove(Bullets[n]);
            }

            for (int n = 0; n < Asteroids.Count; n++) {

                Asteroids[n].Tick();

                if (Vector2.Distance(new Vector2(Asteroids[n].X, Asteroids[n].Y), Player.GetPosition()) > 2000)
                    Asteroids[n].Dead = true;

                else {

                    for (int c = 0; c < 9; c++) {

                        if (Asteroids[n].HitPlanet(Chunks[_loaded[c]].Planets)) {
                            Asteroids[n].Dead = true;
                            break;
                        }
                    }
                }
            }
        }
        public static Vector2 getGravityEffects(Vector2 positition) {

            Vector2 acceleration = Vector2.Zero;

            for (int n = 0; n < 9; n++) {
                acceleration += Chunks[_loaded[n]].getGravityEffects(positition);
            }

            float mangnitude = Math.Min((float)Math.Sqrt(Math.Pow(acceleration.X, 2) + Math.Pow(acceleration.Y, 2)), .0425f);
            float angle = (float)(Math.Atan(acceleration.Y / (acceleration.X)));

            if (acceleration.X < 0 && acceleration.Y > 0 || acceleration.X < 0 && acceleration.Y < 0) {
                angle += (float)Math.PI;
                angle %= (float)Math.PI * 2;
            }

            return new Vector2(mangnitude, angle);
        }

        public static void WorldRender(SpriteBatch _spriteBatch) {

            //yes these must be seperate loops
            for (int n = 0; n < 9; n++) {
                StarParallaxChunks[_starParallaxLoaded[n]].Render(_spriteBatch);
            }

            for (int n = 0; n < 9; n++) {
                ParallaxChunks1[_parallaxLoaded1[n]].Render(_spriteBatch);
            }

            for (int n = 0; n < 9; n++) {
                ParallaxChunks2[_parallaxLoaded2[n]].Render(_spriteBatch);
            }

            for (int n = 0; n < 9; n++) {
                ParallaxChunks3[_parallaxLoaded3[n]].Render(_spriteBatch);
            }

            for (int n = 0; n < 9; n++) {
                Chunks[_loaded[n]].Render(_spriteBatch);
            }

            foreach (var asteroid in Asteroids) {
                asteroid?.Render(_spriteBatch);
            }

            foreach (var bullet in Bullets) {
                bullet.Render(_spriteBatch);
            }
        }

    }
}
