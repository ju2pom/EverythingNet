namespace EverythingNet.Interfaces
{
  using System.Collections.Generic;

  public interface INameQueryable : IQueryable
  {
    IQuery Contains(string pattern);

    IQuery StartWith(string pattern);

    IQuery EndWith(string pattern);

    IQuery Extension(string extension);

    IQuery Extensions(IEnumerable<string> extensions);

    IQuery Extensions(params string[] extensions);
  }
}