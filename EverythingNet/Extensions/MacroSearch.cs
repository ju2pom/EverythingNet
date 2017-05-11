using EverythingNet.Core;

namespace EverythingNet.Extensions
{
  public static class MacroSearch
  {
    public static IEverything Audio(this IEverything everything)
    {
      everything.SearchText += "audio:";

      return everything;
    }

    public static IEverything Zip(this IEverything everything)
    {
      everything.SearchText += "zip:";

      return everything;
    }

    public static IEverything Video(this IEverything everything)
    {
      everything.SearchText += "video:";

      return everything;
    }

    public static IEverything Picture(this IEverything everything)
    {
      everything.SearchText += "pic:";

      return everything;
    }

    public static IEverything Exe(this IEverything everything)
    {
      everything.SearchText += "exe:";

      return everything;
    }

    public static IEverything Document(this IEverything everything)
    {
      everything.SearchText += "doc:";

      return everything;
    }
  }
}
