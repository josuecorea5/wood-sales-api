namespace WoodSalesApi.Services
{
	public interface ICommonService<T, TI, TU>
	{
		public List<string> Errors { get; }
		Task<IEnumerable<T>> GetAll();
		Task<T> GetById<I>(I id);
		Task<T> Add(TI entity);
		Task<T> Update<I>(I id, TU entity);
		Task<T> Delete<I>(I id);
	}
}
