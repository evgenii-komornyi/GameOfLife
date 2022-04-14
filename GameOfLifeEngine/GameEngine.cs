namespace GameOfLifeEngine
{
    public class GameEngine
    {
        private bool[,] _universe;
        private readonly int _rows;
        private readonly int _cols;

        public GameEngine(int rows, int cols, int density)
        {
            _rows = rows;
            _cols = cols;
            _universe = new bool[rows, cols];

            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    _universe[x, y] = random.Next(density) == 0;
                }
            }
        }

        public bool[,] GetCurrentGeneration()
        {
            bool[,] generation = new bool[_cols, _rows];
            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    generation[x, y] = _universe[x, y];
                }
            }
            return generation;
        }
    }
}