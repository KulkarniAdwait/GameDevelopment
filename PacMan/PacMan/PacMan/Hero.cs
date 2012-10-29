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
    public class Hero : Microsoft.Xna.Framework.GameComponent
    {
        Texture2D pacmanDead;
        Texture2D texture;
        SpriteBatch spriteBatch;
        Vector2 origin;
        Vector2 position;

        private float timer = 0f;
        private float interval = 100f;
        private int currentFrame = 0;
        private int spriteWidth = 32;
        private int spriteHeight = 32;
        private readonly int totalFrames = 2;

        private readonly int STEP = 2;

        private int direction;
        private readonly int spriteStartX = 320;
        private readonly int spriteStartY = 0;

        public Rectangle rectangle;
        private Rectangle spriteFrame;
        private Rectangle collidedObject;
        private Collison collision;

        public Hero(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            spriteFrame = new Rectangle(spriteStartX, spriteStartY, spriteWidth, spriteHeight);
            ResetPosition();
            collision = new Collison(STEP, GameConstants.BlockWidth, 2 * GameConstants.BlockHeight
                , GameConstants.GameWidth - GameConstants.BlockWidth
                , GameConstants.GameHeight - GameConstants.BlockHeight);
        }

        public void ResetPosition()
        {
            position = new Vector2((int)12.5 * GameConstants.BlockWidth, (int)(18.5 * GameConstants.BlockHeight));
            direction = (int)Direction.Up;
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
            texture = Game.Content.Load<Texture2D>("GhostSprites");
            pacmanDead = Game.Content.Load<Texture2D>("Splat");
            // TODO: use this.Content to load your game content here
        }

        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, List<Rectangle> mazeParts)
        {
            // TODO: Add your update code here
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= interval)
            {
                spriteFrame = new Rectangle(spriteStartX + (currentFrame * spriteWidth), spriteStartY + (direction * spriteHeight), spriteWidth, spriteHeight);
                currentFrame++;
                currentFrame = currentFrame % totalFrames;
                timer = 0f;
            }

            origin = new Vector2(spriteFrame.Width / 2, spriteFrame.Height / 2);
            rectangle = new Rectangle((int)position.X - (spriteWidth / 2), (int)position.Y - (spriteWidth / 2), spriteWidth, spriteHeight);

            Move(mazeParts);

            base.Update(gameTime);
        }

        private void Move(List<Rectangle> mazeParts)
        {
            GamePadState gamePadStatePlayerOne = GamePad.GetState(PlayerIndex.One);

            if (Keyboard.GetState().IsKeyDown(Keys.Up) 
                || (gamePadStatePlayerOne.IsConnected && gamePadStatePlayerOne.DPad.Up == ButtonState.Pressed))
            {
                collidedObject = collision.CollidesInDirection(this.rectangle, mazeParts, Direction.Up);
                if (collidedObject == Rectangle.Empty)
                {
                    this.MoveUp();
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down)
                || (gamePadStatePlayerOne.IsConnected && gamePadStatePlayerOne.DPad.Down == ButtonState.Pressed))
            {
                collidedObject = collision.CollidesInDirection(this.rectangle, mazeParts, Direction.Down);
                if (collidedObject == Rectangle.Empty)
                {
                    this.MoveDown();
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)
                || (gamePadStatePlayerOne.IsConnected && gamePadStatePlayerOne.DPad.Left == ButtonState.Pressed))
            {
                collidedObject = collision.CollidesInDirection(this.rectangle, mazeParts, Direction.Left);
                if (collidedObject == Rectangle.Empty)
                {
                    this.MoveLeft();
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right)
                || (gamePadStatePlayerOne.IsConnected && gamePadStatePlayerOne.DPad.Right == ButtonState.Pressed))
            {
                collidedObject = collision.CollidesInDirection(this.rectangle, mazeParts, Direction.Right);
                if (collidedObject == Rectangle.Empty)
                {
                    this.MoveRight();
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, spriteFrame, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public void DeathAnimation(GameTime gametime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(pacmanDead, new Rectangle(rectangle.X, rectangle.Y, 32, 32), Color.White);
            spriteBatch.End();
        }
        public void MoveUp()
        {
            direction = (int)Direction.Up;
            position.Y -= STEP;
        }
        public void MoveDown()
        {
            direction = (int)Direction.Down;
            position.Y += STEP;
        }
        public void MoveLeft()
        {
            direction = (int)Direction.Left;
            position.X -= STEP;
        }
        public void MoveRight()
        {
            direction = (int)Direction.Right;
            position.X += STEP;
        }
    }
}
