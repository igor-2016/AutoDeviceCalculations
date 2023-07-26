using CalcStatistics.Parsers.Base;

namespace CalcStatistics.Parsers
{
    public class DefaultDataParser : DataParserBase<int, byte>
    {
        public DefaultDataParser(char commaSeparator = ' ') : base(commaSeparator)
        {
        }

        public override bool TryParse(string? lineToParse, out (int deviceId, byte[] data) result)
        {
            var parsedData = lineToParse?.Split(CommaSeparator, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            
            if (parsedData.Length > 0)
            {
                var data = new string[parsedData.Length - 1];
                Array.Copy(parsedData, 1, data, 0, parsedData.Length - 1);
                result = new(int.Parse(parsedData[0]), data.Select(byte.Parse).ToArray());
                return true;
            }

            result = EmptyResult;
            return false;
        }
    }
}
