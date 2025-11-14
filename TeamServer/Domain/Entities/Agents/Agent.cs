namespace TeamServer.Domain.Entities.Agents
{
    public class Agent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UniqueId { get; set; }
        public AgentMetadata Metadata { get; set; }

        public Agent(Guid uniqueId, AgentMetadata metadata)
        {
            UniqueId = uniqueId;
            Metadata = metadata;
        }

        protected Agent() { } // EF core
    }
}
