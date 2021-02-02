using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code;
using SuperNova.Code.Object;

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
                    return GetTexture("EARTH_PLANET");

                case 1:
                    return GetTexture("MARS_PLANET");

                case 2:
                    return GetTexture("SAND_PLANET");

                case 3:
                    return GetTexture("ROCK_PLANET");

                default:
                    return GetTexture("PLANET");
            }
        }

        public static Texture2D MakeAstroidTexture() {

            var type = rand.Next(2);

            switch (type) {

                case 0:
                    return GetTexture("ASTEROID"); ;

                case 1:
                    return GetTexture("ASTEROID2"); ;

                default:
                    return GetTexture("ASTEROID"); ;
            }
        }

        public static Texture2D rotationShade(Texture2D texture, float angle) {

            return Texture2dHelper.Shade(_game.GraphicsDevice, texture, angle);

        }
        public static Texture2D playerRotationShade(Texture2D texture, Planet planet) {

            if (planet == null)
                return texture;

            return Texture2dHelper.PlayerShade(_game.GraphicsDevice, texture, planet);

        }
    }
}
