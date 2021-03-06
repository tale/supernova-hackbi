﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperNova.Code.Object;
using Supernova.Code.Util;
using SuperNova.Code.Utilities;
using SuperNova.Code.Util;

//@author Peter Downey

namespace Supernova.Code.World {
    public class ParallaxChunk {

        private static Random _random = new Random();

        private Vector2 _position;
        private readonly int _scaler, _density;
        private readonly bool _starChunk;
        
        public Image[] ParallaxImages { get; }

        public ParallaxChunk(float x, float y, int scaler, int density, bool starChunk = false) {

            _position = new Vector2(x, y);
            _scaler = scaler;
            _density = density;
            _starChunk = starChunk;

            ParallaxImages = GeneratePlanetImageMap();
        }

        private Image[] GeneratePlanetImageMap() {

            var array = new Image[_density];

            int iteration = 1000000;
            int number = 0;

            List<(int, int)> previous = new List<(int, int)>();

            do {

                var x = _random.Next(2000 - 84 / (_scaler * 2)) + 84 / _scaler;
                var y = _random.Next(2000 - 84 / (_scaler * 2)) + 84 / _scaler;

                Texture2D image = _starChunk ? SpriteManager.GetTexture("STAR") : SpriteManager.MakePlanetTexture();

                if (checkPrevious(x, y, previous)) {

                    if (!_starChunk)
                        array[number] = new Image(x + _position.X, y + _position.Y, 84 / _scaler, 84 / _scaler, image);
                    else
                        array[number] = new Image(x + _position.X, y + _position.Y, 4, 4, image);
                    previous.Add((x, y));
                    number++;
                }

                iteration--;

            } while (iteration > 0 && number < _density);

            return array;


        }


        private Boolean checkPrevious(int x, int y, List<(int, int)> previous) {

            foreach ((int, int) prev in previous) {

                var (x2, y2) = prev;

                if (Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2)) < 300.0 / _scaler)
                    return false;
            }
            return true;
        }


        public void Render(SpriteBatch _spritebatch) {

            foreach (var image in ParallaxImages) {
                if (image != null) image.ParallaxRender(_spritebatch, _scaler);
            }
        }
    }
}


