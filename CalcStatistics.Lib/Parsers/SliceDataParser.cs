using CalcStatistics.Parsers.Base;

namespace CalcStatistics.Parsers
{
    public class SliceDataParser : DataParserBase<int, byte>
    {
        public SliceDataParser(char commaSeparator = ' ') : base(commaSeparator)
        {
        }

        public override bool TryParse(string? lineToParse, out (int deviceId, byte[] data) result)
        {
            if (string.IsNullOrWhiteSpace(lineToParse))
            {
                result = EmptyResult;
                return false;
            }

            var data = new List<byte>();

            var span = lineToParse.AsSpan();

            int nextCommaIndex = 0;
            bool isLastLoop = false;

            int deviceId = -1;

            while (!isLastLoop)
            {
                int indexStart = nextCommaIndex;
                nextCommaIndex = lineToParse.IndexOf(CommaSeparator, indexStart);

                isLastLoop = nextCommaIndex == -1;
                if (isLastLoop)
                {
                    nextCommaIndex = lineToParse.Length;
                }

                var slice = span.Slice(indexStart, nextCommaIndex - indexStart);

                if (deviceId == -1)
                {
                    deviceId = int.Parse(slice);
                }
                else
                {
                    data.Add(byte.Parse(slice));
                }

                nextCommaIndex++;
            }


            if (deviceId != -1)
            {
                result = new(deviceId, data.ToArray());
                return true;
            }

            result = EmptyResult;
            return false;
        }
    }
}
