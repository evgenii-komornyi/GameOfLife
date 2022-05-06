using Files;
using GameOfLifeEngine;

namespace UI
{
    /// <summary>
    /// Class contains all logic layer  
    /// to start a game from User Interface.
    /// </summary>
    public class GameManager
    {
        private GameEngine[] _thousandGames;
        private GameEngine[] _gamesOnScreen;
        private int[] _selectedGamesNumbers;

        private UserInterface _userInterface;
        private FileController _fileController;
        private Window _window;

        /// <summary>
        /// Class contains all logic layer  
        /// to start a game from User Interface.
        /// </summary>
        /// <param name="userInterface">User Interface instance.</param>
        /// <param name="fileController">File Controller instance.</param>
        /// <param name="window">Window instanse.</param>
        public GameManager(UserInterface userInterface, FileController fileController, Window window)
        {
            this._userInterface = userInterface;
            this._fileController = fileController;
            this._window = window;
        }

        /// <summary>
        /// Method starts the application.
        /// </summary>
        public void RunApplication()
        {
            _window.WindowConfiguration();
            _userInterface.ShowGreetingMessage();

            bool exitGame = false;

            while (!exitGame)
            {
                string response = _userInterface.GetResponseFromMenu();

                switch (response)
                {
                    case StringsDictionary.ExitCommandNumber:
                    case StringsDictionary.ExitCommandText:
                        exitGame = true;
                        break;
                    case StringsDictionary.CreateThousandGamesText:
                    case StringsDictionary.CreateThousandGamesNumber:
                        CreateThousandGames();
                        _window.ClearConsole();
                        RunGame();
                        break;
                    case StringsDictionary.SelectGamesText:
                    case StringsDictionary.SelectGamesNumber:
                        if (_thousandGames == null)
                        {
                            _userInterface.ShowMessage(StringsDictionary.OnSelectWithoutAnyGamesMessage);
                            _userInterface.DrawSeparator(20);
                            _userInterface.ShowMessage(StringsDictionary.EmptyString);
                            _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                            Console.ReadKey();
                            continue;

                        }
                        SelectGames();
                        _window.ClearConsole();
                        RunGame();
                        break;
                    case StringsDictionary.SaveGameCommandNumber:
                    case StringsDictionary.SaveGameCommandText:
                        if (_thousandGames == null)
                        {
                            _userInterface.ShowMessage(StringsDictionary.OnSaveWithoutAnyGamesMessage);
                            _userInterface.DrawSeparator(20);
                            _userInterface.ShowMessage(StringsDictionary.EmptyString);
                            _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                            Console.ReadKey();
                            continue;
                        }
                        SaveGame();
                        _userInterface.ShowMessage(StringsDictionary.OnSaveGamesSuccessfulyMessage);
                        _userInterface.DrawSeparator(20);
                        _userInterface.ShowMessage(StringsDictionary.EmptyString);
                        _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                        Console.ReadKey();
                        break;
                    case StringsDictionary.LoadGameCommandNumber:
                    case StringsDictionary.LoadGameCommandText:
                        LoadGame();
                        if (_thousandGames == null)
                        {
                            _userInterface.ShowMessage(StringsDictionary.DirectoryNotExistError);
                            _userInterface.DrawSeparator(20);
                            _userInterface.ShowMessage(StringsDictionary.EmptyString);
                            _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                            Console.ReadKey();
                            continue;
                        }
                        _window.ClearConsole();
                        RunGame();
                        break;
                    case StringsDictionary.ShowHideCursorCommandNumber:
                        _window.ShowHideCursor();
                        break;
                    case StringsDictionary.HelpCommandSign:
                    case StringsDictionary.HelpCommandText:
                        _userInterface.GetHelpCommands();
                        _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                        Console.ReadKey();
                        break;
                    default:
                        _userInterface.ShowMessage(StringsDictionary.UnknownCommandMessage);
                        _userInterface.DrawSeparator(100);
                        _userInterface.ShowMessage(StringsDictionary.EmptyString);
                        _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                        Console.ReadKey();
                        break;
                }
                Thread.Sleep(1000);
            }
        }
                     
        /// <summary>
        /// Method runs the game every second,
        /// while user press Q or Escape button.
        /// </summary>
        private void RunGame()
        {
            bool isGameOnGoing = true;
            while (isGameOnGoing)
            {
                RunThousandGames();
                
                Thread.Sleep(1000);
                ConsoleKey? consoleKey = _userInterface.GetInputKey();
                isGameOnGoing = (consoleKey != ConsoleKey.Q && consoleKey != ConsoleKey.Escape);
            }
            _userInterface.ShowMessage(StringsDictionary.EmptyString);
            _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
            Console.ReadKey();
        }

        /// <summary>
        /// Method creates 1000 games.
        /// </summary>
        private void CreateThousandGames()
        {
            _thousandGames = new GameEngine[1000];
            for (int currentGame = 0; currentGame < _thousandGames.Length; currentGame++)
            {
                GameEngine newGame = new GameEngine(25, 55);
                newGame.InitializeData();
                _thousandGames[currentGame] = newGame;
            }
        }

