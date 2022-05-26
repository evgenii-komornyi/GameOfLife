using Files;
using GameOfLifeEngine;
using Repository;

namespace UI
{
    /// <summary>
    /// Class contains all logic layer to start a game from User Interface.
    /// </summary>
    public class GameManager
    {
        private GameEngine[] _games;
        private GameEngine[] _gamesOnScreen;
        private int[] _selectedGamesNumbers;

        private UserInterface _userInterface;
        private FileManager _fileManager;
        private Window _window;

        /// <summary>
        /// Class contains all logic layer to start a game from User Interface.
        /// </summary>
        /// <param name="userInterface">User Interface instance.</param>
        /// <param name="fileManager">File Controller instance.</param>
        /// <param name="window">Window instanse.</param>
        public GameManager(UserInterface userInterface, FileManager fileManager, Window window)
        {
            this._userInterface = userInterface;
            this._fileManager = fileManager;
            this._window = window;
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public void RunApplication()
        {
            int currentWindowWidth = _window.WindowConfiguration();


            if (currentWindowWidth >= 220)
            {
                _userInterface.ShowGreetingMessage();

                bool exitGame = false;

                while (!exitGame)
                {
                    string response = _userInterface.GetResponseFromMenu();

                    switch (response)
                    {
                        case ConstantsRepository.ExitCommandNumber:
                        case ConstantsRepository.ExitCommandText:
                            exitGame = true;
                            break;
                        case ConstantsRepository.CreateNewGamesText:
                        case ConstantsRepository.CreateNewGamesNumber:
                            CreateGames();
                            ProcessRunGame();
                            break;
                        case ConstantsRepository.SelectGamesText:
                        case ConstantsRepository.SelectGamesNumber:
                            ProcessGameSelection();
                            break;
                        case ConstantsRepository.SaveGameCommandNumber:
                        case ConstantsRepository.SaveGameCommandText:
                            ProcessGameSave();
                            break;
                        case ConstantsRepository.LoadGameCommandNumber:
                        case ConstantsRepository.LoadGameCommandText:
                            ProcessGameLoad();
                            break;
                        case ConstantsRepository.ShowHideCursorCommandNumber:
                            _window.ShowHideCursor();
                            break;
                        case ConstantsRepository.HelpCommandSign:
                        case ConstantsRepository.HelpCommandText:
                            _userInterface.GetHelpCommands();
                            _userInterface.PressAnyKeyMessage();
                            break;
                        default:
                            _userInterface.ShowDetailsMessage(ConstantsRepository.UnknownCommandMessage, ConstantsRepository.BigSeparator);
                            break;
                    }
                    Thread.Sleep(ConstantsRepository.ThreadSleep);
                }
            } 
            else
            {
                _userInterface.ShowMessage(ConstantsRepository.ResolutionError);
                _userInterface.PressAnyKeyMessage();
            }
        }
                     
        /// <summary>
        /// Runs the game every second, while user press Q or Escape button.
        /// </summary>
        private void RunGame()
        {
            bool isGameOnGoing = true;
            while (isGameOnGoing)
            {
                RunAllGames();
                
                Thread.Sleep(ConstantsRepository.ThreadSleep);
                ConsoleKey? consoleKey = _userInterface.GetInputKey();
                isGameOnGoing = (consoleKey != ConsoleKey.Q && consoleKey != ConsoleKey.Escape);
            }
            _userInterface.ShowMessage(ConstantsRepository.EmptyString);
            _userInterface.PressAnyKeyMessage();
        }

        /// <summary>
        /// Creates games.
        /// </summary>
        private void CreateGames()
        {
            _games = new GameEngine[ConstantsRepository.GamesCount];
            for (int currentGame = 0; currentGame < _games.Length; currentGame++)
            {
                GameEngine newGame = new GameEngine(ConstantsRepository.RowsCount, ConstantsRepository.ColumnsCount);
                newGame.InitializeRandomData();
                _games[currentGame] = newGame;
            }
        }

        /// <summary>
        /// Runs all games at once.
        /// If games were choose, then shows them and their statistic.
        /// Otherwise shows statistic only for all games in total.  
        /// </summary>
        private void RunAllGames()
        {
            _window.SetTitle(ConstantsRepository.CreateGamesTitle);
            _userInterface.ShowHeader();
            ShowGamesStatistic(_games, 4);
            
            if (_gamesOnScreen != null)
            {
                _window.SetTitle(ConstantsRepository.SelectedGamesTitle);
                ShowGamesStatistic(_gamesOnScreen, 5);
                _window.SetCursorPosition(0, 7);
                _userInterface.ShowMessage(ConstantsRepository.SelectedGamesMessage, false);
                ShowSelectedGames();

                _window.SetCursorPosition(0, 8);
                _userInterface.DrawSeparator(ConstantsRepository.HugeSeparator);

                for (int currentGame = 0; currentGame < _gamesOnScreen.Length; currentGame++)
                {
                    _userInterface.DrawMultiField(_gamesOnScreen);
                }
            }
            
            for (int currentGame = 0; currentGame < _games.Length; currentGame++)
            {
                _games[currentGame].NextGeneration();
            }
        }

        /// <summary>
        /// Asks user to select games to show.
        /// </summary>
        private void SelectGames()
        {
            _gamesOnScreen = new GameEngine[ConstantsRepository.GamesOnScreen];
            _selectedGamesNumbers = new int[ConstantsRepository.GamesOnScreen];

            for (int currentGame = 0; currentGame < _gamesOnScreen.Length; currentGame++)
            {
                int selectedGame = _userInterface.GetLimitedValue($"{currentGame + 1}.{ConstantsRepository.SelectGamesMessage}", 0, 999);
                _gamesOnScreen[currentGame] = _games[selectedGame];
                _selectedGamesNumbers[currentGame] = selectedGame;
            }

            _userInterface.DrawSeparator(ConstantsRepository.MediumSeparator);
            _userInterface.ShowMessage(ConstantsRepository.EmptyString);
            _userInterface.ShowMessage(ConstantsRepository.SelectedGamesMessage, false);

            ShowSelectedGames();

            _userInterface.ShowMessage(ConstantsRepository.EmptyString);
            _userInterface.DrawSeparator(ConstantsRepository.MediumSeparator);
            _userInterface.ShowMessage(ConstantsRepository.EmptyString);
            _userInterface.PressAnyKeyMessage();
        }

        /// <summary>
        /// Checks and runs games.
        /// </summary>
        private void ProcessGameSelection()
        {
            if (_games == null)
            {
                _userInterface.ShowDetailsMessage(ConstantsRepository.OnSelectWithoutAnyGamesMessage, ConstantsRepository.LowSeparator);
                return;
            }
            SelectGames();
            ProcessRunGame();
        }

        /// <summary>
        /// Loads and runs game.
        /// </summary>
        private void ProcessGameLoad()
        {
            _games = _fileManager.LoadGame<GameEngine[]>();
            if (_games == null)
            {
                _userInterface.ShowDetailsMessage(ConstantsRepository.LoadingFromFileError, ConstantsRepository.LowSeparator);
                return;
            }
            ProcessRunGame();
        }

        /// <summary>
        /// Saves and runs game.
        /// </summary>
        private void ProcessGameSave()
        {
            if (_games == null)
            {
                _userInterface.ShowDetailsMessage(ConstantsRepository.OnSaveWithoutAnyGamesMessage, ConstantsRepository.LowSeparator);
                return;
            }
            _fileManager.SaveGame(_games);
            _userInterface.ShowDetailsMessage(ConstantsRepository.OnSaveGamesSuccessfulyMessage, ConstantsRepository.LowSeparator);
        }

        /// <summary>
        /// Clears console window and runs game.
        /// </summary>
        private void ProcessRunGame()
        {
            _window.ClearConsole();
            RunGame();
        }
      
        /// <summary>
        /// Shows what exact games on the screen. 
        /// </summary>
        private void ShowSelectedGames()
        {
            for (int currentGameNumber = 0; currentGameNumber < _selectedGamesNumbers.Length; currentGameNumber++)
            {
                string cutLastChar = currentGameNumber < _selectedGamesNumbers.Length - 1 ? ConstantsRepository.Comma : ConstantsRepository.Space;
                _userInterface.ShowMessage($"{_selectedGamesNumbers[currentGameNumber]}{cutLastChar}", false);
            }
        }

        /// <summary>
        /// Shows statistic of the games.
        /// </summary>
        /// <param name="games">Games, which needs statistic.</param>
        /// <param name="offsetY">Top offset.</param>
        private void ShowGamesStatistic(GameEngine[] games, int offsetY)
        {
            int countOfAliveGames = games.Length;
            int totalCountAliveCells = 0;
            for (int currentGame = 0; currentGame < games.Length; currentGame++)
            {
                if (_games[currentGame].CountAliveCells() == 0)
                {
                    countOfAliveGames--;
                }
                totalCountAliveCells += games[currentGame].CountAliveCells();
            }
            _window.SetCursorPosition(0, offsetY);
            _userInterface.ShowMessage($"Total games alive of {games.Length} games: {countOfAliveGames}, ", false);
            _userInterface.ShowMessage($"Total cells alive of {games.Length} games: {totalCountAliveCells}. ");
        }
    }
}