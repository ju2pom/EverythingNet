namespace EverythingNet.Interfaces
{
  public interface IFileQueryable : IQueryable
  {
    IQuery Roots();

    IQuery Parent(string parentFolder);

    IQuery Audio(string search = null);

    IQuery Zip(string search = null);

    IQuery Video(string search = null);

    IQuery Picture(string search = null);

    IQuery Exe(string search = null);

    IQuery Document(string search = null);

    IQuery Duplicates(string search = null);
  }
}