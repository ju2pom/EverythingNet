using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using System.Linq;

  using EverythingNet.Query;

  [TestFixture]
  public class MusicSearchTests
  {
    [TestCase("The Wall", ExpectedResult = "album:\"The Wall\"")]
    public string Album(string album)
    {
      var query = new Query().Music.Album(album);

      return query.ToString();
    }

    [TestCase(null)]
    [TestCase("")]
    public void Album_Null(string album)
    {
      Assert.That(() => new Query().Music.Album(album).ToString(), Is.Empty);
    }


    [TestCase("Pink Floyed", ExpectedResult = "artist:\"Pink Floyed\"")]
    public string Artist(string artist)
    {
      var query = new Query().Music.Artist(artist);

      return query.ToString();
    }


    [TestCase(null)]
    [TestCase("")]
    public void Artist_Null(string artist)
    {
      Assert.That(() => new Query().Music.Artist(artist).ToString(), Is.Empty);
    }

    [TestCase(0, ExpectedResult = "track:0")]
    [TestCase(2, ExpectedResult = "track:2")]
    public string Track(int? track)
    {
      var query = new Query().Music.Track(track);

      return query.ToString();
    }


    [TestCase("great music", ExpectedResult = "comment:\"great music\"")]
    public string Comment(string comment)
    {
      var query = new Query().Music.Comment(comment);

      return query.ToString();
    }

    [TestCase("Intro", ExpectedResult = "title:Intro")]
    public string Title(string title)
    {
      var query = new Query().Music.Title(title);

      return query.ToString();
    }

    [TestCase("Rock", ExpectedResult = "genre:Rock")]
    public string Genre(string genre)
    {
      var query = new Query().Music.Genre(genre);

      return query.ToString();
    }


    [Test]
    public void AcceptanceTest()
    {
      using (var everything = new Everything())
      {
        var query = new Query().Music.Genre("Bird");

        Assert.That(everything.Search(query).Count(), Is.GreaterThan(0));
      }
    }
  }
}