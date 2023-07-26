using CalcStatistics.Subscribers.Base;
using Serilog;

namespace CalcStatistics.Subscribers
{
    public class DefaultResultSubscriber : ResultSubscriber<int, double>
    {
        public DefaultResultSubscriber(int deviceId) : base(deviceId)
        {
        }

        public override void OnNext(double value)
        {
            Log.Information("New calculation {value} has been done for device {deviceId}", value, DeviceId);
        }

        public override void OnError(Exception e)
        {
            Log.Error("Error generated {error} for device {deviceId}", e, DeviceId);
        }

        public override void OnCompleted()
        {
            Log.Information("Calculation for device '{deviceId}' is done.", DeviceId);
        }
    }
}
