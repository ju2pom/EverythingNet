using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EverythingNet.Core;
using EverythingNet.Interfaces;
using IQueryable = EverythingNet.Interfaces.IQueryable;

namespace EverythingNet.Query
{
  internal abstract class Queryable : IQueryable, IQueryGenerator
  {
    private readonly IEverythingInternal everything;
    private readonly IQueryGenerator parent;

    protected Queryable(IEverythingInternal everything, IQueryGenerator parent)
    {
      this.everything = everything;
      this.parent = parent;
    }

    public override string ToString()
    {
      return string.Join("", this.GetQueryParts());
    }

    public IEnumerator<string> GetEnumerator()
    {
      var search = this.everything.SendSearch(string.Join("", this.GetQueryParts()));

      return search.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public IQuery And => new LogicalQuery(this.everything, this, " ");

    public IQuery Or => new LogicalQuery(this.everything, this, "|");

    public virtual IEnumerable<string> GetQueryParts()
    {
      return this.parent?.GetQueryParts() ?? Enumerable.Empty<string>();
    }
  }
}