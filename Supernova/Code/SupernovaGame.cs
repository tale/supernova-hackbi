using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SuperNova.Code.Util;
using SuperNova.Code.Object;
using SuperNova.Code.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Supernova.Code.World;
using Supernova.Code.Object;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Supernova.Code {
    internal enum GameState {
        StartScreen,
        GameScreen,
        LoseScreen
    }
    public class SupernovaGame : Game {

        private int _delay = 0;
        private Button start;
        private Button end;
        private Image title;


        public static SpriteFont font;
        
        private GameState _gameState = GameState.StartScreen;
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;

        public SupernovaGame() {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            _graphicsDeviceManager.PreferredBackBufferHeight = 720;
            _graphicsDeviceManager.PreferredBackBufferWidth = 1280;
            _graphicsDeviceManager.PreferMultiSampling = true;

            Window.AllowUserResizing = true;
            Window.IsBorderless = false;

            _graphicsDeviceManager.ApplyChanges();
            SpriteManager.LoadAssets(this);
            base.Initialize();

            start = new Button(640, 600, 480, 120, SpriteManager.GetTexture("START"));
            end = new Button(640, 600, 480, 120, SpriteManager.GetTexture("END"));
            title = new Image(640, 360, 880, 684, SpriteManager.GetTexture("SUPERNOVA"));
        }

        protected override void LoadContent() {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime) {

            System.Threading.Thread.Sleep(5);

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

                    if (keyBoardState.IsKeyDown(Keys.A)) {
                        Player.Angle -= .075f;
                        Player.Angle = Player.Angle < 0 ? Player.Angle + (float)Math.PI * 2: Player.Angle;
                    }


                    if (keyBoardState.IsKeyDown(Keys.D)) {
                        Player.Angle += .075f;
                        Player.Angle %= (float)Math.PI * 2;
                    }

                    if (keyBoardState.IsKeyDown(Keys.W) && Player.Fuel > 0) {
                        Player.AddToVelocity(.13f * WorldManager.AccelerationModifier, Player.Angle);
                        Player.Fuel -= 0.1f;

                    }

                    if(Player.Health <= 0) {
                        _gameState = GameState.LoseScreen;
                        Update(gameTime);
                    }
                    
                    WorldManager.WorldTick();
                    WorldManager.WorldTick2();
                    Player.Tick();
                    //SupernovaObject.Tick();

                    break;
                case GameState.LoseScreen:
                    if (end.Tick(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && _delay <= 0) {
                        _gameState = GameState.StartScreen;
                        Player.Reset();
                        SupernovaObject.Reset();
                        WorldManager.Reset();
                        _delay = 10;
                        Camera.SetX(0);
                        Camera.SetY(0);
                        Update(gameTime);
                    }
                    break;
                default:
                    throw new Exception("Unknown Game State");
            }

            _delay = Math.Max(_delay - 1, 0);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, DepthStencilState.DepthRead);


            switch (_gameState) {
                case GameState.StartScreen:
                    title.Render(_spriteBatch);
                    start.Render(_spriteBatch);
  

                    break;
                case GameState.GameScreen:
                    WorldManager.WorldRender(_spriteBatch);
                    Player.Render(_spriteBatch);
                    SupernovaObject.Render(_spriteBatch);
                    HealthBar.Render(_spriteBatch);
                    FuelBar.Render(_spriteBatch);
                    break;
                case GameState.LoseScreen:
                    font = Content.Load<SpriteFont>("BaseFont");
                    String score = Player.Score.ToString();
                    
                    _spriteBatch.DrawString(
                        font, 
                        "GAME OVER",
                        new Vector2(640 * Camera.GetWidthScalar(), 50),
                        Color.Red,
                        0,
                        new Vector2(font.MeasureString("GAME OVER").X / 2, 0),
                        Vector2.One, 
                        SpriteEffects.None,
                        0);
                    
                    _spriteBatch.DrawString(
                        font, 
                        "Score",
                        new Vector2(640 * Camera.GetWidthScalar(), 200),
                        Color.Gray,
                        0,
                        new Vector2(font.MeasureString("Score").X / 2, 0),
                        new Vector2(0.5F, 0.5F), 
                        SpriteEffects.None,
                        0);
                    
                    _spriteBatch.DrawString(
                        font, 
                        score, 
                        new Vector2(
                            640 * Camera.GetWidthScalar(), 
                            360 * Camera.GetHeightScalar()), 
                        Color.White, 
                        0, 
                        new Vector2(font.MeasureString(score).X / 2, font.MeasureString(score).Y / 2), 
                        Vector2.One, 
                        SpriteEffects.None, 
                        0);
                    end.Render(_spriteBatch);
                    break;
                default:
                    throw new Exception("Unknown Game State");
            }

                    base.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
