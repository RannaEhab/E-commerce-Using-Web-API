namespace Repositories.Contract
{
    public interface IGenericRepository<T>
	{
		IQueryable<T> GetAll();
		void Update(T Entity);
		void Delete(T Entity);
		void Create(T Entity);
	}
}
