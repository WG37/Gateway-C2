using AgentClient.Domain.Models.Agents;

namespace AgentClient.Application.Commands
{
    public abstract class AgentCommand
    {
        public abstract string Name { get; }

        public abstract string Execute(AgentTask task);
    }
}
