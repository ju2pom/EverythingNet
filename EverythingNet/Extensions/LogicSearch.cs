using System;
using EverythingNet.Core;

namespace EverythingNet.Extensions
{
  public static class LogicSearch
  {
    public static IEverything Is(this IEverything everything, string value)
    {
      everything.SearchText += value;

      return everything;
    }

    public static IEverything Greater(this IEverything everything, int value)
    {
      everything.SearchText += $">{value}";

      return everything;
    }

    public static IEverything GreaterOrEqual(this IEverything everything, int value)
    {
      everything.SearchText += $">={value}";

      return everything;
    }

    public static IEverything Less(this IEverything everything, int value)
    {
      everything.SearchText += $"<{value}";

      return everything;
    }

    public static IEverything LessOrEqual(this IEverything everything, int value)
    {
      everything.SearchText += $"<={value}";

      return everything;
    }

    public static IEverything Or(this IEverything everything)
    {
      everything.SearchText += "|";

      return everything;
    }

    public static IEverything And(this IEverything everything)
    {
      everything.SearchText += " ";

      return everything;
    }

    public static IEverything Not(this IEverything everything)
    {
      everything.SearchText += "!";

      return everything;
    }

    public static IEverything Between(this IEverything everything, int min, int max, string unit = "")
    {
      everything.SearchText += $"{min}{unit}-{max}{unit}";

      return everything;
    }

    public static IEverything Between(this IEverything everything, int min, int max, Enum unit)
    {
      string u = unit.ToString().ToLower();

      everything.SearchText += $"{min}{u}-{max}{u}";

      return everything;
    }
  }
}
