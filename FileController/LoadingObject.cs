using GameOfLifeEngine;

namespace Files
{
    /// <summary>
    /// Class contained properties,
    /// required to load from file.
    /// </summary>
    public class LoadingObject
    {
        public int RowsCount { get; set; }
        public int ColsCount { get; set; }
        public GameEngine GameCore { get; set; }
        public bool[,] GenerationToLoad { get; set; }
        public uint CurrentIterationCount { get; set; }
    }
}
