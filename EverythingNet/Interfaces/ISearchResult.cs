using System;

namespace EverythingNet.Interfaces
{
  public interface ISearchResult
  {
    string FullPath { get; }

    string Path { get; }

    string FileName { get; }

    UInt64 Size { get; }

    DateTime Created { get; }

    DateTime Modified { get; }

    DateTime Accessed { get; }

    DateTime Executed { get; }
  }
}