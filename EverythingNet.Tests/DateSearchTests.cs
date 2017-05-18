using System.IO;
using EverythingNet.Core;
using EverythingNet.Extensions;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [TestFixture]
  public class DateSearchTests
  {
    private Everything everyThing;

    [SetUp]
    public void Setup()
    {
      this.everyThing = new Everything();
    }

    [TearDown]
    public void TearDown()
    {
      this.everyThing.CleanUp();
    }

    [Test, Ignore("Too long to execute")]
    public void CreationDate()
    {
      // Arrange
      string filename = "FileCreatedToday.txt";
      using (File.Create(filename)) {}
      string expectedResult = Path.Combine(Directory.GetCurrentDirectory(), filename);

      // Act
      var results = this.everyThing
        .DateCreated()
        .Today()
        .Search(true);

      // Assert
      Assert.That(results.ErrorCode, Is.EqualTo(ErrorCode.Ok));
      Assert.That(results.Results, Has.Member(expectedResult));
    }

    [Test]
    public void DateAccess()
    {
      // Act
      this.everyThing.DateAccess();

      // Assert
      Assert.That(this.everyThing.SearchText, Is.EqualTo("da:"));
    }

    [Test]
    public void DateModified()
    {
      // Act
      this.everyThing.DateModified();

      // Assert
      Assert.That(this.everyThing.SearchText, Is.EqualTo("dm:"));
    }

    [Test]
    public void DateRun()
    {
      // Act
      this.everyThing.DateRun();

      // Assert
      Assert.That(this.everyThing.SearchText, Is.EqualTo("dr:"));
    }

    [Test]
    public void DateAccessToday()
    {
      // Act
      this.everyThing.DateAccess().Today();

      // Assert
      Assert.That(this.everyThing.SearchText, Is.EqualTo("da:today"));
    }

    [Test]
    public void DateAccessYesterday()
    {
      // Act
      this.everyThing.DateAccess().Yesterday();

      // Assert
      Assert.That(this.everyThing.SearchText, Is.EqualTo("da:yesterday"));
    }

    [TestCase(DateSearch.Dates.Year, ExpectedResult = "dr:lastyear")]
    [TestCase(DateSearch.Dates.Month, ExpectedResult = "dr:lastmonth")]
    [TestCase(DateSearch.Dates.Week, ExpectedResult = "dr:lastweek")]
    public string LastDate(DateSearch.Dates date)
    {
      // Act
      this.everyThing.DateRun().Last(date);

      // Assert
      return this.everyThing.SearchText;
    }

    [TestCase(DateSearch.Dates.Year, 2, ExpectedResult = "dr:last2years")]
    [TestCase(DateSearch.Dates.Month, 3, ExpectedResult = "dr:last3months")]
    [TestCase(DateSearch.Dates.Week, 4, ExpectedResult = "dr:last4weeks")]
    public string LastXDate(DateSearch.Dates date, int num)
    {
      // Act
      this.everyThing.DateRun().Last(date, num);

      // Assert
      return this.everyThing.SearchText;
    }

    [TestCase(DateSearch.Times.Hours, ExpectedResult = "dr:lasthours")]
    [TestCase(DateSearch.Times.Minutes, ExpectedResult = "dr:lastminutes")]
    [TestCase(DateSearch.Times.Seconds, ExpectedResult = "dr:lastseconds")]
    public string LastTime(DateSearch.Times time)
    {
      // Act
      this.everyThing.DateRun().Last(time);

      // Assert
      return this.everyThing.SearchText;
    }

    [TestCase(DateSearch.Times.Hours,  2, ExpectedResult = "dr:last2hours")]
    [TestCase(DateSearch.Times.Minutes, 3, ExpectedResult = "dr:last3minutes")]
    [TestCase(DateSearch.Times.Seconds, 4, ExpectedResult = "dr:last4seconds")]
    public string LastXTime(DateSearch.Times time, int num)
    {
      // Act
      this.everyThing.DateRun().Last(time, num);

      // Assert
      return this.everyThing.SearchText;
    }
  }
}
