using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;

namespace SuperNova.Code.Utilities {

    public class Image {

        private Vector2 position, dimensions;

        private Texture2D texture;

        public Image(int x, int y, int width, int height, Texture2D texture) {

            position.X = x;
            position.Y = y;

            dimensions.X = width;
            dimensions.Y = height;

            this.texture = texture;
        }


        public void render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(texture, destinationRectangle: new Rectangle((int)(Camera.GetWidthScalar() * (position.X - dimensions.X / 2)), (int)(Camera.GetHeightScalar() * (position.Y - dimensions.Y / 2 )), (int)(Camera.GetWidthScalar() * dimensions.X), (int)(Camera.GetHeightScalar() * dimensions.Y)), Color.White);
        }
    }
}