using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EverythingNet.Core
{
  public static class EverythingState
  {
    public enum StartMode
    {
      Install,
      Service
    }

    public static bool IsStarted()
    {
      return GetEverythingProcess() != null;
    }

    public static bool StartService(bool admin, StartMode mode)
    {
      if (GetEverythingProcess() == null)
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

        return GetEverythingProcess() != null;
      }

      return true;
    }

    public static bool IsReady()
    {
      return EverythingWrapper.Everything_IsDBLoaded();
    }

    public static Version GetVersion()
    {
      int major = EverythingWrapper.Everything_GetMajorVersion();
      int minor = EverythingWrapper.Everything_GetMinorVersion();
      int build = EverythingWrapper.Everything_GetBuildNumber();
      int revision = EverythingWrapper.Everything_GetRevision();

      return new Version(Convert.ToInt32(major), Convert.ToInt32(minor), Convert.ToInt32(build), Convert.ToInt32(revision));
    }

    public static ErrorCode GetLastError()
    {
      return (ErrorCode)EverythingWrapper.Everything_GetLastError();
    }

    private static Process GetEverythingProcess()
    {
      // TODO: Check if it's the correct process
      return Process.GetProcessesByName("Everything").FirstOrDefault();
    }

    private static void StartProcess(string options)
    {
      string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      string exePath = Path.GetFullPath(Path.Combine(path, @"Everything.exe"));

      Process.Start(exePath, options);
    }
  }
}
