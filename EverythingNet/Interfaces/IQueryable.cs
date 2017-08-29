using System.Collections.Generic;

namespace EverythingNet.Interfaces
{
  public interface IQueryable : IEnumerable<ISearchResult>
  {
    IQuery And { get; }

    IQuery Or { get; }
  }
}