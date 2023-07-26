using CalcStatistics.Buffers;
using CalcStatistics.Builders;
using CalcStatistics.FileReaders;
using CalcStatistics.FileReaders.Base;
using CalcStatistics.Parsers;
using CalcStatistics.Parsers.Base;
using CalcStatistics.Processors;
using CalcStatistics.Strategies;
using Serilog;
using System.Text;

namespace CalcStatistics
{

    public class Calculator
    {

        private Calculator()
        {
            
        }

        public static void Calc(string dataFilePath)
        {
            Calc(dataFilePath, new CalcSettings());
        }

        public static void Calc(string dataFilePath, CalcSettings settings)
        {
            IDataFileReader linesReader = settings.ReaderType == ReaderTypes.LineByLine
                ? new LineByLineDataFileReader(Encoding.UTF8) 
                : new DefaultDataFileReader();

            DataParserBase<int, byte> lineParser = settings.ParserType == ParserTypes.Split 
                ? new SplitDataParser() 
                : settings.ParserType == ParserTypes.Slice 
                    ? new SliceDataParser() 
                    : new DefaultDataParser(settings.DataSeparator);

            var lineProcessor = new DeviceByteDataProcessor();
            var strategy = new CalcMedianStrategy();
            var publisherBuilder = new DefaultResultPublisherBuilder();
            var subscriberBuilder = new DefaultResultSubscriberBuilder();

            using (var accumulator = new DefaultResultAccumulator(settings.Window, lineProcessor.GetSupportedDeviceIds(),
                publisherBuilder, subscriberBuilder, strategy))
            {

                var lines = linesReader.Read(dataFilePath);

                long lineCounter = 0;
                foreach (var line in lines)
                {

                    if (lineParser.TryParse(line, out (int deviceId, byte[] data) dataByDevice))
                    {
                        var resultAndErrorMessage = lineProcessor.TryCalcResult(dataByDevice, 
                            out (int deviceId, long result) output);

                        if (resultAndErrorMessage.done)
                        {
                            accumulator.AddResult(output);
                        }
                        else
                        {
                            Log.Warning("Invalid calculation: {deviceId}, {error}", 
                                dataByDevice.deviceId, resultAndErrorMessage.errorMessage);
                        }
                    } 
                    else
                    {
                        Log.Warning("Invalid parsing: {deviceId}, {line}", dataByDevice.deviceId, line);
                    }

                    lineCounter++;
                }
            }
        }
    }
}
