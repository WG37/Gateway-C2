using System.Management;

namespace AgentClient.Infrastructure.Native.Windows.Wmi.Processor
{
    public class ProcessorId
    {
        public static string GetProcessorId()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
            {
                foreach (var obj in searcher.Get())
                {
                    var id = obj["ProcessorId"]?.ToString() ?? "";

                    return id;
                }
            }
            return "";
        }
    }
}
