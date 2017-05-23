using System.Collections.Generic;
using EverythingNet.Interfaces;

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
    bool MatchCase { get; set; }

    bool MatchPath { get; set; }

    bool MatchWholeWord { get; set; }

    IQuery Search(bool wait);
  }

  internal interface IEverythingInternal
  {
    IEnumerable<string> SendSearch(string searchPattern);
  }
}
