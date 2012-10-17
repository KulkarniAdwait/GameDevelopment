using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PacMan
{
    public class Collison
    {
        private int OFFSET;
        private int MinX;
        private int MinY;
        private int MaxX;
        private int MaxY;
        public Collison(int offset, int minX, int minY, int MaxX, int MaxY)
        {
            this.OFFSET = offset;
            this.MinX = minX;
            this.MinY = minY;
            this.MaxX = MaxX;
            this.MaxY = MaxY;
        }

        public Rectangle Collides(Rectangle source, List<Rectangle> objects)
        {
            foreach (Rectangle mazePart in objects)
            {
                if (mazePart.Intersects(source))
                {
                    return mazePart;
                }
            }
            return Rectangle.Empty;
        }

        public Rectangle CollidesInDirection(Rectangle source, List<Rectangle> objects, int direction)
        {
            //if (source.Y - OFFSET <= MinY || source.Y + source.Height + OFFSET >= MaxY || source.X - OFFSET <= MinX || source.X + source.Width + OFFSET >= MaxX)
            //{
            //    return new Rectangle(source.X, source.Y, 1, 1);
            //}
            foreach (Rectangle mazePart in objects)
            {
                if (mazePart.Intersects(source))
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            {
                                if (CheckAbove(source, mazePart))
                                {
                                    return mazePart;
                                }
                                break;
                            }
                        case Direction.Down:
                            {
                                if (CheckBelow(source, mazePart))
                                {
                                    return mazePart;
                                }
                                break;
                            }
                        case Direction.Left:
                            {
                                if (CheckLeft(source, mazePart))
                                {
                                    return mazePart;
                                }
                                break;
                            }
                        case Direction.Right:
                            {
                                if (CheckRight(source, mazePart))
                                {
                                    return mazePart;
                                }
                                break;
                            }
                    }
                }
            }
            return Rectangle.Empty;
        }

        private Boolean CheckAbove(Rectangle rectangle, Rectangle mazePart)
        {
            if (mazePart.Y + mazePart.Height - OFFSET <= rectangle.Y)
                return true;
            return false;
        }
        private Boolean CheckBelow(Rectangle rectangle, Rectangle mazePart)
        {
            if (mazePart.Y + OFFSET >= rectangle.Y + rectangle.Height)
                return true;
            return false;
        }
        private Boolean CheckLeft(Rectangle rectangle, Rectangle mazePart)
        {
            if (mazePart.X + mazePart.Width - OFFSET <= rectangle.X)
                return true;
            return false;
        }
        private Boolean CheckRight(Rectangle rectangle, Rectangle mazePart)
        {
            if (mazePart.X + OFFSET >= rectangle.X + rectangle.Width)
                return true;
            return false;
        }
    }
}
