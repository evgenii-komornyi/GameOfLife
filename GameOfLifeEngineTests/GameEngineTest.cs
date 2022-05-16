using GameOfLifeEngine;
using Xunit;

namespace GameOfLifeEngineTests
{
    public class GameEngineTest
    {
        private const int RowsCount = 5;
        private const int ColumnsCount = 5;
        private const int ExpectedLength = 25;
        private const int LifelessField = 0;
        private const int LifeField = 3;

        private GameEngine _gameEngine;

        public GameEngineTest()
        {
            _gameEngine = new GameEngine(RowsCount, ColumnsCount);
        }

        [Fact]
        public void CheckLengthOfArrayOnCreate()
        {
            var actual = _gameEngine.GameField.Length;

            Assert.Equal(ExpectedLength, actual);
        }

        [Fact]
        public void CheckAliveCells_ReturnsZero()
        {
            FillArrayWithAllDeadCells();

            var actual = _gameEngine.CountAliveCells();

            Assert.Equal(LifelessField, actual);
        }

        [Fact]
        public void CheckAliveCells_ReturnsThree()
        {
            CreateBlinkerPattern();

            var actual = _gameEngine.CountAliveCells();

            Assert.Equal(LifeField, actual);
        }

        [Fact]
        public void CheckNextGeneration_ReturnsNewArrayWithNextGeneration()
        {
            CreateBlinkerPattern();
            _gameEngine.NextGeneration();

            var actual = _gameEngine.GameField;
            
            var nextGeneration = NextBlinker();

            Assert.Equal(nextGeneration, actual);
        }

        private void FillArrayWithAllDeadCells()
        {
            for (int currentColumn = 0; currentColumn < ColumnsCount; currentColumn++)
            {
                for (int currentRow = 0; currentRow < RowsCount; currentRow++)
                {
                    _gameEngine.GameField[currentColumn, currentRow] = false; 
                }
            }
        }

        private void CreateBlinkerPattern()
        {
            _gameEngine.GameField[0, 0] = false;
            _gameEngine.GameField[0, 1] = false;
            _gameEngine.GameField[0, 2] = false;
            _gameEngine.GameField[0, 3] = false;
            _gameEngine.GameField[0, 4] = false;

            _gameEngine.GameField[1, 0] = false;
            _gameEngine.GameField[1, 1] = false;
            _gameEngine.GameField[1, 2] = false;
            _gameEngine.GameField[1, 3] = false;
            _gameEngine.GameField[1, 4] = false;

            _gameEngine.GameField[2, 0] = false;
            _gameEngine.GameField[2, 1] = true;
            _gameEngine.GameField[2, 2] = true;
            _gameEngine.GameField[2, 3] = true;
            _gameEngine.GameField[2, 4] = false;

            _gameEngine.GameField[3, 0] = false;
            _gameEngine.GameField[3, 1] = false;
            _gameEngine.GameField[3, 2] = false;
            _gameEngine.GameField[3, 3] = false;
            _gameEngine.GameField[3, 4] = false;

            _gameEngine.GameField[4, 0] = false;
            _gameEngine.GameField[4, 1] = false;
            _gameEngine.GameField[4, 2] = false;
            _gameEngine.GameField[4, 3] = false;
            _gameEngine.GameField[4, 4] = false;
        }

        private bool[,] NextBlinker()
        {
            bool[,] nextGeneration = new bool [RowsCount, ColumnsCount];

            nextGeneration[0, 0] = false;
            nextGeneration[0, 1] = false;
            nextGeneration[0, 2] = false;
            nextGeneration[0, 3] = false;
            nextGeneration[0, 4] = false;

            nextGeneration[1, 0] = false;
            nextGeneration[1, 1] = false;
            nextGeneration[1, 2] = true;
            nextGeneration[1, 3] = false;
            nextGeneration[1, 4] = false;

            nextGeneration[2, 0] = false;
            nextGeneration[2, 1] = false;
            nextGeneration[2, 2] = true;
            nextGeneration[2, 3] = false;
            nextGeneration[2, 4] = false;

            nextGeneration[3, 0] = false;
            nextGeneration[3, 1] = false;
            nextGeneration[3, 2] = true;
            nextGeneration[3, 3] = false;
            nextGeneration[3, 4] = false;

            nextGeneration[4, 0] = false;
            nextGeneration[4, 1] = false;
            nextGeneration[4, 2] = false;
            nextGeneration[4, 3] = false;
            nextGeneration[4, 4] = false;

            return nextGeneration; 
        }
    }
}