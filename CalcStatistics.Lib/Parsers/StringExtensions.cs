using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcStatistics.Lib.Parsers
{
    using System;

    public static class StringExtensions
    {
        public static LineSplitEnumerator SplitLines(this string str, char separator)
        {
            return new LineSplitEnumerator(str.AsSpan(), separator.ToString().AsSpan());
        }

        public ref struct LineSplitEnumerator
        {
            private ReadOnlySpan<char> _str;
            private ReadOnlySpan<char> _separator;

            public LineSplitEnumerator(ReadOnlySpan<char> str, ReadOnlySpan<char> separator)
            {
                _str = str;
                _separator = separator;
                Current = default;
            }

            public LineSplitEnumerator GetEnumerator() => this;

            public bool MoveNext()
            {
                var span = _str;
                if (span.Length == 0) 
                    return false;

                var index = span.IndexOfAny(_separator);
                if (index == -1)
                {
                    _str = ReadOnlySpan<char>.Empty;
                    Current = new LineSplitEntry(span, ReadOnlySpan<char>.Empty);
                    return true;
                }

                Current = new LineSplitEntry(span.Slice(0, index), span.Slice(index, 1));
                _str = span.Slice(index + 1);
                return true;
            }

            public LineSplitEntry Current { get; private set; }
        }

        public readonly ref struct LineSplitEntry
        {
            public LineSplitEntry(ReadOnlySpan<char> line, ReadOnlySpan<char> separator)
            {
                Line = line;
                Separator = separator;
            }

            public ReadOnlySpan<char> Line { get; }
            public ReadOnlySpan<char> Separator { get; }

            public void Deconstruct(out ReadOnlySpan<char> line, out ReadOnlySpan<char> separator)
            {
                line = Line;
                separator = Separator;
            }

            public static implicit operator ReadOnlySpan<char>(LineSplitEntry entry) => entry.Line;
        }
    }
}
