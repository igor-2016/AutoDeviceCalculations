using CalcStatistics.Processors.Base;
using System.Numerics;

namespace CalcStatistics.Processors
{
    public abstract class DataProcessor<TDevice, TData, TResult> : IDataProcessor<TDevice, TData, TResult>
        where TDevice : INumber<TDevice>
        where TData : INumber<TData>
        where TResult : INumber<TResult>
    {

        public static (TDevice deviceId, TResult result) EmptyResult = new(TDevice.Zero, TResult.Zero);

        protected Dictionary<TDevice, IDataProcessor<TDevice, TData, TResult>> Processors = new();

        public virtual IEnumerable<TDevice> GetSupportedDeviceIds() => Processors.Select(x => x.Key).ToList();

        public abstract (bool done, string errorMessage) 
            TryCalcResult((TDevice deviceId, TData[] data) input, out (TDevice deviceId, TResult result) output);

        public virtual IDataProcessor<TDevice, TData, TResult> 
            RegisterDeviceProcessor(TDevice deviceId, IDataProcessor<TDevice, TData, TResult> processor)
        {
            if (Processors.TryGetValue(deviceId, out _))
            {
                throw new InvalidOperationException($"Processor for device {deviceId} is already exist");
            }
            
            Processors.Add(deviceId, processor);

            return this;
        }
    }
}
