
using AgentClient.Domain.Models.Agents;

namespace AgentClient.Application.Commands
{
    public class TestCommand : AgentCommand
    {
        public override string Name => "TestCommand";

        public override string Execute(AgentTask task)
        {
            return "Hello from TestCommand";
        }
    }
}
