using System;
using System.Collections.Generic;
using System.Text;

namespace EverythingNet.Core
{
  public class SearchResult : ISearchResult, IDisposable
  {
    public SearchResult(ErrorCode getError)
    {
      this.ErrorCode = getError;
    }

    public ErrorCode ErrorCode { get; }

    public IEnumerable<string> GetResults()
    {
      StringBuilder builder = new StringBuilder(260);
      int numResults = EverythingWrapper.Everything_GetNumResults();

      for (int i = 0; i < numResults; i++)
      {
        EverythingWrapper.Everything_GetResultFullPathNameW(i, builder, 260);

        yield return builder.ToString();
      }
    }

    public void Dispose()
    {
      EverythingWrapper.Everything_Reset();
    }
  }
}
