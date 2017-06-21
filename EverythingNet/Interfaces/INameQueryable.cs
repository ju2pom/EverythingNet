namespace EverythingNet.Interfaces
{
  public interface INameQueryable : IQueryable
  {
    INameQueryable StartWith(string pattern);

    INameQueryable EndWith(string pattern);
  }
}