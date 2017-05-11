using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EverythingNet.Core
{
  public class Everything : IEverything
  {
    public string SearchText
    {
      get
      {
        var ptr = EverythingWrapper.Everything_GetSearchA();

        return Marshal.PtrToStringAnsi(ptr);
      }

      set { EverythingWrapper.Everything_SetSearchA(value); }
    }

    public bool MatchCase
    {
      get { return EverythingWrapper.Everything_GetMatchCase(); }

      set { EverythingWrapper.Everything_SetMatchCase(value); }
    }

    public bool MatchPath
    {
      get { return EverythingWrapper.Everything_GetMatchPath(); }

      set { EverythingWrapper.Everything_SetMatchPath(value); }
    }

    public bool MatchWholeWord
    {
      get { return EverythingWrapper.Everything_GetMatchWholeWord(); }

      set { EverythingWrapper.Everything_SetMatchWholeWord(value); }
    }

    public ErrorCode Search(bool wait)
    {
      return this.Search(wait, IntPtr.Zero);
    }

    public ErrorCode Search(bool wait, IntPtr handle)
    {
      if (!wait && handle != IntPtr.Zero)
        EverythingWrapper.Everything_SetReplyWindow(handle);

      EverythingWrapper.Everything_QueryA(wait);

      return GetError();
    }

    public SearchResult DisposeSearch(IntPtr handle, bool wait)
    {
      if (!wait && handle != IntPtr.Zero)
        EverythingWrapper.Everything_SetReplyWindow(handle);

      EverythingWrapper.Everything_QueryA(wait);

      return new SearchResult(GetError());
    }

    public IEnumerable<string> GetResults()
    {
      //int totResults = EverythingWrapper.Everything_GetTotResults();

      var builder = new StringBuilder(260);
      var numResults = EverythingWrapper.Everything_GetNumResults();

      for (var i = 0; i < numResults; i++)
      {
        EverythingWrapper.Everything_GetResultFullPathNameW(i, builder, 260);

        yield return builder.ToString();
      }
    }

    public void CleanUp()
    {
      EverythingWrapper.Everything_Reset();
    }

    public ErrorCode GetError()
    {
      var error = EverythingWrapper.Everything_GetLastError();

      return (ErrorCode) error;
    }
  }
}