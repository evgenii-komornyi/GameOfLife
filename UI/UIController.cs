using FileController;
using GameOfLifeEngine;

namespace UI
{
    public class UIController
    {
        private bool _isVisible = true;
        private bool[,] _currentGeneration;
        private RWController rwController = new RWController();

        public UIController()
        {
        }

        public void start()
        {
            _greatings();

            bool exitMainMenu = false;

            while (!exitMainMenu)
            {
                _readMainMenuCommands();

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
                            _configureGame();
                            break;
                        case "2":
                        case "clear":
                            Console.Clear();
                            break;
                        case "3":
                            _isVisible = !_isVisible;      
                            _settings(_isVisible);
                            break;
                        case "4":
                        case "save":
                            if (_currentGeneration == null || _currentGeneration.Length == 0)
                            {
                                Console.WriteLine("You need to start new game before save file.");
                                continue;
                            }

                            Console.Write("Enter file name: ");
                            string fileName = Console.ReadLine();

                            rwController.Write(_currentGeneration, fileName);
                            Console.WriteLine("File saved.");
                            break;
                        case "?":
                        case "help":
                            Console.WriteLine("(0), or exit - to exit from the program;");
                            Console.WriteLine("(1), or new - to start a new game;");
                            Console.WriteLine("(2), or clear - to clear console;");
                            Console.WriteLine("(3) - to show/hide cursor;");
                            Console.WriteLine("(4), or save - to save current game;");
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

        private void _settings(bool isVisible)
        {
            Console.CursorVisible = isVisible;
        }

        private void _greatings()
        {
            try
            {
                Console.WriteLine(File.ReadAllText(@"C:\Users\evgenii.komornyi\source\repos\GameOfLife\Greating.txt"));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void _readMainMenuCommands()
        {
            try
            {
                Console.WriteLine(File.ReadAllText(@"C:\Users\evgenii.komornyi\source\repos\GameOfLife\MainMenuCommands.txt"));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void _configureGame()
        {
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
                _renderGame(gameEngine);
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return;
            }
        }

        private void _renderGame(GameEngine gameEngine)
        {
            Console.SetCursorPosition(0, 20);
            while (!Console.KeyAvailable)
            {
                var currentGeneration = gameEngine.GetCurrentGeneration();
                _currentGeneration = currentGeneration;

                for (int y = 0; y < currentGeneration.GetLength(1); y++)
                {
                    var str = new char[currentGeneration.GetLength(0)];

                    for (int x = 0; x < currentGeneration.GetLength(0); x++)
                    {
                        if (currentGeneration[x, y])
                        {
                            str[x] = '#';
                        }
                        else
                        {
                            str[x] = ' ';
                        }
                    }
                    Console.WriteLine(str);
                }
                Console.SetCursorPosition(0, 20);
                gameEngine.NextGeneration();
            }
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}