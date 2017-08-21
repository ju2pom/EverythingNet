using EverythingNet.Core;
using System.Collections.Generic;

namespace EverythingNet.Interfaces
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
    ResultKind ResulKind { get; set; }

    bool MatchCase { get; set; }

    bool MatchPath { get; set; }

    bool MatchWholeWord { get; set; }

    IQuery Search();
  }

  internal interface IEverythingInternal
  {
    IEnumerable<string> SendSearch(string searchPattern);
  }
}
