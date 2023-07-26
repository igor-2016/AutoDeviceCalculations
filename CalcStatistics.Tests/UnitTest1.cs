using CalcStatistics.Lib.Parsers;
using System.Diagnostics;

namespace CalcStatistics.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string data = "1 2 3 4 5 6 7 8 9 10 11 123452";
            foreach(ReadOnlySpan<char> a in data.SplitLines(' '))
            {
                Debug.WriteLine(a.ToString());
            }
        }
    }
}