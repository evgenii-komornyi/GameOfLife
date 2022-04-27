namespace GameOfLifeEngine
{
    /// <summary>
    /// Class contains all logic and fields 
    /// that required description, provided from 
    /// https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
    /// </summary>
    [Serializable]
    public class GameEngine
    {
        public bool[,] GameField;
        private readonly int _density = 2;
        public uint CurrentGeneration { get; private set; }

        /// <summary>
        /// Class contains all logic and fields
        /// that required description, provided from 
        /// https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life        
        /// </summary>
        /// <param name="rows">Count of the rows.</param>
        /// <param name="columns">Count of the columns.</param>
        public GameEngine(int rows, int columns)
        {
             GameField = new bool[columns, rows];
        }

        /// <summary>
        /// Method initializes game data.
        /// </summary>
        public void InitializeData()
        {
            Random random = new Random();
            for (int currentColumn = 0; currentColumn < GameField.GetLength(0); currentColumn++)
            {
                for (int currentRow = 0; currentRow < GameField.GetLength(1); currentRow++)
                {
                    GameField[currentColumn, currentRow] = random.Next(_density) == 0;
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
                    // Actual column is mapped so that is not outside of the game field.
                    int actualColumn = (currentColumn + columnOffset + GameField.GetLength(0)) % GameField.GetLength(0);

                    // Actual row is mapped so that is not outside of the game field.
                    int actualRow = (currentRow + rowOffset + GameField.GetLength(1)) % GameField.GetLength(1);

                    count += GameField[actualColumn, actualRow] ? 1 : 0;
                }
            }

            count -= GameField[currentColumn, currentRow] ? 1 : 0;

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

            for (int currentColumn = 0; currentColumn < GameField.GetLength(0); currentColumn++)
            {
                for (int currentRow = 0; currentRow < GameField.GetLength(1); currentRow++)
                {
                    aliveCells += GameField[currentColumn, currentRow] ? 1 : 0;
                }
            }

            return aliveCells;
        }

        /// <summary>
        /// Method calculates next generation of the cells.
        /// </summary>
        public void NextGeneration()
        {
            var newField = new bool[GameField.GetLength(0), GameField.GetLength(1)];
            
            for (int currentColumn = 0; currentColumn < GameField.GetLength(0); currentColumn++)
            {
                for (int currentRow = 0; currentRow < GameField.GetLength(1); currentRow++)
                {
                    var neighboursCount = CountNeighbours(currentColumn, currentRow);

                    if (!GameField[currentColumn, currentRow] && neighboursCount == 3)
                    {
                        newField[currentColumn, currentRow] = true;
                    } else if (GameField[currentColumn, currentRow] && (neighboursCount < 2 || neighboursCount > 3))
                    {
                        newField[currentColumn, currentRow] = false;
                    } else
                    {
                        newField[currentColumn, currentRow] = GameField[currentColumn, currentRow];
                    }
                }
            }

            GameField = newField;
            CurrentGeneration++;
        }
    }
}