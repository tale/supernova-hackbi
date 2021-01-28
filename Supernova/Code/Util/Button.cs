using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code;
using SuperNova.Code.Object;
using SuperNova.Code.Util;

namespace SuperNova.Code.Utilities {
    public class Button {

        private Vector2 _position, _dimensions;

        private float _expand = 1;

        private Texture2D _texture;
        private string _word;

        public Button(int x, int y, int width, int height, Texture2D texture) {

            _position.X = x;
            _position.Y = y;

            _dimensions.X = width;
            _dimensions.Y = height;

            _texture = texture;
        }
        
        public Button(int x, int y, int width, int height, string word) {

            _position.X = x;
            _position.Y = y;

            _dimensions.X = width;
            _dimensions.Y = height;
            _word = word;
        }


        public Button(Vector2 position, Vector2 dimensions, Texture2D texture) {

            _position = position;
            _dimensions = dimensions;
            _texture = texture;
        }


        public bool Tick(int mouseX, int mouseY) {

            if (mouseX > Camera.GetWidthScalar() * (_position.X - _dimensions.X / 2 * _expand) && mouseX < Camera.GetWidthScalar() * (_position.X + _dimensions.X / 2 * _expand) && mouseY > Camera.GetHeightScalar() * (_position.Y - _dimensions.Y / 2 * _expand) && mouseY < Camera.GetHeightScalar() * (_position.Y + _dimensions.Y / 2 * _expand)) {

                _expand = (float)Math.Min(_expand + .05, 1.15);
                return true;
            }
            _expand = (float)Math.Max(_expand - .05, 1);
            return false;
        }


        public void Render(SpriteBatch _spriteBatch) {

            if (_texture == null) {
                Texture2D rect = new Texture2D(_spriteBatch.GraphicsDevice, 
                    (int)(Camera.GetWidthScalar() * (_dimensions.X * _expand)), 
                    (int)(Camera.GetHeightScalar() * (_dimensions.Y * _expand)));

                Color[] data = new Color[rect.Width * rect.Height];
                for(int i=0; i < data.Length; ++i) data[i] = Color.DarkCyan;
                rect.SetData(data);
                
                _spriteBatch.Draw(rect, destinationRectangle: 
                    new Rectangle(
                        (int)(Camera.GetWidthScalar() * (_position.X - _dimensions.X / 2 * _expand)), 
                        (int)(Camera.GetHeightScalar() * (_position.Y - _dimensions.Y / 2 * _expand)), 
                        (int)(Camera.GetWidthScalar() * (_dimensions.X * _expand)), 
                        (int)(Camera.GetHeightScalar() * (_dimensions.Y * _expand))),
                    Color.White);

                SpriteFont font = SupernovaGame.font;

                _spriteBatch.DrawString(
                    font, 
                    _word,
                    new Vector2(
                        (int)(Camera.GetWidthScalar() * (_position.X)), 
                        (int)(Camera.GetHeightScalar() * (_position.Y))),
                    Color.White,
                    0,
                    new Vector2(
                        font.MeasureString(_word).X / 2,
                        font.MeasureString(_word).Y / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0);
            }
            else {
                _spriteBatch.Draw(_texture, destinationRectangle: 
                    new Rectangle(
                        (int)(Camera.GetWidthScalar() * (_position.X - _dimensions.X / 2 * _expand)), 
                        (int)(Camera.GetHeightScalar() * (_position.Y - _dimensions.Y / 2 * _expand)), 
                        (int)(Camera.GetWidthScalar() * (_dimensions.X * _expand)), 
                        (int)(Camera.GetHeightScalar() * (_dimensions.Y * _expand))),
                    Color.White);
            }
        }
    }
}