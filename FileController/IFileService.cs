namespace Files
{
    /// <summary>
    /// Interface to saving and loading games' file.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Saves data to binary file.
        /// </summary>
        /// <typeparam name="T">Type of saving object.</typeparam>
        /// <param name="filePath">Path to file.</param>
        /// <param name="objectToWrite">Saving object.</param>
        void WriteToBinaryFile<T>(string filePath, T objectToWrite);

        /// <summary>
        /// Loads data from binary file.
        /// </summary>
        /// <typeparam name="T">Type of loading object.</typeparam>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Loading object.</returns>
        T ReadFromBinaryFile<T>(string filePath);
    }
}
