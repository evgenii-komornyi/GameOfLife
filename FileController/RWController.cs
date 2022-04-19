namespace FileController
{
    public class RWController
    {
        public RWController()
        {
        }

        public void Write(bool[,] arrayToWrite, string fileName = "default")
        {
            StreamWriter streamWriter = new StreamWriter(@"C:\Users\evgenii.komornyi\source\repos\GameOfLife\" + fileName + ".txt");

            string output = "";

            for (int x = 0; x < arrayToWrite.GetLength(0); x++)
            {
                for (int y = 0; y < arrayToWrite.GetLength(1); y++)
                {
                    output += arrayToWrite[x, y].ToString();
                }
                streamWriter.WriteLine(output);
                output = "";
            }
            streamWriter.Close();
        }
    }
}