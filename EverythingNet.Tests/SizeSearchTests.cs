using EverythingNet.Core;
using EverythingNet.Query;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using System.Linq;

  using Query = EverythingNet.Query.Query;

  [TestFixture]
  public class SizeSearchTests
  {
    [TestCase(Sizes.Empty, ExpectedResult = "size:empty")]
    [TestCase(Sizes.Gigantic, ExpectedResult = "size:gigantic")]
    [TestCase(Sizes.Huge, ExpectedResult = "size:huge")]
    [TestCase(Sizes.Large, ExpectedResult = "size:large")]
    [TestCase(Sizes.Medium, ExpectedResult = "size:medium")]
    [TestCase(Sizes.Small, ExpectedResult = "size:small")]
    [TestCase(Sizes.Tiny, ExpectedResult = "size:tiny")]
    [TestCase(Sizes.Unknown, ExpectedResult = "size:unknown")]
    public string StandardSize(Sizes standardSize)
    {
      var query = new Query().Size.Equal(standardSize);

      return query.ToString();
    }

    [TestCase(SizeUnit.Kb, 10, ExpectedResult = "size:>10kb")]
    [TestCase(SizeUnit.Mb, 10, ExpectedResult = "size:>10mb")]
    [TestCase(SizeUnit.Gb, 10, ExpectedResult = "size:>10gb")]
    public string SizeGreater(SizeUnit unit, int value)
    {
      var query = new Query().Size.GreaterThan(value, unit);

      return query.ToString();
    }

    [TestCase(SizeUnit.Kb, 10, ExpectedResult = "size:>=10kb")]
    [TestCase(SizeUnit.Mb, 10, ExpectedResult = "size:>=10mb")]
    [TestCase(SizeUnit.Gb, 10, ExpectedResult = "size:>=10gb")]
    public string SizeGreaterOrEqual(SizeUnit unit, int value)
    {
      var query = new Query().Size.GreaterOrEqualThan(value, unit);

      return query.ToString();
    }

    [TestCase(SizeUnit.Kb, 10, ExpectedResult = "size:<10kb")]
    [TestCase(SizeUnit.Mb, 10, ExpectedResult = "size:<10mb")]
    [TestCase(SizeUnit.Gb, 10, ExpectedResult = "size:<10gb")]
    public string SizeLess(SizeUnit unit, int value)
    {
      var query = new Query().Size.LessThan(value, unit);

      return query.ToString();
    }

    [TestCase(SizeUnit.Kb, 10, ExpectedResult = "size:<=10kb")]
    [TestCase(SizeUnit.Mb, 10, ExpectedResult = "size:<=10mb")]
    [TestCase(SizeUnit.Gb, 10, ExpectedResult = "size:<=10gb")]
    public string SizeLessOrEqual(SizeUnit unit, int value)
    {
      var query = new Query().Size.LessOrEqualThan(value, unit);

      return query.ToString();
    }

    [TestCase(1, 10, ExpectedResult = "size:1Kb-10Kb")]
    [TestCase(400, 800, ExpectedResult = "size:400Kb-800Kb")]
    [TestCase(1000, 2000, ExpectedResult = "size:1000Kb-2000Kb")]
    public string SizeBetweenDefaultUnit(int min, int max)
    {
      var query = new Query().Size.Between(min, max);

      return query.ToString();
    }

    [TestCase(SizeUnit.Kb, 10, 100, ExpectedResult = "size:10Kb-100Kb")]
    [TestCase(SizeUnit.Mb, 10, 100, ExpectedResult = "size:10Mb-100Mb")]
    [TestCase(SizeUnit.Gb, 10, 100, ExpectedResult = "size:10Gb-100Gb")]
    public string SizeBetweenSameUnit(SizeUnit unit, int min, int max)
    {
      var query = new Query().Size.Between(min, max, unit);

      return query.ToString();
    }

    [TestCase(10, SizeUnit.Kb, 100, SizeUnit.Mb, ExpectedResult = "size:10Kb-100Mb")]
    [TestCase(10, SizeUnit.Mb, 100, SizeUnit.Gb, ExpectedResult = "size:10Mb-100Gb")]
    [TestCase(10, SizeUnit.Kb, 100, SizeUnit.Gb, ExpectedResult = "size:10Kb-100Gb")]
    public string SizeBetweenDifferentUnits(int min, SizeUnit minUnit, int max, SizeUnit maxUnit)
    {
      var query = new Query().Size.Between(min, minUnit, max, maxUnit);

      return query.ToString();
    }

    [Test]
    public void AcceptanceTest()
    {
      using (var everything = new Everything())
      {
        var query = new Query().Size.GreaterThan(10, SizeUnit.Kb);

        Assert.That(everything.Search(query).Count(), Is.GreaterThan(0));
      }
    }
  }
}