using System.Collections.Generic;

namespace EverythingNet.Interfaces
{
  internal interface IQueryGenerator
  {
    IEnumerable<string> GetQueryParts();
  }
}