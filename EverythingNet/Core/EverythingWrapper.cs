using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace EverythingNet.Core
{
  internal class EverythingWrapper
  {
    private static readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();


    private class Locker : IDisposable
    {
      private readonly ReaderWriterLockSlim locker;

      public Locker(ReaderWriterLockSlim locker)
      {
        this.locker = locker;
        this.locker.EnterWriteLock();
      }

      public void Dispose()
      {
        this.locker.ExitWriteLock();
      }
    }

#if WIN32
    private const string EverythingDLL = "Everything32.dll";
#else
    private const string EverythingDLL = "Everything64.dll";
#endif

    const int EVERYTHING_OK = 0;
    const int EVERYTHING_ERROR_MEMORY = 1;
    const int EVERYTHING_ERROR_IPC = 2;
    const int EVERYTHING_ERROR_REGISTERCLASSEX = 3;
    const int EVERYTHING_ERROR_CREATEWINDOW = 4;
    const int EVERYTHING_ERROR_CREATETHREAD = 5;
    const int EVERYTHING_ERROR_INVALIDINDEX = 6;
    const int EVERYTHING_ERROR_INVALIDCALL = 7;

    internal static IDisposable Lock()
    {
      return new Locker(locker);
    }

    [DllImport(EverythingDLL)]
    public static extern bool Everything_IsDBLoaded();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetMajorVersion();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetMinorVersion();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetRevision();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetBuildNumber();

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern int Everything_SetSearchW(string lpSearchString);
    [DllImport(EverythingDLL)]
    public static extern int Everything_SetSearchA(string lpSearchString);
    [DllImport(EverythingDLL)]
    public static extern void Everything_SetMatchPath(bool bEnable);
    [DllImport(EverythingDLL)]
    public static extern void Everything_SetMatchCase(bool bEnable);
    [DllImport(EverythingDLL)]
    public static extern void Everything_SetMatchWholeWord(bool bEnable);
    [DllImport(EverythingDLL)]
    public static extern void Everything_SetRegex(bool bEnable);
    [DllImport(EverythingDLL)]
    public static extern void Everything_SetMax(int dwMax);
    [DllImport(EverythingDLL)]
    public static extern void Everything_SetOffset(int dwOffset);
    [DllImport(EverythingDLL)]
    public static extern void Everything_SetReplyWindow(IntPtr handler);
    [DllImport(EverythingDLL)]
    public static extern void Everything_SetReplyID(uint nId);

    [DllImport(EverythingDLL)]
    public static extern void Everything_Reset();
    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetMatchPath();
    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetMatchCase();
    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetMatchWholeWord();
    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetRegex();
    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetMax();
    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetOffset();
    [DllImport(EverythingDLL)]
    public static extern IntPtr Everything_GetSearchW();
    [DllImport(EverythingDLL)]
    public static extern IntPtr Everything_GetSearchA();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetLastError();

    [DllImport(EverythingDLL)]
    public static extern bool Everything_QueryW(bool bWait);
    [DllImport(EverythingDLL)]
    public static extern bool Everything_QueryA(bool bWait);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SortResultsByPath();

    [DllImport(EverythingDLL)]
    public static extern int Everything_GetNumFileResults();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetNumFolderResults();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetNumResults();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetTotFileResults();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetTotFolderResults();
    [DllImport(EverythingDLL)]
    public static extern int Everything_GetTotResults();
    [DllImport(EverythingDLL)]
    public static extern bool Everything_IsVolumeResult(int nIndex);
    [DllImport(EverythingDLL)]
    public static extern bool Everything_IsFolderResult(int nIndex);
    [DllImport(EverythingDLL)]
    public static extern bool Everything_IsFileResult(int nIndex);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetRequestFlags(uint flags);

    [DllImport(EverythingDLL)]
    public static extern void Everything_GetResultFullPathNameA(int nIndex, StringBuilder lpString, int nMaxCount);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern void Everything_GetResultFullPathNameW(int nIndex, StringBuilder lpString, int nMaxCount);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static extern string Everything_GetResultFileNameW(int nIndex);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern IntPtr Everything_GetResultPath(int nIndex);

    [DllImport(EverythingDLL)]
    public static extern IntPtr Everything_GetResultSize(int nIndex, IntPtr size);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern IntPtr Everything_GetResultDateModified(int nIndex, IntPtr date);
  }
}
