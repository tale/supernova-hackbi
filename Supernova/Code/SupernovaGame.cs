using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SuperNova.Code.Util;
using SuperNova.Code.Object;

namespace Supernova.Code {
    internal enum GameState {
        StartScreen,
        GameScreen,
        LoseScreen,
        MenuScreen,
        SettingsScreen
    }
    public class SupernovaGame : Game {
        Planet test;
        
        private GameState _gameState = GameState.GameScreen;
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        public SupernovaGame() {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            _graphicsDeviceManager.PreferredBackBufferHeight = 450;
            _graphicsDeviceManager.PreferredBackBufferWidth = 720;

            Window.AllowUserResizing = true;
            Window.IsBorderless = false;

            _graphicsDeviceManager.ApplyChanges();
            base.Initialize();
        }

        protected override void Update(GameTime gameTime) {
            Camera.Update(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (_gameState) {
                case GameState.StartScreen:
                    break;
                case GameState.GameScreen:
                    break;
                case GameState.LoseScreen:
                    break;
                case GameState.MenuScreen:
                    break;
                case GameState.SettingsScreen:
                    break;
                default:
                    Console.WriteLine("Invalid game state. Exiting...");
                    Exit();
                    break;
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
