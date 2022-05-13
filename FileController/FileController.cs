namespace Files
{
    /// <summary>
    /// Class to saving and loading games' file.
    /// </summary>
    public class FileController
    {
        /// <summary>
        /// Saves data to binary file.
        /// </summary>
        /// <typeparam name="T">Type of saving object.</typeparam>
        /// <param name="filePath">Path to file.</param>
        /// <param name="objectToWrite">Saving object.</param>
        public void WriteToBinaryFile<T>(string filePath, T objectToWrite)
        {
            CreateDirectoryIfNotExist(filePath);
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Creates the directory to saving games if it does not exist.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        private void CreateDirectoryIfNotExist(string filePath)
        {
            if (!string.IsNullOrEmpty(Path.GetDirectoryName(filePath)) && !IsDirectoryExist(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
        }

        /// <summary>
        /// Loads data from binary file.
        /// </summary>
        /// <typeparam name="T">Type of loading object.</typeparam>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Loading object.</returns>
        public T ReadFromBinaryFile<T>(string filePath)
        {
            if (!IsDirectoryExist(filePath) || !File.Exists(filePath))
            {
                return default(T);
            }

            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Checks if directory does exist.
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Directory does/does not exist</returns>
        private bool IsDirectoryExist(string filePath)
        {
            return Directory.Exists(Path.GetDirectoryName(filePath));
        }
    }
}