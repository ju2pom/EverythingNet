using System;
using System.Collections.Generic;
using EverythingNet.Interfaces;

namespace EverythingNet.Core
{
  public class SearchResult : ISearchResult, IDisposable
  {
    internal SearchResult(ErrorCode getError, IEnumerable<string> results)
    {
      this.ErrorCode = getError;
      this.Results = results;
    }

    public ErrorCode ErrorCode { get; }

    public IEnumerable<string> Results { get; }

    public void Dispose()
    {
      EverythingWrapper.Everything_Reset();
    }
  }
}
