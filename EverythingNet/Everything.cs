namespace EverythingNet
{
  using System;
  using System.Collections.Generic;
  using System.Runtime.InteropServices;
  using System.Text;

  public class Everything : IEverything
  {
    public string SearchText
    {
      get
      {
        IntPtr ptr = EverythingWrapper.Everything_GetSearchA();

        return Marshal.PtrToStringAnsi(ptr);
      }

      set
      {
        EverythingWrapper.Everything_SetSearchA(value);
      }
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

    public ErrorCode Search(IntPtr handle, bool wait)
    {
      if (!wait && handle != IntPtr.Zero)
      {
        EverythingWrapper.Everything_SetReplyWindow(handle);
      }

      EverythingWrapper.Everything_QueryA(wait);

      return this.GetError();
    }

    public SearchResult DisposeSearch(IntPtr handle, bool wait)
    {
      if (!wait && handle != IntPtr.Zero)
      {
        EverythingWrapper.Everything_SetReplyWindow(handle);
      }

      EverythingWrapper.Everything_QueryA(wait);

      return new SearchResult(this.GetError());
    }

    public IEnumerable<string> GetResults()
    {
      //int totResults = EverythingWrapper.Everything_GetTotResults();


      StringBuilder builder = new StringBuilder(260);
      int numResults = EverythingWrapper.Everything_GetNumResults();

      for (int i = 0; i < numResults; i++)
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
      int error = EverythingWrapper.Everything_GetLastError();

      return (ErrorCode)error;
    }
  }
}
