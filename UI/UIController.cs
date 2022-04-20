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
                            ConfigureGame();
                            break;
                        case "2":
                        case "clear":
                            Console.Clear();
                            break;
                        case "3":
                            _isCursorVisible = !_isCursorVisible;
                            Settings(_isCursorVisible);
                            break;
                        case "?":
                        case "help":
                            Console.WriteLine("(0), or exit - to exit from the program;");
                            Console.WriteLine("(1), or new - to start a new game;");
                            Console.WriteLine("(2), or clear - to clear console;");
                            Console.WriteLine("(3) - to show/hide cursor;");
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
        private void Greatings()
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
        private void ConfigureGame()
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
                RenderGame(gameEngine);
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return;
            }
        }

        /// <summary>
        /// Method draws field and cells on it, changing every second.
        /// </summary>
        /// <param name="gameEngine">Game's core.</param>
        private void RenderGame(GameEngine gameEngine)
        {
            Console.SetCursorPosition(0, 20);
            while (!Console.KeyAvailable)
            {
                var currentGeneration = gameEngine.GetCurrentGeneration();

                for (int y = 0; y < currentGeneration.GetLength(1); y++)
                {
                    var aliveDeadSymbols = new char[currentGeneration.GetLength(0)];

                    for (int x = 0; x < currentGeneration.GetLength(0); x++)
                    {
                        if (currentGeneration[x, y])
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
                Console.SetCursorPosition(0, 20);
                gameEngine.NextGeneration();
            }
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}