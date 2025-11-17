using System.Text.Json.Serialization;

namespace TeamServer.Domain.Entities.Agents
{
    public class Agent
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UniqueId { get; set; }
        public AgentMetadata Metadata { get; set; }

        public DateTime LastSeen { get; private set; }

        public Agent(Guid uniqueId, AgentMetadata metadata)
        {
            UniqueId = uniqueId;
            Metadata = metadata;
            LastSeen = DateTime.UtcNow;
        }

        public void CheckIn()
        {
            LastSeen = DateTime.UtcNow;
        }

        protected Agent() { } // EF core
    }
}
