using EverythingNet.Core;

namespace EverythingNet.Extensions
{
  public static class LogicSearch
  {
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
  }
}
