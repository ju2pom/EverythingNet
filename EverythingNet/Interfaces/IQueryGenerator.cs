namespace EverythingNet.Interfaces
{
  using System.Collections.Generic;

  internal interface IQueryGenerator
  {
    RequestFlags Flags { get; }

    IEnumerable<string> GetQueryParts();
  }
}