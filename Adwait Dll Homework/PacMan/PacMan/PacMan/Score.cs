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


namespace PacMan
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Score : Microsoft.Xna.Framework.GameComponent
    {
        int ScoreP1;
        int Lives;

        public Score(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            Reset();
        }

        /// <summary>
        /// Reset all the scores to zero
        /// </summary>
        private void Reset()
        {
            ScoreP1 = 0;
            Lives = GameConstants.Lives;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        /// <summary>
        /// Increments the score of the player that is passed.
        /// </summary>
        /// <param name="player">Player whose score is to be incremented</param>
        public void IncrementScore(int Value)
        {
            ScoreP1 += Value;
        }

        public void DecrementLife()
        {
            Lives -= 1;
        }

        public bool GameOver()
        {
            if (Lives == 0)
                return true;
            return false;
        }

        public int getScore()
        {
            return ScoreP1;
        }

        public int getLives()
        {
            return Lives;
        }

    }
}
