using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

//@author Peter Downey


namespace Fondue.Code.Utilities {

    public static class Camera {


        public static readonly int WIDTH = 720, HEIGHT = 450;

        public static int currWidth = WIDTH, currHeight = HEIGHT;

        public static Vector2 flippedPlayerCords = Vector2.Zero;

        public static void update(int currWidth, int currHeight) {

            Camera.currWidth = currWidth;
            Camera.currHeight = currHeight;
        }

        public static int getX() {

            return (int)flippedPlayerCords.X;
        }

        public static void setX(float x) {

            flippedPlayerCords.X = x;
        }

        public static int getY() {

            return (int)flippedPlayerCords.Y;
        }

        public static void setY(float y) {

            flippedPlayerCords.Y = y;
        }

        public static Vector2 getPoint() {

            return new Vector2(flippedPlayerCords.X + currWidth - WIDTH, flippedPlayerCords.Y + currHeight - HEIGHT);
        }

        public static float getWidthScaler() {

            return (float)currWidth / WIDTH;
        }

        public static float getHeightScaler() {

            return (float)currHeight / HEIGHT;
        }

        public static bool isOnScreen(Vector2 position, Vector2 dimensions) {

            return (position.X + Camera.getX() - dimensions.X / 2 < WIDTH && position.X + Camera.getX() + dimensions.X / 2 > 0 && position.Y + Camera.getY() - dimensions.Y / 2 < HEIGHT && position.Y + Camera.getY() + dimensions.Y / 2 > 0);
        }
    }
}