using System.Diagnostics;
using System.Threading;
using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [SetUpFixture]
  public class Init
  {
    private static int Timeout = 120 * 1000; // 2 min

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
      Assert.Pass($"Everything version: {EverythingState.GetVersion()}");

      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      while (!EverythingState.IsReady() && stopwatch.ElapsedMilliseconds < Timeout)
      {
        Thread.Sleep(200);
      }
    }
  }
}
