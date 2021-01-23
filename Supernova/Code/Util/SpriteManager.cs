using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code;

namespace SuperNova.Code.Util {
    public static class SpriteManager {
        private static Dictionary<string, Texture2D> _spriteMap;
        public static void LoadAssets(SupernovaGame game) {
            _spriteMap = new Dictionary<string, Texture2D>();
            var assetsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                assetsPath = assetsPath?.Substring(0, assetsPath.IndexOf("bin", StringComparison.Ordinal)) + "Assets\\";
            }
            else {
                assetsPath = assetsPath?.Substring(0, assetsPath.IndexOf("bin", StringComparison.Ordinal)) + "Assets/";
            }

            var files = Directory.EnumerateFiles(assetsPath, "*.png", SearchOption.AllDirectories);

            foreach (var file in files) {
                var identifier = file.Substring(file.LastIndexOf("/", StringComparison.Ordinal) + 1);
                identifier = identifier.Substring(0, identifier.IndexOf(".png", StringComparison.Ordinal)).ToUpper();
                _spriteMap.Add(identifier, Texture2D.FromFile(game.GraphicsDevice, file));
                Console.WriteLine($"Loaded {identifier}");
            }
        }

        public static Texture2D GetTexture(string identifier) {
            return _spriteMap.ContainsKey(identifier) ? _spriteMap[identifier] : _spriteMap["MISSING"];
        }
    }
}
