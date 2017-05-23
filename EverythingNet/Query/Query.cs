using System.Collections.Generic;
using System.Linq;
using EverythingNet.Core;
using EverythingNet.Interfaces;
using IQueryable = EverythingNet.Interfaces.IQueryable;

namespace EverythingNet.Query
{
  internal class Query : IQuery, IQueryGenerator
  {
    private readonly IEverythingInternal everything;
    private readonly IQueryGenerator parent;

    public Query(IEverythingInternal everything, IQueryGenerator parent = null)
    {
      this.everything = everything;
      this.parent = parent;
    }

    public IQueryable Name(string namePattern)
    {
      return new NameQuery(this.everything, this, namePattern);
    }

    public IQueryable Size(string sizePattern)
    {
      return new SizeQuery(this.everything, this, sizePattern);
    }

    public virtual IEnumerable<string> GetQueryParts()
    {
      return this.parent?.GetQueryParts() ?? Enumerable.Empty<string>();
    }
  }
}