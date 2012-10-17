using System.Collections.Generic;
namespace PacMan
{
    //public enum Direction
    //{
    //    Right = 0,
    //    Down = 1,
    //    Left = 2,
    //    Up = 3,
    //}
    public static class Direction
    {
        public const int Right = 0;
        public const int Down = 1;
        public const int Left = 2;
        public const int Up = 3;
        public const int numberOfDirection = 4;

        public static List<int> GetAllDirections()
        {
            List<int> returnList = new List<int>();
            returnList.Add(Right);
            returnList.Add(Down);
            returnList.Add(Left);
            returnList.Add(Up);
            return (returnList);
        }

        public static List<int> GetOtherDirections(int currentDirection)
        {
            List<int> returnList = new List<int>();
            returnList = GetAllDirections();
            foreach (int direction in returnList)
            {
                if (direction == currentDirection)
                {
                    returnList.Remove(direction);
                    break;
                }
            }
            return (returnList);
        }

        public static int GetOppositeDirection(int currentDirection)
        {
            return ((currentDirection + 2) % numberOfDirection);
        }
    }
}