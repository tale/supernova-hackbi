using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code.World;
using SuperNova.Code.Object;
using SuperNova.Code.Util;

namespace Supernova.Code.Object {
    public static class InfoBar {
        
        private static Vector2 _positionH = new Vector2(30, 75);
        private static Vector2 _dimensionsH = new Vector2(20, 100);
        private static Texture2D _textureH = SpriteManager.GetTexture("HEALTH");
        private static Texture2D _textureB = SpriteManager.GetTexture("HEALTH_BACKGROUND");

        private static Vector2 _positionF = new Vector2(60, 75);
        private static Vector2 _dimensionsF = new Vector2(20, 100);
        private static Texture2D _textureF = SpriteManager.GetTexture("FUEL");

        private static Vector2 _positionP = new Vector2(90, 75);
        private static Vector2 _dimensionsP = new Vector2(20, 100);
        private static Texture2D _textureP = SpriteManager.GetTexture("FUEL");
        private static Texture2D _textureShip = SpriteManager.GetTexture("Player");


        public static void Render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(_textureB, destinationRectangle: new Rectangle(
                (int)(Camera.GetWidthScalar() * (_positionH.X - _dimensionsH.X / 2 - 3)),
                (int)(Camera.GetHeightScalar() * (_positionH.Y - _dimensionsH.Y / 2 - 3)),
                (int)(Camera.GetWidthScalar() * _dimensionsH.X + 6),
                (int)(Camera.GetHeightScalar() * (_dimensionsH.Y + 6))), Color.White);

            _spriteBatch.Draw(_textureH, destinationRectangle: new Rectangle(
                (int)(Camera.GetWidthScalar() * (_positionH.X - _dimensionsH.X / 2)),
                (int)(Camera.GetHeightScalar() * (_positionH.Y - _dimensionsH.Y / 2)),
                (int)(Camera.GetWidthScalar() * _dimensionsH.X),
                (int)(Camera.GetHeightScalar() * (_dimensionsH.Y / 100 * Player.Health))), Color.White);

            _spriteBatch.Draw(_textureB, destinationRectangle: new Rectangle(
                (int)(Camera.GetWidthScalar() * (_positionF.X - _dimensionsF.X / 2 - 3)),
                (int)(Camera.GetHeightScalar() * (_positionF.Y - _dimensionsF.Y / 2 - 3)),
                (int)(Camera.GetWidthScalar() * _dimensionsF.X + 6),
                (int)(Camera.GetHeightScalar() * (_dimensionsF.Y + 6))), Color.White);

            _spriteBatch.Draw(_textureF, destinationRectangle: new Rectangle(
                (int)(Camera.GetWidthScalar() * (_positionF.X - _dimensionsF.X / 2)),
                (int)(Camera.GetHeightScalar() * (_positionF.Y - _dimensionsF.Y / 2)),
                (int)(Camera.GetWidthScalar() * _dimensionsF.X),
                (int)(Camera.GetHeightScalar() * (_dimensionsF.Y / 100 * Player.Fuel))), Color.White);

            _spriteBatch.Draw(_textureB, destinationRectangle: new Rectangle(
                (int)(Camera.GetWidthScalar() * (_positionP.X - _dimensionsP.X / 2 - 3)),
                (int)(Camera.GetHeightScalar() * (_positionP.Y - _dimensionsP.Y / 2 - 3)),
                (int)(Camera.GetWidthScalar() * _dimensionsP.X + 6),
                (int)(Camera.GetHeightScalar() * (_dimensionsP.Y + 6))), Color.White);

            _spriteBatch.Draw(_textureP, destinationRectangle: new Rectangle(
                (int)(Camera.GetWidthScalar() * (_positionP.X - _dimensionsP.X / 2)),
                (int)(Camera.GetHeightScalar() * (_positionP.Y - _dimensionsP.Y / 2)),
                (int)(Camera.GetWidthScalar() * _dimensionsP.X),
                (int)(Camera.GetHeightScalar() * (_dimensionsP.Y / 5000 * Math.Min(Player.getDistance(), 5000)))), Color.Orange);


        }
    }
}
