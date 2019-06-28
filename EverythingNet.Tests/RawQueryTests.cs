using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using System.Linq;

  using EverythingNet.Query;

  [TestFixture]
  public class RawQueryTests
  {
    [TestCase("path:my/custom/path ext:jpg dm:today")]
    public void Pattern(string rawQuery)
    {
      var query = new Query().That(rawQuery);

      Assert.That(query.ToString(), Is.EqualTo(rawQuery));
    }

    [Test]
    public void AcceptanceTest()
    {
      using (var everything = new Everything())
      {
        var query = new Query().That(@"path:EverythingNet\EverythingNet.Tests\RawQueryTests.cs");

        Assert.That(everything.Search(query).Count(), Is.GreaterThan(0));
      }
    }
  }
}