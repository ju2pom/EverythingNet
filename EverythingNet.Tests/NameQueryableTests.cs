using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [TestFixture]
  public class NameQueryable
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

    [TestCase("*.abc", ExpectedResult = "*.abc")]
    [TestCase("Any value", ExpectedResult = "\"Any value\"")]
    public string Contains(string name)
    {
      var queryable = this.everyThing
        .Search()
        .Name
        .Contains(name);

      return queryable.ToString();
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc|*.def")]
    [TestCase("\"Any value\"", "\"another value\"", ExpectedResult = "\"Any value\"|\"another value\"")]
    public string Or(string search1, string search2)
    {
      var queryable = this.everyThing
        .Search()
        .Name
        .Contains(search1)
        .Or
        .Name
        .Contains(search2);

      return queryable.ToString();
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc *.def")]
    [TestCase("\"Any value\"", "\"another value\"", ExpectedResult = "\"Any value\" \"another value\"")]

    public string And(string search1, string search2)
    {
      var queryable = this.everyThing
        .Search()
        .Name
        .Contains(search1)
        .And
        .Name
        .Contains(search2);

      return queryable.ToString();
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc !*.def")]
    public string Not(string search1, string search2)
    {
      var queryable = this.everyThing
        .Search()
        .Name
        .Contains(search1)
        .And
        .Not
        .Name
        .Contains(search2);

      return queryable.ToString();
    }

    [TestCase("prefix", ExpectedResult = "startwith:prefix")]
    public string StartWith(string pattern)
    {
      var queryable = this.everyThing
        .Search()
        .Name
        .StartWith(pattern);

      return queryable.ToString();
    }

    [TestCase("postfix", ExpectedResult = "endwith:postfix")]
    public string EndWith(string pattern)
    {
      var queryable = this.everyThing
        .Search()
        .Name
        .EndWith(pattern);

      return queryable.ToString();
    }

    [TestCase("cs", ExpectedResult = "ext:cs")]
    [TestCase("xaml", ExpectedResult = "ext:xaml")]
    public string Extension(string search)
    {
      var queryable = this.everyThing.
        Search()
        .Name
        .Extension(search);

      return queryable.ToString();
    }

    [TestCase("cs csproj xaml", ExpectedResult = "ext:cs;csproj;xaml")]
    [TestCase("jpg png tif", ExpectedResult = "ext:jpg;png;tif")]
    public string Extensions(string search)
    {
      var extensions = search.Split(' ');
      var queryable = this.everyThing
        .Search()
        .Name
        .Extensions(extensions);

      return queryable.ToString();
    }

    [Test]
    public void ExtensionsWithParam()
    {
      var queryable = this.everyThing
                          .Search()
                          .Name
                          .Extensions("jpg", "png", "bmp", "tif");

      Assert.That(queryable.ToString(), Is.EqualTo("ext:jpg;png;bmp;tif"));
    }
  }
}