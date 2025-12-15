using AgentClient.Domain.Models.Agents;
using AgentClient.Infrastructure.Utilities.SharpSploit;
using AgentClient.Infrastructure.Native;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace AgentClient.Application.Commands
{
    public class ListProcess : AgentCommand
    {
        public override string Name => "ps";

        public override string Execute(AgentTask task)
        {
            var results = new SharpSploitResultList<ListProcessResult>();

            var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                var result = new ListProcessResult
                {
                    ProcessName = process.ProcessName,
                    ProcessPath = GetProcessPath(process),
                    ProcessOwner = GetProcessOwner(process),
                    ProcessId = process.Id,
                    SessionId = process.SessionId,
                    ProcessArchitecture = GetProcessArchitecture(process)
                };
                results.Add(result);
            }
            return results.ToString();
        }

        //if process does not have permission to file/folder print: "-"
        private string GetProcessPath(Process process)
        {
            try
            {
                return process.MainModule.FileName;
            }
            catch (Win32Exception)
            {
                return "-";
            }
        }

        private string GetProcessOwner(Process process)
        {
            var hToken = IntPtr.Zero;
            try
            {
                if (!Advapi32.OpenProcessToken(
                    process.Handle,
                    Advapi32.DesiredAccess.TOKEN_ALL_ACCESS,
                    out hToken))
                {
                    int error = Marshal.GetLastWin32Error();
                    return $"Failed to open process token. Error: {error}";
                }

                try
                {
                    using (var identity = new WindowsIdentity(hToken))
                    {
                        return identity.Name;
                    }
                }
                finally
                {
                    Advapi32.RevertToSelf();
                }

            }
            catch (Win32Exception)
            {
                return "-";
            }
            finally
            {
                if (hToken != IntPtr.Zero)
                Kernel32.CloseHandle(hToken);
            }
        }

        private string GetProcessArchitecture(Process process)
        {
            var Is64Bit = Environment.Is64BitProcess;

            if (!Is64Bit)
            {
                return "x86";
            }

            try
            {
                if (!Kernel32.IsWow64Process(process.Handle, out var isWow64Process))
                {
                    int error = Marshal.GetLastWin32Error();
                    return $"Failed to determine process architecture. Error {error}";
                }

                if (Is64Bit && isWow64Process)
                {
                    return "x86";
                }
            }
            catch (Win32Exception)
            {
                return "-";
            }

            return "x64";
        }
    }

    public sealed class ListProcessResult : SharpSploitResult
    {
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }
        public string ProcessOwner { get; set; }
        public int ProcessId { get; set; }
        public int SessionId { get; set; }
        public string ProcessArchitecture { get; set; }

        protected internal override IList<SharpSploitResultProperty> ResultProperties => new List<SharpSploitResultProperty>
            {
                new SharpSploitResultProperty {Name = nameof(ProcessName), Value = ProcessName},
                new SharpSploitResultProperty {Name = nameof(ProcessPath), Value = ProcessPath},
                new SharpSploitResultProperty {Name = nameof(ProcessOwner), Value = ProcessOwner},
                new SharpSploitResultProperty {Name = "PID", Value = ProcessId},
                new SharpSploitResultProperty {Name = nameof(SessionId), Value = SessionId },
                new SharpSploitResultProperty {Name = nameof(ProcessArchitecture), Value = ProcessArchitecture}
            };
    }
}
