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
        /// <param name="objectToSave">
        /// Object contained data to save.
        /// </param>
        /// <param name="fileName">
        /// File name to write the data.
        /// </param>
        public void Write(SavingObject objectToSave, string fileName)
        {
            fileName = fileName.Trim();
            StreamWriter streamWriter = new StreamWriter(@"../../../../games/" + fileName + ".gof");

            string output = "";

            for (int y = 0; y < objectToSave.CurrentGeneration.GetLength(1); y++)
            {
                for (int x = 0; x < objectToSave.CurrentGeneration.GetLength(0); x++)
                {
                    output += objectToSave.CurrentGeneration[x, y] + " ";
                }
                streamWriter.WriteLine(output);
                output = "";
            }
            streamWriter.Write(objectToSave.CurrentIterationCount);
            streamWriter.Close();
        }

        /// <summary>
        /// Method reads file's content and loads it into field.
        /// </summary>
        /// <param name="fileName">
        /// File name to read the data.
        /// </param>
        /// <returns>
        /// Object with created field, 
        /// loaded positions and current iteration's count.
        /// </returns>
        public LoadingObject Read(string fileName)
        {
            String fileContent = File.ReadAllText(@"../../../../games/" + fileName + ".gof");
            LoadingObject objectToLoad = new LoadingObject();

            LoadingObject loadedGame = LoadGame(fileContent, objectToLoad);
                       
            if (loadedGame.GameCore == null && loadedGame.RowsCount == 0 && loadedGame.ColsCount == 0)
            {
                return loadedGame;
            }

            bool[,] loadedGeneration = new bool[loadedGame.ColsCount, loadedGame.RowsCount];

            string[] rows = fileContent.Split("\n");
            string[] cols;

            try
            {
                loadedGame.CurrentIterationCount = uint.Parse(rows[rows.Length - 1]);
            }
            catch (FormatException e)
            {
                Console.WriteLine("File is broken.");
                Console.WriteLine($"Error: {e.Message}");
            }
            
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
            loadedGame.GameCore.LoadGame(loadedGeneration);
            loadedGame.GenerationToLoad = loadedGeneration;
            
            return objectToLoad;
        }

        /// <summary>
        /// Method generates field based on rows and cols in the file.
        /// </summary>
        /// <param name="fileContent">
        /// File content from loading file.
        /// </param>
        /// <returns>
        /// Object with generated field, 
        /// rows count and cols count.
        /// </returns>
        private LoadingObject LoadGame(string fileContent, LoadingObject objectToLoad)
        {
            string[] rows = fileContent.Split("\n");
            string[] cols = null;

            for (int y = 0; y < rows.Length - 1; y++)
            {
                cols = rows[y].Trim().Split(' ');
            }

            if ((rows.Length < 20 || rows.Length > 50) && (cols.Length < 20 || cols.Length > 260))
            {
                objectToLoad.GameCore = null;
                objectToLoad.RowsCount = 0;
                objectToLoad.ColsCount = 0;
            }

            GameEngine loadedGame = new GameEngine(rows.Length - 1, cols.Length);

            objectToLoad.GameCore = loadedGame;
            objectToLoad.RowsCount = rows.Length - 1;
            objectToLoad.ColsCount = cols.Length;

            return objectToLoad;
        }
    }
}