using BenchmarkDotNet.Running;
using CinderBlockHtml.Benchmarks;

BenchmarkRunner.Run<HelloWorldBenchmark>();
BenchmarkRunner.Run<ListBenchmark>();
BenchmarkRunner.Run<NestedBenchmark>();