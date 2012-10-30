using System;

namespace AceSoft
{
    public enum OSVersion
    {
        Windows95,
        Windows98SecondEdition,
        Windows98,
        WindowsMe,
        WindowsNT351,
        WindowsNT4,
        Windows2000,
        WindowsXP,
        WindowsVista,
        Windows7,
        Unknown
    }
    public class OSInformation
    {
        public static OSVersion getOSVersion()
        {
            OSVersion osRetValue = OSVersion.Unknown;
 
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            switch (osInfo.Platform)
            {
                case System.PlatformID.Win32Windows:

                    switch (osInfo.Version.Minor)
                    {
                        case 0:
                            osRetValue = OSVersion.Windows95; break;
                        case 10:
                            if (osInfo.Version.Revision.ToString() == "2222A")
                                osRetValue = OSVersion.Windows98SecondEdition; 
                            else
                                osRetValue = OSVersion.Windows98SecondEdition;
                            break;
                        case 90:
                            osRetValue = OSVersion.WindowsMe; break;
                    }
                    break;
                case PlatformID.Win32NT:
                    switch (osInfo.Version.Major)
                    {
                        case 3:
                            osRetValue = OSVersion.WindowsNT351; break;
                        case 4:
                            osRetValue = OSVersion.WindowsNT4; break;
                        case 5:
                            if (osInfo.Version.Minor == 0)
                                osRetValue = OSVersion.Windows2000; 
                            else
                                osRetValue = OSVersion.WindowsXP;
                            break;
                        case 6:
                            if (osInfo.Version.Minor == 0)
                                osRetValue = OSVersion.WindowsVista;
                            else
                                osRetValue = OSVersion.Windows7;
                            break;

                    }
                    break;
                default:
                    osRetValue = OSVersion.Unknown; break;
            }
            return osRetValue;
        }
    }
}
