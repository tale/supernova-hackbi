using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SuperNova.Code.Util;
using SuperNova.Code.Object;
using SuperNova.Code.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code.World;

namespace Supernova.Code {
    internal enum GameState {
        StartScreen,
        GameScreen,
        LoseScreen,
        MenuScreen,
        SettingsScreen
    }
    public class SupernovaGame : Game {

        private int _delay = 0;
        private Button start;
        private Image title;
        Planet test;
        
        private GameState _gameState = GameState.StartScreen;
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;

        public SupernovaGame() {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            _graphicsDeviceManager.PreferredBackBufferHeight = 450;
            _graphicsDeviceManager.PreferredBackBufferWidth = 720;
            _graphicsDeviceManager.PreferMultiSampling = true;

            Window.AllowUserResizing = true;
            Window.IsBorderless = false;

            _graphicsDeviceManager.ApplyChanges();
            SpriteManager.LoadAssets(this);
            base.Initialize();

            start = new Button(360, 350, 240, 60, SpriteManager.GetTexture("START"));
            title = new Image(360, 150, 360, 280, SpriteManager.GetTexture("SUPERNOVA"));
        }

        protected override void LoadContent() {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime) {

            Camera.Update(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyBoardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (_gameState) {
                case GameState.StartScreen:

                    if (start.Tick(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && _delay <= 0) {
                        _gameState = GameState.GameScreen;
                        _delay = 10;
                        Camera.SetX(0);
                        Camera.SetY(0);
                        Update(gameTime);
                    }
                    break;
                case GameState.GameScreen:
                    WorldManager.WorldTick();

                    if (keyBoardState.IsKeyDown(Keys.A))
                        Player.SetAngle(Player.GetAngle() - .05f);

                    if (keyBoardState.IsKeyDown(Keys.D))
                        Player.SetAngle(Player.GetAngle() + .05f);

                    if (keyBoardState.IsKeyDown(Keys.W))
                        Player.addToVelocity(.2f, Player.GetAngle());
                    

                    Player.Tick();

                    break;
                case GameState.LoseScreen:
                    break;
                case GameState.MenuScreen:
                    break;
                case GameState.SettingsScreen:
                    break;
                default:
                    throw new Exception("Unkown Game State");
            }

            _delay = Math.Max(_delay - 1, 0);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            switch (_gameState) {
                case GameState.StartScreen:
                    start.Render(_spriteBatch);
                    title.Render(_spriteBatch);

                    break;
                case GameState.GameScreen:
                    WorldManager.WorldRender(_spriteBatch);
                    Player.Render(_spriteBatch);
                    break;
                case GameState.LoseScreen:
                    break;
                case GameState.MenuScreen:
                    break;
                case GameState.SettingsScreen:
                    break;
                default:
                    throw new Exception("Unkown Game State");
            }

                    base.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
