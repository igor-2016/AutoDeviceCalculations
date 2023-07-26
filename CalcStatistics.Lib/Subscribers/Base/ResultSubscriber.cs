using System.Numerics;

namespace CalcStatistics.Subscribers.Base
{
    public class ResultSubscriber<TDevice, TResult> : IObserver<TResult>
         where TDevice : INumber<TDevice>
         where TResult : INumber<TResult>
    {
        private IDisposable? unsubscriber;

        private readonly TDevice _deviceId;

        public ResultSubscriber(TDevice deviceId)
        {
            _deviceId = deviceId;
        }

        public TDevice DeviceId => _deviceId;

        public virtual void Subscribe(IObservable<TResult> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {
        }

        public virtual void OnNext(TResult value)
        {
        }

        public virtual void Unsubscribe()
        {
            unsubscriber?.Dispose();
        }
    }
}
