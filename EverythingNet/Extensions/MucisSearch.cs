using EverythingNet.Core;

namespace EverythingNet.Extensions
{
  public static class MucisSearch
  {
    public static IEverything Album(this IEverything everything, string album = null)
    {
      return Search(everything, $"album:{album ?? string.Empty}");
    }

    public static IEverything Artist(this IEverything everything, string artist = null)
    {
      return Search(everything, $"artist:{artist ?? string.Empty}");
    }

    public static IEverything Genre(this IEverything everything, string genre = null)
    {
      return Search(everything, $"genre:{genre ?? string.Empty}");
    }

    public static IEverything Title(this IEverything everything, string title = null)
    {
      return Search(everything, $"title:{title ?? string.Empty}");
    }

    public static IEverything Track(this IEverything everything, int track = -1)
    {
      return Search(everything, track >= 0 ? $"track:{track}" : "track:");
    }

    public static IEverything Comment(this IEverything everything, string comment = null)
    {
      return Search(everything, $"comment:{comment ?? string.Empty}");
    }

    private static IEverything Search(IEverything everything, string search)
    {
      everything.SearchText += search;

      return everything;
    }
  }
}
