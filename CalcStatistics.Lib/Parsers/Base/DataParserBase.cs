using System.Numerics;

namespace CalcStatistics.Parsers.Base
{

    public abstract class DataParserBase<TDevice, TData> : IDataParser<TDevice, TData>
        where TDevice : INumber<TDevice>
        where TData : INumber<TData>
    {
        protected char CommaSeparator;

        public static (TDevice, TData[]) EmptyResult = new(TDevice.Zero, Array.Empty<TData>());

        protected DataParserBase(char commaSeparator = ' ')
        {
            CommaSeparator = commaSeparator;
        }

        public abstract bool TryParse(string? lineToParse, out (TDevice deviceId, TData[] data) result);
    }
}
