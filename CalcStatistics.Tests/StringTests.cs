using CalcStatistics.Lib.Parsers;

namespace CalcStatistics.Tests
{
    public class StringTests
    {
        [Fact]
        public void SplitLinesTest()
        {
            string data = "1 2 3 4";
            var result = new List<string>();
            foreach(ReadOnlySpan<char> a in data.SplitLines(' '))
            {
                result.Add(a.ToString());
            }

            Assert.Equal(4, result.Count);
            Assert.Equal("1", result[0]);
            Assert.Equal("2", result[1]);
            Assert.Equal("3", result[2]);
            Assert.Equal("4", result[3]);
        }
    }

    //TODO здесь должно было быть много других тестов. Но в них на момент написания этого кода не было смысла.
    // Примеры тестов можно посмотреть в другом репозитории, например https://github.com/igor-2016/Eval
}
