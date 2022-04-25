using Files;
using GameOfLifeEngine;

namespace UI
{
    /// <summary>
    /// Bootstrapper class, contained 
    /// settings for starting UI.
    /// </summary>
    public class Bootstrapper
    {
        private bool _isCursorVisible = true;
        private bool[,] _currentGeneration;
        private uint _currentIteration;
        private FileController _fileController;

        /// <summary>
        /// Constructor for Dependency Injjection.
        /// </summary>
        /// <param name="fileController">
        /// File controller.
        /// </param>
        public Bootstrapper(FileController fileController)
        {
            this._fileController = fileController;
        }

        /// <summary>
        /// Method shows the main menu.
        /// </summary>
        public void StartMenu()
        {
            bool exitMainMenu = false;

            while (!exitMainMenu)
            {
                SetTitle(StringsDictionary.GameTitle);
                ClearConsole();
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
                        GameEngine newGame = ConfigureNewGame();
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
                        ClearConsole();
                        break;
                    case StringsDictionary.ShowHideCursorCommandNumber:
                        _isCursorVisible = !_isCursorVisible;
                        Settings(_isCursorVisible);
                        break;
                    case StringsDictionary.SaveGameCommandNumber:
                    case StringsDictionary.SaveGameCommandText:
                        if (_currentGeneration == null || _currentGeneration.Length == 0)
                        {
                            Console.WriteLine(StringsDictionary.OnSaveWithoutAnyGamesMessage);
                            continue;
                        }
                        SaveGame();
                        break;
                    case StringsDictionary.LoadGameCommandNumber:
                    case StringsDictionary.LoadGameCommandText:
                        GetDirectoryFiles();
                        LoadGame();
                        break;
                    case StringsDictionary.ResumeGameCommandNumber:
                    case StringsDictionary.ResumeGameCommandText:
                        if (_currentGeneration == null || _currentGeneration.Length == 0)
                        {
                            Console.WriteLine(StringsDictionary.OnResumeWithoutAnyGamesMessage);
                            continue;
                        }
                        ResumeGame();
                        break;
                    case StringsDictionary.HelpCommandSign:
                    case StringsDictionary.HelpCommandText:
                        GetHelpCommands();
                        break;
                    default:
                        Console.WriteLine(StringsDictionary.UnknownCommandMessage);
                        break;
                }
                Thread.Sleep(1000);
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
        public void Greatings()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + StringsDictionary.GreatingFileName;

            if (!File.Exists(filePath))
            {
                Console.WriteLine(StringsDictionary.FileNotExistError);
            }
            else
            {
                var fileContent = File.ReadAllText(filePath);

                Console.WriteLine(fileContent);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Method reads file with main menu's commands.
        /// </summary>
        private void ReadMainMenuCommands()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + StringsDictionary.MainMenuCommandsFileName;

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
        /// 
        /// </summary>
        private GameEngine ConfigureNewGame()
        {
            SetTitle(StringsDictionary.CreateGameTitle);

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
            gameEngine.Generate();
            _currentIteration = 0;

            return gameEngine;
        }

        /// <summary>
        /// Method for saving game into file.
        /// </summary>
        private void SaveGame()
        {
            SetTitle(StringsDictionary.SaveGameTitle);

            SavingObject objectToSave = new SavingObject();

            Console.Write(StringsDictionary.EnterFileNameMessage);
            string fileName = Console.ReadLine();
            fileName = fileName == null || fileName.Equals(StringsDictionary.EmptyString) ? StringsDictionary.DefaultFileName : fileName;

            objectToSave.CurrentGeneration = _currentGeneration;
            objectToSave.CurrentIterationCount = _currentIteration;

            _fileController.Write(objectToSave, fileName);
            Console.WriteLine(StringsDictionary.OnSuccessSavedFileMessage + fileName + StringsDictionary.SavingLoadingFilesFormat);
        }

        /// <summary>
        /// Method gets list of files into /games directory.
        /// </summary>
        private void GetDirectoryFiles()
        {
            Console.WriteLine(StringsDictionary.TreeDirectory);
            Console.WriteLine(StringsDictionary.TreeFiles);
            _fileController.GetDirectoryFiles();
            Console.WriteLine(StringsDictionary.Devider);
        }

        /// <summary>
        /// Method for loading game from file 
        /// into /games directory.
        /// </summary>
        private void LoadGame()
        {
            SetTitle(StringsDictionary.LoadGameTitle);

            Console.Write(StringsDictionary.EnterFileNameMessage);
            string fileNameToLoad = Console.ReadLine();

            LoadingObject loadedGame = _fileController.Read(fileNameToLoad);
            if (loadedGame.GameCore == null && loadedGame.GenerationToLoad == null)
            {
                Console.WriteLine(StringsDictionary.BrokenFileError);
            }
            else
            {
                _currentGeneration = loadedGame.GenerationToLoad;
                _currentIteration = loadedGame.CurrentIterationCount;
                RenderGame(loadedGame.GameCore);
            }
        }

        /// <summary>
        /// Method for resuming the game.
        /// </summary>
        private void ResumeGame()
        {
            GameEngine gameEngine = new GameEngine(_currentGeneration.GetLength(1), _currentGeneration.GetLength(0));
            gameEngine.LoadGame(_currentGeneration);
            RenderGame(gameEngine);
        }

        /// <summary>
        /// Method for getting help information.
        /// </summary>
        private void GetHelpCommands()
        {
            Console.WriteLine(StringsDictionary.ExitHelpDescription);
            Console.WriteLine(StringsDictionary.NewGameHelpDesctiption);
            Console.WriteLine(StringsDictionary.ClearConsoleHelpDesctiption);
            Console.WriteLine(StringsDictionary.ShowHideCursorHelpDesctiption);
            Console.WriteLine(StringsDictionary.SaveGameHelpDescription);
            Console.WriteLine(StringsDictionary.LoadGameHelpDesctiption);
            Console.WriteLine(StringsDictionary.ResumeGameHelpDesctiption);
        }
 
        /// <summary>
        /// Method draws field and cells on it, changing every second.
        /// </summary>
        /// <param name="gameEngine">Game's core.</param>
        private void RenderGame(GameEngine gameEngine)
        {
            SetWindowSize(272, 70);
            SetCursorPosition(0, 0);
            ClearConsole();
            while (!Console.KeyAvailable)
            {
                _currentGeneration = gameEngine.GameField; 
                _currentIteration++;
                
                SetTitle($"Current iteration: {_currentIteration.ToString()} - Alive cells: {gameEngine.CountAliveCells().ToString()}");
                for (int y = 0; y < _currentGeneration.GetLength(1); y++)
                {
                    var aliveDeadSymbols = new char[_currentGeneration.GetLength(0)];

                    for (int x = 0; x < _currentGeneration.GetLength(0); x++)
                    {
                        if (_currentGeneration[x, y])
                        {
                            aliveDeadSymbols[x] = StringsDictionary.AliveCellSymbol;
                        }
                        else
                        {
                            aliveDeadSymbols[x] = StringsDictionary.DeadCellSymbol;
                        }
                    }
                    Console.WriteLine(aliveDeadSymbols);
                }

                SetCursorPosition(0, 0);
                gameEngine.NextGeneration();
            }
        }

        /// <summary>
        /// Method clears console window.
        /// </summary>
        private void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Method sets console window's title.
        /// </summary>
        /// <param name="title"></param>
        private void SetTitle(string title)
        {
            Console.Title = title;
        }

        /// <summary>
        /// Method sets beginning cursor's position. 
        /// </summary>
        /// <param name="left">
        /// Shift from left. 
        /// </param>
        /// <param name="top">
        /// Shift from top.
        /// </param>
        private void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        private void SetWindowSize(int width, int height)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            Console.SetWindowSize(width, height);
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
