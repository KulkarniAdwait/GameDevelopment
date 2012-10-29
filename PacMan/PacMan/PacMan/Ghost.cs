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
    public enum GhostType
    {
        Normal,
        Edible,
    }
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ghost : Microsoft.Xna.Framework.GameComponent
    {
        Effect colorEffect;
        Texture2D texture;
        SpriteBatch spriteBatch;
        Vector2 position;
        Vector2 origin;

        private float timer = 0f;
        private float interval = 100f;
        private int currentFrame = 0;
        private int spriteWidth = 32;
        private int spriteHeight = 32;
        private readonly int totalFrames = 2;

        private readonly int STEP = 1;
        private bool intersectionHit = false;
        private readonly int ghostNumber;
        public int direction;

        private readonly int spriteStartY = 0;

        public Rectangle rectangle;
        private Rectangle spriteFrame;
        private Rectangle collidedObject;
        private Rectangle futurePath;
        private Collison collision;
        private List<int> legalDirections;
        public GhostType ghostType;

        private List<Color> ghostColors;

        public Ghost(Game game, int ghostNumber)
            : base(game)
        {
            // TODO: Construct any child components here
            if (ghostNumber < 0 || ghostNumber > 4)
            {
                throw new Exception("Incorrect Ghost number");
            }
            this.ghostNumber = ghostNumber; ;
            InitialGhostPositions();
            collision = new Collison(STEP, GameConstants.BlockWidth, 2 * GameConstants.BlockHeight
                , GameConstants.GameWidth - GameConstants.BlockWidth
                , GameConstants.GameHeight - GameConstants.BlockHeight);
            legalDirections = new List<int>();
            ghostColors = new List<Color>();
            ghostColors.Add(Color.Blue);
            ghostColors.Add(Color.Green);
            ghostColors.Add(Color.Red);
            ghostColors.Add(Color.Yellow);
            ghostColors.Add(Color.Pink);
            ghostColors.Add(Color.Black);

        }

        private void InitialGhostPositions()
        {
            position = new Vector2((GameConstants.BlockWidth * 12) + (GameConstants.BlockHeight / 2), (GameConstants.BlockHeight * 0) - (GameConstants.BlockHeight * ghostNumber));
            direction = 1;
        }

        public void ResetGhost()
        {
            position = new Vector2((GameConstants.BlockWidth * 12) + (GameConstants.BlockHeight / 2), (GameConstants.BlockHeight * 0) - (GameConstants.BlockHeight * ghostNumber));
            direction = 1;
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
            // TODO: use this.Content to load your game content here
            colorEffect = Game.Content.Load<Effect>("Colorize");
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, List<Rectangle> mazeParts, List<Rectangle> intersections, List<Rectangle> trapDoors, GhostType ghostType)
        {
            // TODO: Add your update code here
            this.ghostType = ghostType;
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= interval)
            {
                //if (ghostType == GhostType.Normal)
                //    spriteFrame = new Rectangle((2 * ghostNumber * spriteWidth) + (currentFrame * spriteWidth)
                //                        , spriteStartY + (direction * spriteHeight)
                //                        , spriteWidth, spriteHeight);
                //else
                //    spriteFrame = new Rectangle((12 * spriteWidth) + (currentFrame * spriteWidth)
                //                        , spriteStartY, spriteWidth, spriteHeight);

                spriteFrame = new Rectangle((currentFrame * spriteWidth)
                                       , spriteStartY + (direction * spriteHeight)
                                       , spriteWidth, spriteHeight);

                currentFrame++;
                currentFrame = currentFrame % totalFrames;
                timer = 0f;
            }

            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            rectangle = new Rectangle((int)position.X - (spriteWidth / 2), (int)position.Y - (spriteWidth / 2), spriteWidth, spriteHeight);
            Animate(mazeParts, intersections, trapDoors);
            base.Update(gameTime);
        }


        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            //spriteBatch.Begin();
            //spriteBatch.Draw(texture, position, spriteFrame, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            //spriteBatch.End();

            if (this.ghostType == GhostType.Edible)
            {
                colorEffect.Parameters["DestColor"].SetValue(ghostColors[ghostNumber].ToVector4());
            }
            else
            {
                colorEffect.Parameters["DestColor"].SetValue(ghostColors[ghostColors.Count - 1].ToVector4());
            }
            spriteBatch.Begin(0, null, null, null, null, colorEffect);

            spriteBatch.Draw(texture, position, spriteFrame, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);

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

        bool trapDoorHit = false;
        Rectangle rectTrapDoor;
        Rectangle currentIntersection = Rectangle.Empty;
        private void Animate(List<Rectangle> mazeParts, List<Rectangle> intersections, List<Rectangle> trapDoors)
        {
            Random rand = new Random();

            foreach (Rectangle intersection in intersections)
            {
                if (!intersectionHit && !trapDoorHit && this.rectangle.Intersects(intersection))
                {
                    intersectionHit = true;
                    currentIntersection = intersection;
                    if (!trapDoorHit)
                    {
                        foreach (Rectangle trapDoor in trapDoors)
                        {
                            if (this.rectangle.Intersects(trapDoor))
                            {
                                trapDoorHit = true;
                                break;
                            }
                        }
                    }
                    if (trapDoorHit)
                    {
                        int newLocation = rand.Next(0, trapDoors.Count);
                        position.X = trapDoors[newLocation].X + (trapDoors[newLocation].Width / 2);
                        position.Y = trapDoors[newLocation].Y + (trapDoors[newLocation].Height / 2);
                        rectTrapDoor = trapDoors[newLocation];
                        legalDirections = GetLegalMoves(mazeParts, new Rectangle(trapDoors[newLocation].X + (trapDoors[newLocation].Width / 2)
                            , trapDoors[newLocation].Y + (trapDoors[newLocation].Height / 2), 1, 1), this.direction);
                        //legalDirections.Remove(Direction.GetOppositeDirection(this.direction));
                    }
                    else
                    {
                        position.X = intersection.X;
                        position.Y = intersection.Y;
                        legalDirections = GetLegalMoves(mazeParts, intersection, this.direction);
                        legalDirections.Remove(Direction.GetOppositeDirection(this.direction));
                    }

                    if (legalDirections.Count > 0)
                    {
                        int newDirection = rand.Next(0, legalDirections.Count);
                        this.direction = legalDirections[newDirection];
                    }
                }
                else if (currentIntersection != Rectangle.Empty && this.rectangle.Intersects(currentIntersection) == false)
                {
                    intersectionHit = false;
                    currentIntersection = Rectangle.Empty;
                }
                else if (trapDoorHit)
                {
                    //dont intersect until new trap door has been cleared
                    if (!this.rectangle.Intersects(rectTrapDoor))
                    {
                        trapDoorHit = false;
                        intersectionHit = false;
                        currentIntersection = Rectangle.Empty;
                    }
                }
            }

            switch (this.direction)
            {
                case (int)Direction.Up: { MoveUp(); break; }
                case (int)Direction.Down: { MoveDown(); break; }
                case (int)Direction.Left: { MoveLeft(); break; }
                case (int)Direction.Right: { MoveRight(); break; }
            }
        }

        private List<int> GetLegalMoves(List<Rectangle> mazeParts, Rectangle intersection, int currentDirection)
        {
            List<int> legalDirections = new List<int>();
            List<int> directions = Direction.GetAllDirections();
            foreach (int direction in directions)
            {
                futurePath = MoveAheadPosition(intersection, direction);
                collidedObject = collision.Collides(futurePath, mazeParts);
                if (collidedObject == Rectangle.Empty)
                {
                    legalDirections.Add(direction);
                }
            }
            return legalDirections;
        }

        private Rectangle MoveAheadPosition(Rectangle current, int direction)
        {
            Rectangle rectangle = Rectangle.Empty;
            switch (direction)
            {
                case Direction.Up:
                    {
                        rectangle = new Rectangle(current.X, current.Y, 1, 1 * GameConstants.BlockWidth);
                        rectangle.Y = rectangle.Y - ((int)1.0 * GameConstants.BlockWidth);
                        break;
                    }
                case Direction.Down:
                    {
                        rectangle = new Rectangle(current.X, current.Y, 1, 1 * GameConstants.BlockWidth);
                        break;
                    }
                case Direction.Left:
                    {
                        rectangle = new Rectangle(current.X, current.Y, 1 * GameConstants.BlockWidth, 1);
                        rectangle.X = rectangle.X - ((int)1.0 * GameConstants.BlockWidth);
                        break;
                    }
                case Direction.Right:
                    {
                        rectangle = new Rectangle(current.X, current.Y, 1 * GameConstants.BlockWidth, 1);
                        break;
                    }
            }
            return rectangle;
        }
    }
}
