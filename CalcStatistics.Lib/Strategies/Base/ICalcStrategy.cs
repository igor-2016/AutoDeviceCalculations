using System.Numerics;

namespace CalcStatistics.Strategies.Base
{
    public interface ICalcStrategy<TData, TResult>
        where TData : INumber<TData>
        where TResult : INumber<TResult>
    {
        TResult CalcStrategy(IEnumerable<TData> data);
    }
}
