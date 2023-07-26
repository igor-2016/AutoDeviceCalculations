using CalcStatistics.Publishers.Base;
using CalcStatistics.Strategies.Base;
using System.Numerics;

namespace CalcStatistics.Builders.Base
{
    public interface IPublisherBuilder<TDevice, TData, TResult>
         where TDevice : INumber<TDevice>
            where TData : INumber<TData>
            where TResult : INumber<TResult>
    {
        ResultPublisher<TDevice, TData, TResult> BuildPublisher(
            TDevice deviceId, int window, int minWindow, ICalcStrategy<TData, TResult> strategy);
    }
}
