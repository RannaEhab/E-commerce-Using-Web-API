using Models.Models;

namespace Repositories.Contract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

		IQueryable<Category> Search(string s);
		Category GetCategoryByID(int ID);
	}
}
