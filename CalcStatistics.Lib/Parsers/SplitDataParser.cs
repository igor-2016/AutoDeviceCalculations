using CalcStatistics.Lib.Parsers;
using CalcStatistics.Parsers.Base;

namespace CalcStatistics.Parsers
{
    public class SplitDataParser : DataParserBase<int, byte>
    {
        public SplitDataParser(char commaSeparator = ' ') : base(commaSeparator)
        {
        }

        public override bool TryParse(string? lineToParse, out (int deviceId, byte[] data) result)
        {
            if (string.IsNullOrWhiteSpace(lineToParse))
            {
                result = EmptyResult;
                return false;
            }
            
            int deviceId = -1;
            var data = new List<byte>();

            foreach (var slice in lineToParse.SplitLines(CommaSeparator))
            {
                if (deviceId == -1)
                {
                    deviceId = int.Parse(slice);
                }
                else
                {
                    data.Add(byte.Parse(slice));
                }
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
