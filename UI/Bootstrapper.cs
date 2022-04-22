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
                SetTitle("Game of Life - by Conway");
                ReadMainMenuCommands();

                try
                {
                    Console.Write("Type command: ");
                    string commandForMainMenu = Console.ReadLine();

                    switch (commandForMainMenu)
                    {
                        case "0":
                        case "exit":
                            exitMainMenu = true;
                            break;
                        case "1":
                        case "new":
                            ConfigureNewGame();
                            break;
                        case "2":
                        case "clear":
                            ClearConsole();
                            break;
                        case "3":
                            _isCursorVisible = !_isCursorVisible;
                            Settings(_isCursorVisible);
                            break;
                        case "4":
                        case "save":
                            if (_currentGeneration == null || _currentGeneration.Length == 0)
                            {
                                Console.WriteLine("You need to start new game before save file.");
                                continue;
                            }
                            SaveGame();
                            break;
                        case "5":
                        case "load":
                            GetDirectoryFiles();
                            LoadGame();
                            break;
                        case "6":
                        case "resume":
                            if (_currentGeneration == null || _currentGeneration.Length == 0)
                            {
                                Console.WriteLine("Create new game, or load saving game.");
                                continue;
                            }
                            ResumeGame();
                            break;
                        case "?":
                        case "help":
                            GetHelpCommands();
                            break;
                        default:
                            Console.WriteLine("This command isn't support. Please read help documentation. (For help type \"?\", or \"help\")");
                            break;
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Error: {e.Message}");
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
        public void Greatings()
        {
            try
            {
                Console.WriteLine(File.ReadAllText(@"..\..\..\..\Greating.txt"));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        /// <summary>
        /// Method reads file with main menu's commands.
        /// </summary>
        private void ReadMainMenuCommands()
        {
            try
            {
                Console.WriteLine(File.ReadAllText(@"..\..\..\..\MainMenuCommands.txt"));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        /// <summary>
        /// Method configures a game, using user's input.
        /// </summary>
        private void ConfigureNewGame()
        {
            SetTitle("Creating game");
            
            Console.WriteLine("Before you start a new game, you need to configurate it.");

            try
            {
                Console.Write("Rows count (20-50): ");
                int rowsCount = Convert.ToInt32(Console.ReadLine());

                Console.Write("Columns count(20-260): ");
                int colsCount = Convert.ToInt32(Console.ReadLine());

                if ((rowsCount < 20 || rowsCount > 50) && (colsCount < 20 || colsCount > 260))
                {
                    Console.WriteLine("Please, check your input and try again.");
                    return;
                }

                GameEngine gameEngine = new GameEngine(rowsCount, colsCount);
                gameEngine.Generate();
                RenderGame(gameEngine);
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return;
            }
        }

        /// <summary>
        /// Method for saving game into file.
        /// </summary>
        private void SaveGame()
        {
            SetTitle("Saving game");

            Console.Write("Enter file name: ");
            string fileName = Console.ReadLine();
            fileName = fileName == null || fileName.Equals("") ? "default" : fileName;

            _fileController.Write(_currentGeneration, fileName);
            Console.WriteLine($"File saved into {fileName}.gof");
        }

        /// <summary>
        /// Method gets list of files into /games directory.
        /// </summary>
        private void GetDirectoryFiles()
        {
            Console.WriteLine("===> Directory /games");
            Console.WriteLine("==> Files: ");
            _fileController.GetDirectoryFiles();
            Console.WriteLine("============");
        }

        /// <summary>
        /// Method for loading game from file 
        /// into /games directory.
        /// </summary>
        private void LoadGame()
        {
            SetTitle("Loading game");

            Console.Write("Enter file name: ");
            string fileNameToLoad = Console.ReadLine();

            var loadedGame = _fileController.Read(fileNameToLoad);
            if (loadedGame.loadedGame == null && loadedGame.loadedGeneration == null)
            {
                Console.WriteLine("File is broken.");
            }
            else
            {
                _currentGeneration = loadedGame.loadedGeneration;
                RenderGame(loadedGame.loadedGame);
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
            Console.WriteLine("(0), or exit - to exit from the program;");
            Console.WriteLine("(1), or new - to start a new game;");
            Console.WriteLine("(2), or clear - to clear console;");
            Console.WriteLine("(3) - to show/hide cursor;");
            Console.WriteLine("(4), or save - to save current game;");
            Console.WriteLine("(5), or load - to load saved game;");
            Console.WriteLine("(6), or resume - to resume stopped game;");
        }
 
        /// <summary>
        /// Method draws field and cells on it, changing every second.
        /// </summary>
        /// <param name="gameEngine">Game's core.</param>
        private void RenderGame(GameEngine gameEngine)
        {
            SetWindowSize(272, 70);
            SetCursorPosition(0, 12);
            while (!Console.KeyAvailable)
            {
                var currentGeneration = gameEngine.GetCurrentGeneration();
                _currentGeneration = currentGeneration;
                SetTitle($"Current iteration: {gameEngine.CurrentGeneration.ToString()} - Alive cells: {gameEngine.CountAliveCells().ToString()}");

                for (int y = 0; y < _currentGeneration.GetLength(1); y++)
                {
                    var aliveDeadSymbols = new char[_currentGeneration.GetLength(0)];

                    for (int x = 0; x < _currentGeneration.GetLength(0); x++)
                    {
                        if (_currentGeneration[x, y])
                        {
                            aliveDeadSymbols[x] = '#';
                        }
                        else
                        {
                            aliveDeadSymbols[x] = ' ';
                        }
                    }
                    Console.WriteLine(aliveDeadSymbols);
                }
                SetCursorPosition(0, 12);
                gameEngine.NextGeneration();
            }
            Thread.Sleep(1000);
            ClearConsole();
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
            Console.SetWindowSize(width, height);
        }
    }
}
