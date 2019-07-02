using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using System.Linq;
    using EverythingNet.Interfaces;
    using EverythingNet.Query;

  [TestFixture]
  public class LogicalQueryTests
  {

    [Test]
    public void And()
    {
      var query = new Query()
          .Name
          .StartWith("prefix")
          .And
          .Name
          .EndWith("suffix");

      Assert.That(query.ToString(), Is.EqualTo("startwith:prefix endwith:suffix"));
    }

    [Test]
    public void AndQuery()
    {
      var sizeQuery = new Query()
          .Size.LessOrEqualThan(100);
      var andQuery = new Query()
          .Name
          .StartWith("prefix")
          .And
          .That(sizeQuery);

      Assert.That(andQuery.ToString(), Is.EqualTo("startwith:prefix size:<=100kb"));
    }

    [Test]
    public void OrAndQuery()
    {
      var nameQuery = new Query()
          .Name.Contains("pattern1")
          .Or
          .Name.Contains("pattern2");

      var andQuery = new Query()
          .Size.GreaterThan(100)
          .And
          .That(nameQuery);

      Assert.That(andQuery.ToString(), Is.EqualTo("size:>100kb pattern1|pattern2"));
    }

    [Test]
    public void MultipleOrQuery()
    {
      var mulitpleOrQuery = new Query()
          .Name.Contains("pattern1")
          .Or
          .Name.Contains("pattern2")
          .Or
          .Name.Contains("pattern3");

      Assert.That(mulitpleOrQuery.ToString(), Is.EqualTo("pattern1|pattern2|pattern3"));
    }

    [Test]
    public void MultipleOrQuery2()
    {
      var q1 = new Query().Name.Contains("pattern1");
      var q2 = new Query().Name.Contains("pattern2");
      var q3 = new Query().Name.Contains("pattern3");
      var q4 = q1.Or.That(q2);
      var q5 = q4.Or.That(q3);

      Assert.That(q5.ToString(), Is.EqualTo("pattern1|pattern2|pattern3"));
    }

    [Test]
    public void FilesQuery()
    {
      var filesQuery = new Query().Files.Name;

      Assert.That(filesQuery.ToString(), Is.EqualTo("files:"));
    }

    [Test]
    public void FoldersQuery()
    {
      var foldersQuery = new Query().Folders.Name;

      Assert.That(foldersQuery.ToString(), Is.EqualTo("folders:"));
    }


    [Test]
    public void AndOrCombinationQuery()
    {
      string[] includedPath = new[] { "first", "second", "third" };
      string[] excludedPath = new[] { "earth", "mars", "saturn" };
      string[] extensions = new[] { "abc", "def", "ghi" };

      IQuery extQuery = new Query().Name.Extensions(extensions);
      IQuery incQuery = includedPath.Any()
        ? includedPath.Skip(1).Aggregate(new Query().Name.Contains(includedPath.First()), (current, next) => current.Or.Name.Contains(next))
        : new Query();
      IQuery excQuery = excludedPath.Any()
        ? excludedPath.Skip(1).Aggregate(new Query().Not.Name.Contains(excludedPath.First()), (current, next) => current.And.Not.Name.Contains(next))
        : new Query();

      var resultQuery = extQuery.And.That(incQuery).And.That(excQuery);

      Assert.That(resultQuery.ToString(), Is.EqualTo("ext:abc;def;ghi first|second|third !earth !mars !saturn"));
    }

    [Test]
    public void AcceptanceTest()
    {
      using (var everything = new Everything())
      {
        var query = new Query()
            .Name
            .StartWith("Date")
            .And
            .Name
            .Extension("cs");

        Assert.That(everything.Search(query).Count(), Is.GreaterThan(0));
      }
    }
  }
}
