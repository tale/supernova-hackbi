using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;

namespace SuperNova.Code.Utilities {

    public class Image {

        private Texture2D _texture;

        private Vector2 _position, _dimensions;

        public Image(int x, int y, int width, int height, Texture2D texture) {

            _position.X = x;
            _position.Y = y;

            _dimensions.X = width;
            _dimensions.Y = height;

            this._texture = texture;
        }


        public void Render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(_texture, destinationRectangle: new Rectangle(
                (int)(Camera.GetWidthScalar() * (_position.X - _dimensions.X / 2)),
                (int)(Camera.GetHeightScalar() * (_position.Y - _dimensions.Y / 2 )),
                (int)(Camera.GetWidthScalar() * _dimensions.X),
                (int)(Camera.GetHeightScalar() * _dimensions.Y)), Color.White);
        }
    }
}