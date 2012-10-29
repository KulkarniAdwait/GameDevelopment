namespace PacMan
{
    public static class GameConstants
    {
        public static readonly int BlockWidth = 40;
        public static readonly int BlockHeight = 40;
        public static readonly int verticalMazeBlocks = 17;
        public static readonly int horizontalMazeBlocks = 23;
        public static readonly int GameWidth = BlockWidth * (horizontalMazeBlocks + 1);
        public static readonly int GameHeight = BlockHeight * (verticalMazeBlocks + 3);
        public static readonly int Lives = 3;

    }
}