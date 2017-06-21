using System.Collections.Generic;

namespace EverythingNet.Interfaces
{
  public interface INameQueryable : IQueryable
  {
    INameQueryable StartWith(string pattern);

    INameQueryable EndWith(string pattern);

    IQueryable Extension(string extension);

    IQueryable Extensions(IEnumerable<string> extensions);
  }
}