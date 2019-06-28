namespace EverythingNet.Query
{
  using System.Collections.Generic;

  using EverythingNet.Interfaces;

  internal class RawQuery : Query, IRawQuery
  {
    private readonly string rawQuery;

    public RawQuery(IQueryGenerator parent, string rawQuery)
      : base(parent)
    {
      this.rawQuery = rawQuery;
    }

    public override IEnumerable<string> GetQueryParts()
    {
      foreach (var queryPart in base.GetQueryParts())
      {
        yield return queryPart;
      }

      if (!string.IsNullOrEmpty(this.rawQuery))
      {
        yield return this.rawQuery;
      }
    }
  }
}