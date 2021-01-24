using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;

namespace Supernova.Code.World {
    public static class WorldManager {

        private static Dictionary<(int, int), Chunk> chunks = new Dictionary<(int, int), Chunk>();



        public static void WorldTick() {


            int cordX = -Camera.GetX();
            int cordY = -Camera.GetY();

            cordX = (cordX < 0 ? cordX -= 5000 : cordX) / 5000;
            cordY = (cordY < 0 ? cordY -= 5000 : cordY) / 5000;

            for (int j = -1; j <= 1; j++) {

                for (int n = -1; n <= 1; n++) {

                    if (!chunks.ContainsKey((cordX + n, cordY + j)))
                        chunks.Add((cordX + n, cordY + j), new Chunk((cordX + n) * 5000, (cordY + j) * 5000));


                }
            }

  
        }

        public static void WorldRender(SpriteBatch _spriteBatch) {

            for (int j = -1; j <= 1; j++) {

                for (int n = -1; n <= 1; n++) {

                    chunks[(0, 0)].Render(_spriteBatch);

                }
            }

        }



    }
}
