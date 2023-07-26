using CalcStatistics.Publishers.Base;
using CalcStatistics.Strategies.Base;

namespace CalcStatistics.Publishers
{
    public class DefaultResultPublisher : ResultPublisher<int, long, double>
    {
     

        public DefaultResultPublisher(int deviceId, int window, int minWindow, ICalcStrategy<long, double> strategy) :
            base(deviceId, window, minWindow, strategy)
        {
        }
    }
}
