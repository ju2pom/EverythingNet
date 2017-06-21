using System.Collections.Generic;
using EverythingNet.Interfaces;

namespace EverythingNet.Query
{
  internal class LogicalQuery : Query
  {
    private readonly string logicalOperator;

    public LogicalQuery(IEverythingInternal everything, IQueryGenerator parent, string logicalOperator)
      : base(everything, parent)
    {
      this.logicalOperator = logicalOperator;
    }

    public override IEnumerable<string> GetQueryParts()
    {
      List<string> query = new List<string>();

      query.AddRange(base.GetQueryParts());
      query.Add(logicalOperator);

      return query;
    }
  }
}