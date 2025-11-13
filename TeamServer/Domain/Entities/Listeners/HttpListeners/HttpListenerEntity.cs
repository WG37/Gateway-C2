

namespace TeamServer.Domain.Entities.Listeners.HttpListeners
{
    public class HttpListenerEntity : ListenerEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int BindPort { get; set; }
        public override string Name { get; set; }

        public HttpListenerEntity(string name, int bindPort)
        {
            Name = name;
            BindPort = bindPort;
        }

        protected HttpListenerEntity() { } // EF Core
    }
}
