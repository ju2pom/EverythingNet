using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EverythingNet.Interfaces;

namespace EverythingNet.Core
{
  public class Everything : IEverything, IEverythingInternal, IDisposable
  {
    private readonly uint replyId;

    public Everything()
    {
      this.ResulKind = ResultKind.Both;
      this.replyId = Convert.ToUInt32(this.GetHashCode());
      if (!EverythingState.IsStarted())
      {
        throw new InvalidOperationException("Everything service must be started");
      }
    }

    public ResultKind ResulKind { get; set; }

    public bool MatchCase { get; set; }

    public bool MatchPath { get; set; }

    public bool MatchWholeWord { get; set; }

    public ErrorCode LastErrorCode { get; set; }

    public IQuery Search()
    {
      return new Query.Query(this);

    }

    IEnumerable<ISearchResult> IEverythingInternal.SendSearch(string searchPattern)
    {
      using (EverythingWrapper.Lock())
      {
        EverythingWrapper.Everything_SetReplyID(this.replyId);

        EverythingWrapper.Everything_SetMatchWholeWord(this.MatchWholeWord);
        EverythingWrapper.Everything_SetMatchPath(this.MatchPath);
        EverythingWrapper.Everything_SetMatchCase(this.MatchCase);
        EverythingWrapper.Everything_SetRequestFlags((uint)
          (RequestFlags.EVERYTHING_REQUEST_SIZE
          |RequestFlags.EVERYTHING_REQUEST_FILE_NAME
          |RequestFlags.EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME));
        searchPattern = this.ApplySearchResultKind(searchPattern);
        EverythingWrapper.Everything_SetSearchA(searchPattern);
        EverythingWrapper.Everything_QueryA(true);

        this.LastErrorCode = this.GetError();

        return this.GetResults();
      }
    }

    private string ApplySearchResultKind(string searchPatten)
    {
      switch(this.ResulKind)
      {
        case ResultKind.FilesOnly:
          return $"files: {searchPatten}";
        case ResultKind.FoldersOnly:
          return $"folders: {searchPatten}";
        default:
          return searchPatten;
      }
    }

    private IEnumerable<ISearchResult> GetResults()
    {
      var numResults = EverythingWrapper.Everything_GetNumResults();

      return Enumerable.Range(0, numResults).Select(x => new SearchResult(x));
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