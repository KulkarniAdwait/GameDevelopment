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
    public class Maze : Microsoft.Xna.Framework.GameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D pixel;
        private int spriteWidth = GameConstants.BlockWidth;
        private int spriteHeight = GameConstants.BlockHeight;
        //private readonly int wallThickness = 34;

        private readonly int PADDING = 0;

        public List<Rectangle> mazeParts;
        public List<Rectangle> intersections;
        public Maze(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            mazeParts = new List<Rectangle>();
            intersections = new List<Rectangle>();
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

            // Somewhere in your LoadContent() method:
            pixel = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.Blue }); // so that we can draw whatever color we want on top of it

            BuildMaze();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, GhostType ghostType)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            //Draw maze
            foreach (Rectangle rect in mazeParts)
            {
                spriteBatch.Draw(pixel, rect, Color.Blue);
            }
            ////TODO: remove
            //foreach (Rectangle rect in intersections)
            //{
            //    spriteBatch.Draw(pixel, rect, Color.Blue);
            //}
            spriteBatch.End();
        }

        const int divisor = 8;
        int intersectionSize = 1;
        
        int smallOffset = (int)GameConstants.BlockWidth / 2;

        private void BuildMaze()
        {
            //left wall
            Rectangle leftWall = new Rectangle(0, spriteHeight, spriteWidth + PADDING, (GameConstants.verticalMazeBlocks + 1) * spriteHeight + PADDING);
            mazeParts.Add(leftWall);
            //bottom wall
            Rectangle bottomWall = new Rectangle(0, (GameConstants.verticalMazeBlocks + 2) * spriteHeight, GameConstants.horizontalMazeBlocks * spriteWidth + PADDING, spriteHeight + PADDING);
            mazeParts.Add(bottomWall);
            //right wall
            Rectangle rightWall = new Rectangle(GameConstants.horizontalMazeBlocks * spriteWidth, spriteHeight, spriteWidth + PADDING, (GameConstants.verticalMazeBlocks + 1) * spriteHeight + PADDING);
            mazeParts.Add(rightWall);
            //top wall
            Rectangle topWall = new Rectangle(0, spriteHeight, GameConstants.horizontalMazeBlocks * spriteWidth + PADDING, spriteHeight + PADDING);
            mazeParts.Add(topWall);
            
            
            //quarter 1
            Rectangle rect_1_1 = new Rectangle(2 * spriteWidth, 3 * spriteHeight, 5 * spriteWidth, 1 * spriteHeight);
            mazeParts.Add(rect_1_1);
            Rectangle rect_1_2 = new Rectangle(2 * spriteWidth, 5 * spriteHeight, 2 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_1_2);
            Rectangle rect_1_3 = new Rectangle(2 * spriteWidth, 8 * spriteHeight, 5 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_1_3);
            Rectangle rect_1_4 = new Rectangle(5 * spriteWidth, 5 * spriteHeight, 2 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_1_4);
            Rectangle rect_1_5 = new Rectangle(8 * spriteWidth, 3 * spriteHeight, 4 * spriteWidth, 3 * spriteHeight);
            mazeParts.Add(rect_1_5);
            Rectangle rect_1_6 = new Rectangle(8 * spriteWidth, 7 * spriteHeight, 4 * spriteWidth, 3 * spriteHeight);
            mazeParts.Add(rect_1_6);
            //Top Left
            intersections.Add(new Rectangle(rect_1_1.X - smallOffset, rect_1_1.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_1_2.X - smallOffset, rect_1_2.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_1_3.X - smallOffset, rect_1_3.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_1_4.X - smallOffset, rect_1_4.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_1_5.X - smallOffset, rect_1_5.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_1_6.X - smallOffset, rect_1_6.Y - smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_1_5.X - smallOffset, rect_1_4.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_1_4.X - smallOffset, rect_1_3.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_1_6.X - smallOffset, rect_1_3.Y - smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_1_6.X - smallOffset, rect_1_6.Y + rect_1_6.Height + smallOffset, intersectionSize, intersectionSize));

            ////quarter 2
            Rectangle rect_2_1 = new Rectangle(13 * spriteWidth, 3 * spriteHeight, 4 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_2_1);
            Rectangle rect_2_2 = new Rectangle(13 * spriteWidth, 6 * spriteHeight, 9 * spriteWidth, 1 * spriteHeight);
            mazeParts.Add(rect_2_2);
            Rectangle rect_2_3 = new Rectangle(13 * spriteWidth, 8 * spriteHeight, 2 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_2_3);
            Rectangle rect_2_4 = new Rectangle(16 * spriteWidth, 7 * spriteHeight, 3 * spriteWidth, 3 * spriteHeight);
            mazeParts.Add(rect_2_4);
            Rectangle rect_2_5 = new Rectangle(18 * spriteWidth, 3 * spriteHeight, 4 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_2_5);
            Rectangle rect_2_6 = new Rectangle(20 * spriteWidth, 8 * spriteHeight, 2 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_2_6);
            intersections.Add(new Rectangle(rect_2_1.X - smallOffset, rect_2_1.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_2.X - smallOffset, rect_2_2.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_3.X - smallOffset, rect_2_3.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_5.X - smallOffset, rect_2_5.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_6.X - smallOffset, rect_2_6.Y - smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_2_2.X - smallOffset, rect_2_2.Y + smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_4.X - smallOffset, rect_2_3.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_5.X - smallOffset, rect_2_2.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_5.X + rect_2_5.Width + smallOffset, rect_2_5.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_5.X + rect_2_5.Width + smallOffset, rect_2_2.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_6.X + rect_2_6.Width + smallOffset, rect_2_6.Y - smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_2_2.X + rect_2_2.Width + smallOffset, rect_2_2.Y + rect_2_2.Height + smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_2_4.X - smallOffset, rect_2_4.Y + rect_2_4.Height + smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_2_6.X - smallOffset, rect_2_6.Y + rect_2_6.Height + smallOffset, intersectionSize, intersectionSize));

            
            ////quarter 3
            Rectangle rect_3_1 = new Rectangle(2 * spriteWidth, 11 * spriteHeight, 3 * spriteWidth, 3 * spriteHeight);
            mazeParts.Add(rect_3_1);
            Rectangle rect_3_2 = new Rectangle(2 * spriteWidth, 15 * spriteHeight, 3 * spriteWidth, 3 * spriteHeight);
            mazeParts.Add(rect_3_2);
            Rectangle rect_3_3 = new Rectangle(6 * spriteWidth, 11 * spriteHeight, 6 * spriteWidth, 1 * spriteHeight);
            mazeParts.Add(rect_3_3);
            Rectangle rect_3_4 = new Rectangle(6 * spriteWidth, 13 * spriteHeight, 3 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_3_4);
            Rectangle rect_3_5 = new Rectangle(6 * spriteWidth, 16 * spriteHeight, 6 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_3_5);
            Rectangle rect_3_6 = new Rectangle(10 * spriteWidth, 13 * spriteHeight, 2 * spriteWidth, 2 * spriteHeight);
            mazeParts.Add(rect_3_6);
            intersections.Add(new Rectangle(rect_3_1.X - smallOffset, rect_3_1.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_2.X - smallOffset, rect_3_2.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_3.X - smallOffset, rect_3_3.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_4.X - smallOffset, rect_3_4.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_5.X - smallOffset, rect_3_5.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_6.X - smallOffset, rect_3_6.Y - smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_3_4.X - smallOffset, rect_3_2.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_5.X - smallOffset, rect_3_5.Y + rect_3_5.Height + smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_6.X - smallOffset, rect_3_3.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_6.X - smallOffset, rect_3_5.Y - smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_3_4.X - smallOffset, rect_3_4.Y + rect_3_4.Height + smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_6.X - smallOffset, rect_3_4.Y + rect_3_4.Height + smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_3_2.X - smallOffset, rect_3_2.Y + rect_3_2.Height + smallOffset, intersectionSize, intersectionSize));

            //Center
            intersections.Add(new Rectangle(rect_3_3.X + rect_3_3.Width + smallOffset, rect_3_6.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_3_5.X + rect_3_5.Width + smallOffset, rect_3_5.Y - smallOffset, intersectionSize, intersectionSize));

            ////quarter 4
            Rectangle rect_4_1 = new Rectangle(13 * spriteWidth, 11 * spriteHeight, 4 * spriteWidth, 3 * spriteHeight);
            mazeParts.Add(rect_4_1);
            Rectangle rect_4_2 = new Rectangle(13 * spriteWidth, 15 * spriteHeight, 4 * spriteWidth, 3 * spriteHeight);
            mazeParts.Add(rect_4_2);
            Rectangle rect_4_3 = new Rectangle(18 * spriteWidth, 11 * spriteHeight, 4 * spriteWidth, 3 * spriteHeight);
            mazeParts.Add(rect_4_3);
            Rectangle rect_4_4 = new Rectangle(18 * spriteWidth, 15 * spriteHeight, 4 * spriteWidth, 3 * spriteHeight);
            mazeParts.Add(rect_4_4);
            intersections.Add(new Rectangle(rect_4_1.X - smallOffset, rect_4_1.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_4_2.X - smallOffset, rect_4_2.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_4_3.X - smallOffset, rect_4_3.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_4_4.X - smallOffset, rect_4_4.Y - smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_4_2.X - smallOffset, rect_4_2.Y + rect_4_2.Height + smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_4_2.X - smallOffset, rect_4_2.Y + rect_4_2.Height + smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_4_2.X - smallOffset, rect_4_2.Y + rect_4_2.Height + smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_4_4.X - smallOffset, rect_4_2.Y + rect_4_2.Height + smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_4_4.X + rect_4_4.Width + smallOffset, rect_4_4.Y + rect_4_4.Height + smallOffset, intersectionSize, intersectionSize));

            intersections.Add(new Rectangle(rect_4_4.X + rect_4_4.Width + smallOffset, rect_4_4.Y - smallOffset, intersectionSize, intersectionSize));
            intersections.Add(new Rectangle(rect_4_3.X + rect_4_3.Width + smallOffset, rect_4_3.Y - smallOffset, intersectionSize, intersectionSize)); 

            

        }

    }
}
