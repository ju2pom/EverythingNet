using System;

namespace EverythingNet.Core
{
  public static class EverythingState
  {
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
  }
}
