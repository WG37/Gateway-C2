using TeamServer.Domain.Entities.Agents;

namespace TeamServer.Application.Services.AgentServices.AgentCore
{
    public interface IAgentCore
    {
        // checkIn
        Task CheckIn();

        // queueTask
        Task QueueTask(AgentTask task);

        // pendingTasks -- Queued
        Task<IEnumerable<AgentTask>> GetPendingTask();

        // addTaskResult
        Task AddTaskResult(IEnumerable<AgentTaskResult> result);

        // getTaskResult
        Task<AgentTaskResult> GetTaskResult(Guid taskId);

        // getTaskResults
        Task<IEnumerable<AgentTaskResult>> GetTaskResults();

    }
}
