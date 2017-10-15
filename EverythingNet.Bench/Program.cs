namespace EverythingNet.Bench
{
  using System.Reflection;

  using BenchmarkDotNet.Running;

  internal class Program
  {
    private static void Main(string[] args)
    {
      BenchmarkSwitcher.FromAssembly(Assembly.GetCallingAssembly()).RunAll();
    }
  }
}
