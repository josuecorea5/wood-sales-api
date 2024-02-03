using System.Data;

namespace WoodSalesApi.Repositories
{
	public interface IRepository<TEntity>
	{
		Task<IEnumerable<TEntity>> GetAll();
		Task<TEntity> GetById<T>(T id);
		Task Add(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		Task Save();
	}

	public interface ITransactionalRepository<TEntity> : IRepository<TEntity>
	{
		public IDbTransaction BeginTransaction();
	}
}
