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
    public string Is(string name)
    {
      var queryable = this.everyThing.Search(true).Name(name);

      return queryable.ToString();
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc|*.def")]
    [TestCase("\"Any value\"", "\"another value\"", ExpectedResult = "\"Any value\"|\"another value\"")]
    public string Or(string search1, string search2)
    {
      var queryable = this.everyThing.Search(true).Name(search1).Or.Name(search2);

      return queryable.ToString();
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc *.def")]
    [TestCase("\"Any value\"", "\"another value\"", ExpectedResult = "\"Any value\" \"another value\"")]

    public string And(string search1, string search2)
    {
      var queryable = this.everyThing.Search(true).Name(search1).And.Name(search2);

      return queryable.ToString();
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc !*.def")]
    public string Not(string search1, string search2)
    {
      var queryable = this.everyThing.Search(true).Name(search1).And.Not.Name(search2);

      return queryable.ToString();
    }

    [TestCase("prefix", ExpectedResult = "startwith:prefix")]
    public string StartWith(string pattern)
    {
      var queryable = this.everyThing.Search(true).Name().StartWith(pattern);

      return queryable.ToString();
    }

    [TestCase("postfix", ExpectedResult = "endwith:postfix")]
    public string EndWith(string pattern)
    {
      var queryable = this.everyThing.Search(true).Name().EndWith(pattern);

      return queryable.ToString();
    }
  }
}