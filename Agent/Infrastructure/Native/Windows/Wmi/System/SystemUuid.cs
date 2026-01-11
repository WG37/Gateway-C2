using System.Management;

namespace AgentClient.Infrastructure.Native.Windows.Wmi.System
{
    public static class SystemUuid
    {
        public static string GetSystemUuid()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct"))
            {
                foreach (var obj in searcher.Get())
                {
                    var uuid = obj["UUID"]?.ToString() ?? "";

                    return uuid;
                }
            }

            return "";
        }
    }
}
