using EverythingNet.Core;

namespace EverythingNet.Tests
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Reflection;

  using NUnit.Framework;

  [TestFixture]
  public class EverythingTests
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

    [TestCase("text1")]
    [TestCase("text2")]
    public void SearchText(string searchText)
    {
      // Act
      this.everyThing.SearchText = searchText;

      // Assert
      Assert.AreEqual(searchText, this.everyThing.SearchText);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void MatchCase(bool matchCase)
    {
      // Act
      this.everyThing.MatchCase = matchCase;

      // Assert
      Assert.AreEqual(matchCase, this.everyThing.MatchCase);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void MatchPath(bool matchPath)
    {
      // Act
      this.everyThing.MatchPath = matchPath;

      // Assert
      Assert.AreEqual(matchPath, this.everyThing.MatchPath);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void MatchWholeWord(bool matchWholeWord)
    {
      // Act
      this.everyThing.MatchWholeWord = matchWholeWord;

      // Assert
      Assert.AreEqual(matchWholeWord, this.everyThing.MatchWholeWord);
    }

    [Test]
    public void Search()
    {
      // Arrange
      string searchText = Assembly.GetExecutingAssembly().GetName().Name + ".dll";
      this.everyThing.SearchText = searchText;
      this.everyThing.MatchWholeWord = true;

      // Act
      ErrorCode errorCode = this.everyThing.Search(true, IntPtr.Zero);

      IEnumerable<string> results = this.everyThing.GetResults().ToList();

      // Assert
      Assert.AreEqual(ErrorCode.Ok, errorCode);
      CollectionAssert.IsNotEmpty(results);
    }

    [TestCase("*.cr2")]
    [TestCase("*.nef")]
    [TestCase("*.jpg")]
    [TestCase("*.srw")]
    public void Search_Images(string searchText)
    {
      // Arrange
      this.everyThing.SearchText = searchText;
      this.everyThing.MatchWholeWord = true;

      // Act
      ErrorCode errorCode = this.everyThing.Search(true, IntPtr.Zero);

      IEnumerable<string> results = this.everyThing.GetResults().ToList();

      // Assert
      Assert.AreEqual(ErrorCode.Ok, errorCode);
      CollectionAssert.IsNotEmpty(results);
    }

    [TestCase("child:cr2|nef|srw|arw|crw|mrw|raf|pef|orf|rw2|nrw|dng")]
    public void Search_ForFolders(string searchText)
    {
      // Arrange
      this.everyThing.SearchText = searchText;

      // Act
      ErrorCode errorCode = this.everyThing.Search(true, IntPtr.Zero);
      List<string> results = this.everyThing.GetResults().ToList();

      // Assert
      Assert.AreEqual(ErrorCode.Ok, errorCode);
      CollectionAssert.IsNotEmpty(results);
      results.ForEach(x => Directory.Exists(x));
    }

    [TestCase("parents:0")]
    public void Search_ForRootFolders(string searchText)
    {
      // Arrange
      this.everyThing.SearchText = searchText;

      // Act
      ErrorCode errorCode = this.everyThing.Search(true, IntPtr.Zero);
      List<string> results = this.everyThing.GetResults().ToList();

      // Assert
      Assert.AreEqual(ErrorCode.Ok, errorCode);
      CollectionAssert.IsNotEmpty(results);
      results.ForEach(x => Directory.Exists(x));
    }
  }
}
