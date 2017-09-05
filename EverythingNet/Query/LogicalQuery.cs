namespace EverythingNet.Query
{
  using System.Collections.Generic;

  using EverythingNet.Interfaces;

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
      query.Add(this.logicalOperator);

      return query;
    }
  }
}