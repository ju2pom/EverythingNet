using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [TestFixture]
  public class MusicSearchTests
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

    [TestCase("The Wall", ExpectedResult = "album:\"The Wall\"")]
    public string Album(string album)
    {
      var queryable = this.everyThing.Search(true).Music().Album(album);

      return queryable.ToString();
    }

    [TestCase(null)]
    [TestCase("")]
    public void Album_Throws(string album)
    {
      Assert.That(() => this.everyThing.Search(true).Music().Album(album), Throws.ArgumentNullException);
    }


    [TestCase("Pink Floyed", ExpectedResult = "artist:\"Pink Floyed\"")]
    public string Artist(string artist)
    {
      var queryable = this.everyThing.Search(true).Music().Artist(artist);

      return queryable.ToString();
    }


    [TestCase(null)]
    [TestCase("")]
    public void Artist_Throws(string artist)
    {
      Assert.That(() => this.everyThing.Search(true).Music().Artist(artist), Throws.ArgumentNullException);
    }

    [TestCase(0, ExpectedResult = "track:0")]
    [TestCase(2, ExpectedResult = "track:2")]
    public string Track(int track)
    {
      var queryable = this.everyThing.Search(true).Music().Track(track);

      return queryable.ToString();
    }

    /*
    [TestCase(0, 5, ExpectedResult = "track:0-5")]
    [TestCase(2, 7, ExpectedResult = "track:2-7")]
    public string TrackBetween(int min, int max)
    {
      IEverything everything = new Everything();

      everything.Track().Between(min, max);

      return everything.SearchText;
    }

    [TestCase(null, ExpectedResult = "comment:")]
    [TestCase("", ExpectedResult = "comment:")]
    [TestCase("great music", ExpectedResult = "comment:\"great music\"")]
    public string Comment(string comment)
    {
      IEverything everything = new Everything();

      everything.Comment(comment);

      return everything.SearchText;
    }

    [TestCase(null, ExpectedResult = "title:")]
    [TestCase("", ExpectedResult = "title:")]
    [TestCase("Intro", ExpectedResult = "title:Intro")]
    public string Title(string title)
    {
      IEverything everything = new Everything();

      everything.Title(title);

      return everything.SearchText;
    }

    [TestCase(null, ExpectedResult = "genre:")]
    [TestCase("", ExpectedResult = "genre:")]
    [TestCase("Rock", ExpectedResult = "genre:Rock")]
    public string Genre(string genre)
    {
      IEverything everything = new Everything();

      everything.Genre(genre);

      return everything.SearchText;
    }
    */
  }
}