namespace TeamServer.Domain.Entities.Agents
{
    public class AgentMetadata
    {
        public string Hwid { get; set; }
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string ProcessName { get; set; }
        public int ProcessId { get; set; }
        public string Architecture { get; set; }
        public string Integrity { get; set; }
    }
}
