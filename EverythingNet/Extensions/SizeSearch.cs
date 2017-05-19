using System.Runtime.CompilerServices;
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

    public static IEverything Kb(this IEverything everything)
    {
      return everything.Unit(SizeUnit.Kb);
    }

    public static IEverything Mb(this IEverything everything)
    {
      return everything.Unit(SizeUnit.Mb);
    }

    public static IEverything Gb(this IEverything everything)
    {
      return everything.Unit(SizeUnit.Gb);
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
  }
}
