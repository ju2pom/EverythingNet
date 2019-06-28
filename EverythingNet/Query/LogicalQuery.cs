namespace EverythingNet.Query
{
  using System.Collections.Generic;
  using System.Linq;

  using EverythingNet.Interfaces;

  internal class LogicalQuery : Query
  {
    private readonly string logicalOperator;

    public LogicalQuery(IQueryGenerator parent, string logicalOperator)
      : base(parent)
    {
      this.logicalOperator = logicalOperator;
    }

    public override IEnumerable<string> GetQueryParts()
    {
      return base.GetQueryParts().Union(new[] { this.logicalOperator });
    }
  }
}