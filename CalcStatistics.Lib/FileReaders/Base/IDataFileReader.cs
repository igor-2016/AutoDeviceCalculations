namespace CalcStatistics.FileReaders.Base
{
    public interface IDataFileReader
    {
        IEnumerable<string> Read(string filePath);
    }
}
