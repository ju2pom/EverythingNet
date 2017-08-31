namespace EverythingNet.Query
{
  using System;
  using System.Collections.Generic;

  using EverythingNet.Interfaces;

  internal class DateQueryable : Queryable, IDateQueryable
  {
    private string searchPattern;

    internal DateQueryable(IEverythingInternal everything, IQueryGenerator parent, string kind)
      : base(everything, parent)
    {
      this.searchPattern = kind;
    }

    public override IEnumerable<string> GetQueryParts()
    {
      foreach (var queryPart in base.GetQueryParts())
      {
        yield return queryPart;
      }

      yield return this.searchPattern;
    }

    public IQueryable Before(DateTime date)
    {
      return this.DateSearch($"<{date.ToShortDateString()}");
    }

    public IQueryable After(DateTime date)
    {
      return this.DateSearch($">{date.ToShortDateString()}");
    }

    public IQueryable Equal(DateTime date)
    {
      return this.DateSearch($"={date.ToShortDateString()}");
    }

    public IQueryable Between(DateTime from, DateTime to)
    {
      return this.DateSearch($"{from.ToShortDateString()}-{to.ToShortDateString()}");
    }

    public IQueryable Before(Dates date)
    {
      return this.DateSearch($"<{date}");
    }

    public IQueryable After(Dates date)
    {
      return this.DateSearch($">{date}");
    }

    public IQueryable Equal(Dates date)
    {
      return this.DateSearch($"{date}");
    }

    public IQueryable Between(Dates from, Dates to)
    {
      return this.DateSearch($"{from}-{to}");
    }

    public IQueryable Last(int count, CountableDates date)
    {
      return this.DateSearch($"last{count}{date}");
    }

    public IQueryable Next(int count, CountableDates date)
    {
      return this.DateSearch($"next{count}{date}");
    }

    private IQueryable DateSearch(string pattern)
    {
      this.searchPattern += pattern;

      return this;
    }
  }
}