namespace Files
{
    /// <summary>
    /// Class contained properties, 
    /// required to save into file.
    /// </summary>
    public class SavingObject
    {
        public bool[,] CurrentGeneration { get; set; }
        public uint CurrentIterationCount { get; set; }
    }
}
