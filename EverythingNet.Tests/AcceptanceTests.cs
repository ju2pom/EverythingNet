using System.Threading;
using System.Threading.Tasks;
using EverythingNet.Core;
using EverythingNet.Interfaces;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using System.IO;
  using System.Linq;
    using System.Reflection;
    using EverythingNet.Query;

  [TestFixture]
  public class AcceptanceTests
  {
    private const string FileToSearchA = "EverythingState.cs";
    private const string FileToSearchB = "DateSearchTests.cs";

    private IEverything everything;

    [SetUp]
    public void Setup()
    {
      this.everything = new Everything();
    }

    [TearDown]
    public void TearDown()
    {
      this.everything.Dispose();
    }

    [Test]
    public void Query()
    {
      var query = new Query()
        .Name.Contains(FileToSearchA)
        .Or
        .Name.Contains(FileToSearchB);
      var results = this.everything.Search(query);

      Assert.That(results.Where(x => x.FileName == FileToSearchA), Is.Not.Empty);
      Assert.That(results.Where(x => x.FileName == FileToSearchB), Is.Not.Empty);
    }

    [Test]
    public void SearchGetSize()
    {
      var query = new Query().Name.Contains(FileToSearchA);
      var results = this.everything.Search(query);
      foreach (var result in results)
      {
        Assert.That(result.Size, Is.GreaterThan(0));
      }
    }

    [Test]
    public void SearchGetFileName()
    {
      var query = new Query().Name.Contains(FileToSearchA);

      foreach (var result in this.everything.Search(query))
      {
        Assert.That(result.FileName, Is.EqualTo(FileToSearchA));
      }
    }

    [Test]
    public void SearchGetPath()
    {
      var query = new Query().Name.Contains(FileToSearchA);

      foreach (var result in this.everything.Search(query))
      {
        Assert.That(result.Path, Is.Not.Empty.And.Not.Null);
      }
    }

    [Test]
    public void SearchGetFullPath()
    {
      var query = new Query().Name.Contains(FileToSearchA);

      foreach (var result in this.everything.Search(query))
      {
        Assert.That(result.FullPath, Does.Contain($"EverythingNet\\core\\{FileToSearchA}").IgnoreCase);
      }
    }

    [Test]
    public void SearchGetDate()
    {
      var query = new Query().Name.Contains(FileToSearchA);

      foreach (var result in this.everything.Search(query))
      {
        Assert.That(result.Modified.Year, Is.GreaterThanOrEqualTo(2017));
      }
    }

    [Test]
    public void SearchGetAttributes()
    {
      var query = new Query().Name.Contains(FileToSearchA);

      foreach (var result in this.everything.Search(query))
      {
        Assert.That(result.Attributes, Is.GreaterThan(0));
      }
    }

    [Test]
    public void Zip_Succeeds()
    {
      // Arrange
      string zipFile = "acceptanceTest.zip";
      File.Create(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), zipFile)).Close();
      Thread.Sleep(1000);

      var query = new Query().File.Zip();

      // Assert
      Assert.That(this.everything.Search(query).Where(x => x.FileName == zipFile), Is.Not.Empty);

      File.Delete(zipFile);
    }

    [Test, Repeat(100)]
    public void StressTest()
    {
      // Arrange
      var query = new Query().Name.Contains(FileToSearchA);
      var results = this.everything.Search(query).ToArray();

      // Assert
      Assert.That(this.everything.LastErrorCode, Is.EqualTo(ErrorCode.Ok));
      Assert.That(results, Is.Not.Null);
      Assert.That(results, Is.Not.Empty);
    }


    [Test, Ignore("Not yet ready")]
    public void ThreadSafety()
    {
      ManualResetEventSlim resetEvent1 = this.StartSearchInBackground(FileToSearchA);
      ManualResetEventSlim resetEvent2 = this.StartSearchInBackground(FileToSearchB);

      Assert.That(resetEvent1.Wait(15000), Is.True);
      Assert.That(resetEvent2.Wait(15000), Is.True);
    }

    [Test, Ignore("To be fixe !")]
    public void MultipleInstances()
    {
      var firstQuery = new Query().Name.Contains("IImageQueryable.cs");
      var secondQuery = new Query().Name.Contains("IMusicQueryable.cs");

      var firstResult = this.everything.Search(firstQuery);
      var secondResult = new Everything().Search(secondQuery);

      Assert.That(firstResult.First().FileName, Is.EqualTo("IImageQueryable.cs"));
      Assert.That(secondResult.First().FileName, Is.EqualTo("IMusicQueryable.cs"));

    }

    [Test]
    public void CountPerformance()
    {
      var query = new Query().Name.Contains("micro");
      var results = this.everything.Search(query);

      Assert.That(results.Count, Is.GreaterThan(0));
    }

    private ManualResetEventSlim StartSearchInBackground(string searchString)
    {
      ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);

      Task.Factory.StartNew(() =>
      {
        // Arrange
        this.everything.MatchWholeWord = true;
        var query = new Query().Name.Contains(searchString);

        // Act
        var results = this.everything.Search(query);

        // Assert
        Assert.That(this.everything.LastErrorCode, Is.EqualTo(ErrorCode.Ok));
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
