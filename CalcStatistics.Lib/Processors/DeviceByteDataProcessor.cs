using CalcStatistics.Processors.Base;

namespace CalcStatistics.Processors
{


    public class DeviceByteDataProcessor : DataProcessor<int, byte, long>
    {

        public DeviceByteDataProcessor()
        {
            RegisterDeviceProcessor(1, new DeviceFirstDataProcessor())
             .RegisterDeviceProcessor(2, new DeviceSecondDataProcessor())
             .RegisterDeviceProcessor(3, new DeviceThirdDataProcessor())
             .RegisterDeviceProcessor(4, new DeviceFouthDataProcessor());
        }
       
        public override (bool done, string errorMessage) TryCalcResult((int deviceId, byte[] data) input, 
            out (int deviceId, long result) output)
        {
            IDataProcessor<int, byte, long>? processor;

            if (Processors.TryGetValue(input.deviceId, out processor))
            {
                return processor.TryCalcResult(input, out output);
            }
            else
            {
                output = EmptyResult;
                return new (false, $"Processor for device {input.deviceId} not found!");
            }
        }



        private class DeviceFirstDataProcessor : DataProcessor<int, byte, long>
        {
            public override (bool done, string errorMessage) TryCalcResult((int deviceId, byte[] data) input, out (int deviceId, long result) output)
            {
                long result = 0;
                foreach (var d in input.data)
                {
                    result += d;
                }
                output = (input.deviceId, result);
                return (true, string.Empty);
            }

            public override IEnumerable<int> GetSupportedDeviceIds() => new[] { 1 };
        }

        private class DeviceSecondDataProcessor : DataProcessor<int, byte, long>
        {
            public override (bool done, string errorMessage) TryCalcResult((int deviceId, byte[] data) input, out (int deviceId, long result) output)
            {
                long result = 0;
                foreach (var d in input.data)
                {
                    result *= d;
                }
                output = (input.deviceId, result);
                return (true, string.Empty);
            }

            public override IEnumerable<int> GetSupportedDeviceIds() => new[] { 2 };
        }

        private class DeviceThirdDataProcessor : DataProcessor<int, byte, long>
        {
            public override (bool done, string errorMessage) TryCalcResult((int deviceId, byte[] data) input, out (int deviceId, long result) output)
            {
                long result = 0;
                foreach (var d in input.data)
                {
                    if (d > result)
                        result = d;
                }
                output = (input.deviceId, result);
                return (true, string.Empty);
            }

            public override IEnumerable<int> GetSupportedDeviceIds() => new[] { 3 };
        }

        private class DeviceFouthDataProcessor : DataProcessor<int, byte, long>
        {
            public override (bool done, string errorMessage) TryCalcResult((int deviceId, byte[] data) input, out (int deviceId, long result) output)
            {
                long result = byte.MaxValue;
                foreach (var d in input.data)
                {
                    if (d < result)
                        result = d;
                }
                output = (input.deviceId, result);
                return (true, string.Empty);
            }

            public override IEnumerable<int> GetSupportedDeviceIds() => new[] { 4 };
        }

    }
}
