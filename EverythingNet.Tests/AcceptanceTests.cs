using System.Threading;
using System.Threading.Tasks;
using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [Ignore("Need Everything service to be running")]
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
      this.everyThing.CleanUp();
    }

    [Test, Repeat(100)]
    public void StressTest()
    {
      this.everyThing.SearchText = "AcceptanceTests.cs";
      
      // Act
      var results = this.everyThing.Search(true);

      // Assert
      Assert.That(results, Is.Not.Null);
      Assert.That(results.ErrorCode, Is.EqualTo(ErrorCode.Ok));
      Assert.That(results.Results, Is.Not.Empty);
    }

    [Test]
    public void ThreadSafety()
    {
      ManualResetEventSlim resetEvent1 = this.StartSearchInBackground("Everything.cs");
      ManualResetEventSlim resetEvent2 = this.StartSearchInBackground("SearchResult.cs");

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
        everything.SearchText = searchString;
       
        // Act
        var results = everything.Search(true);

        // Assert
        Assert.That(results.ErrorCode, Is.EqualTo(ErrorCode.Ok));
        Assert.That(results.Results, Is.Not.Empty);
        foreach (var result in results.Results)
        {
          StringAssert.Contains(searchString, result);
        }
        resetEvent.Set();
      });

      return resetEvent;
    }
  }
}
