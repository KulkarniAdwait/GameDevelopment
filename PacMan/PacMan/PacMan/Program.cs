using System;

namespace PacMan
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PacMan game = new PacMan())
            {
                game.Run();
            }
        }
    }
#endif
}

