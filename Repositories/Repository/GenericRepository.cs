using Context.Context;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

namespace Repositories.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		ECommerceWebsiteContext DBContext;
		DbSet<T> dbSet;

		public GenericRepository(ECommerceWebsiteContext dbContext)
		{
			DBContext = dbContext;
			dbSet = dbContext.Set<T>();
		}
		public IQueryable<T> GetAll()
		{
			return dbSet.Select(e => e);
		}
		public void Create(T Entity)
		{
			dbSet.Add(Entity);
			DBContext.SaveChanges();
		}
		public void Delete(T Entity)
		{
			dbSet.Remove(Entity);
			DBContext.SaveChanges();
		}
		public void Update(T Entity)
		{
			DBContext.SaveChanges();
		}
	}
}
