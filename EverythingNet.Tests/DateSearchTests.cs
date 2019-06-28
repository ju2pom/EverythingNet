using System.IO;
using EverythingNet.Core;
using EverythingNet.Interfaces;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using EverythingNet.Query;

  [TestFixture]
  public class DateSearchTests
  {
    [Test, Ignore("Too long to execute")]
    public void CreationDate()
    {
      using (var everything = new Everything())
      {
        // Arrange
        string filename = "FileCreatedToday.txt";
        using (File.Create(filename))
        {
        }
        string expectedResult = Path.Combine(Directory.GetCurrentDirectory(), filename);
        var query = new Query().CreationDate.Equal(Dates.Today);

        // Act
        var results = everything.Search(query);

        // Assert
        Assert.That(everything.LastErrorCode, Is.EqualTo(ErrorCode.Ok));
        Assert.That(results, Has.Member(expectedResult));
      }
    }

    [TestCase(Dates.Yesterday, ExpectedResult = "da:yesterday")]
    [TestCase(Dates.Today, ExpectedResult = "da:today")]
    [TestCase(Dates.LastYear, ExpectedResult = "da:lastyear")]
    [TestCase(Dates.LastMonth, ExpectedResult = "da:lastmonth")]
    [TestCase(Dates.LastWeek, ExpectedResult = "da:lastweek")]
    [TestCase(Dates.ThisYear, ExpectedResult = "da:thisyear")]
    [TestCase(Dates.ThisMonth, ExpectedResult = "da:thismonth")]
    [TestCase(Dates.ThisWeek, ExpectedResult = "da:thisweek")]
    [TestCase(Dates.NextYear, ExpectedResult = "da:nextyear")]
    [TestCase(Dates.NextMonth, ExpectedResult = "da:nextmonth")]
    [TestCase(Dates.NextWeek, ExpectedResult = "da:nextweek")]
    [TestCase(Dates.LastJanuary, ExpectedResult = "da:lastjanuary")]
    [TestCase(Dates.LastFebuary, ExpectedResult = "da:lastfebuary")]
    [TestCase(Dates.LastMarch, ExpectedResult = "da:lastmarch")]
    [TestCase(Dates.LastApril, ExpectedResult = "da:lastapril")]
    [TestCase(Dates.LastMay, ExpectedResult = "da:lastmay")]
    [TestCase(Dates.LastJune, ExpectedResult = "da:lastjune")]
    [TestCase(Dates.LastJuly, ExpectedResult = "da:lastjuly")]
    [TestCase(Dates.LastAugust, ExpectedResult = "da:lastaugust")]
    [TestCase(Dates.LastSeptember, ExpectedResult = "da:lastseptember")]
    [TestCase(Dates.LastOctober, ExpectedResult = "da:lastoctober")]
    [TestCase(Dates.LastNovember, ExpectedResult = "da:lastnovember")]
    [TestCase(Dates.LastDecember, ExpectedResult = "da:lastdecember")]
    [TestCase(Dates.ThisJanuary, ExpectedResult = "da:thisjanuary")]
    [TestCase(Dates.ThisFebuary, ExpectedResult = "da:thisfebuary")]
    [TestCase(Dates.ThisMarch, ExpectedResult = "da:thismarch")]
    [TestCase(Dates.ThisApril, ExpectedResult = "da:thisapril")]
    [TestCase(Dates.ThisMay, ExpectedResult = "da:thismay")]
    [TestCase(Dates.ThisJune, ExpectedResult = "da:thisjune")]
    [TestCase(Dates.ThisJuly, ExpectedResult = "da:thisjuly")]
    [TestCase(Dates.ThisAugust, ExpectedResult = "da:thisaugust")]
    [TestCase(Dates.ThisSeptember, ExpectedResult = "da:thisseptember")]
    [TestCase(Dates.ThisOctober, ExpectedResult = "da:thisoctober")]
    [TestCase(Dates.ThisNovember, ExpectedResult = "da:thisnovember")]
    [TestCase(Dates.ThisDecember, ExpectedResult = "da:thisdecember")]
    [TestCase(Dates.LastSunday, ExpectedResult = "da:lastsunday")]
    [TestCase(Dates.LastMonday, ExpectedResult = "da:lastmonday")]
    [TestCase(Dates.LastTuesday, ExpectedResult = "da:lasttuesday")]
    [TestCase(Dates.LastWednesday, ExpectedResult = "da:lastwednesday")]
    [TestCase(Dates.LastThursday, ExpectedResult = "da:lastthursday")]
    [TestCase(Dates.LastFriday, ExpectedResult = "da:lastfriday")]
    [TestCase(Dates.LastSaturday, ExpectedResult = "da:lastsaturday")]
    [TestCase(Dates.ThisSunday, ExpectedResult = "da:thissunday")]
    [TestCase(Dates.ThisMonday, ExpectedResult = "da:thismonday")]
    [TestCase(Dates.ThisTuesday, ExpectedResult = "da:thistuesday")]
    [TestCase(Dates.ThisWednesday, ExpectedResult = "da:thiswednesday")]
    [TestCase(Dates.ThisThursday, ExpectedResult = "da:thisthursday")]
    [TestCase(Dates.ThisFriday, ExpectedResult = "da:thisfriday")]
    [TestCase(Dates.ThisSaturday, ExpectedResult = "da:thissaturday")]
    public string AccessDate(Dates date)
    {
      // Act
      var query = new Query().AccessDate.Equal(date);

      // Assert
      return query.ToString().ToLower();
    }

    [TestCase(Dates.Yesterday, ExpectedResult = "dm:yesterday")]
    [TestCase(Dates.Today, ExpectedResult = "dm:today")]
    [TestCase(Dates.LastYear, ExpectedResult = "dm:lastyear")]
    [TestCase(Dates.LastMonth, ExpectedResult = "dm:lastmonth")]
    [TestCase(Dates.LastWeek, ExpectedResult = "dm:lastweek")]
    [TestCase(Dates.ThisYear, ExpectedResult = "dm:thisyear")]
    [TestCase(Dates.ThisMonth, ExpectedResult = "dm:thismonth")]
    [TestCase(Dates.ThisWeek, ExpectedResult = "dm:thisweek")]
    [TestCase(Dates.NextYear, ExpectedResult = "dm:nextyear")]
    [TestCase(Dates.NextMonth, ExpectedResult = "dm:nextmonth")]
    [TestCase(Dates.NextWeek, ExpectedResult = "dm:nextweek")]
    [TestCase(Dates.LastJanuary, ExpectedResult = "dm:lastjanuary")]
    [TestCase(Dates.LastFebuary, ExpectedResult = "dm:lastfebuary")]
    [TestCase(Dates.LastMarch, ExpectedResult = "dm:lastmarch")]
    [TestCase(Dates.LastApril, ExpectedResult = "dm:lastapril")]
    [TestCase(Dates.LastMay, ExpectedResult = "dm:lastmay")]
    [TestCase(Dates.LastJune, ExpectedResult = "dm:lastjune")]
    [TestCase(Dates.LastJuly, ExpectedResult = "dm:lastjuly")]
    [TestCase(Dates.LastAugust, ExpectedResult = "dm:lastaugust")]
    [TestCase(Dates.LastSeptember, ExpectedResult = "dm:lastseptember")]
    [TestCase(Dates.LastOctober, ExpectedResult = "dm:lastoctober")]
    [TestCase(Dates.LastNovember, ExpectedResult = "dm:lastnovember")]
    [TestCase(Dates.LastDecember, ExpectedResult = "dm:lastdecember")]
    [TestCase(Dates.ThisJanuary, ExpectedResult = "dm:thisjanuary")]
    [TestCase(Dates.ThisFebuary, ExpectedResult = "dm:thisfebuary")]
    [TestCase(Dates.ThisMarch, ExpectedResult = "dm:thismarch")]
    [TestCase(Dates.ThisApril, ExpectedResult = "dm:thisapril")]
    [TestCase(Dates.ThisMay, ExpectedResult = "dm:thismay")]
    [TestCase(Dates.ThisJune, ExpectedResult = "dm:thisjune")]
    [TestCase(Dates.ThisJuly, ExpectedResult = "dm:thisjuly")]
    [TestCase(Dates.ThisAugust, ExpectedResult = "dm:thisaugust")]
    [TestCase(Dates.ThisSeptember, ExpectedResult = "dm:thisseptember")]
    [TestCase(Dates.ThisOctober, ExpectedResult = "dm:thisoctober")]
    [TestCase(Dates.ThisNovember, ExpectedResult = "dm:thisnovember")]
    [TestCase(Dates.ThisDecember, ExpectedResult = "dm:thisdecember")]
    [TestCase(Dates.LastSunday, ExpectedResult = "dm:lastsunday")]
    [TestCase(Dates.LastMonday, ExpectedResult = "dm:lastmonday")]
    [TestCase(Dates.LastTuesday, ExpectedResult = "dm:lasttuesday")]
    [TestCase(Dates.LastWednesday, ExpectedResult = "dm:lastwednesday")]
    [TestCase(Dates.LastThursday, ExpectedResult = "dm:lastthursday")]
    [TestCase(Dates.LastFriday, ExpectedResult = "dm:lastfriday")]
    [TestCase(Dates.LastSaturday, ExpectedResult = "dm:lastsaturday")]
    [TestCase(Dates.ThisSunday, ExpectedResult = "dm:thissunday")]
    [TestCase(Dates.ThisMonday, ExpectedResult = "dm:thismonday")]
    [TestCase(Dates.ThisTuesday, ExpectedResult = "dm:thistuesday")]
    [TestCase(Dates.ThisWednesday, ExpectedResult = "dm:thiswednesday")]
    [TestCase(Dates.ThisThursday, ExpectedResult = "dm:thisthursday")]
    [TestCase(Dates.ThisFriday, ExpectedResult = "dm:thisfriday")]
    [TestCase(Dates.ThisSaturday, ExpectedResult = "dm:thissaturday")]
    public string ModificationDate(Dates date)
    {
      // Act
      var query = new Query().ModificationDate.Equal(date);

      // Assert
      return query.ToString().ToLower();
    }


    [TestCase(Dates.Yesterday, ExpectedResult = "dc:yesterday")]
    [TestCase(Dates.Today, ExpectedResult = "dc:today")]
    [TestCase(Dates.LastYear, ExpectedResult = "dc:lastyear")]
    [TestCase(Dates.LastMonth, ExpectedResult = "dc:lastmonth")]
    [TestCase(Dates.LastWeek, ExpectedResult = "dc:lastweek")]
    [TestCase(Dates.ThisYear, ExpectedResult = "dc:thisyear")]
    [TestCase(Dates.ThisMonth, ExpectedResult = "dc:thismonth")]
    [TestCase(Dates.ThisWeek, ExpectedResult = "dc:thisweek")]
    [TestCase(Dates.NextYear, ExpectedResult = "dc:nextyear")]
    [TestCase(Dates.NextMonth, ExpectedResult = "dc:nextmonth")]
    [TestCase(Dates.NextWeek, ExpectedResult = "dc:nextweek")]
    [TestCase(Dates.LastJanuary, ExpectedResult = "dc:lastjanuary")]
    [TestCase(Dates.LastFebuary, ExpectedResult = "dc:lastfebuary")]
    [TestCase(Dates.LastMarch, ExpectedResult = "dc:lastmarch")]
    [TestCase(Dates.LastApril, ExpectedResult = "dc:lastapril")]
    [TestCase(Dates.LastMay, ExpectedResult = "dc:lastmay")]
    [TestCase(Dates.LastJune, ExpectedResult = "dc:lastjune")]
    [TestCase(Dates.LastJuly, ExpectedResult = "dc:lastjuly")]
    [TestCase(Dates.LastAugust, ExpectedResult = "dc:lastaugust")]
    [TestCase(Dates.LastSeptember, ExpectedResult = "dc:lastseptember")]
    [TestCase(Dates.LastOctober, ExpectedResult = "dc:lastoctober")]
    [TestCase(Dates.LastNovember, ExpectedResult = "dc:lastnovember")]
    [TestCase(Dates.LastDecember, ExpectedResult = "dc:lastdecember")]
    [TestCase(Dates.ThisJanuary, ExpectedResult = "dc:thisjanuary")]
    [TestCase(Dates.ThisFebuary, ExpectedResult = "dc:thisfebuary")]
    [TestCase(Dates.ThisMarch, ExpectedResult = "dc:thismarch")]
    [TestCase(Dates.ThisApril, ExpectedResult = "dc:thisapril")]
    [TestCase(Dates.ThisMay, ExpectedResult = "dc:thismay")]
    [TestCase(Dates.ThisJune, ExpectedResult = "dc:thisjune")]
    [TestCase(Dates.ThisJuly, ExpectedResult = "dc:thisjuly")]
    [TestCase(Dates.ThisAugust, ExpectedResult = "dc:thisaugust")]
    [TestCase(Dates.ThisSeptember, ExpectedResult = "dc:thisseptember")]
    [TestCase(Dates.ThisOctober, ExpectedResult = "dc:thisoctober")]
    [TestCase(Dates.ThisNovember, ExpectedResult = "dc:thisnovember")]
    [TestCase(Dates.ThisDecember, ExpectedResult = "dc:thisdecember")]
    [TestCase(Dates.LastSunday, ExpectedResult = "dc:lastsunday")]
    [TestCase(Dates.LastMonday, ExpectedResult = "dc:lastmonday")]
    [TestCase(Dates.LastTuesday, ExpectedResult = "dc:lasttuesday")]
    [TestCase(Dates.LastWednesday, ExpectedResult = "dc:lastwednesday")]
    [TestCase(Dates.LastThursday, ExpectedResult = "dc:lastthursday")]
    [TestCase(Dates.LastFriday, ExpectedResult = "dc:lastfriday")]
    [TestCase(Dates.LastSaturday, ExpectedResult = "dc:lastsaturday")]
    [TestCase(Dates.ThisSunday, ExpectedResult = "dc:thissunday")]
    [TestCase(Dates.ThisMonday, ExpectedResult = "dc:thismonday")]
    [TestCase(Dates.ThisTuesday, ExpectedResult = "dc:thistuesday")]
    [TestCase(Dates.ThisWednesday, ExpectedResult = "dc:thiswednesday")]
    [TestCase(Dates.ThisThursday, ExpectedResult = "dc:thisthursday")]
    [TestCase(Dates.ThisFriday, ExpectedResult = "dc:thisfriday")]
    [TestCase(Dates.ThisSaturday, ExpectedResult = "dc:thissaturday")]
    public string CreationDate(Dates date)
    {
      // Act
      var query = new Query().CreationDate.Equal(date);

      // Assert
      return query.ToString().ToLower();
    }


    [TestCase(Dates.Yesterday, ExpectedResult = "dr:yesterday")]
    [TestCase(Dates.Today, ExpectedResult = "dr:today")]
    [TestCase(Dates.LastYear, ExpectedResult = "dr:lastyear")]
    [TestCase(Dates.LastMonth, ExpectedResult = "dr:lastmonth")]
    [TestCase(Dates.LastWeek, ExpectedResult = "dr:lastweek")]
    [TestCase(Dates.ThisYear, ExpectedResult = "dr:thisyear")]
    [TestCase(Dates.ThisMonth, ExpectedResult = "dr:thismonth")]
    [TestCase(Dates.ThisWeek, ExpectedResult = "dr:thisweek")]
    [TestCase(Dates.NextYear, ExpectedResult = "dr:nextyear")]
    [TestCase(Dates.NextMonth, ExpectedResult = "dr:nextmonth")]
    [TestCase(Dates.NextWeek, ExpectedResult = "dr:nextweek")]
    [TestCase(Dates.LastJanuary, ExpectedResult = "dr:lastjanuary")]
    [TestCase(Dates.LastFebuary, ExpectedResult = "dr:lastfebuary")]
    [TestCase(Dates.LastMarch, ExpectedResult = "dr:lastmarch")]
    [TestCase(Dates.LastApril, ExpectedResult = "dr:lastapril")]
    [TestCase(Dates.LastMay, ExpectedResult = "dr:lastmay")]
    [TestCase(Dates.LastJune, ExpectedResult = "dr:lastjune")]
    [TestCase(Dates.LastJuly, ExpectedResult = "dr:lastjuly")]
    [TestCase(Dates.LastAugust, ExpectedResult = "dr:lastaugust")]
    [TestCase(Dates.LastSeptember, ExpectedResult = "dr:lastseptember")]
    [TestCase(Dates.LastOctober, ExpectedResult = "dr:lastoctober")]
    [TestCase(Dates.LastNovember, ExpectedResult = "dr:lastnovember")]
    [TestCase(Dates.LastDecember, ExpectedResult = "dr:lastdecember")]
    [TestCase(Dates.ThisJanuary, ExpectedResult = "dr:thisjanuary")]
    [TestCase(Dates.ThisFebuary, ExpectedResult = "dr:thisfebuary")]
    [TestCase(Dates.ThisMarch, ExpectedResult = "dr:thismarch")]
    [TestCase(Dates.ThisApril, ExpectedResult = "dr:thisapril")]
    [TestCase(Dates.ThisMay, ExpectedResult = "dr:thismay")]
    [TestCase(Dates.ThisJune, ExpectedResult = "dr:thisjune")]
    [TestCase(Dates.ThisJuly, ExpectedResult = "dr:thisjuly")]
    [TestCase(Dates.ThisAugust, ExpectedResult = "dr:thisaugust")]
    [TestCase(Dates.ThisSeptember, ExpectedResult = "dr:thisseptember")]
    [TestCase(Dates.ThisOctober, ExpectedResult = "dr:thisoctober")]
    [TestCase(Dates.ThisNovember, ExpectedResult = "dr:thisnovember")]
    [TestCase(Dates.ThisDecember, ExpectedResult = "dr:thisdecember")]
    [TestCase(Dates.LastSunday, ExpectedResult = "dr:lastsunday")]
    [TestCase(Dates.LastMonday, ExpectedResult = "dr:lastmonday")]
    [TestCase(Dates.LastTuesday, ExpectedResult = "dr:lasttuesday")]
    [TestCase(Dates.LastWednesday, ExpectedResult = "dr:lastwednesday")]
    [TestCase(Dates.LastThursday, ExpectedResult = "dr:lastthursday")]
    [TestCase(Dates.LastFriday, ExpectedResult = "dr:lastfriday")]
    [TestCase(Dates.LastSaturday, ExpectedResult = "dr:lastsaturday")]
    [TestCase(Dates.ThisSunday, ExpectedResult = "dr:thissunday")]
    [TestCase(Dates.ThisMonday, ExpectedResult = "dr:thismonday")]
    [TestCase(Dates.ThisTuesday, ExpectedResult = "dr:thistuesday")]
    [TestCase(Dates.ThisWednesday, ExpectedResult = "dr:thiswednesday")]
    [TestCase(Dates.ThisThursday, ExpectedResult = "dr:thisthursday")]
    [TestCase(Dates.ThisFriday, ExpectedResult = "dr:thisfriday")]
    [TestCase(Dates.ThisSaturday, ExpectedResult = "dr:thissaturday")]
    public string RunDate(Dates date)
    {
      // Act
      var query = new Query().RunDate.Equal(date);

      // Assert
      return query.ToString().ToLower();
    }

    [TestCase(1, CountableDates.Hours, ExpectedResult = "dm:last1hours")]
    [TestCase(2, CountableDates.Minutes, ExpectedResult = "dm:last2minutes")]
    [TestCase(3, CountableDates.Seconds, ExpectedResult = "dm:last3seconds")]
    [TestCase(4, CountableDates.Weeks, ExpectedResult = "dm:last4weeks")]
    [TestCase(5, CountableDates.Months, ExpectedResult = "dm:last5months")]
    [TestCase(6, CountableDates.Years, ExpectedResult = "dm:last6years")]
    public string LastDate(int count, CountableDates date)
    {
      // Act
      var query = new Query().ModificationDate.Last(count, date);

      // Assert
      return query.ToString().ToLower();
    }
  }
}