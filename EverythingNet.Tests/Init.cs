using System.Diagnostics;
using System.IO;
using System.Reflection;
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
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      while (!EverythingState.IsReady() && stopwatch.ElapsedMilliseconds < Timeout)
      {
        Thread.Sleep(200);
      }

      stopwatch.Stop();

      if (stopwatch.ElapsedMilliseconds > Timeout)
      {
        string path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        string exePath = Path.GetFullPath(Path.Combine(path, @"Everything.exe"));
        Process.Start(exePath, "-startup");

        Assert.Warn($"Everything version: {EverythingState.GetVersion()}");
      }
    }
  }
}
