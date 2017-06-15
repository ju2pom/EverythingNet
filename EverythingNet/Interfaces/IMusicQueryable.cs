namespace EverythingNet.Interfaces
{
  public interface IMusicQueryable
  {
    IMusicQueryable Album(string album);

    IMusicQueryable Artist(string artist);

    IMusicQueryable Genre(string genre);

    IMusicQueryable Title(string title);

    IMusicQueryable Track(int track);

    IMusicQueryable Comment(string comment);
  }
}