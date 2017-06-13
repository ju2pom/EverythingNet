using System;
using System.Linq;
using System.Collections.Generic;

using EverythingNet.Core;
using EverythingNet.Interfaces;
using IQueryable = EverythingNet.Interfaces.IQueryable;

namespace EverythingNet.Query
{
  internal class NameQueryable : Queryable, INameQueryable
  {
    private readonly string pattern;

    private string startWith;
    private string endWith;

    public NameQueryable(IEverythingInternal everything, IQueryGenerator parent)
      : this(everything, parent, null)
    {
    }

    public NameQueryable(IEverythingInternal everything, IQueryGenerator parent, string pattern)
      : base(everything, parent)
    {
      this.pattern = this.QuoteIfNeeded(pattern);
    }

    public override IEnumerable<string> GetQueryParts()
    {
      foreach (var queryPart in base.GetQueryParts())
      {
        yield return queryPart;
      }

      if (!string.IsNullOrEmpty(this.startWith))
      {
        yield return $"startwith:{this.startWith}";
      }

      if (!string.IsNullOrEmpty(this.pattern))
      {
        yield return this.pattern;
      }

      if (!string.IsNullOrEmpty(this.endWith))
      {
        yield return $"endwith:{this.endWith}";
      }
    }

    public INameQueryable StartWith(string pattern)
    {
      this.startWith = this.QuoteIfNeeded(pattern);

      return this;
    }

    public INameQueryable EndWith(string pattern)
    {
      this.endWith = this.QuoteIfNeeded(pattern);

      return this;
    }

    private string QuoteIfNeeded(string text)
    {
      if (text == null)
      {
        return String.Empty;
      }

      if (text.Contains(" ") && text.First() != '\"' && text.Last() != '\"')
      {
        return $"\"{text}\"";
      }

      return text;
    }
  }

  public interface INameQueryable : IQueryable
  {
    INameQueryable StartWith(string pattern);

    INameQueryable EndWith(string pattern);
  }
}