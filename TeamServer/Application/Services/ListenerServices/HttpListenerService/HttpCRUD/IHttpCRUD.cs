using TeamServer.Domain.Entities.Listeners.HttpListeners;

namespace TeamServer.Application.Services.ListenerServices.HttpListenerService.HttpCRUD
{
    public interface IHttpCRUD
    {
        Task AddListenerAsync(HttpListenerEntity listener);
        Task<IEnumerable<HttpListenerEntity>> GetAllListenersAsync();
        Task<HttpListenerEntity> GetListenerAsync(string name);
        Task<bool> RemoveListenerAsync(string name);
    }
}
