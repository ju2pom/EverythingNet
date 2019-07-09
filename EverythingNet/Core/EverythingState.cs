namespace EverythingNet.Core
{
  using System;
  using System.IO;
  using System.Reflection;
  using System.Threading;
  using EverythingNet.Interfaces;

  public static class EverythingState
  {
    private const int ReadyTimeout = 60 * 1000; // 1min

    public enum StartMode
    {
      Install,
      Service,
    }

    public static bool IsStarted()
    {
      Version version = GetVersion();

      return version.Major > 0;
    }

    public static bool StartService(bool admin, StartMode mode)
    {
      if (!IsStarted())
      {
        string option = admin ? "-admin" : string.Empty;

        switch (mode)
        {
          case StartMode.Install:
            option += " -install-service";
            break;
          case StartMode.Service:
            option += " -startup";
            break;
        }

        StartProcess(option);

        int idleTime = 100;
        int remainingTime = ReadyTimeout;
        while(remainingTime > 0 && !IsStarted())
        {
          Thread.Sleep(idleTime);
          remainingTime -= idleTime;
        }

        return IsStarted();
      }

      return true;
    }

    public static bool IsReady()
    {
      return EverythingWrapper.Everything_IsDBLoaded();
    }

    public static Version GetVersion()
    {
      UInt32 major = EverythingWrapper.Everything_GetMajorVersion();
      UInt32 minor = EverythingWrapper.Everything_GetMinorVersion();
      UInt32 build = EverythingWrapper.Everything_GetBuildNumber();
      UInt32 revision = EverythingWrapper.Everything_GetRevision();

      return new Version(Convert.ToInt32(major), Convert.ToInt32(minor), Convert.ToInt32(build), Convert.ToInt32(revision));
    }

    public static ErrorCode GetLastError()
    {
      return (ErrorCode)EverythingWrapper.Everything_GetLastError();
    }

    internal static void StartProcess(string options)
    {
      string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      string exePath = Path.GetFullPath(Path.Combine(path, @"Everything.exe"));

      System.Diagnostics.Process.Start(exePath, options);
    }
  }
}