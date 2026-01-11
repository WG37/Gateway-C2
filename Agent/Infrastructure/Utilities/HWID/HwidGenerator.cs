using AgentClient.Infrastructure.Native.Windows.Wmi.LogicalDisk;
using AgentClient.Infrastructure.Native.Windows.Wmi.Processor;
using AgentClient.Infrastructure.Native.Windows.Wmi.System;

namespace AgentClient.Infrastructure.Utilities.HWID
{
    public static class HwidGenerator
    {
        private const string Version = "HWID01";

        public static string GenerateHwid()
        {
            var systemUuid = SystemUuid.GetSystemUuid();
            var processorId = ProcessorId.GetProcessorId();
            var volumeSerial = LogicalDiskSerialNumber.GetLogicalDiskSerialNumber();

            var uuid = HwidNormalization.Normalize(systemUuid);
            var cpu = HwidNormalization.Normalize(processorId);
            var volume = HwidNormalization.Normalize(volumeSerial);

            var idCount = 0;

            if (!string.IsNullOrEmpty(uuid)) 
                idCount++;

            if (!string.IsNullOrEmpty(cpu)) 
                idCount++;

            if (!string.IsNullOrEmpty(volume)) 
                idCount++;

            if (idCount < 2)
                return "";

            var canonicalData = $"{Version}|UUID={uuid}|CPUID={cpu}|VOLUMEID={volume}";
            
            return HwidHash.Sha256Hex(canonicalData);
        }
    }
}
