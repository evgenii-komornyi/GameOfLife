﻿namespace GameOfLifeEngine
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
        private readonly int _cols;
        private readonly int _density = 2;

        /// <summary>
        /// Constructor creates based-on user input universe
        /// and fills this universe randomly.
        /// </summary>
        /// <param name="rows">Count of the rows.</param>
        /// <param name="cols">Count of the cols.</param>
        /// <param name="density">Count of density.</param>
        public GameEngine(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _field = new bool[cols, rows];
        }

        public void Generate()
        {
            Random random = new Random();
            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    _field[x, y] = random.Next(_density) == 0;
                }
            }
        }
        
        public void LoadGame(bool[,] loadedGeneration)
        {
            _field = loadedGeneration;
        }
        /// <summary>
        /// Method calculates how many neighbors are near current cell.
        /// </summary>
        /// <param name="x">Current col number.</param>
        /// <param name="y">Current row number.</param>
        /// <returns>Neighbour count.</returns>
        private int CountNeighbours(int x, int y)
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + _cols) % _cols; // if last col, then will first
                    int row = (y + j + _rows) % _rows; // if last row, then will first

                    bool isSelfChecking = col == x && row == y;

                    if (HasLife(col, row) && !isSelfChecking)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Method returns true if universe has at least
        /// one life cell.
        /// </summary>
        /// <param name="col">Current col.</param>
        /// <param name="row">Current row.</param>
        /// <returns>The existence of a living cell.</returns>
        private bool HasLife(int col, int row)
        {
            return _field[col, row];
        }

        /// <summary>
        /// Method calculates the count
        /// of alive cells.
        /// </summary>
        /// <returns>Alive cells count.</returns>
        public int CountAliveCells()
        {
            int aliveCells = 0;

            for (int x = 0; x < _cols; x++)
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
            var newField = new bool[_cols, _rows];
            
            for (int x = 0; x < _cols; x++)
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
            bool[,] generation = new bool[_cols, _rows];
            for (int x = 0; x < _cols; x++)
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