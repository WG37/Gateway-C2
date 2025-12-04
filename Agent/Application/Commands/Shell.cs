using AgentClient.Domain.Models.Agents;

namespace AgentClient.Application.Commands
{
    public class Shell : AgentCommand
    {
        public override string Name => "shell";

        public override string Execute(AgentTask task)
        {
            var args = string.Join(" ", task.Arguments);

            return Services.ExecuteCommandService.ExecuteCommand(@"C:\Windows\System32\cmd.exe", $"/c {args}");
        }
    }
}
