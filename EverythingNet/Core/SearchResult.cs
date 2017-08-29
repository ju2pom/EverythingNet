using System;
using System.Runtime.InteropServices;
using System.Text;
using EverythingNet.Interfaces;

namespace EverythingNet.Core
{
  internal class SearchResult : ISearchResult
  {
    private readonly int index;

    public SearchResult(int index)
    {
      this.index = index;
    }

    public string FullPath
    {
      get
      {
        var builder = new StringBuilder(260);

        EverythingWrapper.Everything_GetResultFullPathNameW(this.index, builder, 260);

        return builder.ToString();
      }
    }

    public string Path
    {
      get
      {
        IntPtr ptr = EverythingWrapper.Everything_GetResultPath(this.index);

        return Marshal.PtrToStringAuto(ptr);
      }
    }

    public string FileName
    {
      get
      {
        StringBuilder builder = new StringBuilder(260);
        EverythingWrapper.Everything_GetResultFileName(this.index, builder, 260);

        return builder.ToString();
      }
    }

    public long Size
    {
      get
      {
        IntPtr ptr = new IntPtr();
        EverythingWrapper.Everything_GetResultSize(this.index, ptr);

        return ptr.ToInt64();
      }
    }

    public DateTime Created { get; }
    public DateTime Modified { get; }
    public DateTime Accessed { get; }
    public DateTime Executed { get; }
  }
}