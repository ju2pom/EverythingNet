namespace EverythingNet.Interfaces
{
  using System;

  public enum Dates
  {
    Today,
    Yesterday,
    ThisWeek,
    ThisMonth,
    ThisYear,
    ThisSunday,
    ThisMonday,
    ThisTuesday,
    ThisWednesday,
    ThisThursday,
    ThisFriday,
    ThisSaturday,
    ThisJanuary,
    ThisFebuary,
    ThisMarch,
    ThisApril,
    ThisMay,
    ThisJune,
    ThisJuly,
    ThisAugust,
    ThisSeptember,
    ThisOctober,
    ThisNovember,
    ThisDecember,
    LastSunday,
    LastMonday,
    LastTuesday,
    LastWednesday,
    LastThursday,
    LastFriday,
    LastSaturday,
    LastWeek,
    LastMonth,
    LastYear,
    LastJanuary,
    LastFebuary,
    LastMarch,
    LastApril,
    LastMay,
    LastJune,
    LastJuly,
    LastAugust,
    LastSeptember,
    LastOctober,
    LastNovember,
    LastDecember,
    NextYear,
    NextMonth,
    NextWeek
  }

  public enum CountableDates
  {
    Seconds,
    Minutes,
    Hours,
    Weeks,
    Months,
    Years
  }

  public interface IDateQueryable
  {
    IQueryable Equal(DateTime date);

    IQueryable Equal(Dates date);

    IQueryable Before(DateTime date);

    IQueryable Before(Dates date);

    IQueryable After(DateTime date);

    IQueryable After(Dates date);

    IQueryable Between(DateTime from, DateTime to);

    IQueryable Between(Dates from, Dates to);

    IQueryable Last(int count, CountableDates date);

    IQueryable Next(int count, CountableDates date);
  }
}