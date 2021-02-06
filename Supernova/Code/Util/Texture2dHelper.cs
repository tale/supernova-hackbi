
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Object;

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
        public static Texture2D Shade(GraphicsDevice graphics, Texture2D texture, float angle = 0) {

            Color[] decomposedTexture = GetPixels(texture);

            for (int n = 0; n < texture.Height * texture.Width; n++) {

                if (decomposedTexture[n].A > 0) {

                    float y = n / texture.Height - texture.Height / 2;
                    float x = n % texture.Height - texture.Width / 2;

                    float distance = (float)Math.Sqrt(x * x + y * y);
                    float angle2 = x != 0 ?(float)(Math.Atan((float) y / x)): (float)Math.PI / 2;

                    if (x < 0 && y >= 0 || x <= 0 && y <= 0)
                        angle2 += (float)Math.PI;

                    y = (float)(distance * Math.Sin(angle2 + angle)) - texture.Height / 16;

                    var scaler = (float)y * 1.1;

                    //scaler += (float)(Math.Abs(x) * Math.Min(1, Math.Pow(2, y / 3f)));
                    scaler = scaler < 0 ? (float)(scaler * 1.6f) : scaler;
                    scaler = ((1 / (1 + Math.Pow(Math.E, -scaler * .2f))) + .5f);

                    decomposedTexture[n].R = Convert.ToByte(Math.Max(0, Math.Min(255, decomposedTexture[n].R * scaler)));
                    decomposedTexture[n].G = Convert.ToByte(Math.Max(0, Math.Min(255, decomposedTexture[n].G * scaler)));
                    decomposedTexture[n].B = Convert.ToByte(Math.Max(0, Math.Min(255, decomposedTexture[n].B * scaler)));
                }
            }

            Texture2D newTexture = new Texture2D(graphics, texture.Width, texture.Height);
            newTexture.SetData<Color>(decomposedTexture);

            return newTexture;
        }

       public static Texture2D PlayerShade(GraphicsDevice graphics, Texture2D texture, Planet planet) {

            Color[] decomposedTexture = GetPixels(texture);

            for (int n = 0; n < texture.Height * texture.Width; n++) {

                if (decomposedTexture[n].A > 0) {

                    float y = n / texture.Height - texture.Height / 2;
                    float x = n % texture.Height - texture.Width / 2;

                    float distance = (float)Math.Sqrt(x * x + y * y);
                    float angle2 = x != 0 ? (float)(Math.Atan((float)y / x)) : (float)Math.PI / 2;

                    if (x < 0 && y >= 0 || x <= 0 && y <= 0)
                        angle2 += (float)Math.PI;

                    y = (float)(distance * Math.Sin(Player.Angle + Math.PI / 2 + angle2)) - texture.Height / 16;
                    x = (float)(distance * Math.Cos(Player.Angle + Math.PI / 2 + angle2));

                    float scaler = 1;

                    if (Math.Abs(planet.X - (x + Player.X)) < planet.Radius + (y + Player.Y - planet.Y) / 15f && y + Player.Y - planet.Y < 0)
                        scaler = Math.Min(.5f, .001f * -(y + Player.Y - planet.Y)) + .5f;

                    decomposedTexture[n].R = Convert.ToByte(Math.Max(0, Math.Min(255, decomposedTexture[n].R * scaler)));
                    decomposedTexture[n].G = Convert.ToByte(Math.Max(0, Math.Min(255, decomposedTexture[n].G * scaler)));
                    decomposedTexture[n].B = Convert.ToByte(Math.Max(0, Math.Min(255, decomposedTexture[n].B * scaler)));
                }
            }

            Texture2D newTexture = new Texture2D(graphics, texture.Width, texture.Height);
            newTexture.SetData<Color>(decomposedTexture);

            return newTexture;
        }


    }
}