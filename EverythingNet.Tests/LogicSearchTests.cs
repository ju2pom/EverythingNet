using EverythingNet.Core;
using EverythingNet.Extensions;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [TestFixture]
  public class LogicSearchTests
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

    [TestCase("*.abc", ExpectedResult = "*.abc")]
    [TestCase("Any value", ExpectedResult = "\"Any value\"")]
    public string Is(string search)
    {
      this.everyThing.Is(search);

      return everyThing.SearchText;
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc|*.def")]
    [TestCase("\"Any value\"", "\"another value\"", ExpectedResult = "\"Any value\"|\"another value\"")]
    public string Or(string search1, string search2)
    {
      this.everyThing
        .Is(search1)
        .Or()
        .Is(search2);

      return everyThing.SearchText;
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc *.def")]
    [TestCase("\"Any value\"", "\"another value\"", ExpectedResult = "\"Any value\" \"another value\"")]

    public string And(string search1, string search2)
    {
      this.everyThing
        .Is(search1)
        .And()
        .Is(search2);

      return everyThing.SearchText;
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc !*.def")]
    public string Not(string search1, string search2)
    {
      this.everyThing
        .Is(search1)
        .And().Not()
        .Is(search2);

      return everyThing.SearchText;
    }

    [TestCase(2002, ExpectedResult = "year:>=2002")]
    public string GreaterOrEqual(int value)
    {
      this.everyThing.Is("year:").GreaterOrEqualThan(value);

      return everyThing.SearchText;
    }

    [TestCase(2002, ExpectedResult = "year:<=2002")]
    public string LessOrEqual(int value)
    {
      this.everyThing.Is("year:").LessOrEqualThan(value);

      return everyThing.SearchText;
    }

    [TestCase(2002, ExpectedResult = "year:>2002")]
    public string Greater(int value)
    {
      this.everyThing.Is("year:").GreaterThan(value);

      return everyThing.SearchText;
    }

    [TestCase(2002, ExpectedResult = "year:<2002")]
    public string Less(int value)
    {
      this.everyThing.Is("year:").LessThan(value);

      return everyThing.SearchText;
    }
  }
}