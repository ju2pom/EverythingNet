using System.Threading;
using System.Threading.Tasks;
using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [TestFixture]
  public class AcceptanceTests
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
      this.everyThing.Dispose();
    }

    [Test]
    public void Query()
    {
      var queryable = new Everything()
        .Search(true)
        .Name("AcceptanceTests.cs")
        .Or
        .Name("SearchResult.cs");

      foreach (var s in queryable)
      {
        Assert.That(s, Is.Not.Empty);
      }
    }
    [Test, Repeat(100)]
    public void StressTest()
    {
      // Arrange
      var queryable = this.everyThing.Search(true).Name("AcceptanceTests.cs");

      // Assert
      Assert.That(everyThing.LastErrorCode, Is.EqualTo(ErrorCode.Ok));
      Assert.That(queryable, Is.Not.Null);
      Assert.That(queryable, Is.Not.Empty);
    }


    [Test, Ignore("Not yet ready")]
    public void ThreadSafety()
    {
      ManualResetEventSlim resetEvent1 = this.StartSearchInBackground("Everything.cs");
      ManualResetEventSlim resetEvent2 = this.StartSearchInBackground("AcceptanceTests.cs");

      Assert.That(resetEvent1.Wait(15000), Is.True);
      Assert.That(resetEvent2.Wait(15000), Is.True);
    }

    private ManualResetEventSlim StartSearchInBackground(string searchString)
    {
      ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);

      Task.Factory.StartNew(() =>
      {
        IEverything everything = new Everything();
        everything.MatchWholeWord = true;
       
        // Act
        var results = everything.Search(true).Name(searchString);

        // Assert
        Assert.That(everyThing.LastErrorCode, Is.EqualTo(ErrorCode.Ok));
        Assert.That(results, Is.Not.Empty);
        foreach (var result in results)
        {
          StringAssert.Contains(searchString, result);
        }
        resetEvent.Set();
      });

      return resetEvent;
    }
  }
}
