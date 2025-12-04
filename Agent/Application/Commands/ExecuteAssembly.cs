using AgentClient.Domain.Models.Agents;

namespace AgentClient.Application.Commands
{
    public class ExecuteAssembly : AgentCommand
    {
        public override string Name => "execute-assembly";

        public override string Execute(AgentTask task)
        {
            throw new NotImplementedException();
        }
    }
}
