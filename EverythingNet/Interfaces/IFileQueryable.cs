namespace EverythingNet.Interfaces
{
  public interface IFileQueryable
  {
    IQueryable Only();

    IQueryable Audio(string search = null);

    IQueryable Zip(string search = null);

    IQueryable Video(string search = null);

    IQueryable Picture(string search = null);

    IQueryable Exe(string search = null);

    IQueryable Document(string search = null);

    IQueryable Duplicates(string search = null);
  }
}