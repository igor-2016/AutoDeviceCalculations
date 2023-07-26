using CalcStatistics.FileReaders.Base;

namespace CalcStatistics.FileReaders
{
    public class DefaultDataFileReader : IDataFileReader
    {
        public IEnumerable<string> Read(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}
