namespace EverythingNet.Bench
{
  using BenchmarkDotNet.Configs;
  using BenchmarkDotNet.Engines;
  using BenchmarkDotNet.Jobs;
  using BenchmarkDotNet.Reports;
  using BenchmarkDotNet.Running;

  class Program
  {
    static void Main(string[] args)
    {
      /*IConfig config = ManualConfig
          .Create(DefaultConfig.Instance)
          .With(new Job()
          {
            Run =
            {
              RunStrategy = RunStrategy.ColdStart,
              LaunchCount = 2,
              TargetCount = 3,
              UnrollFactor = 2,
              InvocationCount = 4,
            }
          });

      BenchmarkRunner.Run<SimpleQueryBench>(config);
      BenchmarkRunner.Run<CombinedQueryBench>(config);
      */

      BenchmarkRunner.Run<SimpleQueryBench>();
      BenchmarkRunner.Run<CombinedQueryBench>();
    }
  }
}
