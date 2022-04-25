using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public static class StringsDictionary
    {
        #region Titles
        public const string GameTitle = "Game of Life - by Conway";
        public const string CreateGameTitle = "Creating game";
        public const string SaveGameTitle = "Saving game";
        public const string LoadGameTitle = "Loading game";
        #endregion

        #region MainMenuCommands 
        public const string TypeCommand = "Type command: ";

        public const string ExitCommandText = "exit";
        public const string ExitCommandNumber = "0";

        public const string NewGameCommandText = "new";
        public const string NewGameCommandNumber = "1";

        public const string ClearConsoleCommandText = "clear";
        public const string ClearConsoleCommandNumber = "2";

        public const string ShowHideCursorCommandNumber = "3";

        public const string SaveGameCommandText = "save";
        public const string SaveGameCommandNumber = "4";

        public const string LoadGameCommandText = "load";
        public const string LoadGameCommandNumber = "5";

        public const string ResumeGameCommandText = "resume";
        public const string ResumeGameCommandNumber = "6";

        public const string HelpCommandText = "help";
        public const string HelpCommandSign = "?";
        #endregion

        #region HelpDescriptions
        public const string ExitHelpDescription = "(0), or exit - to exit from the program;";
        public const string NewGameHelpDesctiption = "(1), or new - to start a new game;";
        public const string ClearConsoleHelpDesctiption = "(2), or clear - to clear console;";
        public const string ShowHideCursorHelpDesctiption = "(3) - to show/hide cursor;";
        public const string SaveGameHelpDescription = "(4), or save - to save current game;";
        public const string LoadGameHelpDesctiption = "(5), or load - to load saved game;";
        public const string ResumeGameHelpDesctiption = "(6), or resume - to resume stopped game;";

        #endregion

        #region DirectoriesAndFiles
        public const string GreatingFileName = "Greating.txt";
        public const string MainMenuCommandsFileName = "MainMenuCommands.txt";

        public const string DefaultFileName = "default";

        public const string TreeDirectory = "===> Directory /games";
        public const string TreeFiles = "Files: ";
        public const string Devider = "=============";

        public const string SavingLoadingFilesFormat = ".gof";
        #endregion

        #region Messages
        public const string UnknownCommandMessage = "This command isn't support. Please read help documentation. (For help type \"?\", or \"help\")";

        public const string PressAnyKeyMessage = "Press any key to continue...";

        public const string OnConfigMessage = "Before you start a new game, you need to configurate it.";
        public const string RowsCountConfigMessage = "Rows count (20-50): ";
        public const string ColumnsCountConfigMessage = "Columns count(20-260): ";

        public const string OnSaveWithoutAnyGamesMessage = "You need to start new game before save file.";
        public const string OnResumeWithoutAnyGamesMessage = "Create new game, or load saving game.";

        public const string EnterFileNameMessage = "Enter file name: ";

        public const string OnSuccessSavedFileMessage = "File is successfully saved into";
        #endregion

        #region Errors
        public const string InputOutOfRangeError = "Input was out of range. Please, check your input and try again.";
        public const string InputFormatError = "Input format must be a number. Please, check your input and try again";

        public const string FileNotExistError = "Such file does not exist.";
        public const string RenderNewGameError = "You do not have any games.";

        public const string BrokenFileError = "File is broken.";
        #endregion

        #region Others
        public const string EmptyString = "";

        public const char AliveCellSymbol = '#';
        public const char DeadCellSymbol = ' ';
        #endregion
    }
}
