using Microsoft.EntityFrameworkCore;
using TeamServer.Domain.Entities.Listeners.HttpListeners;
using TeamServer.Infrastructure.Data;

namespace TeamServer.Application.Services.ListenerServices.HttpListenerService.HttpCRUD
{
    public class HttpCRUD : IHttpCRUD
    {
        private readonly AppDbContext _db;

        public HttpCRUD(AppDbContext db) => _db = db;

        public async Task AddListenerAsync(HttpListenerEntity listener)
        {
            if (listener == null)
                throw new ArgumentNullException(nameof(listener), "Cannot add listener to the database.");

            try
            {
                _db.HttpListeners.Add(listener);
                await _db.SaveChangesAsync();
            }
            catch ( DbUpdateException)
            {
                throw new DbUpdateException();
            }
        }

        public async Task<IEnumerable<HttpListenerEntity>> GetAllListenersAsync()
        {
            var listeners = await _db.HttpListeners.ToListAsync();
            if (listeners == null)
                throw new InvalidOperationException();

            return listeners;
        }

        public async Task<HttpListenerEntity> GetListenerAsync(string name)
        {
            var listener = _db.HttpListeners.FirstOrDefault(l => l.Name == name);

            return listener;   
        }

        public async Task<bool> RemoveListenerAsync(string name)
        {
            try
            {
                var listener = await GetListenerAsync(name);
                _db.HttpListeners.Remove(listener);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }
        }
    }
}
