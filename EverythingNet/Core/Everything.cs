using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverythingNet.Core
{
  public class Everything : IEverything
  {
    private readonly uint replyId;

    public Everything()
    {
      this.replyId = Convert.ToUInt32(this.GetHashCode());
    }

    public string SearchText { get; set; }

    public bool MatchCase { get; set; }

    public bool MatchPath { get; set; }

    public bool MatchWholeWord { get; set; }

    public ISearchResult Search(bool wait)
    {
      ErrorCode errorCode;
      string[] results;

      using (EverythingWrapper.Lock())
      {
        EverythingWrapper.Everything_SetReplyID(this.replyId);

        EverythingWrapper.Everything_SetMatchWholeWord(this.MatchWholeWord);
        EverythingWrapper.Everything_SetMatchPath(this.MatchPath);
        EverythingWrapper.Everything_SetMatchCase(this.MatchCase);

        EverythingWrapper.Everything_SetSearchA(this.SearchText);
        EverythingWrapper.Everything_QueryA(wait);

        errorCode = this.GetError();
        results = this.GetResults().ToArray();
      }

      return new SearchResult(errorCode, results);

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

    public void CleanUp()
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