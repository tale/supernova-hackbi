using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using SuperNova.Code.Util;
using SuperNova.Code.Object;

namespace Supernova.Code
{
    public class SupernovaGame : Game
    {

        Planet test;

        public const int STARTSCREEN = 0, GAMESCREEN = 1, MENUSCREEN = 2, SETTINGSCREEN = 3;
        public int gameState = GAMESCREEN;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public SupernovaGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = 450;
            graphics.PreferredBackBufferWidth = 720;

            Window.AllowUserResizing = true;
            Window.IsBorderless = false;

            graphics.ApplyChanges();

            test = new Planet(new Vector2(0, 0), 50, 50, 1);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {

            Camera.update(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyBoardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState) {

                case STARTSCREEN:

                    break;

                case MENUSCREEN:

                    break;

                case SETTINGSCREEN:


                    break;
                 
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (gameState) {

                case STARTSCREEN:

                    break;


                case GAMESCREEN:

                    test.render(spriteBatch);


                    break;

                case MENUSCREEN:

                    break;

                case SETTINGSCREEN:

                    break;

                default:
                    throw new Exception("Unkown Game State");

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
