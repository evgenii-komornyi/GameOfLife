using Files;
using Repository;
using System.Runtime.Serialization;

namespace UI
{
    /// <summary>
    /// Class to work with files. 
    /// </summary>
    public class FileManager
    {
        private FileService _fileService;

        public FileManager(FileService fileController)
        {
            this._fileService = fileController;
        }
        
        /// <summary>
        /// Saves games into file.
        /// </summary>
        public void SaveGame<T>(T objectToSave)
        {
            _fileService.WriteToBinaryFile(BuildPath(ConstantsRepository.SavingLoadingFilesDirectory), objectToSave);
        }

        /// <summary>
        /// Loads games from file into /saves directory.
        /// </summary>
        public T LoadGame<T>()
        {
            try
            {
                return _fileService.ReadFromBinaryFile<T>(BuildPath(ConstantsRepository.SavingLoadingFilesDirectory));
            } 
            catch (SerializationException ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Builds path to save, or load data.
        /// </summary>
        /// <param name="directory">Directory name.</param>
        /// <returns>Built path.</returns>
        private string BuildPath(string directory = ConstantsRepository.EmptyString)
        {
            string savingLoadingDirectory = string.IsNullOrEmpty(directory) ? string.Empty : directory;

            return $"{AppDomain.CurrentDomain.BaseDirectory}{savingLoadingDirectory}{ConstantsRepository.SavingLoadingFileName}";
        }
    }
}
