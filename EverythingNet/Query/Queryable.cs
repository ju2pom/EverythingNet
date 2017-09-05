namespace EverythingNet.Query
{
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;

  using EverythingNet.Interfaces;

  using IQueryable = EverythingNet.Interfaces.IQueryable;

  internal abstract class Queryable : IQueryable, IQueryGenerator
  {
    private readonly IEverythingInternal everything;
    private IQueryGenerator parent;

    protected Queryable(IEverythingInternal everything, IQueryGenerator parent)
    {
      this.everything = everything;
      this.parent = parent;
    }

    public IQuery And => new LogicalQuery(this.everything, this, " ");

    public IQuery Or => new LogicalQuery(this.everything, this, "|");

    public override string ToString()
    {
      return string.Join("", this.GetQueryParts());
    }

    public IEnumerator<ISearchResult> GetEnumerator()
    {
      var search = this.everything.SendSearch(string.Join("", this.GetQueryParts()));

      return search.GetEnumerator();
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

    internal void SetParent(IQueryGenerator onTheFlyparent)
    {
      this.parent = onTheFlyparent;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}