namespace EverythingNet.Interfaces
{
  public interface IMusicQueryable : IQueryable
  {
    IQuery Album(string album);

    IQuery Artist(string artist);

    IQuery Genre(string genre);

    IQuery Title(string title);

    // TODO: Add a way to nicely support constraints on track value (<, >, between)
    IQuery Track(int? track);

    IQuery Comment(string comment);
  }
}