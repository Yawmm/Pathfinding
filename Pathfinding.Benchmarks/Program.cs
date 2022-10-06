using Algorithms.Benchmarks;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var config = ManualConfig.Create(DefaultConfig.Instance)
    .WithOptions(ConfigOptions.JoinSummary)
    .WithOptions(ConfigOptions.DisableLogFile);

BenchmarkRunner.Run(new []
{
    typeof(EmptyAStarBenchmarks),
    typeof(EmptyDijkstraBenchmarks),
    typeof(BlockedAStarBenchmarks),
    typeof(BlockedDijkstraBenchmarks)
}, config);