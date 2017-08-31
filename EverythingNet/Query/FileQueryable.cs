namespace EverythingNet.Query
{
  using System.Collections.Generic;

  using EverythingNet.Interfaces;

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