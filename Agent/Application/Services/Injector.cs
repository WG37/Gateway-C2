namespace AgentClient.Application.Services
{
    public abstract class Injector
    {
        public abstract bool Inject(byte[] shellcode, int pid = 0);
    }
}
