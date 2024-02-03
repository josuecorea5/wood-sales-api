using Microsoft.EntityFrameworkCore;
using WoodSalesApi.Models;

namespace WoodSalesApi.Repositories
{
	public class SaleDetailRepository : IRepository<SaleDetail>
	{
		private readonly WoodSalesContext _context;

		public SaleDetailRepository(WoodSalesContext context)
		{
			_context = context;
		}

		public async Task Add(SaleDetail entity)
		{
			await _context.SaleDetails.AddAsync(entity);
		}

		public void Delete(SaleDetail entity)
		{
			_context.SaleDetails.Remove(entity);
		}

		public Task<IEnumerable<SaleDetail>> GetAll()
		{
			throw new NotImplementedException();
		}

		public async Task<SaleDetail> GetById<T>(T id)
			=> await _context.SaleDetails.FindAsync(id);

		public void Update(SaleDetail entity)
		{
			_context.SaleDetails.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}
	}
}
