using System.Diagnostics;

namespace AgentClient.Application.Services
{
    public static class ExecutionService
    {
        public static string ExecuteCommand(string fileName, string args)
        {
            var stdout = "";

            var pStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args,
                WorkingDirectory = Directory.GetCurrentDirectory(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(pStartInfo);

            using (process.StandardOutput)
            {
                stdout += process.StandardOutput.ReadToEnd();
            }

            using (process.StandardError)
            {
                stdout += process.StandardError.ReadToEnd();
            }

            return stdout;
        }
    }
}
