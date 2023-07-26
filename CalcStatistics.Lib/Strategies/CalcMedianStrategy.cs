using CalcStatistics.Strategies.Base;
using MathNet.Numerics.Statistics;

namespace CalcStatistics.Strategies
{

    public class CalcMedianStrategy : ICalcStrategy<long, double>
    {
        public double CalcStrategy(IEnumerable<long> data)
        {
            return data.Select(x => (double)x).Median();
        }
    }
}
