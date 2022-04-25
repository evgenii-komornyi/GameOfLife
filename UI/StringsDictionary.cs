using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public static class StringsDictionary
    {
        #region MainMenuCommands 
        public const string TypeCommand = "Type command: ";

        public const string ExitCommandText = "exit";
        public const string ExitCommandNumber = "0";

        public const string NewGameCommandText = "new";
        public const string NewGameCommandNumber = "1";

        public const string ClearConsoleCommandText = "clear";
        public const string ClearConsoleCommandNumber = "2";

        public const string ShowHideCursorCommand = "3";

        public const string HelpCommandText = "help";
        public const string HelpCommandSign = "?";

        public const string UnknownCommand = "This command isn't support. Please read help documentation. (For help type \"?\", or \"help\")";

        public const string HelpDescriptionExit = "(0), or exit - to exit from the program;";
        public const string HelpDesctiptionNewGame = "(1), or new - to start a new game;";
        public const string HelpDesctiptionClearConsole = "(2), or clear - to clear console;";
        public const string HelpDesctiptionShowHideCursor = "(3) - to show/hide cursor;";
        #endregion

        #region DirectoriesAndFiles
        public const string GreatingFileName = "Greating";
        public const string MainMenuCommandsFileName = "MainMenuCommands";
        #endregion

        #region NewGameConfiguration
        public const string OnConfigMessage = "Before you start a new game, you need to configurate it.";
        public const string RowsCountConfigMessage = "Rows count (20-50): ";
        public const string ColumnsCountConfigMessage = "Columns count(20-260): ";
        #endregion

        #region Errors
        public const string InputOutOfRangeError = "Input was out of range. Please, check your input and try again.";
        public const string InputFormatError = "Input format must be a number. Please, check your input and try again";

        public const string FileNotExistError = "Such file does not exist.";
        public const string RenderNewGameError = "You do not have any games.";
        #endregion
    }
}
