namespace FileController
{
    public class RWController
    {
        public void Write(bool[,] arrayToWrite, string fileName)
        {
            fileName = fileName.Trim();
            StreamWriter streamWriter = new StreamWriter(@"../../../../" + fileName + ".txt");

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
    }
}