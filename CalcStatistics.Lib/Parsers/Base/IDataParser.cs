using System.Numerics;

namespace CalcStatistics.Parsers.Base
{
    public interface IDataParser<TDevice, TData>
        where TDevice : INumber<TDevice>
        where TData : INumber<TData>
    {
        bool TryParse(string? lineToParse, out (TDevice deviceId, TData[] data) result);
    }
}
