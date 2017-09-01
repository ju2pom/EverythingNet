namespace EverythingNet.Core
{
  using System;
  using System.Text;

  using EverythingNet.Interfaces;

  internal class SearchResult : ISearchResult
  {
    private delegate bool MyDelegate(uint index, out long date);

    private readonly uint index;

    public SearchResult(int index)
    {
      this.index = Convert.ToUInt32(index);
    }

    public string FullPath
    {
      get
      {
        var builder = new StringBuilder(260);

        EverythingWrapper.Everything_GetResultFullPathName(this.index, builder, 260);

        return builder.ToString();
      }
    }

    public string Path
    {
      get
      {
        //return EverythingWrapper.Everything_GetResultPath(this.index);

        // Temporary implementation until the native function works as expected
        return System.IO.Path.GetDirectoryName(this.FullPath);
      }
    }

    public string FileName
    {
      get
      {
        //return EverythingWrapper.Everything_GetResultFileName(this.index);

        // Temporary implementation until the native function works as expected
        return System.IO.Path.GetFileName(this.FullPath);
      }
    }

    public long Size
    {
      get
      {
        long size;

        EverythingWrapper.Everything_GetResultSize(this.index, out size);

        return size;
      }
    }

    public uint Attributes
    {
      get { return EverythingWrapper.Everything_GetResultAttributes(this.index); }
    }

    public DateTime Created
    {
      get { return this.GenericDate(EverythingWrapper.Everything_GetResultDateCreated); }
    }

    public DateTime Modified
    {
      get { return this.GenericDate(EverythingWrapper.Everything_GetResultDateModified); }
    }

    public DateTime Accessed
    {
      get { return this.GenericDate(EverythingWrapper.Everything_GetResultDateAccessed); }
    }

    public DateTime Executed
    {
      get { return this.GenericDate(EverythingWrapper.Everything_GetResultDateRun); }
    }

    private DateTime GenericDate(MyDelegate func)
    {
      long date;
      if (func(this.index, out date) && date >= 0)
      {
        return DateTime.FromFileTime(date);
      }

      return DateTime.MinValue;
    }
  }
}