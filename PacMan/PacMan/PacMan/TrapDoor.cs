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
    public class TrapDoor : Microsoft.Xna.Framework.GameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D texture;
        int spriteSize = 40;
        public List<Rectangle> trapDoors;
        public TrapDoor(Game game)
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


        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("Door");
            trapDoors = new List<Rectangle>();
            Vector2 trapDoor_1 = new Vector2(GameConstants.BlockWidth, 2 * GameConstants.BlockHeight);
            Vector2 trapDoor_2 = new Vector2((GameConstants.horizontalMazeBlocks - 1) * GameConstants.BlockWidth, 2 * GameConstants.BlockHeight);
            Vector2 trapDoor_3 = new Vector2(GameConstants.BlockWidth, (GameConstants.verticalMazeBlocks + 1) * GameConstants.BlockHeight);
            Vector2 trapDoor_4 = new Vector2((GameConstants.horizontalMazeBlocks - 1) * GameConstants.BlockWidth, (GameConstants.verticalMazeBlocks + 1) * GameConstants.BlockHeight);
            Vector2 trapDoor_5 = new Vector2((int)((GameConstants.horizontalMazeBlocks + 1) / 2) * GameConstants.BlockWidth
                , (int)((GameConstants.verticalMazeBlocks + 4) / 2) * GameConstants.BlockHeight);
            List<Vector2> positions = new List<Vector2>();
            positions.Add(trapDoor_1);
            positions.Add(trapDoor_2);
            positions.Add(trapDoor_3);
            positions.Add(trapDoor_4);
            positions.Add(trapDoor_5);
            foreach (Vector2 position in positions)
            {
                trapDoors.Add(new Rectangle((int)position.X, (int)position.Y, spriteSize, spriteSize));
            }
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

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Rectangle rectangle in trapDoors)
            {
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
            spriteBatch.End();
        }
    }
}
