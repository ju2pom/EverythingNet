namespace EverythingNet.Query
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using EverythingNet.Interfaces;

  using IQueryable = EverythingNet.Interfaces.IQueryable;

  internal class NameQueryable : Queryable, INameQueryable
  {
    private readonly string pattern;

    private string startWith;
    private string endWith;
    private string extensions;

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

      if (!string.IsNullOrEmpty(this.extensions))
      {
        yield return $"ext:{this.extensions}";
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

    public IQueryable Extension(string extension)
    {
      if (extension.Contains("."))
      {
        throw new ArgumentException("Do not specify the dot character when specifying an extension");
      }

      this.extensions = string.IsNullOrEmpty(this.extensions)
        ? extension
        : $"{this.extensions};{extension}";

      return this;
    }

    public IQueryable Extensions(IEnumerable<string> newExtensions)
    {
      if (newExtensions == null)
      {
        throw new ArgumentNullException(nameof(newExtensions));
      }

      if (!newExtensions.Any())
      {
        throw new ArgumentException("The list of exceptions must not be empty");
      }

      if (newExtensions.Any(x => x.Contains(".")))
      {
        throw new ArgumentException("Do not specify the dot character when specifying an extension");
      }

      return this.Extension(string.Join(";", newExtensions));
    }
  }
}