using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code.Util;
using SuperNova.Code.Util;

namespace Supernova.Code.World {
    public static class WorldManager {
        public static readonly NoiseGenerator Generator = new NoiseGenerator(new Random().Next(10000, 1000000));
        private static Dictionary<(int, int), Chunk> chunks = new Dictionary<(int, int), Chunk>();
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

  
        }

        public static void WorldRender(SpriteBatch _spriteBatch) {


            for (int n = 0; n < 9; n++) {

                chunks[loaded[n]].Render(_spriteBatch);
            }
        }
    }
}
