
using Microsoft.EntityFrameworkCore;
using TeamServer.Application.Services.AgentServices.AgentCore;
using TeamServer.Application.Services.AgentServices.AgentCRUD;
using TeamServer.Infrastructure.Controllers.ImplantControllers;
using TeamServer.Infrastructure.Data;

namespace TeamServer.Application.Services.ListenerServices.HttpListenerService.HttpCore
{
    public class HttpCore : IHttpCore
    {
        private readonly IConfiguration _config;
        private readonly IAgentCore _agentCore;

        private CancellationTokenSource _tokenSource;
        
        private WebApplication _host;
        private Task _runTask;

        public HttpCore(IConfiguration config, AgentCore agentCore)
        {
            _config = config;
            _agentCore = agentCore;
        }

        public async Task StartHttpListenerAsync(int bindPort)
        {
            if (_host != null)
                throw new InvalidOperationException("Http listener already running");

            _tokenSource = new CancellationTokenSource();

            var builder = WebApplication.CreateBuilder();

            // shares connection string/ other configs
            builder.Configuration.AddConfiguration(_config);

            builder.WebHost.UseUrls($"http://0.0.0.0:{bindPort}");

            builder.Services.AddControllers()
                            .AddApplicationPart(typeof(HttpImplantController).Assembly);

            builder.Services.AddDbContext<AppDbContext>(o =>
                o.UseSqlServer(builder.Configuration.GetConnectionString("TeamServerDb")));

            builder.Services.AddSingleton<IAgentCore>(_agentCore);

            builder.Services.AddScoped<IAgentCRUD, AgentCRUD>();

            _host = builder.Build();

            _host.MapControllers();

            _runTask = _host.RunAsync(_tokenSource.Token);

            await Task.CompletedTask;
        }

        public async Task StopHttpListenerAsync()
        {
            if (_host == null || _tokenSource == null)
                return;

            _tokenSource.Cancel();

            if (_runTask != null)
            {
                try { await _runTask; }
                catch (OperationCanceledException) { }
            }

            await _host.StopAsync();
            await _host.DisposeAsync();

            _host = null;
            _tokenSource = null;
            _runTask = null;
        }
    }
}
