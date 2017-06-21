using System;

namespace EverythingNet.Interfaces
{
  public enum Dates
  {
    Yesterday, Today,
    LastYear, LastMonth, LastWeek,
    ThisYear, ThisMonth, ThisWeek,
    NextYear, NextMonth, NextWeek,
    LastJanuary, LastFebuary, LastMarch, LastApril, LastMay, LastJune, LastJuly, LastAugust, LastSeptember, LastOctober, LastNovember, LastDecember,
    ThisJanuary, ThisFebuary, ThisMarch, ThisApril, ThisMay, ThisJune, ThisJuly, ThisAugust, ThisSeptember, ThisOctober, ThisNovember, ThisDecember,
    LastSunday, LastMonday, LastTuesday, LastWednesday, LastThursday, LastFriday, LastSaturday,
    ThisSunday, ThisMonday, ThisTuesday, ThisWednesday, ThisThursday, ThisFriday, ThisSaturday,
  }

  public enum CountableDates
  {
    Years, Months, Weeks,
    Hours, Minutes, Seconds,
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