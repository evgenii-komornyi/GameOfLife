using GameOfLifeEngine;

namespace UI
{
    /// <summary>
    /// The class is contained all settings 
    /// to start a game from UI.
    /// </summary>
    public class UIController
    {
        private bool _isCursorVisible = true;
        
        /// <summary>
        /// Method is cantained all methods 
        /// that needed to start a game.
        /// </summary>
        public void start()
        {
            Greatings();
            StartMenu();
        }

        /// <summary>
        /// Simple menu to commmunicate with user.
        /// </summary>
        private void StartMenu()
        {
            bool exitMainMenu = false;

            while (!exitMainMenu)
            {
                ReadMainMenuCommands();
               
                Console.Write(StringsDictionary.TypeCommand);
                string commandForMainMenu = Console.ReadLine();

                switch (commandForMainMenu)
                {
                    case StringsDictionary.ExitCommandNumber:
                    case StringsDictionary.ExitCommandText:
                        exitMainMenu = true;
                        break;
                    case StringsDictionary.NewGameCommandNumber:
                    case StringsDictionary.NewGameCommandText:
                        GameEngine newGame = ConfigureGame();
                        if (newGame != null)
                        {
                            RenderGame(newGame);
                        }
                        else
                        {
                            Console.WriteLine(StringsDictionary.RenderNewGameError);
                        }
                        break;
                    case StringsDictionary.ClearConsoleCommandNumber:
                    case StringsDictionary.ClearConsoleCommandText:
                        Console.Clear();
                        break;
                    case StringsDictionary.ShowHideCursorCommand:
                        _isCursorVisible = !_isCursorVisible;
                        Settings(_isCursorVisible);
                        break;
                    case StringsDictionary.HelpCommandSign:
                    case StringsDictionary.HelpCommandText:
                        Console.WriteLine(StringsDictionary.HelpDescriptionExit);
                        Console.WriteLine(StringsDictionary.HelpDesctiptionNewGame);
                        Console.WriteLine(StringsDictionary.HelpDesctiptionClearConsole);
                        Console.WriteLine(StringsDictionary.HelpDesctiptionShowHideCursor);
                        break;
                    default:
                        Console.WriteLine(StringsDictionary.UnknownCommand);
                        break;
                }
            }
        }

        /// <summary>
        /// UI's settings.
        /// </summary>
        /// <param name="isCursorVisible">
        /// Sets the visibility
        /// of the cursor in the console.
        /// </param>
        private void Settings(bool isCursorVisible)
        {
            Console.CursorVisible = isCursorVisible;
        }

        /// <summary>
        /// Method reads file with ASCII game's name.
        /// </summary>
        private void Greatings()
        {
            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory + StringsDictionary.GreatingFileName}.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine(StringsDictionary.FileNotExistError);
            }
            else
            {
                var fileContent = File.ReadAllText(filePath);

                Console.WriteLine(fileContent);
            }
        }

        /// <summary>
        /// Method reads file with main menu's commands.
        /// </summary>
        private void ReadMainMenuCommands()
        {
            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory + StringsDictionary.MainMenuCommandsFileName}.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine(StringsDictionary.FileNotExistError);
            }
            else
            {
                var fileContent = File.ReadAllText(filePath);

                Console.WriteLine(fileContent);
            }
        }

        /// <summary>
        /// Method configures a game, using user's input.
        /// </summary>
        private GameEngine ConfigureGame()
        {
            Console.WriteLine(StringsDictionary.OnConfigMessage);

            Console.Write(StringsDictionary.RowsCountConfigMessage);
            int rowsCount;
            string rowsCountInput = Console.ReadLine();
            bool isRowsNumber = int.TryParse(rowsCountInput, out rowsCount);

            if (!isRowsNumber)
            {
                Console.WriteLine(StringsDictionary.InputFormatError);

                return null;
            }

            Console.Write(StringsDictionary.ColumnsCountConfigMessage);
            int columnsCount;
            string columnsCountInput = Console.ReadLine();
            bool isColumnsNumber = int.TryParse(columnsCountInput, out columnsCount);

            if (!isColumnsNumber)
            {
                Console.WriteLine(StringsDictionary.InputFormatError);

                return null;
            }

            if ((rowsCount < 20 || rowsCount > 50) && (columnsCount < 20 || columnsCount > 260))
            {
                Console.WriteLine(StringsDictionary.InputOutOfRangeError);
                
                return null;
            }        

            GameEngine gameEngine = new GameEngine(rowsCount, columnsCount);
            
            return gameEngine;
        }

        /// <summary>
        /// Method draws field and cells on it, changing every second.
        /// </summary>
        /// <param name="gameEngine">Game's core.</param>
        private void RenderGame(GameEngine gameEngine)
        {
            Console.SetCursorPosition(0, 0);
            while (!Console.KeyAvailable)
            {
                var currentGeneration = gameEngine.GetCurrentGeneration();

                for (int currentRow = 0; currentRow < currentGeneration.GetLength(1); currentRow++)
                {
                    var aliveDeadSymbols = new char[currentGeneration.GetLength(0)];

                    for (int currentColumn = 0; currentColumn < currentGeneration.GetLength(0); currentColumn++)
                    {
                        if (currentGeneration[currentColumn, currentRow])
                        {
                            aliveDeadSymbols[currentColumn] = '#';
                        }
                        else
                        {
                            aliveDeadSymbols[currentColumn] = ' ';
                        }
                    }
                    Console.WriteLine(aliveDeadSymbols);
                }
                Console.SetCursorPosition(0, 0);
                gameEngine.NextGeneration();
            }
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}