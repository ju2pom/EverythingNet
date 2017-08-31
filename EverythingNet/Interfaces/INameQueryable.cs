namespace EverythingNet.Interfaces
{
  using System.Collections.Generic;

  public interface INameQueryable : IQueryable
  {
    INameQueryable StartWith(string pattern);

    INameQueryable EndWith(string pattern);

    IQueryable Extension(string extension);

    IQueryable Extensions(IEnumerable<string> extensions);
  }
}