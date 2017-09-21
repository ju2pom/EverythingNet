namespace EverythingNet.Query
{
  using System.Collections.Generic;
  using System.Linq;

  using EverythingNet.Interfaces;

  using IQueryable = Interfaces.IQueryable;

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

    public IQuery Files => new LogicalQuery(this.everything, this, "files:");

    public IQuery Folders => new LogicalQuery(this.everything, this, "folders:");

    public IQuery NoSubFolder => new LogicalQuery(this.everything, this, "nosubfolders:");

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

    public IImageQueryable Image()
    {
      return new ImageQueryable(this.everything, this);
    }

    public IQueryable Queryable(IQueryable queryable)
    {
      ((Queryable)queryable).SetParent(this);

      return queryable;
    }
  }
}