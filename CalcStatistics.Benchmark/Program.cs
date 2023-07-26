using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Reflection;

namespace CalcStatistics.Benchmark
{
    internal class Program
    {

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<CalcStatisticTest>();
        }
    }

    [MemoryDiagnoser]
    public class CalcStatisticTest
    {
        
        string dataFilePath = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName + @"\performance.txt";

      

        [Benchmark]
        public void DefaultConfiguration()
        {
           
            Calculator.Calc(dataFilePath, new CalcSettings()
            {
                Window = 3,
            });
        }

       

        [Benchmark]
        public void LineByLineReaderConfiguration()
        {
            Calculator.Calc(dataFilePath, new CalcSettings()
            {
                Window = 3,
                ReaderType = ReaderTypes.LineByLine,
            });
        }

     
       

        [Benchmark]
        public void SliceParserConfiguration()
        {
            Calculator.Calc(dataFilePath, new CalcSettings()
            {
                Window = 3,
                ReaderType = ReaderTypes.Default,
                ParserType = ParserTypes.Slice,
            });
        }

        [Benchmark]
        public void SliceParserAndLineByLineConfiguration()
        {
            Calculator.Calc(dataFilePath, new CalcSettings()
            {
                Window = 3,
                ReaderType = ReaderTypes.LineByLine,
                ParserType = ParserTypes.Slice,
            });
        }

        [Benchmark]
        public void SplitParserConfiguration()
        {
            Calculator.Calc(dataFilePath, new CalcSettings()
            {
                Window = 3,
                ReaderType = ReaderTypes.Default,
                ParserType = ParserTypes.Split,
            });
        }

        [Benchmark]
        public void SplitParserAndLineByLineReaderConfiguration()
        {
            Calculator.Calc(dataFilePath, new CalcSettings()
            {
                Window = 3,
                ReaderType = ReaderTypes.LineByLine,
                ParserType = ParserTypes.Split,
            });
        }
    }
}