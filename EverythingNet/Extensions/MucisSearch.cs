using EverythingNet.Core;

namespace EverythingNet.Extensions
{
  public static class MucisSearch
  {
    public static IEverything SearchAlbumSync(this IEverything everything, string album)
    {
      everything.SearchText += $"album:{album}";

      return everything;
    }

    public static IEverything SearchArtistSync(this IEverything everything, string artist)
    {
      everything.SearchText += $"artist:{artist}";

      return everything;
    }

    public static IEverything SearchGenre(this IEverything everything, string genre)
    {
      everything.SearchText += $"genre:{genre}";

      return everything;
    }

    public static IEverything SearchTitle(this IEverything everything, string title)
    {
      everything.SearchText += $"title:{title}";

      return everything;
    }

    public static IEverything SearchComment(this IEverything everything, string comment)
    {
      everything.SearchText += $"comment:{comment}";

      return everything;
    }
  }
}
