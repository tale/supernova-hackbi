using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code.World;
using SuperNova.Code.Object;
using SuperNova.Code.Util;

namespace Supernova.Code.Object {
    
    public static class FuelBar {

        private static Vector2 _position = new Vector2(60, 75);
        private static Vector2 _dimensions = new Vector2(20, 100);
        private static Texture2D _texture = SpriteManager.GetTexture("FUEL");
        private static Texture2D _texture2 = SpriteManager.GetTexture("HEALTH_BACKGROUND");

        public static void Render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(_texture2, destinationRectangle: new Rectangle((int)(Camera.GetWidthScalar() * (_position.X - _dimensions.X / 2 - 3)), (int)(Camera.GetHeightScalar() * (_position.Y - _dimensions.Y / 2 - 3)), (int)(Camera.GetWidthScalar() * _dimensions.X + 6), (int)(Camera.GetHeightScalar() * (_dimensions.Y + 6))), Color.White);

            _spriteBatch.Draw(_texture, destinationRectangle: new Rectangle((int)(Camera.GetWidthScalar() * (_position.X - _dimensions.X / 2)), (int)(Camera.GetHeightScalar() * (_position.Y - _dimensions.Y / 2)), (int)(Camera.GetWidthScalar() * _dimensions.X), (int)(Camera.GetHeightScalar() * (_dimensions.Y / 100 * Player.Fuel))), Color.White);

        }
    }
}
