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

    public long Index => this.index;

    public bool IsFile => EverythingWrapper.Everything_IsFileResult(this.index);

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
        try
        {
          return System.IO.Path.GetDirectoryName(this.FullPath);
        }
        catch (Exception e)
        {
          this.LastException = e;

          return this.FullPath;
        }
      }
    }

    public string FileName
    {
      get
      {
        //return EverythingWrapper.Everything_GetResultFileName(this.index);

        // Temporary implementation until the native function works as expected
        try
        {
          return System.IO.Path.GetFileName(this.FullPath);
        }
        catch (Exception e)
        {
          this.LastException = e;

          return this.FullPath;
        }
      }
    }

    public long Size
    {
      get
      {
        EverythingWrapper.Everything_GetResultSize(this.index, out var size);

        return size;
      }
    }

    public uint Attributes => EverythingWrapper.Everything_GetResultAttributes(this.index);

    public DateTime Created => this.GenericDate(EverythingWrapper.Everything_GetResultDateCreated);

    public DateTime Modified => this.GenericDate(EverythingWrapper.Everything_GetResultDateModified);

    public DateTime Accessed => this.GenericDate(EverythingWrapper.Everything_GetResultDateAccessed);

    public DateTime Executed => this.GenericDate(EverythingWrapper.Everything_GetResultDateRun);

    public Exception LastException { get; private set; }

    private DateTime GenericDate(MyDelegate func)
    {
      if (func(this.index, out var date) && date >= 0)
      {
        return DateTime.FromFileTime(date);
      }

      return DateTime.MinValue;
    }
  }
}