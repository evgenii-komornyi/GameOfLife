using GameOfLifeEngine;

namespace Files
{
    /// <summary>
    /// Class to saving and loading games' files,
    /// and getting files within directory.
    /// </summary>
    public class FileController
    {
        /// <summary>
        /// Method reads files within /games directory
        /// and show list of files.
        /// </summary>
        public void GetDirectoryFiles()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"../../../../games");
            if (!directoryInfo.Exists)
            {
                Console.WriteLine("Such directory does not exist.");
                Console.WriteLine("Before load a game, you need to start a new game and save it.");
            }

            FileInfo[] files = directoryInfo.GetFiles();
            if (files.Length == 0)
            {
                Console.WriteLine("Before load a game, you need to start a new game and save it.");
            }

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{ i+1 }. {files[i].Name}");
            }
        }

        /// <summary>
        /// Method saves data to the specific file.
        /// </summary>
        /// <param name="arrayToWrite">
        /// Data to write.
        /// </param>
        /// <param name="fileName">
        /// File name to write the data.
        /// </param>
        public void Write(bool[,] arrayToWrite, string fileName)
        {
            fileName = fileName.Trim();
            StreamWriter streamWriter = new StreamWriter(@"../../../../games/" + fileName + ".gof");

            string output = "";

            for (int y = 0; y < arrayToWrite.GetLength(1); y++)
            {
                for (int x = 0; x < arrayToWrite.GetLength(0); x++)
                {
                    output += arrayToWrite[x, y] + " ";
                }
                streamWriter.WriteLine(output);
                output = "";
            }
            streamWriter.Close();
        }

        /// <summary>
        /// Method reads file's content and loads it into field.
        /// </summary>
        /// <param name="fileName">
        /// File name to read the data.
        /// </param>
        /// <returns>Tuple of created field and loaded positions.</returns>
        public (GameEngine loadedGame, bool[,] loadedGeneration) Read(string fileName)
        {
            String fileContent = File.ReadAllText(@"../../../../games/" + fileName + ".gof");
            var loadedGame = LoadGame(fileContent);

            if (loadedGame.loadedGame == null && loadedGame.rowsCount == 0 && loadedGame.colsCount == 0)
            {
                return (loadedGame: null, loadedGeneration: null);
            }

            bool[,] loadedGeneration = new bool[loadedGame.colsCount, loadedGame.rowsCount];

            var tuple = (loadedGame: loadedGame.loadedGame, loadedGeneration: loadedGeneration);

            string[] rows = fileContent.Split("\n");
            string[] cols;

            for (int y = 0; y < rows.Length - 1; y++)
            {
                cols = rows[y].Trim().Split(' ');

                for (int x = 0; x < cols.Length; x++)
                {
                    bool boolString;
                    if (bool.TryParse(cols[x], out boolString))
                    {
                        loadedGeneration[x, y] = boolString;
                    }
                }
            }
            loadedGame.loadedGame.LoadGame(loadedGeneration);
            tuple.loadedGeneration = loadedGeneration;

            return tuple;
        }

        /// <summary>
        /// Method generates field based on rows and cols in the file.
        /// </summary>
        /// <param name="fileContent">
        /// File content from loading file.
        /// </param>
        /// <returns>Tuple of generated field, rows count and cols count.</returns>
        private (GameEngine loadedGame, int rowsCount, int colsCount) LoadGame(string fileContent)
        {
            string[] rows = fileContent.Split("\n");
            string[] cols = null;

            for (int y = 0; y < rows.Length - 1; y++)
            {
                cols = rows[y].Trim().Split(' ');
            }

            if ((rows.Length < 20 || rows.Length > 50) && (cols.Length < 20 || cols.Length > 260))
            {
                return (loadedGame: null, rowsCount: 0, colsCount: 0);
            }

            GameEngine loadedGame = new GameEngine(rows.Length - 1, cols.Length);

            return (loadedGame: loadedGame, rowsCount: rows.Length - 1, colsCount: cols.Length);
        }
    }
}