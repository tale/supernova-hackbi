using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Util;

namespace SuperNova.Code.Utilities {
    public class Button {

        private Vector2 position, dimensions;

        private float expand = 1;

        private Texture2D texture;

        public Button(int x, int y, int width, int height, Texture2D texture) {

            position.X = x;
            position.Y = y;

            dimensions.X = width;
            dimensions.Y = height;

            this.texture = texture;
        }

        public Button(Vector2 position, Vector2 dimensions, Texture2D texture) {

            this.position = position;
            this.dimensions = dimensions;

            this.texture = texture;
        }


        public bool tick(int mouseX, int mouseY) {


            if (mouseX > Camera.GetWidthScalar() * (position.X - dimensions.X / 2 * expand) && mouseX < Camera.GetWidthScalar() * (position.X + dimensions.X / 2 * expand) && mouseY > Camera.GetHeightScalar() * (position.Y - dimensions.Y / 2 * expand) && mouseY < Camera.GetHeightScalar() * (position.Y + dimensions.Y / 2 * expand)) {

                expand = (float)Math.Min(expand + .05, 1.15);
                return true;
            }
            expand = (float)Math.Max(expand - .05, 1);
            return false;
        }


        public void render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(texture, destinationRectangle: new Rectangle((int)(Camera.GetWidthScalar() * (position.X - dimensions.X / 2 * expand)), (int)(Camera.GetHeightScalar() * (position.Y - dimensions.Y / 2 * expand)), (int)(Camera.GetWidthScalar() * (dimensions.X * expand)), (int)(Camera.GetHeightScalar() * (dimensions.Y * expand))), Color.White);
        }
    }
}