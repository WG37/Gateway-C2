using System.Collections.Concurrent;
using TeamServer.Domain.Entities.Agents;

namespace TeamServer.Application.Services.AgentServices.AgentCore
{
    public class AgentCore : IAgentCore
    {
        public DateTime LastSeen { get; private set; }

        private readonly ConcurrentQueue<AgentTask> _pendingTasks = new();
        private readonly List<AgentTaskResult> _taskResults = new();

        
        public Task CheckIn()
        {
            LastSeen = DateTime.UtcNow;
            return Task.CompletedTask;
        }
        
        public Task QueueTask(AgentTask task)
        {
            _pendingTasks.Enqueue(task);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<AgentTask>> GetPendingTask()
        {
            List<AgentTask> tasks = new();

            while (_pendingTasks.TryDequeue(out var task))
            {
                tasks.Add(task);
            }
            return Task.FromResult<IEnumerable<AgentTask>>(tasks);
        }
       
        public Task AddTaskResult(IEnumerable<AgentTaskResult> result)
        {
            _taskResults.AddRange(result);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<AgentTaskResult>> GetTaskResults()
        {
            var results = _taskResults.ToList();

            return Task.FromResult<IEnumerable<AgentTaskResult>>(results);
        }

        public Task<AgentTaskResult> GetTaskResult(Guid taskId)
        {
            if (taskId == Guid.Empty)
                throw new ArgumentException("Invalid taskId");

            var result = _taskResults.FirstOrDefault(r => r.Id == taskId)
                ?? throw new KeyNotFoundException($"The id: {taskId} does not exist");

            return Task.FromResult(result);
        }
    }
}
