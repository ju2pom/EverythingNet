using System.Collections.Generic;

namespace EverythingNet.Interfaces
{
  public interface IQueryable : IEnumerable<string>
  {
    IQuery And { get; }

    IQuery Or { get; }
  }
}