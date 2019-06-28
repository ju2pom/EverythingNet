namespace EverythingNet.Core
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;

  using EverythingNet.Interfaces;

  public class Everything : IEverything
  {
    private static int lastReplyId;

    private const uint DefaultSearchFlags = (uint)(
      RequestFlags.EVERYTHING_REQUEST_SIZE
    | RequestFlags.EVERYTHING_REQUEST_FILE_NAME
    | RequestFlags.EVERYTHING_REQUEST_EXTENSION
    | RequestFlags.EVERYTHING_REQUEST_PATH
    | RequestFlags.EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME
    | RequestFlags.EVERYTHING_REQUEST_DATE_MODIFIED);

    private readonly uint replyId;

    public Everything()
    {
      this.ResulKind = ResultKind.Both;
      Interlocked.Increment(ref lastReplyId);
      this.replyId = Convert.ToUInt32(lastReplyId);
      if (!EverythingState.IsStarted())
      {
        throw new InvalidOperationException("Everything service must be started");
      }
    }

    public ResultKind ResulKind { get; set; }

    public bool MatchCase { get; set; }

    public bool MatchPath { get; set; }

    public bool MatchWholeWord { get; set; }

    public SortingKey SortKey { get; set; }

    public ErrorCode LastErrorCode { get; private set; }

    public long Count => EverythingWrapper.Everything_GetNumResults();

    public IEnumerable<ISearchResult> Search(IQuery query)
    {
      using (EverythingWrapper.Lock())
      {
        EverythingWrapper.Everything_SetReplyID(this.replyId);
        EverythingWrapper.Everything_SetMatchWholeWord(this.MatchWholeWord);
        EverythingWrapper.Everything_SetMatchPath(this.MatchPath);
        EverythingWrapper.Everything_SetMatchCase(this.MatchCase);
        EverythingWrapper.Everything_SetRequestFlags((uint)((IQueryGenerator)query).Flags | DefaultSearchFlags);
        var searchPattern = this.ApplySearchResultKind(query.ToString());
        EverythingWrapper.Everything_SetSearch(searchPattern);

        if (this.SortKey != SortingKey.None)
        {
          EverythingWrapper.Everything_SetSort((uint)this.SortKey);
        }

        EverythingWrapper.Everything_Query(true);

        this.LastErrorCode = this.GetError();

        return this.GetResults();
      }
    }

    public void Reset()
    {
      EverythingWrapper.Everything_Reset();
    }

    public void Dispose()
    {
      this.Reset();
    }

    private string ApplySearchResultKind(string searchPatten)
    {
      switch (this.ResulKind)
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

      return Enumerable.Range(0, (int)numResults).Select(x => new SearchResult(x, this.replyId));
    }

    private ErrorCode GetError()
    {
      var error = EverythingWrapper.Everything_GetLastError();

      return (ErrorCode)error;
    }
  }
}