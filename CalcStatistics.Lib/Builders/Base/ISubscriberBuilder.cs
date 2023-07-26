using CalcStatistics.Subscribers.Base;
using System.Numerics;

namespace CalcStatistics.Builders.Base
{
    public interface ISubscriberBuilder<TDevice, TResult>
         where TDevice : INumber<TDevice>
         where TResult : INumber<TResult>
    {
        ResultSubscriber<TDevice, TResult> BuildSubscriber(int deviceId);
    }
}
