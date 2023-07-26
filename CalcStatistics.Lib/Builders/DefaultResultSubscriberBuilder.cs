using CalcStatistics.Builders.Base;
using CalcStatistics.Publishers;
using CalcStatistics.Strategies.Base;
using CalcStatistics.Subscribers;
using CalcStatistics.Subscribers.Base;

namespace CalcStatistics.Builders
{
    public class DefaultResultSubscriberBuilder : ISubscriberBuilder<int, double>
    {
        public ResultSubscriber<int, double> BuildSubscriber(int deviceId)
        {
            return new DefaultResultSubscriber(deviceId);
        }
    }
}
