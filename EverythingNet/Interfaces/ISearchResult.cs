namespace EverythingNet.Interfaces
{
  using System;

  public interface ISearchResult
  {
    bool IsFile { get; }

    string FullPath { get; }

    string Path { get; }

    string FileName { get; }

    long Size { get; }

    uint Attributes { get; }

    DateTime Created { get; }

    DateTime Modified { get; }

    DateTime Accessed { get; }

    DateTime Executed { get; }
  }
}