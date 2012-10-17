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
    public class CountDownTimer : Microsoft.Xna.Framework.GameComponent
    {
        private readonly TimeSpan LevelTime = new TimeSpan(0, 2, 0);
        private readonly TimeSpan TimeOver = new TimeSpan(0, 0, 0);
        private readonly TimeSpan HurryUpTime = new TimeSpan(0, 1, 40);
        TimeSpan Time;

        public CountDownTimer(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            Reset();
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

        public void Reset()
        {
            Time = LevelTime;
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            Time = LevelTime - gameTime.TotalGameTime;
            base.Update(gameTime);
        }

        public bool TimeOut()
        {
            if (Time <= TimeOver)
            {
                return true;
            }
            return false;
        }

        internal string getTime()
        {
            return (Time.ToString("mm") + ":" + Time.ToString("ss"));
        }

        internal bool LowTime()
        {
            if (Time <= HurryUpTime)
            {
                return true;
            }
            return false;
        }
    }
}
