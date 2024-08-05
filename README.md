# string.Substring / (ReadOnly)Span.Slice Benchmarks

This is a collection of string.Substring / (ReadOnly)Span.Slice benchmarks.

These methods have two overloads `(int startIndex, int length)` and `(int startIndex)`, respectively.

## Design

The main difference of two overloads is the range checking logic.
The arguments passed to the main processes ([`InternalSubstring`](https://source.dot.net/#System.Private.CoreLib/src/libraries/System.Private.CoreLib/src/System/String.Manipulation.cs,115953b3f767ea7d) and `new Span`) of those methods are the same.
For `string.Substring`, the shorter the source string, the bigger the difference between two overloads is thought to be.

## Results

```bash
dotnet run -c Release -- -f '*' -j Long -r net9.0 --noOverWrite -m --statisticalTest "10%"
```

### `ReadOnlySpan.Slice`

```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
Intel Core i7-9700 CPU 3.00GHz, 1 CPU, 8 logical and 8 physical cores
.NET SDK 9.0.100-preview.6.24328.19
  [Host]  : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX2
  LongRun : .NET 9.0.0 (9.0.24.32707), X64 RyuJIT AVX2

Job=LongRun  Runtime=.NET 9.0  Toolchain=net9.0  
IterationCount=100  LaunchCount=3  WarmupCount=15  

```
| Method          | Mean     | Error     | StdDev    | Median   | Ratio | MannWhitney(10%) | RatioSD | Allocated | Alloc Ratio |
|---------------- |---------:|----------:|----------:|---------:|------:|----------------- |--------:|----------:|------------:|
| .Slice(int,int) | 3.281 ns | 0.0334 ns | 0.1635 ns | 3.182 ns |  1.00 | Base             |    0.00 |         - |          NA |
| .Slice(int)     | 1.945 ns | 0.0105 ns | 0.0538 ns | 1.930 ns |  0.59 | Faster           |    0.03 |         - |          NA |


### `string.Substring`

```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
Intel Core i7-9700 CPU 3.00GHz, 1 CPU, 8 logical and 8 physical cores
.NET SDK 9.0.100-preview.6.24328.19
  [Host]  : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX2
  LongRun : .NET 9.0.0 (9.0.24.32707), X64 RyuJIT AVX2

Job=LongRun  Runtime=.NET 9.0  Toolchain=net9.0  
IterationCount=100  LaunchCount=3  WarmupCount=15  

```
| Method              | Mean     | Error    | StdDev   | Median   | Ratio | MannWhitney(10%) | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------------- |---------:|---------:|---------:|---------:|------:|----------------- |--------:|-------:|----------:|------------:|
| .Substring(int,int) | 28.20 ns | 0.149 ns | 0.762 ns | 27.95 ns |  1.00 | Base             |    0.00 | 0.0140 |      88 B |        1.00 |
| .Substring(int)     | 27.29 ns | 0.151 ns | 0.786 ns | 27.20 ns |  0.97 | Same             |    0.04 | 0.0140 |      88 B |        1.00 |

The main process (`InternalSubString`) takes more time than `new Span` due to extra allocation, so the difference is relatively smaller.