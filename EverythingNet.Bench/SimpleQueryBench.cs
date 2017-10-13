namespace EverythingNet.Bench
{
  using BenchmarkDotNet.Attributes;
  using BenchmarkDotNet.Attributes.Jobs;
  using BenchmarkDotNet.Engines;

  using EverythingNet.Core;
  using EverythingNet.Interfaces;
  using EverythingNet.Query;

  [SimpleJob(RunStrategy.ColdStart, launchCount: 2, warmupCount: 1, targetCount: 4, invocationCount: 8, id: "FastAndDirtyJob")]
  public class SimpleQueryBench
  {
    [Benchmark]
    public long NameContains()
    {
      var queryable = new Everything()
          .Search()
          .Name.Contains("windows");

      return queryable.Count;
    }

    [Benchmark]
    public long NameStartWith()
    {
      var queryable = new Everything()
          .Search()
          .Name.StartWith("user");

      return queryable.Count;
    }

    [Benchmark]
    public long DateModified()
    {
      var queryable = new Everything()
          .Search()
          .ModificationDate.Equal(Dates.Today);

      return queryable.Count;
    }

    /*[Benchmark]
    public long DateCreated()
    {
      var queryable = new Everything()
          .Search()
          .CreationDate.Equal(Dates.Today);

      return queryable.Count;
    }
    
    [Benchmark]
    public long DateAccessed()
    {
      var queryable = new Everything()
          .Search()
          .AccessDate.Equal(Dates.Today);

      return queryable.Count;
    }
    */

    [Benchmark]
    public long DateRun()
    {
      var queryable = new Everything()
          .Search()
          .RunDate.Equal(Dates.Today);

      return queryable.Count;
    }

    [Benchmark]
    public long SmallSize()
    {
      var queryable = new Everything()
          .Search()
          .Size.Equal(Sizes.Small);

      return queryable.Count;
    }

    [Benchmark]
    public long GreaterThan1MoSize()
    {
      var queryable = new Everything()
          .Search()
          .Size.GreaterThan(1, SizeUnit.Mb);

      return queryable.Count;
    }
  }
}