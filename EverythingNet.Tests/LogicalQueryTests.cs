using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using System.Linq;

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
