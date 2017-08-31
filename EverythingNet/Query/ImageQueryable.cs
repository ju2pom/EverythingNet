namespace EverythingNet.Query
{
  using System.Collections.Generic;

  using EverythingNet.Interfaces;

  internal class ImageQueryable : Queryable, IImageQueryable
  {
    private string pattern;

    public ImageQueryable(IEverythingInternal everything, IQueryGenerator parent)
      : base(everything, parent)
    {
    }

    public IQueryable Width(int width)
    {
      return this.Search($"width:{width}");
    }

    public IQueryable Height(int height)
    {
      return this.Search($"height:{height}");
    }

    public IQueryable Portrait()
    {
      return this.Search("orienation:portrait");
    }

    public IQueryable Landscape()
    {
      return this.Search("orienation:landscape");
    }

    public IQueryable BitDepth(Bpp bpp)
    {
      return this.Search($"bitdepth:{(int)bpp}");
    }

    public override IEnumerable<string> GetQueryParts()
    {
      foreach (var queryPart in base.GetQueryParts())
      {
        yield return queryPart;
      }

      yield return this.pattern;
    }

    private IQueryable Search(string search)
    {
      this.pattern = search;

      return this;
    }
  }
}