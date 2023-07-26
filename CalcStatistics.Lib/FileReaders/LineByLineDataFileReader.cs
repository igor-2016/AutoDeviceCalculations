using System.Text;
using CalcStatistics.FileReaders.Base;

namespace CalcStatistics.FileReaders
{

    public class LineByLineDataFileReader : IDataFileReader
    {
        protected int BufferSize;

        protected Encoding FileEncoding;
        public LineByLineDataFileReader(Encoding encoding, int bufferSize = 1024)
        {
            BufferSize = bufferSize;
            FileEncoding = encoding;
        }

        public IEnumerable<string> Read(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                using (var streamReader = new StreamReader(fileStream, FileEncoding, true, BufferSize))
                {
                    string? line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }
    }
}
