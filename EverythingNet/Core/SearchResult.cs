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
        string ptr = EverythingWrapper.Everything_GetResultFileNameW(this.index);

        return ptr.ToString();
      }
    }

    public UInt64 Size
    {
      get
      {
        UInt64 size = 0;

        unsafe
        {
          IntPtr ptr = new IntPtr(&size);
          EverythingWrapper.Everything_GetResultSize(this.index, ptr);
        }

        return size;
      }
    }

    public DateTime Created { get; }
    public DateTime Modified { get; }
    public DateTime Accessed { get; }
    public DateTime Executed { get; }
  }
}