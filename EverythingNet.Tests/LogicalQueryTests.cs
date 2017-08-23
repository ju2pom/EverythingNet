using EverythingNet.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingNet.Tests
{
  [TestFixture]
  public class LogicalQueryTests
  {
    private Everything everything;

    [SetUp]
    public void Setup()
    {
      this.everything = new Everything();
    }

    [TearDown]
    public void TearDown()
    {
      this.everything.Dispose();
    }

    [Test]
    public void And()
    {
      var queryable = this.everything.Search().Name().StartWith("prefix").And.Name().EndWith("suffix");

      Assert.That(queryable.ToString(), Is.EqualTo("startwith:prefix endwith:suffix"));
    }

    [Test]
    public void AndQuery()
    {
      var sizeQuery = this.everything.Search().Size().LessOrEqualThan(100);
      var queryable = this.everything.Search().Name().StartWith("prefix").And.Queryable(sizeQuery);

      Assert.That(queryable.ToString(), Is.EqualTo("startwith:prefix size:<=100kb"));
    }
  }
}
