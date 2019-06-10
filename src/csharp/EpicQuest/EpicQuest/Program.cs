using EpicQuest.Manager;

namespace EpicQuest
{
    /// <summary>
    /// Program class for EpicQuest.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Program class 'entry' point.
        /// </summary>
        /// <param name="args">Input arguments from calls to this exe.</param>
        static void Main(string[] args)
        {
            //Create a new GameManager object and start the main game loop
            GameManager manager = new GameManager();
            manager.RunMainGameLoop();
        }
    }
}