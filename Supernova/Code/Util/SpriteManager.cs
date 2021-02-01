using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code;

namespace SuperNova.Code.Util {

    public static class SpriteManager {

        private static Dictionary<string, Texture2D> _spriteMap;
        static Random rand = new Random();

        private static Game _game;

        public static void LoadAssets(SupernovaGame game) {

            _game = game;

            _spriteMap = new Dictionary<string, Texture2D>();
            var assetsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location);

            string separator;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                assetsPath = assetsPath?.Substring(0, assetsPath.IndexOf("bin", StringComparison.Ordinal)) + "Assets\\";
                separator = "\\";
            }
            else {
                assetsPath = assetsPath?.Substring(0, assetsPath.IndexOf("bin", StringComparison.Ordinal)) + "Assets/";
                separator = "/";
            }

            var files = Directory.EnumerateFiles(assetsPath, "*.png", SearchOption.AllDirectories);

            foreach (var file in files) {
                var identifier = file.Substring(file.LastIndexOf(separator, StringComparison.Ordinal) + 1);
                identifier = identifier.Substring(0, identifier.IndexOf(".png", StringComparison.Ordinal)).ToUpper();
                _spriteMap.Add(identifier, Texture2D.FromFile(game.GraphicsDevice, file));
                Console.WriteLine($"Loaded {identifier}");
            }
        }

        public static Texture2D GetTexture(string identifier) {
            return _spriteMap.ContainsKey(identifier) ? _spriteMap[identifier] : _spriteMap["MISSING"];
        }

        public static Texture2D MakePlanetTexture() {

            int type = rand.Next(4);

            switch (type) {

                case 0:
                    return Texture2dHelper.Shade(_game.GraphicsDevice, SpriteManager.GetTexture("EARTH_PLANET"));

                case 1:
                    return Texture2dHelper.Shade(_game.GraphicsDevice, SpriteManager.GetTexture("MARS_PLANET"));

                case 2:
                    return Texture2dHelper.Shade(_game.GraphicsDevice, SpriteManager.GetTexture("SAND_PLANET"));

                case 3:
                    return Texture2dHelper.Shade(_game.GraphicsDevice, SpriteManager.GetTexture("ROCK_PLANET"));

                default:
                    return SpriteManager.GetTexture("PLANET");
            }
        }
    }
}
