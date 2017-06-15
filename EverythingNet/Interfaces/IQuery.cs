using EverythingNet.Query;

namespace EverythingNet.Interfaces
{
  public interface IQuery
  {
    IQuery Not { get; }

    INameQueryable Name();

    INameQueryable Name(string namePattern);

    ISizeQueryable Size();

    IDateQueryable CreationDate();

    IDateQueryable ModificationDate();

    IDateQueryable AccessDate();

    IDateQueryable RunDate();

    IMusicQueryable Music();
  }
}
