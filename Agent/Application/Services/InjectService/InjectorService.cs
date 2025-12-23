namespace AgentClient.Application.Services.InjectService
{
    public abstract class InjectorService
    {
        public abstract bool Inject(byte[] shellcode, int pid = 0);
    }
}
