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
        private bool _isCursorVisible = true;

        /// <summary>
        /// Method gets response in the main menu from user. 
        /// </summary>
        /// <returns>User prompt.</returns>
        public string GetResponseFromMenu()
        {
            SetTitle(StringsDictionary.GameTitle);
            ClearConsole();
            ReadMainMenuCommands();

            Console.Write(StringsDictionary.TypeCommand);

            return Console.ReadLine();
        }

        /// <summary>
        /// Method draws field and cells on the screen.
        /// </summary>
        /// <param name="field">Game field.</param>
        public void DrawGame(GameEngine field)
        {
            SetWindowSize(272, 70);
            SetCursorPosition(0, 0);
                        
            SetTitle($"Current iteration: {field.CurrentGeneration.ToString()} - Alive cells: {field.CountAliveCells().ToString()}");
            for (int currentRow = 0; currentRow < field.GameField.GetLength(1); currentRow++)
            {
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
                Console.WriteLine(aliveDeadSymbols);
            }

            SetCursorPosition(0, 0);    
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
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Method shows/hides blinking console's cursor.
        /// </summary>
        public void ShowHideCursor()
        {
            _isCursorVisible = !_isCursorVisible;
            Console.CursorVisible = _isCursorVisible;
        }

        /// <summary>
        /// Method reads file with ASCII game's name.
        /// </summary>
        public void ShowGreetingMessage()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + StringsDictionary.GreatingFileName;

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
            string filePath = AppDomain.CurrentDomain.BaseDirectory + StringsDictionary.MainMenuCommandsFileName;

            var fileContent = File.ReadAllText(filePath);

            ShowMessage(fileContent);
        }

        /// <summary>
        /// Method gets help information.
        /// </summary>
        public void GetHelpCommands()
        {
            ShowMessage(StringsDictionary.ExitHelpDescription);
            ShowMessage(StringsDictionary.NewGameHelpDesctiption);
            ShowMessage(StringsDictionary.ClearConsoleHelpDesctiption);
            ShowMessage(StringsDictionary.ShowHideCursorHelpDesctiption);
            ShowMessage(StringsDictionary.SaveGameHelpDescription);
            ShowMessage(StringsDictionary.LoadGameHelpDesctiption);
            ShowMessage(StringsDictionary.ResumeGameHelpDesctiption);
        }

        /// <summary>
        /// Method clears console window.
        /// </summary>
        public void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Method sets console window's title.
        /// </summary>
        /// <param name="title">Title.</param>
        private void SetTitle(string title)
        {
            Console.Title = title;
        }

        /// <summary>
        /// Method sets beginning cursor's position. 
        /// </summary>
        /// <param name="left">Shift from left.</param>
        /// <param name="top">Shift from top.</param>
        private void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        /// <summary>
        /// Method sets window's size.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        private void SetWindowSize(int width, int height)
        {
            #pragma warning disable CA1416 // Validate platform compatibility
            Console.SetWindowSize(width, height);
            #pragma warning restore CA1416 // Validate platform compatibility
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
                Console.Write(prompt);
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
