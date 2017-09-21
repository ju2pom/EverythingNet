namespace EverythingNet.Interfaces
{
  public interface IFileQueryable : IQueryable
  {
    IQueryable Only();

    IQueryable Roots();

    IQueryable Parent(string parentFolder);

    IQueryable Audio(string search = null);

    IQueryable Zip(string search = null);

    IQueryable Video(string search = null);

    IQueryable Picture(string search = null);

    IQueryable Exe(string search = null);

    IQueryable Document(string search = null);

    IQueryable Duplicates(string search = null);
  }
}