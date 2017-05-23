using System;
using System.Collections.Generic;
using System.Text;
using EverythingNet.Interfaces;

namespace EverythingNet.Core
{
  public class Everything : IEverything, IEverythingInternal, IDisposable
  {
    private readonly uint replyId;

    public Everything()
    {
      this.replyId = Convert.ToUInt32(this.GetHashCode());
      if (!EverythingState.IsStarted())
      {
        throw new InvalidOperationException("Everything service must be started");
      }
    }

    public bool MatchCase { get; set; }

    public bool MatchPath { get; set; }

    public bool MatchWholeWord { get; set; }

    public ErrorCode LastErrorCode { get; set; }

    public IQuery Search(bool wait)
    {
      return new Query.Query(this);

    }

    IEnumerable<string> IEverythingInternal.SendSearch(string searchPattern)
    {
      using (EverythingWrapper.Lock())
      {
        EverythingWrapper.Everything_SetReplyID(this.replyId);

        EverythingWrapper.Everything_SetMatchWholeWord(this.MatchWholeWord);
        EverythingWrapper.Everything_SetMatchPath(this.MatchPath);
        EverythingWrapper.Everything_SetMatchCase(this.MatchCase);

        EverythingWrapper.Everything_SetSearchA(searchPattern);
        EverythingWrapper.Everything_QueryA(true);

        this.LastErrorCode = this.GetError();

        return this.GetResults();
      }
    }

    private IEnumerable<string> GetResults()
    {
      var builder = new StringBuilder(260);
      var numResults = EverythingWrapper.Everything_GetNumResults();

      for (var i = 0; i < numResults; i++)
      {
        EverythingWrapper.Everything_GetResultFullPathNameW(i, builder, 260);

        yield return builder.ToString();
      }
    }

    public void Dispose()
    {
      EverythingWrapper.Everything_Reset();
    }

    private ErrorCode GetError()
    {
      var error = EverythingWrapper.Everything_GetLastError();

      return (ErrorCode) error;
    }
  }
}