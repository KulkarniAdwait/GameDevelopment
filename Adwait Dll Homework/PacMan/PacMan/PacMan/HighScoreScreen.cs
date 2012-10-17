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
using System.IO;



namespace PacMan
{

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class HighScoreScreen : Microsoft.Xna.Framework.GameComponent
    {

        SpriteBatch spriteBatch;
        Texture2D texture;
        SpriteFont font;
        List<int> highScores;
        public HighScoreScreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            highScores = new List<int>();
            for (int i = 0; i < 5; i++)
                highScores.Add(0);
            GetScores();
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

        public void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("HighScoreBK");
            font = Game.Content.Load<SpriteFont>("Fonts/ScoreFont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, int score)
        {
            // TODO: Add your update code here
            int minScore = highScores[0];
            foreach (int scr in highScores)
            {
                if (minScore > scr)
                    minScore = scr;
            }

            if (score > minScore)
            {
                for (int i = 0; i < highScores.Count && i < 5; i++)
                {
                    if (highScores[i] == minScore)
                    {
                        highScores[i] = score;
                        break;
                    }
                }
                WriteScores();
            }
            base.Update(gameTime);
        }

        private void GetScores()
        {
            using (StreamReader reader = new StreamReader("..\\..\\..\\highScore.txt"))
            {
                string line;
                int i = 0;
                while ((line = reader.ReadLine()) != null || i < 5)
                {
                    highScores[i] = Convert.ToInt16(line);
                    i++;
                }
            }
        }

        private void WriteScores()
        {
            using (StreamWriter writer = new StreamWriter("..\\..\\..\\highScore.txt"))
            {
                foreach (int score in highScores)
                {
                    writer.WriteLine(score);
                }
            }
        }


        public void Draw(GameTime gametime, String score)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle(10 * GameConstants.BlockWidth, 5 * GameConstants.BlockHeight, 250, 500), Color.Black);
            spriteBatch.DrawString(font, "High Scores", new Vector2(10 * GameConstants.BlockWidth, 5 * GameConstants.BlockHeight), Color.Blue,
                0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(font, "Your Score :" + Convert.ToString(score), new Vector2(10 * GameConstants.BlockWidth, 6 * GameConstants.BlockHeight), Color.Blue,
                0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);

            for (int i = 0; i < highScores.Count; i++)
            {
                spriteBatch.DrawString(font, i + 1 + " : " + Convert.ToString(highScores[i]), new Vector2(10 * GameConstants.BlockWidth, (7 + i) * GameConstants.BlockHeight), Color.Blue,
                    0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            }
            spriteBatch.End();
        }
    }
}
