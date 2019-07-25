namespace EverythingNet.Query
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using EverythingNet.Interfaces;

  internal class NameQueryable : Queryable, INameQueryable
  {
    private string pattern;
    private string startWith;
    private string endWith;
    private string extensions;
    private string paths;

    public NameQueryable(Query parent)
      : base(parent)
    {
      this.Flags = RequestFlags.EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME;
    }

    public IQuery Contains(string contains)
    {
      this.pattern = this.QuoteIfNeeded(contains);

      return new Query(this);
    }

    public IQuery Path(string path)
    {
      this.paths = this.QuoteIfNeeded(path);

      return new Query(this);
    }

    public IQuery Paths(IEnumerable<string> paths)
    {
      this.paths = $"<{string.Join("|", paths.Select(this.QuoteIfNeeded))}>";

      return new Query(this);
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

      if (!string.IsNullOrEmpty(this.paths))
      {
        yield return $"path:{this.paths}";
      }
    }

    public IQuery StartWith(string pattern)
    {
      this.startWith = this.QuoteIfNeeded(pattern);

      return new Query(this);
    }

    public IQuery EndWith(string pattern)
    {
      this.endWith = this.QuoteIfNeeded(pattern);

      return new Query(this);
    }

    public IQuery Extension(string extension)
    {
      if (extension.Contains("."))
      {
        throw new ArgumentException("Do not specify the dot character when specifying an extension");
      }

      this.extensions = string.IsNullOrEmpty(this.extensions)
        ? extension
        : $"{this.extensions};{extension}";

      return new Query(this);
    }

    public IQuery Extensions(IEnumerable<string> newExtensions)
    {
      return this.ExtensionCollection(newExtensions);
    }

    public IQuery Extensions(params string[] newExtensions)
    {
      return this.ExtensionCollection(newExtensions);
    }

    private IQuery ExtensionCollection(IEnumerable<string> newExtensions)
    {
      if (newExtensions == null)
      {
        throw new ArgumentNullException(nameof(newExtensions));
      }

      if (!newExtensions.Any())
      {
        throw new ArgumentException("The list of file extension must not be empty");
      }

      if (newExtensions.Any(x => x.Contains(".")))
      {
        throw new ArgumentException("Do not specify the dot character when specifying an extension");
      }

      return this.Extension(string.Join(";", newExtensions));
    }
  }
}