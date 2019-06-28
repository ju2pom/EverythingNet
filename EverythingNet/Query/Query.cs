namespace EverythingNet.Query
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using EverythingNet.Interfaces;

  internal class QueryHolder : Query
  {
    private readonly IQueryGenerator child;

    public QueryHolder(IQueryGenerator parent, IQueryGenerator child)
      : base(parent)
    {
      this.child = child ?? throw new ArgumentNullException(nameof(child));
    }

    public override IEnumerable<string> GetQueryParts()
    {
      return base.GetQueryParts().Union(this.child.GetQueryParts());
    }
  }

  public class Query : IQuery, IQueryGenerator
  {
    // Should find a way to set the parent as readonly
    private readonly IQueryGenerator parent;
    private RequestFlags flags;

    public Query()
    {
      this.parent = null;
    }

    internal Query(IQueryGenerator parent)
    {
      this.parent = parent;
    }

    public ILogicalQuery And => new LogicalQuery(this, " ");
    public ILogicalQuery Or => new LogicalQuery(this, "|");
    public IQuery Not => new LogicalQuery(this, "!");
    public IQuery Files => new LogicalQuery(this, "files:");
    public IQuery Folders => new LogicalQuery(this, "folders:");
    public IQuery NoSubFolder => new LogicalQuery(this, "nosubfolders:");

    public INameQueryable Name => new NameQueryable(this);
    public ISizeQueryable Size => new SizeQueryable(this);
    public IDateQueryable CreationDate => new DateQueryable(this, "dc:");
    public IDateQueryable ModificationDate => new DateQueryable(this, "dm:");
    public IDateQueryable AccessDate => new DateQueryable(this, "da:");
    public IDateQueryable RunDate => new DateQueryable(this, "dr:");
    public IMusicQueryable Music => new MusicQueryable(this);
    public IFileQueryable File => new FileQueryable(this);
    public IImageQueryable Image => new ImageQueryable(this);
    public IQuery That(IQuery query) => new QueryHolder(this, query as IQueryGenerator);
    public IQuery That(string query) => new RawQuery(this, query);

    public RequestFlags Flags
    {
      get => this.parent != null ? this.parent.Flags | this.flags : this.flags;

      protected set => this.flags = value;
    }

    public override string ToString()
    {
      return string.Join("", this.GetQueryParts());
    }

    public virtual IEnumerable<string> GetQueryParts()
    {
      return this.parent?.GetQueryParts() ?? Enumerable.Empty<string>();
    }
  }
}