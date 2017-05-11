using System.Collections.Generic;

namespace EverythingNet.Core
{
  public interface ISearchResult
  {
    ErrorCode ErrorCode { get; }

    IEnumerable<string> GetResults();
  }
}