using CalcStatistics.Builders.Base;
using CalcStatistics.Publishers.Base;
using CalcStatistics.Strategies.Base;
using System.Numerics;

namespace CalcStatistics.Buffers.Base
{
    public abstract class ResultAccumulatorBase<TDevice, TData, TResult> : IDisposable
        where TDevice : INumber<TDevice>
        where TData : INumber<TData>
        where TResult : INumber<TResult>
    {

        public const int MIN_WINDOW = 3;

        public const int MAX_WINDOW = 1000001;

        bool disposed = false;

        protected Dictionary<TDevice, ResultPublisher<TDevice, TData, TResult>> Buffers = new();

        protected readonly int Window;
        protected readonly IEnumerable<TDevice> DeviceIds;

        protected readonly IPublisherBuilder<TDevice, TData, TResult> PublisherBuilder;
        protected readonly ISubscriberBuilder<TDevice, TResult> SubscriberBuilder;
        protected readonly ICalcStrategy<TData, TResult> Strategy;

        public ResultAccumulatorBase(int window, IEnumerable<TDevice> deviceIds, 
            IPublisherBuilder<TDevice, TData, TResult> publisherBuilder, 
            ISubscriberBuilder<TDevice, TResult> subscriberBuilder,
            ICalcStrategy<TData, TResult> strategy)
        {
            if (window < MIN_WINDOW || window > MAX_WINDOW)
            {
                throw new ArgumentOutOfRangeException($"Window is out of range ({MIN_WINDOW}..{MAX_WINDOW})");
            }

            if (window % 2 == 0)
            {
                throw new InvalidDataException("Window must be odd");
            }

            Window = window;
            DeviceIds = deviceIds;
            PublisherBuilder = publisherBuilder;
            SubscriberBuilder = subscriberBuilder;
            Strategy = strategy;
        }

        public abstract void AddResult((TDevice deviceId, TData result) data);

        ~ResultAccumulatorBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

          

            Buffers.Clear();
            

            disposed = true;
        }
    }
}