        /// <summary>
        /// Method runs all 1000 games at once.
        /// If 8 games were choose, then shows them and their statistic.
        /// Otherwise shows statistic only for 1000 games in total.  
        /// </summary>
        private void RunThousandGames()
        {
            _window.SetTitle(StringsDictionary.CreateGamesTitle);
            ShowHeader();
            ShowGamesStatistic(_thousandGames, 4);
            
            if (_gamesOnScreen != null)
            {
                _window.SetTitle(StringsDictionary.SelectedGamesTitle);
                ShowGamesStatistic(_gamesOnScreen, 5);
                _window.SetCursorPosition(0, 7);
                _userInterface.ShowMessage(StringsDictionary.SelectedGamesMessage, false);
                ShowSelectedGames();

                _window.SetCursorPosition(0, 8);
                _userInterface.DrawSeparator(200);

                for (int currentGame = 0; currentGame < _gamesOnScreen.Length; currentGame++)
                {
                    _userInterface.DrawMultiField(_gamesOnScreen);
                }
            }
            
            for (int currentGame = 0; currentGame < _thousandGames.Length; currentGame++)
            {
                _thousandGames[currentGame].NextGeneration();
            }
        }

        /// <summary>
        /// Method asks user to select 8 games to show.
        /// </summary>
        private void SelectGames()
        {
            _gamesOnScreen = new GameEngine[8];
            _selectedGamesNumbers = new int[8];

            for (int currentGame = 0; currentGame < _gamesOnScreen.Length; currentGame++)
            {
                int selectedGame = _userInterface.GetLimitedValue($"{currentGame + 1}.{StringsDictionary.SelectGamesMessage}", 0, 999);
                _gamesOnScreen[currentGame] = _thousandGames[selectedGame];
                _selectedGamesNumbers[currentGame] = selectedGame;
            }

            _userInterface.DrawSeparator(50);
            _userInterface.ShowMessage(StringsDictionary.EmptyString);
            _userInterface.ShowMessage(StringsDictionary.SelectedGamesMessage, false);

            ShowSelectedGames();

            _userInterface.ShowMessage(StringsDictionary.EmptyString);
            _userInterface.DrawSeparator(50);
            _userInterface.ShowMessage(StringsDictionary.EmptyString);
            _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
            Console.ReadKey();
        }
        
        /// <summary>
        /// Method shows header.
        /// </summary>
        private void ShowHeader()
        {
            _window.SetCursorPosition(0, 0);
            _userInterface.DrawSeparator(60);

            _window.SetCursorPosition(0, 1);
            _userInterface.ShowMessage(StringsDictionary.PressToReturnMessage);

            _window.SetCursorPosition(0, 2);
            _userInterface.DrawSeparator(60);

            _window.SetCursorPosition(0, 3);
            _userInterface.ShowMessage(StringsDictionary.LiveStatisticMessage);
        }

        /// <summary>
        /// Method shows what exact games on the screen. 
        /// </summary>
        private void ShowSelectedGames()
        {
            for (int currentGameNumber = 0; currentGameNumber < _selectedGamesNumbers.Length; currentGameNumber++)
            {
                string cutLastChar = currentGameNumber < _selectedGamesNumbers.Length - 1 ? StringsDictionary.Comma : StringsDictionary.Space;
                _userInterface.ShowMessage($"{_selectedGamesNumbers[currentGameNumber]}{cutLastChar}", false);
            }
        }

        /// <summary>
        /// Method shows statistic of the games.
        /// </summary>
        /// <param name="games">Games, which needs statistic.</param>
        /// <param name="offsetY">Top offset.</param>
        private void ShowGamesStatistic(GameEngine[] games, int offsetY)
        {
            int countOfAliveGames = games.Length;
            int totalCountAliveCells = 0;
            for (int currentGame = 0; currentGame < games.Length; currentGame++)
            {
                if (_thousandGames[currentGame].CountAliveCells() == 0)
                {
                    countOfAliveGames--;
                }
                totalCountAliveCells += games[currentGame].CountAliveCells();
            }
            _window.SetCursorPosition(0, offsetY);
            _userInterface.ShowMessage($"Total games alive of {games.Length} games: {countOfAliveGames}, ", false);
            _userInterface.ShowMessage($"Total cells alive of {games.Length} games: {totalCountAliveCells}. ");
        }

        /// <summary>
        /// Method for saving games into file.
        /// </summary>
        private void SaveGame()
        {
            _fileController.WriteToBinaryFile<GameEngine[]>(BuildPath(StringsDictionary.SavingLoadingFilesDirectory), _thousandGames);
        }

        /// <summary>
        /// Method for loading games from file 
        /// into /games directory.
        /// </summary>
        private void LoadGame()
        {
            _thousandGames = _fileController.ReadFromBinaryFile<GameEngine[]>(BuildPath(StringsDictionary.SavingLoadingFilesDirectory));
        }

        /// <summary>
        /// Method builds path to save, or load data.
        /// </summary>
        /// <param name="directory">Directory name.</param>
        /// <returns>Built path.</returns>
        private string BuildPath(string directory = StringsDictionary.EmptyString)
        {
            string savingLoadingDirectory = string.IsNullOrEmpty(directory) ? string.Empty : directory;

            return $"{AppDomain.CurrentDomain.BaseDirectory}{savingLoadingDirectory}{StringsDictionary.SavingLoadingFileName}";
        }
    }
}