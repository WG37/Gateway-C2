using System.Management;

namespace AgentClient.Infrastructure.Native.Windows.Wmi.LogicalDisk
{
    public static class LogicalDiskSerialNumber
    {
        public static string GetLogicalDiskSerialNumber()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk"))
            {
                foreach (var obj in searcher.Get())
                {
                    var serial = obj["VolumeSerialNumber"]?.ToString() ?? "";

                    return serial;
                }
            }
            return "";
        }
    }
}
