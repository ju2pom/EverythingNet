using System.Collections.Generic;
using EverythingNet.Interfaces;

namespace EverythingNet.Query
{
  internal class ImageQueryable : Queryable, IImageQueryable
  {
    private string pattern;

    public ImageQueryable(IEverythingInternal everything, IQueryGenerator parent) : base(everything, parent)
    {
    }

    public IQueryable Width(int width)
    {
      return Search($"width:{width}");
    }

    public IQueryable Height(int height)
    {
      return Search($"height:{height}");
    }

    public IQueryable Portrait()
    {
      return Search("orienation:portrait");
    }

    public IQueryable Landscape()
    {
      return Search("orienation:landscape");
    }

    public IQueryable BitDepth(Bpp bpp)
    {
      return Search($"bitdepth:{(int) bpp}");
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
      pattern = search;

      return this;
    }
  }
}