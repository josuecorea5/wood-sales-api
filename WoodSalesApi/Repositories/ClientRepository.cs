using Microsoft.EntityFrameworkCore;
using WoodSalesApi.Models;

namespace WoodSalesApi.Repositories
{
	public class ClientRepository : IRepository<Client>
	{
		private readonly WoodSalesContext _context;

        public ClientRepository(WoodSalesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAll()
		{
			return await _context.Clients.ToListAsync();
		}

		public async Task<Client> GetById<T>(T id)
		{
			return await _context.Clients.FindAsync(id);
		}

		public async Task Add(Client entity)
		{
			await _context.Clients.AddAsync(entity);
		}
		public void Update(Client entity)
		{
			_context.Clients.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
		public void Delete(Client entity)
		{
			_context.Clients.Remove(entity);
		}
		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}

		public IEnumerable<Client> Search(Func<Client, bool> predicate)
		{
			return _context.Clients.Where(predicate).ToList();
		}
	}
}
