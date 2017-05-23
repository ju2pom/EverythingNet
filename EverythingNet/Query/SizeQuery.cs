using System.Collections.Generic;
using EverythingNet.Core;
using EverythingNet.Interfaces;

namespace EverythingNet.Query
{
  internal class SizeQuery : Queryable
  {
    private readonly string sizePattern;

    public SizeQuery(IEverythingInternal everything, IQueryGenerator parent, string sizePattern)
      : base(everything, parent)
    {
      this.sizePattern = sizePattern;
    }

    public override IEnumerable<string> GetQueryParts()
    {
      foreach (var queryPart in base.GetQueryParts())
      {
        yield return queryPart;
      }

      yield return $"{ this.sizePattern}b";
    }
  }
}