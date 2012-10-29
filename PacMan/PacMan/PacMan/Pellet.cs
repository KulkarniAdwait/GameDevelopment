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
    public class Pellet : Microsoft.Xna.Framework.GameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D pixel;
        private int spriteWidth = 6;
        private int spriteHeight = 6;
        public List<Rectangle> pellets;
        public Pellet(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
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

        public void LoadContent(List<Rectangle> Positions)
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            pellets = new List<Rectangle>();
            foreach (Rectangle rect in Positions)
            {
                pellets.Add(new Rectangle(rect.X - 3, rect.Y - 3, spriteWidth, spriteHeight));
            }
            // Somewhere in your LoadContent() method:
            pixel = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.Yellow }); // so that we can draw whatever color we want on top of it
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            // TODO: Add your drawing code here

        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Rectangle rect in pellets)
            {
                spriteBatch.Draw(pixel, rect, Color.Yellow);
            }

            spriteBatch.End();
            base.Update(gameTime);
        }

        public void RemovePellet(Rectangle pellet)
        {
            for (int i = 0; i < pellets.Count; i++)
            {
                if (pellets[i].X == pellet.X & pellets[i].Y == pellet.Y)
                {
                    pellets.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
