using CalcStatistics.Builders.Base;
using CalcStatistics.Publishers;
using CalcStatistics.Publishers.Base;
using CalcStatistics.Strategies.Base;

namespace CalcStatistics.Builders
{
    public class DefaultResultPublisherBuilder : IPublisherBuilder<int, long, double>
    {
        public ResultPublisher<int, long, double> 
            BuildPublisher(int deviceId, int window, int minWindow, ICalcStrategy<long, double> strategy)
        {
            return new DefaultResultPublisher(deviceId, window, minWindow, strategy);
        }
    }
}
