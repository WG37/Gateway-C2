using AgentClient.Domain.Models.Agents;
using ApiModels.DTOs.AgentDTOs;

namespace AgentClient.Infrastructure.DTOs
{
    public static class AgentMapper
    {
        public static AgentDTO Dto(this Agent agent)
        {
            return new AgentDTO
            {
                UniqueId = agent.UniqueId,
                MetadataDTO = new AgentMetadataDTO
                {
                    Hostname = agent.Metadata.Hostname,
                    Username = agent.Metadata.Username,
                    ProcessName = agent.Metadata.ProcessName,
                    ProcessId = agent.Metadata.ProcessId,
                    Architecture = agent.Metadata.Architecture,
                    Integrity = agent.Metadata.Integrity
                }
            };
        }
    }
}
