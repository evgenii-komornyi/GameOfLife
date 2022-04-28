using Files;
using GameOfLifeEngine;

namespace UI
{
    /// <summary>
    /// Class contains all logic layer  
    /// to start a game from User Interface.
    /// </summary>
    public class UIController
    {
        private GameEngine _newGame;
        private UserInterface _userInterface;
        private FileController _fileController;

        /// <summary>
        /// Class contains all logic layer  
        /// to start a game from User Interface.
        /// </summary>
        /// <param name="userInterface">User Interface instance.</param>
        /// <param name="fileController">File Controller instance.</param>
        public UIController(UserInterface userInterface, FileController fileController)
        {
            this._userInterface = userInterface;
            this._fileController = fileController;
        }

        /// <summary>
        /// Method starts the application.
        /// </summary>
        public void RunApplication()
        {
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
                    case StringsDictionary.NewGameCommandNumber:
                    case StringsDictionary.NewGameCommandText:
                        ConfigureNewGame();
                        _userInterface.ClearConsole();
                        RunGame();
                        break;
                    case StringsDictionary.ClearConsoleCommandNumber:
                    case StringsDictionary.ClearConsoleCommandText:
                        _userInterface.ClearConsole();
                        break;
                    case StringsDictionary.ShowHideCursorCommandNumber:
                        _userInterface.ShowHideCursor();
                        break;
                    case StringsDictionary.SaveGameCommandNumber:
                    case StringsDictionary.SaveGameCommandText:
                        if (_newGame == null || _newGame.GameField.Length == 0)
                        {
                            _userInterface.ShowMessage(StringsDictionary.OnSaveWithoutAnyGamesMessage);
                            _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                            Console.ReadKey();
                            continue;
                        }
                        SaveGame();
                        break;
                    case StringsDictionary.LoadGameCommandNumber:
                    case StringsDictionary.LoadGameCommandText:
                        if (_newGame == null)
                        {
                            _userInterface.ShowMessage(StringsDictionary.DirectoryNotExist);
                            _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                            Console.ReadKey();
                            continue;
                        }
                        LoadGame();
                        _userInterface.ClearConsole();
                        RunGame();
                        break;
                    case StringsDictionary.ResumeGameCommandNumber:
                    case StringsDictionary.ResumeGameCommandText:
                        if (_newGame == null)
                        {
                            _userInterface.ShowMessage(StringsDictionary.OnResumeWithoutAnyGamesMessage);
                            _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                            Console.ReadKey();
                            continue;
                        }
                        _userInterface.ClearConsole();
                        RunGame();
                        break;
                    case StringsDictionary.HelpCommandSign:
                    case StringsDictionary.HelpCommandText:
                        _userInterface.GetHelpCommands();
                        _userInterface.ShowMessage(StringsDictionary.PressAnyKeyMessage);
                        Console.ReadKey();
                        break;
                    default:
                        _userInterface.ShowMessage(StringsDictionary.UnknownCommandMessage);
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
                _userInterface.DrawGame(_newGame);
                _newGame.NextGeneration();

                Thread.Sleep(1000);
                ConsoleKey? consoleKey = _userInterface.GetInputKey();
                isGameOnGoing = (consoleKey != ConsoleKey.Q && consoleKey != ConsoleKey.Escape);
            }
            Console.WriteLine(StringsDictionary.PressAnyKeyMessage);
            Console.ReadKey();
        }

        /// <summary>
        /// Method configures a game, using user's input.
        /// </summary>
        private void ConfigureNewGame()
        {
            int columnsCount = _userInterface.GetLimitedValue(StringsDictionary.ColumnsCountConfigMessage, 10, 260);
            int rowsCount = _userInterface.GetLimitedValue(StringsDictionary.RowsCountConfigMessage, 10, 50);

            _newGame = new GameEngine(rowsCount, columnsCount);
            _newGame.InitializeData();
        }

        /// <summary>
        /// Method for saving game into file.
        /// </summary>
        private void SaveGame()
        {
            _fileController.WriteToBinaryFile<GameEngine>(BuildPath(StringsDictionary.SavingLoadingFilesFolder), _newGame);
        }

        /// <summary>
        /// Method for loading game from file 
        /// into /games directory.
        /// </summary>
        private void LoadGame()
        {
            _newGame = _fileController.ReadFromBinaryFile<GameEngine>(BuildPath(StringsDictionary.SavingLoadingFilesFolder));
        }

        /// <summary>
        /// Method builds path to save, or load data.
        /// </summary>
        /// <param name="directory">Directory name.</param>
        /// <returns>Built path.</returns>
        private string BuildPath(string directory = "")
        {
            string savingLoadingDirectory = string.IsNullOrEmpty(directory) ? "" : directory;

            return $"{AppDomain.CurrentDomain.BaseDirectory}{savingLoadingDirectory}{StringsDictionary.SavingLoadingFileName}";
        }
    }
}