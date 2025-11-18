using System.Runtime.Serialization;

namespace AgentClient.Domain.Models.Agents
{
    [DataContract]
    public class AgentTask
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "arguments")]
        public string[] Arguments { get; set; }

        [DataMember(Name = "file")]
        public byte[] File { get; set; }
    }
}
