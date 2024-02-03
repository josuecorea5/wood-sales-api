using Microsoft.EntityFrameworkCore;
using WoodSalesApi.Models;

namespace WoodSalesApi.Repositories
{
	public class ProductRepository : IRepository<Product>
	{
		private readonly WoodSalesContext _context;

		public ProductRepository(WoodSalesContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Product>> GetAll()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> GetById<T>(T id)
		{
			return await _context.Products.FindAsync(id);
		}

		public async Task Add(Product product)
		{
			await _context.Products.AddAsync(product);
		}

		public void Update(Product product)
		{
			_context.Products.Attach(product);
			_context.Entry(product).State = EntityState.Modified;
		}

		public void Delete(Product product)
		{
			_context.Products.Remove(product);
		}

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}
	}
}
