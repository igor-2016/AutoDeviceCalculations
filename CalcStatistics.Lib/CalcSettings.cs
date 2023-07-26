namespace CalcStatistics
{
    public class CalcSettings
    {
        public int Window { get; set; } = 3;

        public char DataSeparator { get; set; } = ' ';

        public ReaderTypes ReaderType { get; set; } = ReaderTypes.Default;

        public ParserTypes ParserType { get; set; } = ParserTypes.Default;
    }

    public enum ParserTypes
    {
        Default = 0,
        Slice = 1,
        Split = 2,
    }

    public enum ReaderTypes
    {
        Default = 0,
        LineByLine = 1,
    }
}
