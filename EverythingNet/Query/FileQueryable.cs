using System;
using System.Linq;
using System.Collections.Generic;
using EverythingNet.Core;
using EverythingNet.Interfaces;

using IQueryable = EverythingNet.Interfaces.IQueryable;

namespace EverythingNet.Query
{
  internal class FileQueryable : Queryable, IFileQueryable
  {
    private string searchPattern;

    public FileQueryable(IEverythingInternal everything, IQueryGenerator parent)
      : base(everything, parent)
    {
    }

    public override IEnumerable<string> GetQueryParts()
    {
      foreach (var queryPart in base.GetQueryParts())
      {
        yield return queryPart;
      }

      if (!string.IsNullOrEmpty(this.searchPattern))
      {
        yield return this.searchPattern;
      }
    }

    public IQueryable Only()
    {
      return this.Macro("file:", string.Empty);
    }

    public IQueryable Audio(string search = null)
    {
      return this.Macro("audio:", search);
    }

    public IQueryable Zip(string search = null)
    {
      return this.Macro("zip:", search);
    }

    public IQueryable Video(string search = null)
    {
      return this.Macro("video:", search);
    }

    public IQueryable Picture(string search = null)
    {
      return this.Macro("pic:", search);
    }

    public IQueryable Exe(string search = null)
    {
      return this.Macro("exe:", search);
    }

    public IQueryable Document(string search = null)
    {
      return this.Macro("doc:", search);
    }

    public IQueryable Extension(string extension)
    {
      if (extension.Contains("."))
      {
        throw new ArgumentException("Do not specify the dot character when specifying an extension");
      }

      return this.Macro("ext:", extension);
    }

    public IQueryable Extensions(IEnumerable<string> extensions)
    {
      if (extensions == null)
      {
        throw new ArgumentNullException(nameof(extensions));
      }

      if (!extensions.Any())
      {
        throw new ArgumentException("The list of exceptions must not be empty");
      }

      if (extensions.Any(x => x.Contains(".")))
      {
        throw new ArgumentException("Do not specify the dot character when specifying an extension");
      }

      return this.Macro("ext:", string.Join(";", extensions));
    }

    public IQueryable Duplicates(string search = null)
    {
      return this.Macro("dupe:", search);
    }

    private IQueryable Macro(string tag, string search)
    {
      this.searchPattern = tag + this.QuoteIfNeeded(search);

      return this;
    }
  }
}
