using System.Collections.Generic;
using EverythingNet.Core;
using EverythingNet.Interfaces;

namespace EverythingNet.Query
{
  internal class NameQuery : Queryable
  {
    private readonly string namePattern;

    public NameQuery(IEverythingInternal everything, IQueryGenerator parent, string namePattern)
      : base(everything, parent)
    {
      this.namePattern = namePattern;
    }

    public override IEnumerable<string> GetQueryParts()
    {
      foreach (var queryPart in base.GetQueryParts())
      {
        yield return queryPart;
      }

      yield return this.namePattern;
    }
  }
}