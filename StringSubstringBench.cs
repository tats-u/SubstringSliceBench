using BenchmarkDotNet.Attributes;

namespace SubstringSliceBench;

public class StringSubstringBench
{
    public record struct StringsResult(string A, string B, string C, string D);

    readonly string _str = "0123";
    readonly int _index0 = 0;
    readonly int _index1 = 1;
    readonly int _index2 = 2;
    readonly int _index3 = 3;

    [Benchmark(Description = ".Substring(int,int)", Baseline = true)]
    public StringsResult TwoIntArguments()
    {
#pragma warning disable IDE0057
        return new(
            _str.Substring(_index0, _str.Length - _index0),
            _str.Substring(_index1, _str.Length - _index1),
            _str.Substring(_index2, _str.Length - _index2),
            _str.Substring(_index3, _str.Length - _index3)
        );
#pragma warning restore IDE0057
    }

    [Benchmark(Description = ".Substring(int)")]
    public StringsResult SingleIntArgument()
    {
#pragma warning disable IDE0057
        return new(
            _str.Substring(_index0),
            _str.Substring(_index1),
            _str.Substring(_index2),
            _str.Substring(_index3)
        );
#pragma warning restore IDE0057
    }
}
