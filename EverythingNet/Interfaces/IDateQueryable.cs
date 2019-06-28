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

  public interface IDateQueryable : IQueryable
  {
    IQuery Equal(DateTime date);

    IQuery Equal(Dates date);

    IQuery Before(DateTime date);

    IQuery Before(Dates date);

    IQuery After(DateTime date);

    IQuery After(Dates date);

    IQuery Between(DateTime from, DateTime to);

    IQuery Between(Dates from, Dates to);

    IQuery Last(int count, CountableDates date);

    IQuery Next(int count, CountableDates date);
  }
}