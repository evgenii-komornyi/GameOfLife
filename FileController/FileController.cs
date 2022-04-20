using GameOfLifeEngine;

namespace Files
{
    public class FileController
    {
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
            foreach (FileInfo file in files)
            {
                Console.WriteLine(file.Name);
            }
        }

        public void Write(bool[,] arrayToWrite, string fileName)
        {
            fileName = fileName.Trim();
            StreamWriter streamWriter = new StreamWriter(@"../../../../games/" + fileName + ".gof");

            string output = "";

            for (int x = 0; x < arrayToWrite.GetLength(0); x++)
            {
                for (int y = 0; y < arrayToWrite.GetLength(1); y++)
                {
                    output += arrayToWrite[x, y] + " ";
                }
                streamWriter.WriteLine(output);
                output = "";
            }
            streamWriter.Close();
        }

        public (GameEngine loadedGame, bool[,] loadedGeneration) Read(string fileName)
        {
            String fileContent = File.ReadAllText(@"../../../../games/" + fileName + ".gof");
            var loadedGame = LoadGame(fileContent);
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

        private (GameEngine loadedGame, int rowsCount, int colsCount) LoadGame(string fileContent)
        {
            string[] rows = fileContent.Split("\n");
            string[] cols = null;

            for (int y = 0; y < rows.Length - 1; y++)
            {
                cols = rows[y].Trim().Split(' ');
            }

            GameEngine loadedGame = new GameEngine(cols.Length, rows.Length - 1);

            return (loadedGame: loadedGame, rowsCount: rows.Length - 1, colsCount: cols.Length);
        }
    }
}