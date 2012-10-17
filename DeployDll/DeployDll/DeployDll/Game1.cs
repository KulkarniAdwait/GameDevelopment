using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HUD;

namespace DeployDll
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Hud gameHud;
        private int _buttonPresses;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameHud = new Hud("..//..//..//hudSettings.xml");
            _buttonPresses = 0;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameHud.LoadContent(this.Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        GamePadState oldState;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            GamePadState newState = GamePad.GetState(PlayerIndex.One);
            if (newState.Buttons.B == ButtonState.Pressed && oldState!= newState)
            {
                graphics.PreferredBackBufferHeight = 720;
                graphics.PreferredBackBufferWidth = 1200;
                graphics.ApplyChanges();
                gameHud.ReSize(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                UpdateScore();
            }
            if (newState.Buttons.A == ButtonState.Pressed && oldState != newState)
            {
                graphics.PreferredBackBufferHeight = 480;
                graphics.PreferredBackBufferWidth = 800;
                graphics.ApplyChanges();
                gameHud.ReSize(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                UpdateScore();
            }
            if (newState.Buttons.X == ButtonState.Pressed && oldState != newState)
            {
                graphics.PreferredBackBufferHeight = 240;
                graphics.PreferredBackBufferWidth = 400;
                graphics.ApplyChanges();
                gameHud.ReSize(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                UpdateScore();
            }
            if (newState.Buttons.RightShoulder == ButtonState.Pressed && oldState != newState)
            {
                graphics.PreferredBackBufferHeight += 20;
                graphics.PreferredBackBufferWidth += 32;
                graphics.ApplyChanges();
                gameHud.ReSize(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                UpdateScore();
            }
            if (newState.Buttons.LeftShoulder == ButtonState.Pressed && oldState != newState)
            {
                graphics.PreferredBackBufferHeight -= 20;
                graphics.PreferredBackBufferWidth -= 32;
                graphics.ApplyChanges();
                gameHud.ReSize(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                UpdateScore();
            }
            if (newState.DPad.Up == ButtonState.Pressed && oldState != newState)
            {
                gameHud.SetVisibility(300, true);
                gameHud.SetVisibility(302, true);
                gameHud.UpdateText(301, "Here is the man");
                UpdateScore();
            }
            if (newState.DPad.Down == ButtonState.Pressed && oldState != newState)
            {
                gameHud.SetVisibility(300, false);
                gameHud.SetVisibility(302, false);
                gameHud.UpdateText(301, "Where is the man?");
                UpdateScore();
            }
            if (newState.Buttons.Y == ButtonState.Pressed && oldState != newState)
            {
                Random rand = new Random();
                gameHud.UpdateText(202, new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))); 
                UpdateScore();
            }
            oldState = GamePad.GetState(PlayerIndex.One);
            base.Update(gameTime);
        }

        private void UpdateScore()
        {
            _buttonPresses++;
            gameHud.UpdateText(100, "Score:" + _buttonPresses);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            gameHud.Draw(this.spriteBatch);

            base.Draw(gameTime);
        }
    }
}
