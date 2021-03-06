﻿using Microsoft.Xna.Framework;

namespace SuperNova.Code.Util {

    public static class Camera {
        private const int Width = 1280, Height = 720;
        private static int _currentWidth = 1280, _currentHeight = 720;
        private static Vector2 _flippedCoordinates = Vector2.Zero;

        public static void Update(int width, int height) {
            _currentWidth = width;
            _currentHeight = height;
        }

        public static int GetX() {

            return (int)_flippedCoordinates.X;
        }

        public static void SetX(float x) {

            _flippedCoordinates.X = x;
        }

        public static void SetPosition(Vector2 _flippedCoordinates) {

           Camera._flippedCoordinates = _flippedCoordinates;
        }

        public static int GetY() {

            return (int)_flippedCoordinates.Y;
        }

        public static void SetY(float y) {

            _flippedCoordinates.Y = y;
        }

        public static Vector2 GetPoint() {

            return new Vector2(_flippedCoordinates.X + _currentWidth - Width, _flippedCoordinates.Y + _currentHeight - Height);
        }

        public static float GetWidthScalar() {

            return (float)_currentWidth / Width;
        }

        public static float GetHeightScalar() {

            return (float)_currentHeight / Height;
        }

        public static bool IsOnScreen(Vector2 position, Vector2 dimensions, int scaler = 1) {
            var (dimensionX, dimensionY) = dimensions;
            var (positionX, positionY) = position;

            return (positionX + GetX() / scaler - dimensionX / 2 < Width && positionX + GetX() / scaler + dimensionX / 2 > 0 && positionY + GetY() / scaler - dimensionY / 2 < Height && positionY + GetY() / scaler + dimensionY / 2 > 0);
        }

    }
}