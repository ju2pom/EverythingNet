using System;
using System.Threading;
using System.Threading.Tasks;
using EverythingNet.Core;
using EverythingNet.Interfaces;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using System.IO;
  using System.Linq;

  [TestFixture]
  public class AcceptanceTests
  {
    private const string FileToSearchA = "EverythingState.cs";
    private const string FileToSearchB = "DateSearchTests.cs";

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
        .Search()
        .Name.Contains(FileToSearchA)
        .Or
        .Name.Contains(FileToSearchB);

      Assert.That(queryable.Where(x => x.FileName == FileToSearchA), Is.Not.Empty);
      Assert.That(queryable.Where(x => x.FileName == FileToSearchB), Is.Not.Empty);
    }

    [Test]
    public void SearchGetSize()
    {
      var queryable = new Everything()
        .Search()
        .Name
        .Contains(FileToSearchA);

      foreach (var result in queryable)
      {
        Assert.That(result.Size, Is.GreaterThan(0));
      }
    }

    [Test]
    public void SearchGetFileName()
    {
      var queryable = new Everything()
        .Search()
        .Name
        .Contains(FileToSearchA);

      foreach (var result in queryable)
      {
        Assert.That(result.FileName, Is.EqualTo(FileToSearchA));
      }
    }

    [Test]
    public void SearchGetPath()
    {
      var queryable = new Everything()
        .Search()
        .Name
        .Contains(FileToSearchA);

      foreach (var result in queryable)
      {
        Assert.That(result.Path, Is.Not.Empty.And.Not.Null);
      }
    }

    [Test]
    public void SearchGetFullPath()
    {
      var queryable = new Everything()
        .Search()
        .Name
        .Contains(FileToSearchA);

      foreach (var result in queryable)
      {
        Assert.That(result.FullPath, Does.Contain($"EverythingNet\\core\\{FileToSearchA}").IgnoreCase);
      }
    }

    [Test]
    public void SearchGetDate()
    {
      var queryable = new Everything()
        .Search()
        .Name
        .Contains(FileToSearchA);

      foreach (var result in queryable)
      {
        Assert.That(result.Created.Year, Is.GreaterThanOrEqualTo(2017));
        Assert.That(result.Modified.Year, Is.GreaterThanOrEqualTo(2017));
        Assert.That(result.Accessed.Year, Is.GreaterThanOrEqualTo(2017));
        Assert.That(result.Executed, Is.EqualTo(DateTime.MinValue));
      }
    }

    [Test]
    public void SearchGetAttributes()
    {
      var queryable = new Everything()
        .Search()
        .Name
        .Contains(FileToSearchA);

      foreach (var result in queryable)
      {
        Assert.That(result.Attributes, Is.GreaterThan(0));
      }
    }

    [Test]
    public void Zip_Succeeds()
    {
      // Arrange
      string zipFile = "acceptanceTest.zip";
      File.Create(zipFile).Close();
      Thread.Sleep(1000);

      var queryable = new Everything()
          .Search()
          .File
          .Zip();

      // Assert
      Assert.That(queryable.Where(x => x.FileName == zipFile), Is.Not.Empty);

      File.Delete(zipFile);
    }

    [Test, Repeat(100)]
    public void StressTest()
    {
      // Arrange
      var queryable = this.everyThing
        .Search()
        .Name
        .Contains(FileToSearchA);

      // Assert
      Assert.That(this.everyThing.LastErrorCode, Is.EqualTo(ErrorCode.Ok));
      Assert.That(queryable, Is.Not.Null);
      Assert.That(queryable, Is.Not.Empty);
    }


    [Test, Ignore("Not yet ready")]
    public void ThreadSafety()
    {
      ManualResetEventSlim resetEvent1 = this.StartSearchInBackground(FileToSearchA);
      ManualResetEventSlim resetEvent2 = this.StartSearchInBackground(FileToSearchB);

      Assert.That(resetEvent1.Wait(15000), Is.True);
      Assert.That(resetEvent2.Wait(15000), Is.True);
    }

    [Test]
    public void MultipleInstances()
    {
      var firstResult = this.everyThing
          .Search()
          .Name
          .Contains("IImageQueryable.cs");

      IEverything secondEverything = new Everything();
      var secondResult = secondEverything
          .Search()
          .Name
          .Contains("IMusicQueryable.cs");

      Assert.That(firstResult.First().FileName, Is.EqualTo("IImageQueryable.cs"));
      Assert.That(secondResult.First().FileName, Is.EqualTo("IMusicQueryable.cs"));

    }

    [Test]
    public void CountPerformance()
    {
      var queryable = new Everything()
          .Search()
          .Name.Contains("micro");

      Assert.That(queryable.Count, Is.GreaterThan(0));
    }

    private ManualResetEventSlim StartSearchInBackground(string searchString)
    {
      ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);

      Task.Factory.StartNew(() =>
      {
        IEverything everything = new Everything();
        everything.MatchWholeWord = true;

        // Act
        var results = everything
          .Search()
          .Name
          .Contains(searchString);

        // Assert
        Assert.That(this.everyThing.LastErrorCode, Is.EqualTo(ErrorCode.Ok));
        Assert.That(results, Is.Not.Empty);
        foreach (var result in results)
        {
          StringAssert.Contains(searchString, result.FileName);
        }
        resetEvent.Set();
      });

      return resetEvent;
    }
  }
}
