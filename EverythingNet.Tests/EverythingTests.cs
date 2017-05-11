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
    [SetUp]
    public void SetUp()
    {
    }

    [TestCase("text1")]
    [TestCase("text2")]
    public void SearchText(string searchText)
    {
      // Arrange
      Everything everything = new Everything();

      // Act
      everything.SearchText = searchText;

      // Assert
      Assert.AreEqual(searchText, everything.SearchText);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void MatchCase(bool matchCase)
    {
      // Arrange
      Everything everything = new Everything();

      // Act
      everything.MatchCase = matchCase;

      // Assert
      Assert.AreEqual(matchCase, everything.MatchCase);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void MatchPath(bool matchPath)
    {
      // Arrange
      Everything everything = new Everything();

      // Act
      everything.MatchPath = matchPath;

      // Assert
      Assert.AreEqual(matchPath, everything.MatchPath);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void MatchWholeWord(bool matchWholeWord)
    {
      // Arrange
      Everything everything = new Everything();

      // Act
      everything.MatchWholeWord = matchWholeWord;

      // Assert
      Assert.AreEqual(matchWholeWord, everything.MatchWholeWord);
    }

    [Test]
    public void Search()
    {
      // Arrange
      Everything everything = new Everything();
      string searchText = Assembly.GetExecutingAssembly().GetName().Name + ".dll";
      everything.SearchText = searchText;
      everything.MatchWholeWord = true;

      // Act
      ErrorCode errorCode = everything.Search(IntPtr.Zero, true);

      IEnumerable<string> results = everything.GetResults().ToList();

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
      Everything everything = new Everything();
      everything.SearchText = searchText;
      everything.MatchWholeWord = true;

      // Act
      ErrorCode errorCode = everything.Search(IntPtr.Zero, true);

      IEnumerable<string> results = everything.GetResults().ToList();

      // Assert
      Assert.AreEqual(ErrorCode.Ok, errorCode);
      CollectionAssert.IsNotEmpty(results);
    }

    [TestCase("child:cr2|nef|srw|arw|crw|mrw|raf|pef|orf|rw2|nrw|dng")]
    public void Search_ForFolders(string searchText)
    {
      // Arrange
      Everything everything = new Everything();
      everything.SearchText = searchText;

      // Act
      ErrorCode errorCode = everything.Search(IntPtr.Zero, true);
      List<string> results = everything.GetResults().ToList();

      // Assert
      Assert.AreEqual(ErrorCode.Ok, errorCode);
      CollectionAssert.IsNotEmpty(results);
      results.ForEach(x => Directory.Exists(x));
    }

    [TestCase("parents:0")]
    public void Search_ForRootFolders(string searchText)
    {
      // Arrange
      Everything everything = new Everything();
      everything.SearchText = searchText;

      // Act
      ErrorCode errorCode = everything.Search(IntPtr.Zero, true);
      List<string> results = everything.GetResults().ToList();

      // Assert
      Assert.AreEqual(ErrorCode.Ok, errorCode);
      CollectionAssert.IsNotEmpty(results);
      results.ForEach(x => Directory.Exists(x));
    }
  }
}
