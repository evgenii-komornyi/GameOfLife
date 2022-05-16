namespace Files
{
    public interface IFileService
    {
        void WriteToBinaryFile<T>(string filePath, T objectToWrite);
        T ReadFromBinaryFile<T>(string filePath);
    }
}
