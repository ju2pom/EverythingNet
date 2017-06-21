using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [TestFixture]
  public class FileSearchTests
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
    public void Only()
    {
      var queryable = this.everyThing.Search().File().Only();

      Assert.That(queryable.ToString(), Is.EqualTo("file:"));
    }

    [TestCase("", ExpectedResult = "audio:")]
    [TestCase(null, ExpectedResult = "audio:")]
    [TestCase("music.mp3", ExpectedResult = "audio:music.mp3")]
    public string Audio(string search)
    {
      var queryable = this.everyThing.Search().File().Audio(search);

      return queryable.ToString();
    }

    [TestCase("", ExpectedResult = "zip:")]
    [TestCase(null, ExpectedResult = "zip:")]
    [TestCase("archive.zip", ExpectedResult = "zip:archive.zip")]
    public string Zip(string search)
    {
      var queryable = this.everyThing.Search().File().Zip(search);

      return queryable.ToString();
    }


    [TestCase("", ExpectedResult = "video:")]
    [TestCase(null, ExpectedResult = "video:")]
    [TestCase("movie.avi", ExpectedResult = "video:movie.avi")]
    public string Video(string search)
    {
      var queryable = this.everyThing.Search().File().Video(search);

      return queryable.ToString();
    }

    [TestCase("", ExpectedResult = "pic:")]
    [TestCase(null, ExpectedResult = "pic:")]
    [TestCase("image.jpg", ExpectedResult = "pic:image.jpg")]
    public string Picture(string search)
    {
      var queryable = this.everyThing.Search().File().Picture(search);

      return queryable.ToString();
    }

    [TestCase("", ExpectedResult = "exe:")]
    [TestCase(null, ExpectedResult = "exe:")]
    [TestCase("application.exe", ExpectedResult = "exe:application.exe")]
    public string Exe(string search)
    {
      var queryable = this.everyThing.Search().File().Exe(search);

      return queryable.ToString();
    }

    [TestCase("", ExpectedResult = "doc:")]
    [TestCase(null, ExpectedResult = "doc:")]
    [TestCase("report.doc", ExpectedResult = "doc:report.doc")]
    public string Document(string search)
    {
      var queryable = this.everyThing.Search().File().Document(search);

      return queryable.ToString();
    }

    [TestCase("", ExpectedResult = "dupe:")]
    [TestCase(null, ExpectedResult = "dupe:")]
    [TestCase("main.cs", ExpectedResult = "dupe:main.cs")]
    public string Duplicates(string search)
    {
      var queryable = this.everyThing.Search().File().Duplicates(search);

      return queryable.ToString();
    }
  }
}
