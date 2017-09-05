namespace EverythingNet.Interfaces
{
  using System.Collections.Generic;

  internal interface IQueryGenerator
  {
    IEnumerable<string> GetQueryParts();
  }
}