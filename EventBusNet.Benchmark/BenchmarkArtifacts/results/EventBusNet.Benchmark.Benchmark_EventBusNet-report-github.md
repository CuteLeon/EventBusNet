```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.3932)
13th Gen Intel Core i7-13700 2.10GHz, 1 CPU, 24 logical and 16 physical cores
.NET SDK 10.0.100-preview.7.25366.103
  [Host]     : .NET 9.0.7 (9.0.725.31616), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 9.0.7 (9.0.725.31616), X64 RyuJIT AVX2


```
| Method                     | Mean            | Error         | StdDev        |
|--------------------------- |----------------:|--------------:|--------------:|
| RaiseDynamic1Event         |        45.17 ns |      0.082 ns |      0.072 ns |
| RaiseTrading1Event         |        46.19 ns |      0.078 ns |      0.069 ns |
| RaiseDynamic5Event         |        50.98 ns |      0.036 ns |      0.028 ns |
| RaiseTrading5Event         |       120.54 ns |      0.146 ns |      0.114 ns |
| RaiseDynamic5WorkloadEvent | 9,987,237.72 ns |  2,317.807 ns |  2,054.675 ns |
| RaiseTrading5WorkloadEvent | 1,364,094.52 ns | 14,862.859 ns | 13,902.727 ns |
