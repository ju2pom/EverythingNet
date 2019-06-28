namespace EverythingNet.Interfaces
{
  public interface ILogicalQuery
  {
    IQuery Not { get; }
    IQuery Files { get; }
    IQuery Folders { get; }
    IQuery NoSubFolder { get; }
    IQuery That(IQuery query);
    IQuery That(string query);


    INameQueryable Name { get; }
    ISizeQueryable Size { get; }
    IDateQueryable CreationDate { get; }
    IDateQueryable ModificationDate { get; }
    IDateQueryable AccessDate { get; }
    IDateQueryable RunDate { get; }
    IMusicQueryable Music { get; }
    IFileQueryable File { get; }
    IImageQueryable Image { get; }
  }

  public interface IQuery : ILogicalQuery
  {
    ILogicalQuery And { get; }
    ILogicalQuery Or { get; }
  }
}