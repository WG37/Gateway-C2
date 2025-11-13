namespace AgentClient.Domain.Models.Agents
{
    public class Agent
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public AgentMetadata Metadata { get; set; }

        public Agent(AgentMetadata metadata)
        {
            Metadata = metadata;
        }
    }
}
