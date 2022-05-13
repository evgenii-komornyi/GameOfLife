using Files;
using GameOfLifeEngine;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    /// <summary>
    /// Class to work with files. 
    /// </summary>
    public class FileManager
    {
        private FileController _fileController;

        public FileManager(FileController fileController)
        {
            this._fileController = fileController;
        }
        
        /// <summary>
        /// Saves games into file.
        /// </summary>
        public void SaveGame<T>(T objectToSave)
        {
            _fileController.WriteToBinaryFile(BuildPath(ConstantsRepository.SavingLoadingFilesDirectory), objectToSave);
        }

        /// <summary>
        /// Loads games from file into /saves directory.
        /// </summary>
        public T LoadGame<T>()
        {
            try
            {
                return _fileController.ReadFromBinaryFile<T>(BuildPath(ConstantsRepository.SavingLoadingFilesDirectory));
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
