using System.Collections.Generic;
using System.Linq;
using EverythingNet.Core;
using EverythingNet.Interfaces;

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

    public IQuery Not => new LogicalQuery(this.everything, this, "!");

    public virtual IEnumerable<string> GetQueryParts()
    {
      return this.parent?.GetQueryParts() ?? Enumerable.Empty<string>();
    }

    public INameQueryable Name()
    {
      return new NameQueryable(this.everything, this);
    }

    public INameQueryable Name(string namePattern)
    {
      return new NameQueryable(this.everything, this, namePattern);
    }

    public ISizeQueryable Size()
    {
      return new SizeQueryable(this.everything, this);
    }

    public IDateQueryable CreationDate()
    {
      return new DateQueryable(this.everything, this, "dc:");
    }

    public IDateQueryable ModificationDate()
    {
      return new DateQueryable(this.everything, this, "dm:");
    }

    public IDateQueryable AccessDate()
    {
      return new DateQueryable(this.everything, this, "da:");
    }

    public IDateQueryable RunDate()
    {
      return new DateQueryable(this.everything, this, "dr:");
    }

    public IMusicQueryable Music()
    {
      return new MusicQueryable(this.everything, this);
    }

    public IFileQueryable File()
    {
      return new FileQueryable(this.everything, this);
    }
  }
}