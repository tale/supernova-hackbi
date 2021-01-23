using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperNova.Code.Object {

    public class Player {

        private Vector2 position, scale;

        private Texture2D sprite;

        public Player(Vector2 position, Vector2 scale) {

            this.position = position;
            this.scale = scale;
            this.sprite = makePlayerTexture();
        }

        public void tick() {

        }

        public void render() {



        }

        private static Texture2D makePlayerTexture() {

            return SpriteManager.GetTexture("PLAYER");
        }
    }

}
