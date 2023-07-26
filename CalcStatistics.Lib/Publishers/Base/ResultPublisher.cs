using System.Numerics;
using CalcStatistics.Exceptions;
using CalcStatistics.Strategies.Base;

namespace CalcStatistics.Publishers.Base
{

    public class ResultPublisher<TDevice, TData, TResult> : IObservable<TResult>
        where TDevice : INumber<TDevice>
        where TData : INumber<TData>
        where TResult : INumber<TResult>
    {

        protected LinkedList<TData> Buffer = new LinkedList<TData>();

        protected readonly int Window;

        protected readonly int MinWindow;

        private readonly TDevice _deviceId;

        public TDevice DeviceId => _deviceId;

        protected ICalcStrategy<TData, TResult> Strategy;

        public ResultPublisher(TDevice deviceId, int window, int minWindow, ICalcStrategy<TData, TResult> strategy)
        {
            Strategy = strategy;

            _deviceId = deviceId;

            Window = window;

            MinWindow = minWindow;

            observers = new List<IObserver<TResult>>();
        }

        private readonly List<IObserver<TResult>> observers;

        public IDisposable Subscribe(IObserver<TResult> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<TResult>> _observers;
            private readonly IObserver<TResult> _observer;

            public Unsubscriber(List<IObserver<TResult>> observers, IObserver<TResult> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void AddResult(TData datum)
        {
            Buffer.AddLast(datum);

            if (Buffer.Count >= Window)
            {
                var result = CalcAndCleanBuffer();
                OnNotifyAll(result);
            }
        }

        public virtual TResult CalcAndCleanBuffer()
        {
            var result = Strategy.CalcStrategy(Buffer);

            Buffer.Clear();
            return result;
        }

        protected virtual void OnNotifyAll(TResult result)
        {
            foreach (var observer in observers)
            {
                if (!CalculationIsValid(result, out string errorMessage))
                    observer.OnError(new InvalidCalculationException(errorMessage));
                else
                    observer.OnNext(result);
            }
        }

        protected virtual bool CalculationIsValid(TResult result, out string errorMessage)
        {
            errorMessage = string.Empty;
            // TODO
            return true;
        }

        public void Stop()
        {

            if (Buffer.Count > 0)
            {
                var result = CalcAndCleanBuffer();
                OnNotifyAll(result);
            }



            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }
}
