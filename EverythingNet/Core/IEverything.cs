using System;
using System.Collections.Generic;

namespace EverythingNet.Core
{
  public enum ErrorCode
  {
    Ok = 0,
    Memory,
    Ipc,
    RegisterClassEX,
    CreateWindow,
    CreateThread,
    InvalidIndex,
    Invalidcall,
  }

  public interface IEverything
  {
    string SearchText { get; set; }

    bool MatchCase { get; set; }

    bool MatchPath { get; set; }

    bool MatchWholeWord { get; set; }

    ErrorCode Search(bool wait);

    ErrorCode Search(bool wait, IntPtr handle);

    SearchResult DisposeSearch(IntPtr handle, bool wait);

    ErrorCode GetError();

    IEnumerable<string> GetResults();
  }
}
