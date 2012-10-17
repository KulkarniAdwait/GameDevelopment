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

namespace PacMan
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PacMan : Microsoft.Xna.Framework.Game
    {
        private float timer = 0f;
        private float interval = 3000f;
        private float warningInterval = 60f;
        private float warningTimer = 0f;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Hero hero;
        Rectangle exitDoor;
        bool pelletsOver;
        Hud hud;

        public enum GameStates
        {
            Normal,
            GameBegin,
            PacManDead,
            LevelOver,
            GameOver,
        }

        GameStates gameState;
        List<Ghost> ghosts;
        Score score;
        Maze maze;
        Collison collision;
        Pellet pellet;
        TrapDoor trapDoor;
        private readonly int STEP = 1;
        CountDownTimer gameTimer;
        HighScoreScreen hsScreen;
        Texture2D greenLight;
        SoundEffect heroDead;
        SoundEffect ghostEaten;
        SoundEffect levelCompleted;
        SoundEffect pelletEaten;
        public PacMan()
        {
            gameState = GameStates.Normal;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameConstants.GameWidth;
            graphics.PreferredBackBufferHeight = GameConstants.GameHeight;
            Content.RootDirectory = "Content";

            hero = new Hero(this);
            Ghost ghost_0 = new Ghost(this, 0);
            Ghost ghost_1 = new Ghost(this, 1);
            Ghost ghost_2 = new Ghost(this, 2);
            Ghost ghost_3 = new Ghost(this, 3);
            Ghost ghost_4 = new Ghost(this, 4);
            ghosts = new List<Ghost>();
            ghosts.Add(ghost_0);
            ghosts.Add(ghost_1);
            ghosts.Add(ghost_2);
            ghosts.Add(ghost_3);
            ghosts.Add(ghost_4);
            maze = new Maze(this);
            score = new Score(this);
            collision = new Collison(STEP, GameConstants.BlockWidth, 2 * GameConstants.BlockHeight
                , GameConstants.GameWidth - GameConstants.BlockWidth
                , GameConstants.GameHeight - GameConstants.BlockHeight);
            pellet = new Pellet(this);
            trapDoor = new TrapDoor(this);
            gameTimer = new CountDownTimer(this);
            hsScreen = new HighScoreScreen(this);
            pelletsOver = false;
            exitDoor = new Rectangle((12 * GameConstants.BlockWidth) + (int)GameConstants.BlockWidth / 4
                                , (9 * GameConstants.BlockHeight) + (int)GameConstants.BlockHeight / 2, 20, 20);
            hud = new Hud("..//..//..//HudSettings.xml", Color.Azure);
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

        private void ResetLevel()
        {
            hero.ResetPosition();
            foreach (Ghost ghost in ghosts)
            {
                ghost.ResetGhost();
            }
            pelletsOver = false;
            gameTimer.Reset();
            pellet = new Pellet(this);
            this.gameState = GameStates.Normal;
            this.LoadContent();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hero.LoadContent();
            ghosts[0].LoadContent();
            ghosts[1].LoadContent();
            ghosts[2].LoadContent();
            ghosts[3].LoadContent();
            ghosts[4].LoadContent();
            maze.LoadContent();
            pellet.LoadContent(maze.intersections);
            trapDoor.LoadContent();

            hsScreen.LoadContent();
            greenLight = Content.Load<Texture2D>("GreenLight");
            heroDead = Content.Load<SoundEffect>("Sounds/PacmanDead");
            ghostEaten = Content.Load<SoundEffect>("Sounds/GhostEaten");
            levelCompleted = Content.Load<SoundEffect>("Sounds/LevelCompleted");
            pelletEaten = Content.Load<SoundEffect>("Sounds/PelletEaten");
            hud.LoadContent(Content);
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (this.gameState)
            {
                case GameStates.PacManDead:
                    {
                        //play dying animation
                        timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (timer >= interval)
                        {
                            //reset pacman
                            hero.ResetPosition();
                            //reduce life score
                            score.DecrementLife();
                            if (score.GameOver())
                            {
                                this.gameState = GameStates.GameOver;
                            }
                            else
                            {
                                this.gameState = GameStates.Normal;
                            }
                            timer = 0f;
                        }
                        break;
                    }
                case GameStates.GameOver:
                    {
                        //check if this is high score
                        //update high score table
                        //show high score table
                        //exit game
                        timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (timer >= interval)
                        {
                            hsScreen.Update(gameTime, score.getScore());
                            Exit();
                        }
                        break;
                    }
                case GameStates.Normal:
                    {
                        gameTimer.Update(gameTime);
                        if (gameTimer.TimeOut())
                        {
                            this.gameState = GameStates.GameOver;
                        }
                        else if (gameTimer.LowTime())
                        {
                            hud.UpdateText(100, Color.Red);
                            hud.SetVisibility(101, true);
                            warningTimer++;
                            if (warningTimer == warningInterval)
                            {
                                hud.UpdateText(101, Color.Yellow);
                                warningTimer = warningInterval + 1;
                            }
                            else if (warningTimer >= 2 * warningInterval)
                            {
                                hud.UpdateText(101, Color.Red);
                                warningTimer = 0;
                            }
                        }
                        hero.Update(gameTime, maze.mazeParts);

                        ghosts[0].Update(gameTime, maze.mazeParts, maze.intersections, trapDoor.trapDoors, GhostType.Normal);
                        ghosts[1].Update(gameTime, maze.mazeParts, maze.intersections, trapDoor.trapDoors, GhostType.Edible);
                        ghosts[2].Update(gameTime, maze.mazeParts, maze.intersections, trapDoor.trapDoors, GhostType.Normal);
                        ghosts[3].Update(gameTime, maze.mazeParts, maze.intersections, trapDoor.trapDoors, GhostType.Edible);
                        ghosts[4].Update(gameTime, maze.mazeParts, maze.intersections, trapDoor.trapDoors, GhostType.Normal);

                        foreach (Ghost ghost in ghosts)
                        {
                            if (ghost.rectangle.Intersects(hero.rectangle))
                            {
                                if (ghost.ghostType == GhostType.Normal)
                                {
                                    this.gameState = GameStates.PacManDead;
                                    heroDead.Play();
                                }
                                else
                                {
                                    ghostEaten.Play();
                                    ghost.ResetGhost();
                                    score.IncrementScore(1000);
                                }
                            }
                        }
                        foreach (Rectangle rect in pellet.pellets)
                        {
                            if (hero.rectangle.Intersects(rect))
                            {
                                pelletEaten.Play();
                                score.IncrementScore(100);
                                pellet.RemovePellet(rect);
                                if (pellet.pellets.Count == 0)
                                {
                                    this.pelletsOver = true;
                                }
                                break;
                            }
                        }
                        if (pelletsOver && hero.rectangle.Intersects(exitDoor))
                        {
                            levelCompleted.Play();
                            this.gameState = GameStates.LevelOver;
                        }
                        break;
                    }
                case GameStates.LevelOver:
                    {
                        timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (timer >= interval/2)
                        {                            
                            this.ResetLevel();
                        }
                        break;
                    }
            }
            UpdateHud();
            base.Update(gameTime);
        }

        private void UpdateHud()
        {
            hud.UpdateText(200, "Lives:" + score.getLives().ToString());
            hud.UpdateText(201, "Score:" + score.getScore().ToString());
            hud.UpdateText(100, "Time:" + gameTimer.getTime());
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            hud.Draw(spriteBatch);
            //score.Draw(gameTime);
            maze.Draw(gameTime);
            trapDoor.Draw(gameTime);
            switch (this.gameState)
            {
                case GameStates.PacManDead:
                    {
                        hero.DeathAnimation(gameTime);
                        break;
                    }
                case GameStates.Normal:
                    {
                        //gameTimer.Draw(gameTime);
                        if (pelletsOver)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(greenLight, exitDoor, Color.White);
                            spriteBatch.End();
                        }
                        ghosts[0].Draw(gameTime);
                        ghosts[1].Draw(gameTime);
                        ghosts[2].Draw(gameTime);
                        ghosts[3].Draw(gameTime);
                        ghosts[4].Draw(gameTime);
                        pellet.Draw(gameTime);
                        hero.Draw(gameTime);
                        break;
                    }
                
                case GameStates.GameOver:
                    {
                        hsScreen.Draw(gameTime, score.getScore().ToString());
                        break;
                    }
            }
            base.Draw(gameTime);
        }
    }
}
