using System.Collections.Generic;
using EverythingNet.Interfaces;

namespace EverythingNet.Core
{
  public interface ISearchResult
  {
    ErrorCode ErrorCode { get; }

    IEnumerable<string> Results { get; }
  }
}