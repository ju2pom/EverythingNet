using System.Diagnostics;
using System.Threading;
using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [SetUpFixture]
  public class Init
  {
    private static int Timeout = 60 * 1000; // 1 min

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
      if (!EverythingState.IsStarted())
      {
        EverythingState.StartService(true, EverythingState.StartMode.Service);
      }

      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      while (!EverythingState.IsReady() && stopwatch.ElapsedMilliseconds < Timeout)
      {
        var lastError = EverythingState.GetLastError();
        Assert.Warn($"Current Everything error code: {lastError}");
        Thread.Sleep(200);
      }

      stopwatch.Stop();

      if (stopwatch.ElapsedMilliseconds > Timeout)
      {
        Assert.Fail("Could not start Everything process");
      }
      else
      {
        Assert.Warn($"Everything version: {EverythingState.GetVersion()}");
      }
    }
  }
}
