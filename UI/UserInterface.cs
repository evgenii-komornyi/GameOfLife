using GameOfLifeEngine;
using Repository;

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
        /// Gets response in the main menu from user. 
        /// </summary>
        /// <returns>User prompt.</returns>
        public string GetResponseFromMenu()
        {
            _window.SetTitle(ConstantsRepository.GameTitle);
            _window.ClearConsole();
            ReadMainMenuCommands();
                      
            ShowMessage(ConstantsRepository.TypeCommand, false);

            return Console.ReadLine();
        }

        /// <summary>
        /// Draws field and cells on the screen.
        /// </summary>
        /// <param name="field">Game field.</param>
        public void DrawField(GameEngine field, int offsetX, int offsetY)
        {
            for (int currentRow = 0; currentRow < field.GameField.GetLength(1); currentRow++)
            {
                _window.SetCursorPosition(offsetX, offsetY + currentRow + ConstantsRepository.OffsetY);
                var aliveDeadSymbols = new char[field.GameField.GetLength(0)];

                for (int currentColumn = 0; currentColumn < field.GameField.GetLength(0); currentColumn++)
                {
                    if (field.GameField[currentColumn, currentRow])
                    {
                        aliveDeadSymbols[currentColumn] = ConstantsRepository.AliveCellSymbol;
                    }
                    else
                    {
                        aliveDeadSymbols[currentColumn] = ConstantsRepository.DeadCellSymbol;
                    }
                }
                
                Console.Write(aliveDeadSymbols);
                _window.SetCursorPosition(offsetX, offsetY + currentRow + ConstantsRepository.OffsetY);
            }
        }

        /// <summary>
        /// Draws several game's fields on the screen.
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
        /// Draws separator.
        /// </summary>
        /// <param name="countSeparator">Count of the separators.</param>
        public void DrawSeparator(int countSeparator)
        {
            for (int currentTime = 0; currentTime < countSeparator; currentTime++)
            {
                ShowMessage(ConstantsRepository.Separator, false);
            }
        }

        /// <summary>
        /// Gets input key. 
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
        /// Shows message to user.
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
        /// Reads file with ASCII game's name.
        /// </summary>
        public void ShowGreetingMessage()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + ConstantsRepository.SourceDirectory + ConstantsRepository.GreatingFileName;

            var fileContent = File.ReadAllText(filePath);

            ShowMessage(fileContent);

            PressAnyKeyMessage();
        }

        /// <summary>
        /// Shows header.
        /// </summary>
        public void ShowHeader()
        {
            _window.SetCursorPosition(0, 0);
            DrawSeparator(ConstantsRepository.MediumSeparator);

            _window.SetCursorPosition(0, 1);
            ShowMessage(ConstantsRepository.PressToReturnMessage);

            _window.SetCursorPosition(0, 2);
            DrawSeparator(ConstantsRepository.MediumSeparator);

            _window.SetCursorPosition(0, 3);
            ShowMessage(ConstantsRepository.LiveStatisticMessage);
        }

        /// <summary>
        /// Shows detail message for user.
        /// </summary>
        /// <param name="detailMessage">Detail message.</param>
        /// <param name="countSeparator">Count of separators.</param>
        public void ShowDetailsMessage(string detailMessage, int countSeparator)
        {
            ShowMessage(detailMessage);
            DrawSeparator(countSeparator);
            ShowMessage(ConstantsRepository.EmptyString);
            PressAnyKeyMessage();
        }

        /// <summary>
        /// Shows message to press any key and wait this key from user.
        /// </summary>
        public void PressAnyKeyMessage()
        {
            ShowMessage(ConstantsRepository.PressAnyKeyMessage);
            Console.ReadKey();
        }

        /// <summary>
        /// Reads file with main menu's commands.
        /// </summary>
        private void ReadMainMenuCommands()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + ConstantsRepository.SourceDirectory + ConstantsRepository.MainMenuCommandsFileName;

            var fileContent = File.ReadAllText(filePath);

            ShowMessage(fileContent);
        }

        /// <summary>
        /// Gets help information.
        /// </summary>
        public void GetHelpCommands()
        {
            ShowMessage(ConstantsRepository.ExitHelpDescription);
            ShowMessage(ConstantsRepository.CreateThousandGamesHelpDesctiption);
            ShowMessage(ConstantsRepository.SelectGamesHelpDesctiption);
            ShowMessage(ConstantsRepository.SaveGameHelpDescription);
            ShowMessage(ConstantsRepository.LoadGameHelpDesctiption);
            ShowMessage(ConstantsRepository.ShowHideCursorHelpDesctiption);
        }

        /// <summary>
        /// Converts string value into numeric. 
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
                    ShowMessage(ConstantsRepository.NotANumberError);
                    ShowMessage(ConstantsRepository.PressAnyKeyMessage);
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Limits input value by minimum and maximum limites.   
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
                    ShowMessage(ConstantsRepository.InputOutOfRangeError);
                    ShowMessage(ConstantsRepository.PressAnyKeyMessage);
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
