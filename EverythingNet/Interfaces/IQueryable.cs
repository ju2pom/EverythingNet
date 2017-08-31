namespace EverythingNet.Interfaces
{
  using System.Collections.Generic;

  public interface IQueryable : IEnumerable<ISearchResult>
  {
    IQuery And { get; }

    IQuery Or { get; }
  }
}