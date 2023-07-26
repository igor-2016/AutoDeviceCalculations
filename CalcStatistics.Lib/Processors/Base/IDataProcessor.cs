using System.Numerics;

namespace CalcStatistics.Processors.Base
{
    public interface IDataProcessor<TDevice, TData, TResult>
        where TDevice : INumber<TDevice>
        where TData : INumber<TData>
        where TResult : INumber<TResult>
    {
        (bool done, string errorMessage) TryCalcResult((TDevice deviceId, TData[] data) input, out (TDevice deviceId, TResult result) output);
        IEnumerable<TDevice> GetSupportedDeviceIds();

        IDataProcessor<TDevice, TData, TResult> 
            RegisterDeviceProcessor(TDevice deviceId, IDataProcessor<TDevice, TData, TResult> processor);

        
    }
}
