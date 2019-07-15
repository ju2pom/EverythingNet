namespace EverythingNet.Query
{
  using System;
  using System.Collections.Generic;

  using EverythingNet.Core;
  using EverythingNet.Interfaces;

  internal class DateQueryable : Queryable, IDateQueryable
  {
    private const string DateTimeFormat = "dd/MM/yyyyThh:mm:ss";

    private string searchPattern;

    internal DateQueryable(Query parent, string kind)
      : base(parent)
    {
      this.searchPattern = kind;
      EverythingWrapper.FileInfoIndex fileInfoIndex;

      switch (this.searchPattern)
      {
        default:
          this.Flags = RequestFlags.EVERYTHING_REQUEST_DATE_MODIFIED;
          fileInfoIndex = EverythingWrapper.FileInfoIndex.DateModified;
          break;
        case "dc":
          this.Flags = RequestFlags.EVERYTHING_REQUEST_DATE_CREATED;
          fileInfoIndex = EverythingWrapper.FileInfoIndex.DateCreated;
          break;
        case "dr":
          this.Flags = RequestFlags.EVERYTHING_REQUEST_DATE_RUN;
          fileInfoIndex = EverythingWrapper.FileInfoIndex.DateAccessed;
          break;
        case "da":
          this.Flags = RequestFlags.EVERYTHING_REQUEST_DATE_ACCESSED;
          fileInfoIndex = EverythingWrapper.FileInfoIndex.DateAccessed;
          break;
      }

      this.IsFast = EverythingWrapper.Everything_IsFileInfoIndexed(fileInfoIndex);
    }

    public IQuery Before(DateTime date)
    {
      return this.DateSearch($"<{date.ToString(DateTimeFormat)}");
    }

    public IQuery After(DateTime date)
    {
      return this.DateSearch($">{date.ToString(DateTimeFormat)}");
    }

    public IQuery Equal(DateTime date)
    {
      return this.DateSearch($"={date.ToString(DateTimeFormat)}");
    }

    public IQuery Between(DateTime from, DateTime to)
    {
      return this.DateSearch($"{from.ToString(DateTimeFormat)}-{to.ToString(DateTimeFormat)}");
    }

    public IQuery Before(Dates date)
    {
      return this.DateSearch($"<{date}");
    }

    public IQuery After(Dates date)
    {
      return this.DateSearch($">{date}");
    }

    public IQuery Equal(Dates date)
    {
      return this.DateSearch($"{date}");
    }

    public IQuery Between(Dates from, Dates to)
    {
      return this.DateSearch($"{from}-{to}");
    }

    public IQuery Last(int count, CountableDates date)
    {
      return this.DateSearch($"last{count}{date}");
    }

    public IQuery Next(int count, CountableDates date)
    {
      return this.DateSearch($"next{count}{date}");
    }

    public override IEnumerable<string> GetQueryParts()
    {
      foreach (var queryPart in base.GetQueryParts())
      {
        yield return queryPart;
      }

      yield return this.searchPattern;
    }

    private IQuery DateSearch(string pattern)
    {
      this.searchPattern += pattern;

      return new Query(this);
    }
  }
}