using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using WoodSalesApi.Models;

namespace WoodSalesApi.Repositories
{
	public class SaleRepository : ITransactionalRepository<Sale>
	{
		private readonly WoodSalesContext _context;

		public SaleRepository(WoodSalesContext context)
		{
			_context = context;
		}

		public async Task<Sale> GetById<T>(T id)
			=> await _context.Sales.Include(sale => sale.SaleDetails).FirstOrDefaultAsync(s => s.Id.Equals(id));

		public async Task Add(Sale entity)
		{
			await _context.Sales.AddAsync(entity);
		}

		public void Delete(Sale sale)
		{
			_context.Sales.Remove(sale);
		}

		public async Task<IEnumerable<Sale>> GetAll()
			=> await _context.Sales.Include(d => d.SaleDetails).ToListAsync();

		public void Update(Sale entity)
		{
			_context.Sales.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}

		public IDbTransaction BeginTransaction()
		{
			var transaction = _context.Database.BeginTransaction();

			return transaction.GetDbTransaction();
		}

		public IEnumerable<Sale> Search(Func<Sale, bool> predicate)
		{
			return _context.Sales.Where(predicate).ToList();
		}
	}
}
