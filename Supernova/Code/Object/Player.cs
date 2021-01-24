using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperNova.Code.Util;

namespace SuperNova.Code.Object {

    public static class Player {

        private static readonly Vector2 Position = new Vector2(360, 400);
        private static Vector2 dimensions = new Vector2(48, 48);


        private static Texture2D sprite = SpriteManager.GetTexture("PLAYER");

        public static Vector2 GetPosition() {
            return Position;
        }

        public static void Tick() {

        }

        public static void render(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(sprite, destinationRectangle: new Rectangle((int)(Camera.GetWidthScalar() * (Position.X - dimensions.X / 2)), (int)(Camera.GetHeightScalar() * (Position.Y - dimensions.Y / 2)), (int)(Camera.GetWidthScalar() * (dimensions.X)), (int)(Camera.GetHeightScalar() * dimensions.Y)), Color.White);


        }
    }
}
