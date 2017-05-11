using EverythingNet.Core;

namespace EverythingNet.Extensions
{
  public static class SizeSearch
  {
    public enum SizeStandard
    {
      Empty,
      Unknown,
      Tiny,
      Small,
      Medium,
      Large,
      Huge,
      Gigantic,
    }

    public enum SizeUnit
    {
      Kb,
      Mb,
      Gb,
    }

    public static IEverything Size(this IEverything everything)
    {
      everything.SearchText += "size:";

      return everything;
    }

    public static IEverything Unit(this IEverything everything, SizeUnit unit)
    {
      everything.SearchText += unit.ToString().ToLower();

      return everything;
    }

    public static IEverything Standard(this IEverything everything, SizeStandard size)
    {
      everything.SearchText += size.ToString().ToLower();

      return everything;
    }

    public static IEverything Between(this IEverything everything, int min, int max, SizeUnit unit)
    {
      string u = unit.ToString().ToLower();

      everything.SearchText += $"{min}{u}-{max}{u}";

      return everything;
    }
  }
}
