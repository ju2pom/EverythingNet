using EverythingNet.Core;
using EverythingNet.Extensions;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [TestFixture]
  public class SizeSearchTests
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

    [TestCase(SizeSearch.SizeStandard.Empty, ExpectedResult = "size:empty")]
    [TestCase(SizeSearch.SizeStandard.Gigantic, ExpectedResult = "size:gigantic")]
    [TestCase(SizeSearch.SizeStandard.Huge, ExpectedResult = "size:huge")]
    [TestCase(SizeSearch.SizeStandard.Large, ExpectedResult = "size:large")]
    [TestCase(SizeSearch.SizeStandard.Medium, ExpectedResult = "size:medium")]
    [TestCase(SizeSearch.SizeStandard.Small, ExpectedResult = "size:small")]
    [TestCase(SizeSearch.SizeStandard.Tiny, ExpectedResult = "size:tiny")]
    [TestCase(SizeSearch.SizeStandard.Unknown, ExpectedResult = "size:unknown")]
    public string StandardSize(SizeSearch.SizeStandard standardSize)
    {
      IEverything everything = new Everything();

      everything
        .Size()
        .Standard(standardSize);

      return everything.SearchText;
    }

    [TestCase(SizeSearch.SizeUnit.Kb, 10, ExpectedResult = "size:>10kb")]
    [TestCase(SizeSearch.SizeUnit.Mb, 10, ExpectedResult = "size:>10mb")]
    [TestCase(SizeSearch.SizeUnit.Gb, 10, ExpectedResult = "size:>10gb")]
    public string SizeGreater(SizeSearch.SizeUnit unit, int value)
    {
      IEverything everything = new Everything();

      everything
        .Size()
        .Greater(value)
        .Unit(unit);

      return everything.SearchText;
    }

    [TestCase(SizeSearch.SizeUnit.Kb, 10, 100, ExpectedResult = "size:10kb-100kb")]
    [TestCase(SizeSearch.SizeUnit.Mb, 10, 100, ExpectedResult = "size:10mb-100mb")]
    [TestCase(SizeSearch.SizeUnit.Gb, 10, 100, ExpectedResult = "size:10gb-100gb")]
    public string SizeBetween(SizeSearch.SizeUnit unit, int min, int max)
    {
      IEverything everything = new Everything();

      everything
        .Size()
        .Between(min, max, unit);

      return everything.SearchText;
    }
  }
}