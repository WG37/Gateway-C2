using TeamServer.Domain.Entities.Agents;

namespace TeamServer.Application.Services.AgentServices.AgentCRUD
{
    public interface IAgentCRUD
    {
        Task AddAgentAsync(Agent agent);
        Task<IEnumerable<Agent>> GetAgentsAsync();
        Task<Agent> GetAgentAsync(Guid id);
        Task<bool> RemoveAgentAsync(Guid id);
    }
}
