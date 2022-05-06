using Files;
using GameOfLifeEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UserInterface
    {
        private Window _window;

        public UserInterface(Window window)
        {
            this._window = window;
        }

        /// <summary>
        /// Method gets response in the main menu from user. 
        /// </summary>
        /// <returns>User prompt.</returns>
        public string GetResponseFromMenu()
        {
            _window.SetTitle(StringsDictionary.GameTitle);
            _window.ClearConsole();
            ReadMainMenuCommands();
                      
            ShowMessage(StringsDictionary.TypeCommand, false);

            return Console.ReadLine();
        }

        /// <summary>
        /// Method draws field and cells on the screen.
        /// </summary>
        /// <param name="field">Game field.</param>
        public void DrawField(GameEngine field, int offsetX, int offsetY)
        {
            for (int currentRow = 0; currentRow < field.GameField.GetLength(1); currentRow++)
            {
                _window.SetCursorPosition(offsetX, offsetY + currentRow + 10);
                var aliveDeadSymbols = new char[field.GameField.GetLength(0)];

                for (int currentColumn = 0; currentColumn < field.GameField.GetLength(0); currentColumn++)
                {
                    if (field.GameField[currentColumn, currentRow])
                    {
                        aliveDeadSymbols[currentColumn] = StringsDictionary.AliveCellSymbol;
                    }
                    else
                    {
                        aliveDeadSymbols[currentColumn] = StringsDictionary.DeadCellSymbol;
                    }
                }
                
                Console.Write(aliveDeadSymbols);
                _window.SetCursorPosition(offsetX, offsetY + currentRow + 10);
            }
        }

        /// <summary>
        /// Method draws several game's fields on the screen.
        /// </summary>
        /// <param name="fields">Game fields.</param>
        public void DrawMultiField(GameEngine[] fields)
        {
            int offsetX = 0;
            int offsetY = 0;

            for (int currentGame = 0; currentGame < fields.Length; currentGame++)
            {
                if (currentGame < 4)
                {
                    offsetX = currentGame * (fields[currentGame].GameField.GetLength(0) + 1);
                }
                else
                {
                    offsetX = (currentGame - 4) * (fields[currentGame].GameField.GetLength(0) + 1);
                    offsetY = fields[currentGame].GameField.GetLength(1) + 1;
                }
                DrawField(fields[currentGame], offsetX, offsetY);
            }
        }

        /// <summary>
        /// Method draws separator.
        /// </summary>
        /// <param name="countSeparator">Count of the separators.</param>
        public void DrawSeparator(int countSeparator)
        {
            for (int currentTime = 0; currentTime < countSeparator; currentTime++)
            {
                ShowMessage(StringsDictionary.Separator, false);
            }
        }

        /// <summary>
        /// Method gets input key. 
        /// </summary>
        /// <returns>Pressed button value.</returns>
        public ConsoleKey? GetInputKey()
        {
            if (Console.KeyAvailable)
            {
                return Console.ReadKey(true).Key;
            }
            return null;
        }

        /// <summary>
        /// Method shows message to user.
        /// </summary>
        /// <param name="message">Message to show.</param>
        public void ShowMessage(string message, bool useWriteLine = true)
        {
            if (useWriteLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
        }
        
        /// <summary>
        /// Method reads file with ASCII game's name.
        /// </summary>
        public void ShowGreetingMessage()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + StringsDictionary.SourceDirectory + StringsDictionary.GreatingFileName;

            var fileContent = File.ReadAllText(filePath);

            ShowMessage(fileContent);

            ShowMessage(StringsDictionary.PressAnyKeyMessage);
            Console.ReadKey();
        }

        /// <summary>
        /// Method reads file with main menu's commands.
        /// </summary>
        private void ReadMainMenuCommands()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + StringsDictionary.SourceDirectory + StringsDictionary.MainMenuCommandsFileName;

            var fileContent = File.ReadAllText(filePath);

            ShowMessage(fileContent);
        }

        /// <summary>
        /// Method gets help information.
        /// </summary>
        public void GetHelpCommands()
        {
            ShowMessage(StringsDictionary.ExitHelpDescription);
            ShowMessage(StringsDictionary.CreateThousandGamesHelpDesctiption);
            ShowMessage(StringsDictionary.SelectGamesHelpDesctiption);
            ShowMessage(StringsDictionary.SaveGameHelpDescription);
            ShowMessage(StringsDictionary.LoadGameHelpDesctiption);
            ShowMessage(StringsDictionary.ShowHideCursorHelpDesctiption);
        }

        /// <summary>
        /// Method converts string value into numeric. 
        /// </summary>
        /// <param name="prompt">String value for asking user's input.</param>
        /// <returns>Converted numeric value.</returns>
        private int GetNumericValue(string prompt)
        {
            while(true)
            {
                ShowMessage(prompt, false);
                string valueInput = Console.ReadLine();
            
                if (int.TryParse(valueInput, out int value))
                { 
                    return value;
                }
                else
                {
                    ShowMessage(StringsDictionary.NotANumberError);
                    ShowMessage(StringsDictionary.PressAnyKeyMessage);
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Method limits input value by minimum and maximum limites.   
        /// </summary>
        /// <param name="prompt">String value for asking user's input.</param>
        /// <param name="minValue">Minimal value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns>Limited value.</returns>
        public int GetLimitedValue(string prompt, int minValue, int maxValue) 
        {
            while (true)
            {
                int value = GetNumericValue(prompt);

                if (value < minValue || value > maxValue)
                {
                    ShowMessage(StringsDictionary.InputOutOfRangeError);
                    ShowMessage(StringsDictionary.PressAnyKeyMessage);
                    Console.ReadKey();
                }
                else
                {
                    return value;
                }
            }
        }
    }
}
