namespace EverythingNet.Query
{
  using System.Collections.Generic;
  using System.Linq;

  using EverythingNet.Interfaces;

  using IQueryable = Interfaces.IQueryable;

  internal abstract class Queryable : IQueryable, IQueryGenerator
  {
    private IQueryGenerator parent;

    protected Queryable(IQueryGenerator parent)
    {
      this.parent = parent;
      this.IsFast = true;
    }

    public bool IsFast { get; protected set; }

    public IQuery And => new LogicalQuery(this, " ");

    public IQuery Or => new LogicalQuery(this, "|");

    public RequestFlags Flags { get; protected set; }

    public override string ToString()
    {
      return string.Join("", this.GetQueryParts());
    }

    public virtual IEnumerable<string> GetQueryParts()
    {
      return this.parent?.GetQueryParts() ?? Enumerable.Empty<string>();
    }

    protected string QuoteIfNeeded(string text)
    {
      if (text == null)
      {
        return string.Empty;
      }

      if (text.Contains(" ") && text.First() != '\"' && text.Last() != '\"')
      {
        return $"\"{text}\"";
      }

      return text;
    }

    internal IQueryable FromParent(IQueryGenerator onTheFlyparent)
    {
      this.parent = onTheFlyparent;

      return this;
    }
  }
}