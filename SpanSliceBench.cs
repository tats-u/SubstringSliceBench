using System;

using BenchmarkDotNet.Attributes;

namespace SubstringSliceBench
{
    public readonly ref struct SpansResult(ReadOnlySpan<char> a, ReadOnlySpan<char> b, ReadOnlySpan<char> c, ReadOnlySpan<char> d)
    {
        public readonly ReadOnlySpan<char> A = a;
        public readonly ReadOnlySpan<char> B = b;
        public readonly ReadOnlySpan<char> C = c;
        public readonly ReadOnlySpan<char> D = d;
    }

    public class SpanSliceBench
    {
        readonly string _str = "0123";
        readonly int _index0 = 0;
        readonly int _index1 = 1;
        readonly int _index2 = 2;
        readonly int _index3 = 3;
        [Benchmark(Description = ".Slice(int,int)", Baseline = true)]
        public SpansResult TwoIntArguments()
        {
            var span = _str.AsSpan();
#pragma warning disable IDE0057
            return new(
                span.Slice(_index0, _str.Length - _index0),
                span.Slice(_index1, _str.Length - _index1),
                span.Slice(_index2, _str.Length - _index2),
                span.Slice(_index3, _str.Length - _index3)
            );
#pragma warning restore IDE0057
        }

        [Benchmark(Description = ".Slice(int)")]
        public SpansResult SingleIntArgument()
        {
            var span = _str.AsSpan();
#pragma warning disable IDE0057
            return new(
                span.Slice(_index0),
                span.Slice(_index1),
                span.Slice(_index2),
                span.Slice(_index3)
            );
#pragma warning restore IDE0057
        }
    }
}
