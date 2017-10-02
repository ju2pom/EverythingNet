using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  [TestFixture]
  public class MusicSearchTests
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

    [TestCase("The Wall", ExpectedResult = "album:\"The Wall\"")]
    public string Album(string album)
    {
      var queryable = this.everything
        .Search()
        .Music
        .Album(album);

      return queryable.ToString();
    }

    [TestCase(null)]
    [TestCase("")]
    public void Album_Null(string album)
    {
      Assert.That(() => this.everything.Search().Music.Album(album).ToString(), Is.Empty);
    }


    [TestCase("Pink Floyed", ExpectedResult = "artist:\"Pink Floyed\"")]
    public string Artist(string artist)
    {
      var queryable = this.everything.Search().Music.Artist(artist);

      return queryable.ToString();
    }


    [TestCase(null)]
    [TestCase("")]
    public void Artist_Null(string artist)
    {
      Assert.That(() => this.everything.Search().Music.Artist(artist).ToString(), Is.Empty);
    }

    [TestCase(0, ExpectedResult = "track:0")]
    [TestCase(2, ExpectedResult = "track:2")]
    public string Track(int? track)
    {
      var queryable = this.everything.Search().Music.Track(track);

      return queryable.ToString();
    }


    [TestCase("great music", ExpectedResult = "comment:\"great music\"")]
    public string Comment(string comment)
    {
      var queryable = this.everything.Search().Music.Comment(comment);

      return queryable.ToString();
    }

    [TestCase("Intro", ExpectedResult = "title:Intro")]
    public string Title(string title)
    {
      var queryable = this.everything.Search().Music.Title(title);

      return queryable.ToString();
    }

    [TestCase("Rock", ExpectedResult = "genre:Rock")]
    public string Genre(string genre)
    {
      var queryable = this.everything.Search().Music.Genre(genre);

      return queryable.ToString();
    }
  }
}