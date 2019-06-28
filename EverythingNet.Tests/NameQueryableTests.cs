using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using System.Linq;

  using EverythingNet.Query;

  [TestFixture]
  public class Namequery
  {
    [TestCase("*.abc", ExpectedResult = "*.abc")]
    [TestCase("Any value", ExpectedResult = "\"Any value\"")]
    public string Contains(string name)
    {
      var query = new Query().Name.Contains(name);

      return query.ToString();
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc|*.def")]
    [TestCase("\"Any value\"", "\"another value\"", ExpectedResult = "\"Any value\"|\"another value\"")]
    public string Or(string search1, string search2)
    {
      var query = new Query().Name
        .Contains(search1)
        .Or
        .Name
        .Contains(search2);

      return query.ToString();
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc *.def")]
    [TestCase("\"Any value\"", "\"another value\"", ExpectedResult = "\"Any value\" \"another value\"")]

    public string And(string search1, string search2)
    {
      var query = new Query().Name
        .Contains(search1)
        .And
        .Name
        .Contains(search2);

      return query.ToString();
    }

    [TestCase("*.abc", "*.def", ExpectedResult = "*.abc !*.def")]
    public string Not(string search1, string search2)
    {
      var query = new Query().Name
        .Contains(search1)
        .And
        .Not
        .Name
        .Contains(search2);

      return query.ToString();
    }

    [TestCase("prefix", ExpectedResult = "startwith:prefix")]
    public string StartWith(string pattern)
    {
      var query = new Query().Name.StartWith(pattern);

      return query.ToString();
    }

    [TestCase("postfix", ExpectedResult = "endwith:postfix")]
    public string EndWith(string pattern)
    {
      var query = new Query().Name.EndWith(pattern);

      return query.ToString();
    }

    [TestCase("cs", ExpectedResult = "ext:cs")]
    [TestCase("xaml", ExpectedResult = "ext:xaml")]
    public string Extension(string search)
    {
      var query = new Query().Name.Extension(search);

      return query.ToString();
    }

    [TestCase("cs csproj xaml", ExpectedResult = "ext:cs;csproj;xaml")]
    [TestCase("jpg png tif", ExpectedResult = "ext:jpg;png;tif")]
    public string Extensions(string search)
    {
      var extensions = search.Split(' ');
      var query = new Query().Name.Extensions(extensions);

      return query.ToString();
    }

    [Test]
    public void ExtensionsWithParam()
    {
      var query = new Query().Name.Extensions("jpg", "png", "bmp", "tif");

      Assert.That(query.ToString(), Is.EqualTo("ext:jpg;png;bmp;tif"));
    }

    [Test]
    public void AcceptanceTest()
    {
      using (var everything = new Everything())
      {
        var query = new Query().Name.Contains("user");

        Assert.That(everything.Search(query).Count(), Is.GreaterThan(0));
      }
    }
  }
}