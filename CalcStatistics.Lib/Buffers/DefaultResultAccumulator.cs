using CalcStatistics.Buffers.Base;
using CalcStatistics.Builders.Base;
using CalcStatistics.Strategies.Base;

namespace CalcStatistics.Buffers
{
    public class DefaultResultAccumulator : ResultAccumulatorBase<int, long, double>
    {
        bool disposed = false;

        public DefaultResultAccumulator(int window, IEnumerable<int> deviceIds,
            IPublisherBuilder<int, long, double> publisherBuilder,
            ISubscriberBuilder<int, double> subscriberBuilder,
            ICalcStrategy<long, double> strategy) :
            base (window, deviceIds, publisherBuilder, subscriberBuilder, strategy)
        {
            foreach (var deviceId in deviceIds)
            {
                var publisher = PublisherBuilder.BuildPublisher(deviceId, window, MIN_WINDOW, Strategy);
                var subscriber = SubscriberBuilder.BuildSubscriber(deviceId);
                subscriber.Subscribe(publisher);
                Buffers.Add(deviceId, publisher);
            }
        }

        public override void AddResult((int deviceId, long result) data)
        {
            Buffers[data.deviceId].AddResult(data.result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                foreach (var publisher in Buffers.Values)
                {
                    publisher.Stop();
                }
            }

            disposed = true;

            base.Dispose(disposing);
        }
    }
}
