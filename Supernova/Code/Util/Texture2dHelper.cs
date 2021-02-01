
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperNova.Code.Util {

    public static class Texture2dHelper {

        //these top two methods may or may not of been stolen off of github
        //https://stackoverflow.com/questions/9532919/how-to-get-color-and-coordinatesx-y-from-texture2d-xna-c/9534022

        public static Color GetPixel(this Color[] colors, int x, int y, int width) {
            return colors[x + (y * width)];
        }
        public static Color[] GetPixels(this Texture2D texture) {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(colors1D);
            return colors1D;
        }

        //there is 100% a better way to do this
        public static Texture2D Shade(GraphicsDevice graphics, Texture2D texture) {

            Color[] decomposedTexture = GetPixels(texture);

            for (int n = 0; n < texture.Height * texture.Width; n++) {

                if (decomposedTexture[n].A > 0) {

                    int scaler = (int)(((float)n / (texture.Height * texture.Height) - .5f) * 75);
                    scaler = scaler < 0 ? (int)(scaler * 1.2f) : scaler;
                    scaler += (int)(Math.Abs((float)(n % texture.Height) / texture.Width - .5f) * 20);

                    decomposedTexture[n].R = Convert.ToByte(Math.Max(0, Math.Min(255, decomposedTexture[n].R + scaler)));
                    decomposedTexture[n].G = Convert.ToByte(Math.Max(0, Math.Min(255, decomposedTexture[n].G + scaler)));
                    decomposedTexture[n].B = Convert.ToByte(Math.Max(0, Math.Min(255, decomposedTexture[n].B + scaler)));
                }
            }

            Texture2D newTexture = new Texture2D(graphics, texture.Width, texture.Height);
            newTexture.SetData<Color>(decomposedTexture);

            return newTexture;
        }
    }
}