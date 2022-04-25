namespace GameOfLifeEngine
{
    /// <summary>
    /// The class is contained all logic and fields 
    /// that required description, provided from 
    /// https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
    /// </summary>
    public class GameEngine
    {
        private bool[,] _field;
        private readonly int _rows;
        private readonly int _columns;
        private readonly int _density = 2;

        /// <summary>
        /// Constructor creates based-on user input universe
        /// and fills this universe randomly.
        /// </summary>
        /// <param name="rows">Count of the rows.</param>
        /// <param name="columns">Count of the columns.</param>
        public GameEngine(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _field = new bool[columns, rows];

            Random random = new Random();
            for (int currentColumn = 0; currentColumn < columns; currentColumn++)
            {
                for (int currentRow = 0; currentRow < rows; currentRow++)
                {
                    _field[currentColumn, currentRow] = random.Next(_density) == 0;
                }
            }
        }

        /// <summary>
        /// Method calculates how many neighbors are near current cell.
        /// </summary>
        /// <param name="currentColumn">Current column number.</param>
        /// <param name="currentRow">Current row number.</param>
        /// <returns>Neighbour count.</returns>
        private int CountNeighbours(int currentColumn, int currentRow)
        {
            int count = 0;

            for (int columnOffset = -1; columnOffset < 2; columnOffset++)
            {
                for (int rowOffset = -1; rowOffset < 2; rowOffset++)
                {
                    //
                    int actualColumn = (currentColumn + columnOffset + _columns) % _columns;
                    
                    //
                    int actualRow = (currentRow + rowOffset + _rows) % _rows;

                    if (_field[actualColumn, actualRow])
                    {
                        count++;
                    }
                }
            }

            count -= _field[currentColumn, currentRow] ? 1 : 0;

            return count;
        }

        /// <summary>
        /// Method calculates the count
        /// of alive cells.
        /// </summary>
        /// <returns>Alive cells count.</returns>
        public int CountAliveCells()
        {
            int aliveCells = 0;

            for (int x = 0; x < _columns; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    if (HasLife(x, y))
                    {
                        aliveCells++;
                    }
                }
            }

            return aliveCells;
        }

        /// <summary>
        /// Method calculates next generation of the cells every second.
        /// </summary>
        public void NextGeneration()
        {
            var newField = new bool[_columns, _rows];
            
            for (int x = 0; x < _columns; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    var neighboursCount = CountNeighbours(x, y);
                    var hasLife = HasLife(x, y);

                    if (!hasLife && neighboursCount == 3)
                    {
                        newField[x, y] = true;
                    } else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))
                    {
                        newField[x, y] = false;
                    } else
                    {
                        newField[x, y] = _field[x, y];
                    }
                }
            }
            _field = newField;
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Method calculates current generation.
        /// </summary>
        /// <returns>Current generation.</returns>
        public bool[,] GetCurrentGeneration()
        {
            bool[,] generation = new bool[_columns, _rows];
            for (int x = 0; x < _columns; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    generation[x, y] = _field[x, y];
                }
            }
            return generation;
        }
    }
}