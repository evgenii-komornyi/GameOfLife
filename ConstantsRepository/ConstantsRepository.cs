namespace Repository
{
    public static class ConstantsRepository
    {
        #region Titles
        public const string GameTitle = "Game of Life - by Conway";
        public const string CreateGamesTitle = "Games";
        public const string SelectedGamesTitle = "Selected games";
        #endregion

        #region MainMenuCommands 
        public const string TypeCommand = "Type command: ";

        public const string ExitCommandText = "exit";
        public const string ExitCommandNumber = "0";

        public const string CreateNewGamesText = "create";
        public const string CreateNewGamesNumber = "1";

        public const string SelectGamesText = "select";
        public const string SelectGamesNumber = "2";

        public const string SaveGameCommandText = "save";
        public const string SaveGameCommandNumber = "3";

        public const string LoadGameCommandText = "load";
        public const string LoadGameCommandNumber = "4";

        public const string ShowHideCursorCommandNumber = "5";

        public const string HelpCommandText = "help";
        public const string HelpCommandSign = "?";
        #endregion

        #region HelpDescriptions
        public const string ExitHelpDescription = "(0), or exit - to exit from the program;";
        public const string CreateThousandGamesHelpDesctiption = "(1), or create - to create and run game;";
        public const string SelectGamesHelpDesctiption = "(2), or select - to select 8 games from created games and show it on the screen;";
        public const string SaveGameHelpDescription = "(3), or save - to save all games;";
        public const string LoadGameHelpDesctiption = "(4), or load - to load all games;";
        public const string ShowHideCursorHelpDesctiption = "(5) - to show/hide cursor;";
        #endregion

        #region DirectoriesAndFiles
        public const string SourceDirectory = "source/";
        public const string GreatingFileName = "Greating.txt";
        public const string MainMenuCommandsFileName = "MainMenuCommands.txt";

        public const string SavingLoadingFilesDirectory = "saves/";
        public const string SavingLoadingFileName = "GameOfLifeData";
        #endregion

        #region Messages
        public const string UnknownCommandMessage = "This command isn't support. Please read help documentation. (For help type \"?\", or \"help\")";

        public const string PressAnyKeyMessage = "Press any key to continue...";
        public const string PressToReturnMessage = "Press Q, or Escape to return into main menu...";

        public const string OnSaveWithoutAnyGamesMessage = "You need to start new game before saving it.";
        public const string OnSaveGamesSuccessfulyMessage = "Game were saved successfuly.";
        public const string OnSelectWithoutAnyGamesMessage = "Try to create the games to select 8.";

        public const string LiveStatisticMessage = "Live statistic: ";

        public const string SelectGamesMessage = "Select games from 0 to 999: ";
        public const string SelectedGamesMessage = "Selected games: ";
        #endregion

        #region Errors
        public const string InputOutOfRangeError = "Input was out of range. Please, check your input and try again.";
        public const string NotANumberError = "Input format must be a number. Please, check your input and try again";

        public const string LoadingFromFileError = "Game does not load. Try to create a new game.";
        public const string DrawNewGameError = "You do not have any games.";

        public const string ResolutionError = "Change font size of console window to 10 and rerun the game.";
        #endregion

        #region Others
        public const string EmptyString = "";
        public const string Space = " ";
        public const string Comma = ", ";
        public const string Separator = "-";

        public const char AliveCellSymbol = 'x';
        public const char DeadCellSymbol = '.';

        public const int ThreadSleep = 1000;

        public const int Zero = 0;
        public const int LowSeparator = 20;
        public const int MediumSeparator = 60;
        public const int BigSeparator = 100;
        public const int HugeSeparator = 200;

        public const int GamesCount = 1000;
        public const int RowsCount = 25;
        public const int ColumnsCount = 55;
        public const int GamesOnScreen = 8;

        public const int OffsetY = 10;

        public const int MaximizedWindowSize = 3;
        public const int Disabled = 0;
        public const int ConsoleSize = 61440;
        #endregion
    }
}
